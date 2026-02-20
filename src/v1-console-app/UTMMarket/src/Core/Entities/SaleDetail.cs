namespace UTMMarket.Core.Entities;

/// <summary>
/// Representa el detalle de un producto dentro de una venta.
/// </summary>
public class SaleDetail
{
    public int SaleDetailID { get; init; }
    public Product Product { get; }
    public decimal UnitPrice { get; init; }
    public int Quantity { get; set; }

    /// <summary>
    /// Constructor primario simulado para asegurar la captura del precio actual del producto.
    /// </summary>
    public SaleDetail(Product product, int quantity)
    {
        Product = product ?? throw new ArgumentNullException(nameof(product));
        UnitPrice = product.Price; // Captura de precio histórico
        Quantity = quantity > 0 ? quantity : throw new ArgumentException("La cantidad debe ser mayor a cero.");
    }

    /// <summary>
    /// Cálculo del total del detalle (Precio Unitario * Cantidad).
    /// </summary>
    public decimal TotalDetail => UnitPrice * Quantity;
}
