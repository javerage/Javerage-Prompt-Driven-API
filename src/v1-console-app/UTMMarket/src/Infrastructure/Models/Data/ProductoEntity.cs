namespace UTMMarket.Infrastructure.Models.Data;

/// <summary>
/// Representa el esquema de la tabla dbo.Producto optimizado para Native AOT.
/// </summary>
public partial class ProductoEntity(int productoId, string sku)
{
    public int ProductoID { get; } = productoId;
    public string SKU { get; } = sku;

    public string? Nombre { get; set; }
    public string? Marca { get; set; }

    /// <summary>
    /// Mapeo de DECIMAL(19,4). Validación de restricción CHECK (Precio >= 0).
    /// </summary>
    public decimal Precio
    {
        get => field;
        set => field = value < 0 ? throw new ArgumentOutOfRangeException(nameof(value), "El precio no puede ser negativo.") : value;
    }

    /// <summary>
    /// Validación de restricción CHECK (Stock >= 0).
    /// </summary>
    public int Stock
    {
        get => field;
        set => field = value < 0 ? throw new ArgumentOutOfRangeException(nameof(value), "El stock no puede ser negativo.") : value;
    }
}
