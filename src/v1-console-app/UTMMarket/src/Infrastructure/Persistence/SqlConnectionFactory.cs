using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace UTMMarket.Infrastructure.Persistence;

/// <summary>
/// SQL Server implementation of the connection factory.
/// </summary>
public sealed class SqlConnectionFactory(IConfiguration configuration) : IDbConnectionFactory
{
    private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection") 
        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

    /// <summary>
    /// Validates and provides access to the connection string using C# 14 field keyword.
    /// </summary>
    public string ConnectionString 
    { 
        get => field; 
        init => field = string.IsNullOrWhiteSpace(value) 
            ? throw new ArgumentException("Connection string cannot be empty.", nameof(value)) 
            : value; 
    } = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

    /// <summary>
    /// Creates a new SqlConnection instance.
    /// </summary>
    /// <returns>A configured SqlConnection.</returns>
    public IDbConnection CreateConnection() => new SqlConnection(ConnectionString);
}
