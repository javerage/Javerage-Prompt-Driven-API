using UTMMarket.Core.Entities;

namespace UTMMarket.Core.UseCases;

/// <summary>
/// Use case for updating ONLY the status of an existing sale.
/// Ensures transition rules between sale states are respected.
/// </summary>
public interface IUpdateSaleStatusUseCase
{
    /// <summary>
    /// Executes the partial update of a sale's status.
    /// </summary>
    /// <param name="saleId">The unique identifier of the sale.</param>
    /// <param name="newStatus">The target status to transition the sale to.</param>
    /// <param name="ct">Cancellation token for the asynchronous operation.</param>
    /// <returns>True if the status update was successful; otherwise, false.</returns>
    Task<bool> ExecuteAsync(int saleId, SaleStatus newStatus, CancellationToken ct = default);
}
