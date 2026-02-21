using UTMMarket.Core.Entities;
using UTMMarket.Core.Repositories;

namespace UTMMarket.Core.UseCases;

/// <summary>
/// Use case for searching sales based on provided filter criteria.
/// </summary>
public interface IFetchSalesByFilterUseCase
{
    /// <summary>
    /// Executes the filtered search of sales.
    /// </summary>
    /// <param name="filter">The domain-level filter criteria.</param>
    /// <param name="ct">Cancellation token for the asynchronous operation.</param>
    /// <returns>An asynchronous stream of Sale domain objects matching the criteria.</returns>
    IAsyncEnumerable<Sale> ExecuteAsync(SaleFilter filter, CancellationToken ct = default);
}
