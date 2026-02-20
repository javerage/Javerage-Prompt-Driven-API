using UTMMarket.Core.Entities;

namespace UTMMarket.Core.Repositories;

/// <summary>
/// Define los criterios de búsqueda para productos, evitando el uso de expresiones dinámicas
/// para garantizar la compatibilidad con Native AOT.
/// </summary>
public record ProductFilter(
    string? Name = null,
    string? SKU = null,
    string? Brand = null,
    decimal? MinPrice = null,
    decimal? MaxPrice = null
);

/// <summary>
/// Define el contrato de persistencia para la entidad Product.
/// </summary>
/// <remarks>
/// Este contrato es puramente de dominio y no tiene dependencias con la infraestructura.
/// </remarks>
public interface IProductRepository
{
    /// <summary>
    /// Recupera todos los productos del catálogo mediante streaming asíncrono.
    /// </summary>
    /// <param name="ct">Token de cancelación para abortar la operación I/O.</param>
    /// <returns>Una secuencia asíncrona de objetos de dominio Product.</returns>
    IAsyncEnumerable<Product> GetAllAsync(CancellationToken ct = default);

    /// <summary>
    /// Busca un producto por su identificador único de base de datos.
    /// </summary>
    /// <param name="productId">Identificador único del producto.</param>
    /// <param name="ct">Token de cancelación.</param>
    /// <returns>El objeto Product si existe; de lo contrario, null.</returns>
    Task<Product?> GetByIdAsync(int productId, CancellationToken ct = default);

    /// <summary>
    /// Realiza una búsqueda filtrada de productos basada en criterios estáticos.
    /// </summary>
    /// <param name="filter">Objeto que contiene los criterios de búsqueda.</param>
    /// <param name="ct">Token de cancelación.</param>
    /// <returns>Una secuencia asíncrona de productos que coinciden con los criterios.</returns>
    IAsyncEnumerable<Product> FindAsync(ProductFilter filter, CancellationToken ct = default);

    /// <summary>
    /// Registra un nuevo producto en el sistema de persistencia.
    /// </summary>
    /// <param name="product">Objeto de dominio a persistir.</param>
    /// <param name="ct">Token de cancelación.</param>
    /// <returns>El identificador generado para el nuevo producto.</returns>
    Task<int> AddAsync(Product product, CancellationToken ct = default);

    /// <summary>
    /// Actualiza la información completa de un producto existente.
    /// </summary>
    /// <param name="product">Objeto de dominio con los datos actualizados.</param>
    /// <param name="ct">Token de cancelación.</param>
    /// <returns>Un valor booleano que indica si la operación fue exitosa.</returns>
    Task<bool> UpdateAsync(Product product, CancellationToken ct = default);

    /// <summary>
    /// Realiza una actualización atómica y parcial del stock de un producto.
    /// </summary>
    /// <param name="productId">Identificador del producto.</param>
    /// <param name="newStock">La nueva cantidad disponible.</param>
    /// <param name="ct">Token de cancelación.</param>
    /// <returns>Éxito de la operación.</returns>
    Task<bool> UpdateStockAsync(int productId, int newStock, CancellationToken ct = default);
}
