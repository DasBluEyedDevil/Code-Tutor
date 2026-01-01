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
        var items = await _httpClient.GetFromJsonAsync<List<InventoryItem>>("/api/inventory");
        return items ?? new List<InventoryItem>();
    }
    
    public async Task<InventoryItem?> GetItemBySkuAsync(string sku)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<InventoryItem>($"/api/inventory/{sku}");
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }
    
    public async Task<bool> UpdateQuantityAsync(string sku, int quantity)
    {
        var response = await _httpClient.PutAsJsonAsync(
            $"/api/inventory/{sku}/quantity", 
            new { quantity });
        return response.IsSuccessStatusCode;
    }
    
    public async Task<StockCheckResult?> CheckStockAsync(string sku, int required)
    {
        return await _httpClient.GetFromJsonAsync<StockCheckResult>(
            $"/api/inventory/{sku}/check?required={required}");
    }
}

// Registration example
Console.WriteLine("InventoryApiClient implemented!");
Console.WriteLine("Register with:");
Console.WriteLine("builder.Services.AddHttpClient<InventoryApiClient>(client =>");
Console.WriteLine("    client.BaseAddress = new Uri(\"http://inventory-api\"));");