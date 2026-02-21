using UTMMarket.Core.Entities;
using UTMMarket.Core.UseCases;

namespace UTMMarket.Application;

public static class ProductConsoleUI
{
    public static async Task ShowAllProductsAsync(IGetAllProductsUseCase getAllProducts, CancellationToken ct)
    {
        Console.WriteLine("\n--- Product Catalog ---");
        int count = 0;
        await foreach (var product in getAllProducts.ExecuteAsync(ct))
        {
            Console.WriteLine($"[ID: {product.ProductID}] {product.Name} (SKU: {product.SKU}) - {product.Brand} | Price: {product.Price:C} | Stock: {product.Stock}");
            count++;
        }

        if (count == 0)
        {
            Console.WriteLine("No products found in the catalog.");
        }
        Console.WriteLine("-----------------------\n");
    }

    public static async Task ShowProductByIdAsync(IGetProductByIdUseCase getProductById, CancellationToken ct)
    {
        Console.Write("Enter Product ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID. Please enter a numeric value.");
            return;
        }

        var product = await getProductById.ExecuteAsync(id, ct);
        if (product is null)
        {
            Console.WriteLine($"Product with ID {id} not found.");
            return;
        }

        Console.WriteLine($"\nProduct Details (ID: {product.ProductID})");
        Console.WriteLine($"Name: {product.Name}");
        Console.WriteLine($"SKU: {product.SKU}");
        Console.WriteLine($"Brand: {product.Brand}");
        Console.WriteLine($"Price: {product.Price:C}");
        Console.WriteLine($"Stock: {product.Stock}\n");
    }

    public static async Task RegisterProductAsync(ICreateProductUseCase createProduct, CancellationToken ct)
    {
        Console.WriteLine("\n--- Register New Product ---");
        
        string name = ReadString("Name: ");
        string sku = ReadString("SKU: ");
        string brand = ReadString("Brand: ");
        decimal price = ReadDecimal("Price: ");
        int stock = ReadInt("Initial Stock: ");

        var newProduct = new Product(0, name, sku, brand)
        {
            Price = price,
            Stock = stock
        };

        try
        {
            int id = await createProduct.ExecuteAsync(newProduct, ct);
            Console.WriteLine($"Product successfully registered with ID: {id}\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error registering product: {ex.Message}\n");
        }
    }

    private static string ReadString(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)) return input;
            Console.WriteLine("Value cannot be empty.");
        }
    }

    private static decimal ReadDecimal(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (decimal.TryParse(Console.ReadLine(), out decimal value) && value >= 0) return value;
            Console.WriteLine("Invalid price. Enter a positive decimal value.");
        }
    }

    private static int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out int value) && value >= 0) return value;
            Console.WriteLine("Invalid stock. Enter a positive integer value.");
        }
    }
}
