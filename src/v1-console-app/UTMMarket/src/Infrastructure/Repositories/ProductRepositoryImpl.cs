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
/// Implementation of IProductRepository optimized for Native AOT.
/// Uses manual mapping with SqlDataReader to avoid reflection-based overhead and incompatibilities.
/// </summary>
public sealed class ProductRepositoryImpl(IDbConnectionFactory connectionFactory) : IProductRepository
{
    private readonly IDbConnectionFactory _connectionFactory = connectionFactory;

    public async IAsyncEnumerable<Product> GetAllAsync([EnumeratorCancellation] CancellationToken ct = default)
    {
        using var connection = (SqlConnection)_connectionFactory.CreateConnection();
        const string sql = "SELECT ProductoID, SKU, Nombre, Marca, Precio, Stock FROM dbo.Producto";
        
        using var command = new SqlCommand(sql, connection);
        await connection.OpenAsync(ct);

        using var reader = await command.ExecuteReaderAsync(ct);
        while (await reader.ReadAsync(ct))
        {
            yield return MapFromReader(reader).ToDomain();
        }
    }

    public async Task<Product?> GetByIdAsync(int productId, CancellationToken ct = default)
    {
        using var connection = (SqlConnection)_connectionFactory.CreateConnection();
        const string sql = "SELECT ProductoID, SKU, Nombre, Marca, Precio, Stock FROM dbo.Producto WHERE ProductoID = @id";
        
        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", productId);
        
        await connection.OpenAsync(ct);
        using var reader = await command.ExecuteReaderAsync(ct);

        if (await reader.ReadAsync(ct))
        {
            return MapFromReader(reader).ToDomain();
        }

        return null;
    }

    public async IAsyncEnumerable<Product> FindAsync(ProductFilter filter, [EnumeratorCancellation] CancellationToken ct = default)
    {
        using var connection = (SqlConnection)_connectionFactory.CreateConnection();
        var sql = new System.Text.StringBuilder("SELECT ProductoID, SKU, Nombre, Marca, Precio, Stock FROM dbo.Producto WHERE 1=1");
        
        using var command = new SqlCommand();
        command.Connection = connection;

        if (!string.IsNullOrWhiteSpace(filter.Name))
        {
            sql.Append(" AND Nombre LIKE @name");
            command.Parameters.AddWithValue("@name", $"%{filter.Name}%");
        }

        if (!string.IsNullOrWhiteSpace(filter.SKU))
        {
            sql.Append(" AND SKU = @sku");
            command.Parameters.AddWithValue("@sku", filter.SKU);
        }

        if (!string.IsNullOrWhiteSpace(filter.Brand))
        {
            sql.Append(" AND Marca = @brand");
            command.Parameters.AddWithValue("@brand", filter.Brand);
        }

        if (filter.MinPrice.HasValue)
        {
            sql.Append(" AND Precio >= @minPrice");
            command.Parameters.AddWithValue("@minPrice", filter.MinPrice.Value);
        }

        if (filter.MaxPrice.HasValue)
        {
            sql.Append(" AND Precio <= @maxPrice");
            command.Parameters.AddWithValue("@maxPrice", filter.MaxPrice.Value);
        }

        command.CommandText = sql.ToString();
        await connection.OpenAsync(ct);

        using var reader = await command.ExecuteReaderAsync(ct);
        while (await reader.ReadAsync(ct))
        {
            yield return MapFromReader(reader).ToDomain();
        }
    }

    public async Task<int> AddAsync(Product product, CancellationToken ct = default)
    {
        var entity = product.ToEntity();
        using var connection = (SqlConnection)_connectionFactory.CreateConnection();
        const string sql = @"
            INSERT INTO dbo.Producto (SKU, Nombre, Marca, Precio, Stock)
            VALUES (@sku, @nombre, @marca, @precio, @stock);
            SELECT CAST(SCOPE_IDENTITY() as int);";

        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@sku", entity.SKU);
        command.Parameters.AddWithValue("@nombre", (object?)entity.Nombre ?? DBNull.Value);
        command.Parameters.AddWithValue("@marca", (object?)entity.Marca ?? DBNull.Value);
        command.Parameters.AddWithValue("@precio", entity.Precio);
        command.Parameters.AddWithValue("@stock", entity.Stock);

        await connection.OpenAsync(ct);
        return (int)await command.ExecuteScalarAsync(ct);
    }

    public async Task<bool> UpdateAsync(Product product, CancellationToken ct = default)
    {
        var entity = product.ToEntity();
        using var connection = (SqlConnection)_connectionFactory.CreateConnection();
        const string sql = @"
            UPDATE dbo.Producto 
            SET SKU = @sku, Nombre = @nombre, Marca = @marca, Precio = @precio, Stock = @stock
            WHERE ProductoID = @id";

        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", entity.ProductoID);
        command.Parameters.AddWithValue("@sku", entity.SKU);
        command.Parameters.AddWithValue("@nombre", (object?)entity.Nombre ?? DBNull.Value);
        command.Parameters.AddWithValue("@marca", (object?)entity.Marca ?? DBNull.Value);
        command.Parameters.AddWithValue("@precio", entity.Precio);
        command.Parameters.AddWithValue("@stock", entity.Stock);

        await connection.OpenAsync(ct);
        int rowsAffected = await command.ExecuteNonQueryAsync(ct);
        return rowsAffected > 0;
    }

    public async Task<bool> UpdateStockAsync(int productId, int newStock, CancellationToken ct = default)
    {
        using var connection = (SqlConnection)_connectionFactory.CreateConnection();
        const string sql = "UPDATE dbo.Producto SET Stock = @stock WHERE ProductoID = @id";

        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", productId);
        command.Parameters.AddWithValue("@stock", newStock);

        await connection.OpenAsync(ct);
        int rowsAffected = await command.ExecuteNonQueryAsync(ct);
        return rowsAffected > 0;
    }

    /// <summary>
    /// Manual mapping from SqlDataReader to ProductoEntity to ensure AOT compatibility.
    /// </summary>
    private static ProductoEntity MapFromReader(SqlDataReader reader)
    {
        return new ProductoEntity(
            reader.GetInt32(0), // ProductoID
            reader.GetString(1) // SKU
        )
        {
            Nombre = reader.IsDBNull(2) ? null : reader.GetString(2),
            Marca = reader.IsDBNull(3) ? null : reader.GetString(3),
            Precio = reader.GetDecimal(4),
            Stock = reader.GetInt32(5)
        };
    }
}
