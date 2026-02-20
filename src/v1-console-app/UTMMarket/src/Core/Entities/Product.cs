namespace UTMMarket.Core.Entities;

/// <summary>
/// Representa un producto dentro del catálogo de UTMMarket.
/// </summary>
public class Product(int productId, string name, string sku, string brand)
{
    public int ProductID { get; } = productId;
    public string Name { get; set; } = name;
    public string SKU { get; set; } = sku;
    public string Brand { get; set; } = brand;

    /// <summary>
    /// Precio unitario del producto. No puede ser negativo.
    /// </summary>
    public decimal Price
    {
        get => field;
        set => field = value < 0 ? throw new ArgumentException("El precio no puede ser negativo.") : value;
    }

    /// <summary>
    /// Existencias disponibles. No puede ser negativo.
    /// </summary>
    public int Stock
    {
        get => field;
        set => field = value < 0 ? throw new ArgumentException("El stock no puede ser negativo.") : value;
    }
}
