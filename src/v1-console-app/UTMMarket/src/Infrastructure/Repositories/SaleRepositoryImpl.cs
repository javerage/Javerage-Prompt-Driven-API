using System.Data;
using System.Runtime.CompilerServices;
using Microsoft.Data.SqlClient;
using UTMMarket.Core.Entities;
using UTMMarket.Core.Repositories;
using UTMMarket.Infrastructure.Mappers;
using UTMMarket.Infrastructure.Models.Data;
using UTMMarket.Infrastructure.Persistence;

namespace UTMMarket.Infrastructure.Repositories;

/// <summary>
/// Implementación de ISaleRepository optimizada para Native AOT.
/// Utiliza ADO.NET puro para evitar la dependencia de reflexión en tiempo de ejecución.
/// </summary>
public sealed class SaleRepositoryImpl(IDbConnectionFactory connectionFactory) : ISaleRepository
{
    private readonly IDbConnectionFactory _connectionFactory = connectionFactory;

    public async IAsyncEnumerable<Sale> GetAllAsync([EnumeratorCancellation] CancellationToken ct = default)
    {
        using var connection = (SqlConnection)_connectionFactory.CreateConnection();
        const string sql = @"
            SELECT v.VentaID, v.Folio, v.FechaVenta, v.Estatus,
                   dv.DetalleID, dv.Cantidad, dv.PrecioUnitario,
                   p.ProductoID, p.Nombre, p.SKU, p.Marca, p.Precio, p.Stock
            FROM dbo.Venta v
            LEFT JOIN dbo.DetalleVenta dv ON v.VentaID = dv.VentaID
            LEFT JOIN dbo.Producto p ON dv.ProductoID = p.ProductoID
            ORDER BY v.VentaID";

        await connection.OpenAsync(ct);
        using var command = new SqlCommand(sql, connection);
        using var reader = await command.ExecuteReaderAsync(ct);

        VentaEntity? currentVentaEntity = null;
        var currentDetails = new List<DetalleVentaEntity>();
        var productMap = new Dictionary<int, Product>();

        while (await reader.ReadAsync(ct))
        {
            int ventaId = reader.GetInt32(0);

            if (currentVentaEntity != null && currentVentaEntity.VentaID != ventaId)
            {
                yield return currentVentaEntity.ToDomain(currentDetails, productMap);
                currentDetails.Clear();
                // No limpiamos productMap para reutilizar objetos Product si se repiten entre ventas (aunque aquí es por venta)
            }

            if (currentVentaEntity == null || currentVentaEntity.VentaID != ventaId)
            {
                currentVentaEntity = new VentaEntity(ventaId, reader.GetString(1))
                {
                    FechaVenta = reader.GetDateTime(2),
                    EstatusID = reader.GetByte(3)
                };
            }

            if (!reader.IsDBNull(4)) // Si hay detalle
            {
                int productoId = reader.GetInt32(7);
                if (!productMap.ContainsKey(productoId))
                {
                    productMap[productoId] = new Product(
                        productoId,
                        reader.GetString(8),
                        reader.GetString(9),
                        reader.IsDBNull(10) ? string.Empty : reader.GetString(10)
                    )
                    {
                        Price = reader.GetDecimal(11),
                        Stock = reader.GetInt32(12)
                    };
                }

                currentDetails.Add(new DetalleVentaEntity(reader.GetInt32(4), ventaId, productoId)
                {
                    Cantidad = reader.GetInt32(5),
                    PrecioUnitario = reader.GetDecimal(6)
                });
            }
        }

        if (currentVentaEntity != null)
        {
            yield return currentVentaEntity.ToDomain(currentDetails, productMap);
        }
    }

    public async Task<Sale?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        using var connection = (SqlConnection)_connectionFactory.CreateConnection();
        const string sql = @"
            SELECT VentaID, Folio, FechaVenta, Estatus FROM dbo.Venta WHERE VentaID = @id;
            SELECT dv.DetalleID, dv.VentaID, dv.ProductoID, dv.PrecioUnitario, dv.Cantidad,
                   p.Nombre, p.SKU, p.Marca, p.Precio, p.Stock
            FROM dbo.DetalleVenta dv
            INNER JOIN dbo.Producto p ON dv.ProductoID = p.ProductoID
            WHERE dv.VentaID = @id";

