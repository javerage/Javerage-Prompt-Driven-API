namespace UTMMarket.Infrastructure.Models.Data;

/// <summary>
/// Representa el esquema de la tabla dbo.Venta optimizado para Native AOT.
/// </summary>
public partial class VentaEntity(int ventaId, string folio)
{
    public int VentaID { get; } = ventaId;
    public string Folio { get; } = folio;

    public DateTime FechaVenta { get; set; } = DateTime.Now;
    
    /// <summary>
    /// ID de referencia al catálogo de estatus.
    /// </summary>
    public int EstatusID { get; set; }
}
