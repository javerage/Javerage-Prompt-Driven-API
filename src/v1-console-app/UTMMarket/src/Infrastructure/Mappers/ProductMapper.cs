using UTMMarket.Core.Entities;
using UTMMarket.Infrastructure.Models.Data;

namespace UTMMarket.Infrastructure.Mappers;

/// <summary>
/// Proporciona métodos de extensión para la conversión estática entre 
/// las entidades de persistencia y el dominio de productos.
/// </summary>
/// <remarks>
/// Optimizado para Native AOT: Evita la reflexión y utiliza mapeo directo de propiedades.
/// </remarks>
public static class ProductMapper
{
    /// <summary>
    /// Convierte una entidad de base de datos en un objeto de dominio.
    /// </summary>
    /// <param name="entity">Entidad de persistencia ProductoEntity.</param>
    /// <returns>Objeto de negocio Product.</returns>
    public static Product ToDomain(this ProductoEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        var product = new Product(
            entity.ProductoID, 
            entity.Nombre ?? string.Empty, 
            entity.SKU, 
            entity.Marca ?? string.Empty
        )
        {
            Price = entity.Precio,
            Stock = entity.Stock
        };

        return product;
    }

    /// <summary>
    /// Convierte un objeto de dominio en una entidad de base de datos.
    /// </summary>
    /// <param name="domain">Objeto de negocio Product.</param>
    /// <returns>Entidad de persistencia ProductoEntity.</returns>
    public static ProductoEntity ToEntity(this Product domain)
    {
        ArgumentNullException.ThrowIfNull(domain);

        var entity = new ProductoEntity(domain.ProductID, domain.SKU)
        {
            Nombre = domain.Name,
            Marca = domain.Brand,
            Precio = domain.Price,
            Stock = domain.Stock
        };

        return entity;
    }
}
