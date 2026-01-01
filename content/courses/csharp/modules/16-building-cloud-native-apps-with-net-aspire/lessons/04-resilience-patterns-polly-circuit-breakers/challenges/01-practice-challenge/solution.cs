using Microsoft.Extensions.Http.Resilience;
using Polly;
using System.Net;

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
.AddResilienceHandler("WeatherResilience", pipeline =>
{
    // RETRY: 4 attempts with exponential backoff + jitter
    pipeline.AddRetry(new HttpRetryStrategyOptions
    {
        MaxRetryAttempts = 4,
        Delay = TimeSpan.FromMilliseconds(200),
        BackoffType = DelayBackoffType.Exponential,
        UseJitter = true,
        ShouldHandle = new PredicateBuilder<HttpResponseMessage>()
            .Handle<HttpRequestException>()
            .HandleResult(r => r.StatusCode == HttpStatusCode.ServiceUnavailable)
            .HandleResult(r => r.StatusCode == HttpStatusCode.TooManyRequests),
        OnRetry = args =>
        {
            Console.WriteLine($"Retry attempt {args.AttemptNumber} after {args.RetryDelay.TotalMilliseconds}ms");
            return ValueTask.CompletedTask;
        }
    });
    
    // CIRCUIT BREAKER: 30% failure rate threshold
    pipeline.AddCircuitBreaker(new HttpCircuitBreakerStrategyOptions
    {
        FailureRatio = 0.3,  // 30%
        SamplingDuration = TimeSpan.FromSeconds(10),
        MinimumThroughput = 5,
        BreakDuration = TimeSpan.FromSeconds(20),
        OnOpened = args =>
        {
            Console.WriteLine("Circuit breaker OPENED - weather service unavailable");
            return ValueTask.CompletedTask;
        }
    });
    
    // TIMEOUT: 8 seconds max
    pipeline.AddTimeout(TimeSpan.FromSeconds(8));
});

Console.WriteLine("WeatherApiClient resilience configured!");
Console.WriteLine("Retry: 4 attempts, exponential backoff (200ms base), jitter enabled");
Console.WriteLine("Circuit Breaker: 30% failure ratio, 20s break duration");
Console.WriteLine("Timeout: 8 seconds per request");
Console.WriteLine("Handles: HttpRequestException, 503, 429");