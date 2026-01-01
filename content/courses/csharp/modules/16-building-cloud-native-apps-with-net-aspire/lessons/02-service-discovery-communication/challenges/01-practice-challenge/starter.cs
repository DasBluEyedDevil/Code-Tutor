using System.Net.Http.Json;

public record InventoryItem(int Id, string Sku, int Quantity, string WarehouseId);

public record StockCheckResult(bool InStock, int Available, int Required);

public class InventoryApiClient
{
    private readonly HttpClient _httpClient;
    
    public InventoryApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        // Base address set via DI: http://inventory-api
    }
    
    public async Task<List<InventoryItem>> GetAllItemsAsync()
    {
        // TODO: GET /api/inventory
        return new List<InventoryItem>();
    }
    
    public async Task<InventoryItem?> GetItemBySkuAsync(string sku)
    {
        // TODO: GET /api/inventory/{sku}
        return null;
    }
    
    public async Task<bool> UpdateQuantityAsync(string sku, int quantity)
    {
        // TODO: PUT /api/inventory/{sku}/quantity with { quantity } body
        return false;
    }
    
    public async Task<StockCheckResult?> CheckStockAsync(string sku, int required)
    {
        // TODO: GET /api/inventory/{sku}/check?required={required}
        return null;
    }
}

// Registration example
Console.WriteLine("Register with: builder.Services.AddHttpClient<InventoryApiClient>(...)");