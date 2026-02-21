using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using UTMMarket.Application;
using UTMMarket.Core.UseCases;
using UTMMarket.Infrastructure;

// Optimization for Native AOT
[assembly: System.Reflection.AssemblyMetadata("IsTrimmable", "True")]

var builder = Host.CreateApplicationBuilder(args);

// Ensure User Secrets are loaded in Development
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

// Register Infrastructure and Application Services
builder.Services.AddPersistence();
builder.Services.AddApplication();

using IHost host = builder.Build();

var logger = host.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("UTMMarket CLI v1.0 - .NET 10 Native AOT Ready");

// Interactive Menu
bool exitRequested = false;
while (!exitRequested)
{
    Console.Clear();
    Console.WriteLine("""
    ********************************************************
    *             UTMMarket - Management System            *
    ********************************************************
    1. List all products
    2. Search product by ID
    3. Register new product
    4. Exit
    ********************************************************
    """);
    Console.Write("Select an option: ");
    string? option = Console.ReadLine();

    using (var scope = host.Services.CreateScope())
    {
        var ct = CancellationToken.None;

        switch (option)
        {
            case "1":
                var getAllProducts = scope.ServiceProvider.GetRequiredService<IGetAllProductsUseCase>();
                await ProductConsoleUI.ShowAllProductsAsync(getAllProducts, ct);
                WaitForKey();
                break;
            case "2":
                var getProductById = scope.ServiceProvider.GetRequiredService<IGetProductByIdUseCase>();
                await ProductConsoleUI.ShowProductByIdAsync(getProductById, ct);
                WaitForKey();
                break;
            case "3":
                var createProduct = scope.ServiceProvider.GetRequiredService<ICreateProductUseCase>();
                await ProductConsoleUI.RegisterProductAsync(createProduct, ct);
                WaitForKey();
                break;
            case "4":
                exitRequested = true;
                break;
            default:
                Console.WriteLine("Invalid option. Press any key to try again.");
                Console.ReadKey();
                break;
        }
    }
}

Console.WriteLine("Closing UTMMarket CLI. Goodbye!");

void WaitForKey()
{
    Console.WriteLine("\nPress any key to return to the menu...");
    Console.ReadKey();
}
