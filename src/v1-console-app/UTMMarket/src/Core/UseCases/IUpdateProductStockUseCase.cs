namespace UTMMarket.Core.UseCases;

/// <summary>
/// Use Case for a specific partial update of a product's stock.
/// Useful for inventory adjustments and sales transactions.
/// </summary>
public interface IUpdateProductStockUseCase
{
    /// <summary>
    /// Executes the atomic stock update.
    /// </summary>
    /// <param name="productId">The unique ID of the product.</param>
    /// <param name="newStock">The new total quantity available.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>True if the update was successful; otherwise, false.</returns>
    Task<bool> ExecuteAsync(int productId, int newStock, CancellationToken ct = default);
}
