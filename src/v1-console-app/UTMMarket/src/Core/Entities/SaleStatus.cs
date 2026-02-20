namespace UTMMarket.Core.Entities;

/// <summary>
/// Representa los estados posibles de una venta en el sistema.
/// </summary>
public enum SaleStatus
{
    Pending,
    Completed,
    Canceled,
    Refunded
}
