namespace UTMMarket.Core.Entities;

/// <summary>
/// Representa una transacción de venta completa en UTMMarket.
/// </summary>
public class Sale(int saleId, string folio)
{
    public int SaleID { get; } = saleId;
    public string Folio { get; init; } = folio;
    public DateTime SaleDate { get; init; } = DateTime.Now;
    public SaleStatus Status { get; set; } = SaleStatus.Pending;

    /// <summary>
    /// Lista de detalles asociados a la venta.
    /// </summary>
    public List<SaleDetail> Details { get; } = [];

    /// <summary>
    /// Suma total de unidades vendidas en todos los detalles.
    /// </summary>
    public int TotalItems => Details.Sum(d => d.Quantity);

    /// <summary>
    /// Monto total de la venta calculado dinámicamente.
    /// </summary>
    public decimal TotalSale => Details.Sum(d => d.TotalDetail);

    /// <summary>
    /// Agrega un producto a la venta, creando el detalle correspondiente.
    /// </summary>
    public void AddDetail(Product product, int quantity)
    {
        Details.Add(new SaleDetail(product, quantity));
    }
}
