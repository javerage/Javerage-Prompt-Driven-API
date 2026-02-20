using UTMMarket.Core.Entities;
using UTMMarket.Infrastructure.Models.Data;

namespace UTMMarket.Infrastructure.Mappers;

/// <summary>
/// Mapeador estático de alto rendimiento para la entidad Sale y sus agregados.
/// </summary>
/// <remarks>
/// Utiliza extensiones de C# 14 para minimizar el overhead y garantizar compatibilidad con Native AOT.
/// </remarks>
public static class SaleMapper
{
    /// <summary>
    /// Convierte una VentaEntity y sus detalles a un objeto de dominio Sale.
    /// </summary>
    /// <param name="entity">La entidad de cabecera de la venta.</param>
    /// <param name="details">La colección de entidades de detalle (opcional).</param>
    /// <param name="productMap">Un diccionario de productos ya mapeados para asociar a los detalles.</param>
    /// <returns>Un objeto Sale con sus detalles cargados.</returns>
    public static Sale ToDomain(
        this VentaEntity entity, 
        IEnumerable<DetalleVentaEntity>? details = null,
        Dictionary<int, Product>? productMap = null)
    {
        ArgumentNullException.ThrowIfNull(entity);

        var sale = new Sale(entity.VentaID, entity.Folio)
        {
            Status = (SaleStatus)entity.EstatusID,
            // Nota: SaleDate tiene init, podemos sobreescribir el default de DateTime.Now
            SaleDate = entity.FechaVenta 
        };

        if (details is not null && productMap is not null)
        {
            foreach (var detailEntity in details)
            {
                if (productMap.TryGetValue(detailEntity.ProductoID, out var product))
                {
                    // Creamos el detalle de dominio
                    var detail = new SaleDetail(product, detailEntity.Cantidad)
                    {
                        SaleDetailID = detailEntity.DetalleVentaID,
                        UnitPrice = detailEntity.PrecioUnitario // Preservamos el precio histórico
                    };
                    sale.Details.Add(detail);
                }
            }
        }

        return sale;
    }

    /// <summary>
    /// Convierte un objeto de dominio Sale a su representación de persistencia VentaEntity.
    /// </summary>
    public static VentaEntity ToEntity(this Sale domain)
    {
        ArgumentNullException.ThrowIfNull(domain);

        return new VentaEntity(domain.SaleID, domain.Folio)
        {
            FechaVenta = domain.SaleDate,
            EstatusID = (int)domain.Status
        };
    }

    /// <summary>
    /// Convierte los detalles de una Sale a una lista de DetalleVentaEntity.
    /// </summary>
    public static List<DetalleVentaEntity> ToDetailEntities(this Sale domain)
    {
        ArgumentNullException.ThrowIfNull(domain);

        // Uso de capacidad de lista con tamaño predefinido para eficiencia en .NET 10
        var entities = new List<DetalleVentaEntity>(domain.Details.Count);

        foreach (var detail in domain.Details)
        {
            entities.Add(new DetalleVentaEntity(detail.SaleDetailID, domain.SaleID, detail.Product.ProductID)
            {
                Cantidad = detail.Quantity,
                PrecioUnitario = detail.UnitPrice
            });
        }

        return entities;
    }
}
