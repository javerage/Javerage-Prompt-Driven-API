using UTMMarket.Core.Entities;

namespace UTMMarket.Core.Repositories;

/// <summary>
/// Criteria for filtering sales within the repository.
/// Optimized for Native AOT and Dapper compatibility.
/// </summary>
/// <param name="StartDate">The start date for the search range.</param>
/// <param name="EndDate">The end date for the search range.</param>
/// <param name="Status">The specific status to filter by.</param>
/// <param name="Folio">The unique folio identifier or partial match.</param>
public record SaleFilter(
    DateTime? StartDate = null,
    DateTime? EndDate = null,
    SaleStatus? Status = null,
    string? Folio = null)
{
    /// <summary>
    /// Validates that the end date is not before the start date using C# 14 field keyword.
    /// </summary>
    public DateTime? EndDate
    {
        get => field;
        init => field = (value < StartDate) 
            ? throw new ArgumentException("End date cannot be earlier than start date.") 
            : value;
    } = EndDate;
}

/// <summary>
/// Defines the persistence contract for the Sale aggregate root.
/// Designed for high performance and Native AOT compatibility.
/// </summary>
public interface ISaleRepository
{
    /// <summary>
    /// Retrieves all sales as a stream to optimize memory usage.
    /// </summary>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>An asynchronous stream of Sale domain objects.</returns>
    /// <remarks>
    /// Precondition: The database connection must be available.
    /// Postcondition: Returns an IAsyncEnumerable that yields sales as they are read.
    /// </remarks>
    IAsyncEnumerable<Sale> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a specific sale by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the sale.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>The Sale object if found; otherwise, null.</returns>
    Task<Sale?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds sales based on a set of filtering criteria.
    /// Avoids Expression trees to maintain AOT compatibility with Dapper interceptors.
    /// </summary>
    /// <param name="filter">The criteria to filter sales.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>An asynchronous stream of Sale domain objects matching the criteria.</returns>
    IAsyncEnumerable<Sale> FindAsync(SaleFilter filter, CancellationToken cancellationToken = default);

    /// <summary>
    /// Persists a new sale aggregate into the database.
    /// </summary>
    /// <param name="sale">The sale domain object to persist.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>The persisted Sale object with its generated identity.</returns>
    Task<Sale> AddAsync(Sale sale, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing sale aggregate and its details.
    /// </summary>
    /// <param name="sale">The sale aggregate with updated information.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous update operation.</returns>
    Task UpdateAsync(Sale sale, CancellationToken cancellationToken = default);
}
