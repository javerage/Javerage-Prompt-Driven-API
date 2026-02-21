using System.Data;

namespace UTMMarket.Infrastructure.Persistence;

/// <summary>
/// Contract for managing the lifecycle of database connections.
/// </summary>
public interface IDbConnectionFactory
{
    /// <summary>
    /// Creates and returns a new database connection.
    /// </summary>
    /// <returns>An instance of IDbConnection.</returns>
    IDbConnection CreateConnection();
}
