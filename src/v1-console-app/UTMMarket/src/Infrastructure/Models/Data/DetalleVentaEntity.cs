namespace UTMMarket.Infrastructure.Models.Data;

/// <summary>
/// Representa el esquema de la tabla dbo.DetalleVenta optimizado para Native AOT.
/// </summary>
public partial class DetalleVentaEntity(int detalleVentaId, int ventaId, int productoId)
{
    public int DetalleVentaID { get; } = detalleVentaId;
    public int VentaID { get; } = ventaId;
    public int ProductoID { get; } = productoId;

    /// <summary>
    /// Validación de restricción CHECK (Cantidad > 0).
    /// </summary>
    public int Cantidad
    {
        get => field;
        set => field = value <= 0 ? throw new ArgumentOutOfRangeException(nameof(value), "La cantidad debe ser mayor a cero.") : value;
    }

    /// <summary>
    /// Mapeo de DECIMAL(19,4). Precio capturado al momento de la venta.
    /// </summary>
    public decimal PrecioUnitario
    {
        get => field;
        set => field = value < 0 ? throw new ArgumentOutOfRangeException(nameof(value), "El precio unitario no puede ser negativo.") : value;
    }

    /// <summary>
    /// Propiedad calculada en memoria para evitar redundancia en DB.
    /// </summary>
    public decimal TotalDetalle => PrecioUnitario * Cantidad;
}
