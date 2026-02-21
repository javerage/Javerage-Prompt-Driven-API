using UTMMarket.Core.Entities;
using UTMMarket.Core.Repositories;

namespace UTMMarket.Core.UseCases;

/// <summary>
/// Use Case for searching products based on dynamic filtering criteria.
/// </summary>
public interface ISearchProductsUseCase
{
    /// <summary>
    /// Executes the search logic with the provided filters.
    /// </summary>
    /// <param name="filter">The search criteria (Name, SKU, Brand, Price range).</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>An asynchronous stream of matching products.</returns>
    IAsyncEnumerable<Product> ExecuteAsync(ProductFilter filter, CancellationToken ct = default);
}
