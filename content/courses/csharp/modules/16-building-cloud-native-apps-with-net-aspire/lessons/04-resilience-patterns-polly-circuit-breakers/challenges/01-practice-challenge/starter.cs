using Microsoft.Extensions.Http.Resilience;
using Polly;

public class WeatherApiClient
{
    private readonly HttpClient _httpClient;
    
    public WeatherApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<WeatherData?> GetWeatherAsync(string city)
    {
        var response = await _httpClient.GetAsync($"/api/weather/{city}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<WeatherData>();
    }
}

public record WeatherData(string City, double Temperature, string Conditions);

// Configure the HttpClient with resilience
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<WeatherApiClient>(client =>
{
    client.BaseAddress = new Uri("http://weather-api");
})
// TODO: Add resilience handler with:
// - Retry (4 attempts, 200ms delay, exponential, jitter)
// - Circuit breaker (30% failure, 5 min throughput, 20s break)
// - Timeout (8 seconds)
;

Console.WriteLine("Configure resilience for WeatherApiClient!");