using UTMMarket.Core.Entities;

namespace UTMMarket.Core.UseCases;

/// <summary>
/// Use case for orchestrating the creation and persistence of a new sale.
/// Ensures domain invariants are met before commitment.
/// </summary>
public interface ICreateSaleUseCase
{
    /// <summary>
    /// Executes the creation logic for a new sale.
    /// </summary>
    /// <param name="sale">The Sale domain aggregate root to persist.</param>
    /// <param name="ct">Cancellation token for the asynchronous operation.</param>
    /// <returns>The persisted Sale aggregate with its assigned identity.</returns>
    Task<Sale> ExecuteAsync(Sale sale, CancellationToken ct = default);
}
