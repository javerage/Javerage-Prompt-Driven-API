using UTMMarket.Core.Entities;

namespace UTMMarket.Core.UseCases;

/// <summary>
/// Use case for retrieving a specific sale by its unique identifier.
/// </summary>
public interface IFetchSaleByIdUseCase
{
    /// <summary>
    /// Executes the retrieval of a single sale.
    /// </summary>
    /// <param name="id">The unique identifier of the sale.</param>
    /// <param name="ct">Cancellation token for the asynchronous operation.</param>
    /// <returns>The Sale domain object if found; otherwise, null.</returns>
    Task<Sale?> ExecuteAsync(int id, CancellationToken ct = default);
}
