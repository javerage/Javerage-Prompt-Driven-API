using UTMMarket.Core.Entities;

namespace UTMMarket.Core.UseCases;

/// <summary>
/// Use case for retrieving all sales records from the system.
/// Optimized for streaming performance using IAsyncEnumerable.
/// </summary>
public interface IFetchAllSalesUseCase
{
    /// <summary>
    /// Executes the retrieval of all sales.
    /// </summary>
    /// <param name="ct">Cancellation token for the asynchronous operation.</param>
    /// <returns>An asynchronous stream of Sale domain objects.</returns>
    IAsyncEnumerable<Sale> ExecuteAsync(CancellationToken ct = default);
}