        await connection.OpenAsync(ct);
        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);

        using var reader = await command.ExecuteReaderAsync(ct);

        if (!await reader.ReadAsync(ct)) return null;

        var ventaEntity = new VentaEntity(reader.GetInt32(0), reader.GetString(1))
        {
            FechaVenta = reader.GetDateTime(2),
            EstatusID = reader.GetByte(3)
        };

        await reader.NextResultAsync(ct);

        var details = new List<DetalleVentaEntity>();
        var productMap = new Dictionary<int, Product>();

        while (await reader.ReadAsync(ct))
        {
            int productoId = reader.GetInt32(2);
            if (!productMap.ContainsKey(productoId))
            {
                productMap[productoId] = new Product(
                    productoId,
                    reader.GetString(5),
                    reader.GetString(6),
                    reader.IsDBNull(7) ? string.Empty : reader.GetString(7)
                )
                {
                    Price = reader.GetDecimal(8),
                    Stock = reader.GetInt32(9)
                };
            }

            details.Add(new DetalleVentaEntity(reader.GetInt32(0), id, productoId)
            {
                PrecioUnitario = reader.GetDecimal(3),
                Cantidad = reader.GetInt32(4)
            });
        }

        return ventaEntity.ToDomain(details, productMap);
    }

    public async IAsyncEnumerable<Sale> FindAsync(SaleFilter filter, [EnumeratorCancellation] CancellationToken ct = default)
    {
        using var connection = (SqlConnection)_connectionFactory.CreateConnection();
        var sql = new System.Text.StringBuilder(@"
            SELECT v.VentaID, v.Folio, v.FechaVenta, v.Estatus,
                   dv.DetalleID, dv.Cantidad, dv.PrecioUnitario,
                   p.ProductoID, p.Nombre, p.SKU, p.Marca, p.Precio, p.Stock
            FROM dbo.Venta v
            LEFT JOIN dbo.DetalleVenta dv ON v.VentaID = dv.VentaID
            LEFT JOIN dbo.Producto p ON dv.ProductoID = p.ProductoID
            WHERE 1=1");

        using var command = new SqlCommand();
        command.Connection = connection;

        if (filter.StartDate.HasValue)
        {
            sql.Append(" AND v.FechaVenta >= @start");
            command.Parameters.AddWithValue("@start", filter.StartDate.Value);
        }

        if (filter.EndDate.HasValue)
        {
            sql.Append(" AND v.FechaVenta <= @end");
            command.Parameters.AddWithValue("@end", filter.EndDate.Value);
        }

        if (filter.Status.HasValue)
        {
            sql.Append(" AND v.Estatus = @status");
            command.Parameters.AddWithValue("@status", (int)filter.Status.Value);
        }

        if (!string.IsNullOrWhiteSpace(filter.Folio))
        {
            sql.Append(" AND v.Folio LIKE @folio");
            command.Parameters.AddWithValue("@folio", $"%{filter.Folio}%");
        }

        sql.Append(" ORDER BY v.VentaID");
        command.CommandText = sql.ToString();

        await connection.OpenAsync(ct);
        using var reader = await command.ExecuteReaderAsync(ct);

        VentaEntity? currentVentaEntity = null;
        var currentDetails = new List<DetalleVentaEntity>();
        var productMap = new Dictionary<int, Product>();

        while (await reader.ReadAsync(ct))
        {
            int ventaId = reader.GetInt32(0);

            if (currentVentaEntity != null && currentVentaEntity.VentaID != ventaId)
            {
                yield return currentVentaEntity.ToDomain(currentDetails, productMap);
                currentDetails.Clear();
            }

            if (currentVentaEntity == null || currentVentaEntity.VentaID != ventaId)
            {
                currentVentaEntity = new VentaEntity(ventaId, reader.GetString(1))
                {
                    FechaVenta = reader.GetDateTime(2),
                    EstatusID = reader.GetByte(3)
                };
            }

            if (!reader.IsDBNull(4))
            {
                int productoId = reader.GetInt32(7);
                if (!productMap.ContainsKey(productoId))
                {
                    productMap[productoId] = new Product(
                        productoId,
                        reader.GetString(8),
                        reader.GetString(9),
                        reader.IsDBNull(10) ? string.Empty : reader.GetString(10)
                    )
                    {
                        Price = reader.GetDecimal(11),
                        Stock = reader.GetInt32(12)
                    };
                }

                currentDetails.Add(new DetalleVentaEntity(reader.GetInt32(4), ventaId, productoId)
                {
                    Cantidad = reader.GetInt32(5),
                    PrecioUnitario = reader.GetDecimal(6)
                });
            }
        }

        if (currentVentaEntity != null)
        {
            yield return currentVentaEntity.ToDomain(currentDetails, productMap);
        }
    }

    public async Task<Sale> AddAsync(Sale sale, CancellationToken ct = default)
    {
        using var connection = (SqlConnection)_connectionFactory.CreateConnection();
        await connection.OpenAsync(ct);
        using var transaction = connection.BeginTransaction();

        try
        {
            const string insertVentaSql = @"
                INSERT INTO dbo.Venta (Folio, FechaVenta, TotalArticulos, TotalVenta, Estatus)
                VALUES (@folio, @fecha, @totalArt, @totalVenta, @estatus);
                SELECT CAST(SCOPE_IDENTITY() as int);";

            using var cmdVenta = new SqlCommand(insertVentaSql, connection, transaction);
            cmdVenta.Parameters.AddWithValue("@folio", sale.Folio);
            cmdVenta.Parameters.AddWithValue("@fecha", sale.SaleDate);
            cmdVenta.Parameters.AddWithValue("@totalArt", sale.TotalItems);
            cmdVenta.Parameters.AddWithValue("@totalVenta", sale.TotalSale);
            cmdVenta.Parameters.AddWithValue("@estatus", (int)sale.Status);

            int ventaId = (int)await cmdVenta.ExecuteScalarAsync(ct);

            foreach (var detail in sale.Details)
            {
                const string insertDetailSql = @"
                    INSERT INTO dbo.DetalleVenta (VentaID, ProductoID, PrecioUnitario, Cantidad, TotalDetalle)
                    VALUES (@ventaId, @productoId, @precio, @cantidad, @totalDetail)";

                using var cmdDetail = new SqlCommand(insertDetailSql, connection, transaction);
                cmdDetail.Parameters.AddWithValue("@ventaId", ventaId);
                cmdDetail.Parameters.AddWithValue("@productoId", detail.Product.ProductID);
                cmdDetail.Parameters.AddWithValue("@precio", detail.UnitPrice);
                cmdDetail.Parameters.AddWithValue("@cantidad", detail.Quantity);
                cmdDetail.Parameters.AddWithValue("@totalDetail", detail.TotalDetail);

                await cmdDetail.ExecuteNonQueryAsync(ct);
            }

            await transaction.CommitAsync(ct);
            
            // Re-fetch or reconstruct the domain object
            return (await GetByIdAsync(ventaId, ct))!;
        }
        catch
        {
            await transaction.RollbackAsync(ct);
            throw;
        }
    }

    public async Task UpdateAsync(Sale sale, CancellationToken ct = default)
    {
        using var connection = (SqlConnection)_connectionFactory.CreateConnection();
        await connection.OpenAsync(ct);
        using var transaction = connection.BeginTransaction();

        try
        {
            const string updateVentaSql = @"
                UPDATE dbo.Venta 
                SET Folio = @folio, FechaVenta = @fecha, TotalArticulos = @totalArt, 
                    TotalVenta = @totalVenta, Estatus = @estatus
                WHERE VentaID = @id";

            using var cmdVenta = new SqlCommand(updateVentaSql, connection, transaction);
            cmdVenta.Parameters.AddWithValue("@id", sale.SaleID);
            cmdVenta.Parameters.AddWithValue("@folio", sale.Folio);
            cmdVenta.Parameters.AddWithValue("@fecha", sale.SaleDate);
            cmdVenta.Parameters.AddWithValue("@totalArt", sale.TotalItems);
            cmdVenta.Parameters.AddWithValue("@totalVenta", sale.TotalSale);
            cmdVenta.Parameters.AddWithValue("@estatus", (int)sale.Status);

            await cmdVenta.ExecuteNonQueryAsync(ct);

            // Simple strategy: Delete and Re-insert details for the aggregate update
            const string deleteDetailsSql = "DELETE FROM dbo.DetalleVenta WHERE VentaID = @ventaId";
            using var cmdDelete = new SqlCommand(deleteDetailsSql, connection, transaction);
            cmdDelete.Parameters.AddWithValue("@ventaId", sale.SaleID);
            await cmdDelete.ExecuteNonQueryAsync(ct);

            foreach (var detail in sale.Details)
            {
                const string insertDetailSql = @"
                    INSERT INTO dbo.DetalleVenta (VentaID, ProductoID, PrecioUnitario, Cantidad, TotalDetalle)
                    VALUES (@ventaId, @productoId, @precio, @cantidad, @totalDetail)";

                using var cmdDetail = new SqlCommand(insertDetailSql, connection, transaction);
                cmdDetail.Parameters.AddWithValue("@ventaId", sale.SaleID);
                cmdDetail.Parameters.AddWithValue("@productoId", detail.Product.ProductID);
                cmdDetail.Parameters.AddWithValue("@precio", detail.UnitPrice);
                cmdDetail.Parameters.AddWithValue("@cantidad", detail.Quantity);
                cmdDetail.Parameters.AddWithValue("@totalDetail", detail.TotalDetail);

                await cmdDetail.ExecuteNonQueryAsync(ct);
            }

            await transaction.CommitAsync(ct);
        }
        catch
        {
            await transaction.RollbackAsync(ct);
            throw;
        }
    }
}
