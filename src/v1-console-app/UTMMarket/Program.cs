using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

// Optimizaciones de compilación para Native AOT
[assembly: AssemblyMetadata("IsTrimmable", "True")]

var builder = Host.CreateApplicationBuilder(args);

// Configuración de User Secrets para desarrollo seguro
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

// Registro de servicios bajo principios Zero Trust
builder.Services.AddSingleton<IDataService, SqlDataService>();

using IHost host = builder.Build();

var logger = host.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("UTMMarket CLI v1.0 - .NET 10 Native AOT Ready");

await host.RunAsync();

/// <summary>
/// Interfaz para el servicio de datos, demostrando desvirtualización de interfaces nativas.
/// </summary>
public interface IDataService
{
    ValueTask<string> GetStatusAsync(CancellationToken ct = default);
}

/// <summary>
/// Implementación optimizada con C# 14 'field' keyword (demo conceptual).
/// </summary>
public class SqlDataService : IDataService
{
    // C# 14 'field' keyword permite acceder al campo de respaldo sin declararlo explícitamente
    public string ConnectionString 
    { 
        get => field ?? "Server=localhost;Database=UTMMarket;Trusted_Connection=True;TrustServerCertificate=True;";
        set => field = value; 
    }

    public async ValueTask<string> GetStatusAsync(CancellationToken ct = default)
    {
        // Simulación de I/O asíncrono optimizado
        await Task.Delay(10, ct);
        return "Connected to " + ConnectionString;
    }
}
