---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// ===== ASPIRE'S BUILT-IN RESILIENCE =====
// In ServiceDefaults, this is already configured!

builder.Services.ConfigureHttpClientDefaults(http =>
{
    // AddStandardResilienceHandler adds:
    // - Retry (3 attempts with exponential backoff)
    // - Circuit breaker (opens after failures)
    // - Timeout (30 seconds total)
    // - Rate limiter (prevents overload)
    http.AddStandardResilienceHandler();
});

// ===== CUSTOM POLLY POLICIES =====
// Install: Microsoft.Extensions.Http.Resilience (included in Aspire)

using Microsoft.Extensions.Http.Resilience;
using Polly;

// Configure custom resilience for specific client
builder.Services.AddHttpClient<PaymentApiClient>(client =>
{
    client.BaseAddress = new Uri("http://payment-api");
})
.AddResilienceHandler("PaymentRetry", builder =>
{
    // RETRY: Try 5 times with exponential backoff
    builder.AddRetry(new HttpRetryStrategyOptions
    {
        MaxRetryAttempts = 5,
        Delay = TimeSpan.FromMilliseconds(500),
        BackoffType = DelayBackoffType.Exponential,
        UseJitter = true,  // Randomize delays to avoid thundering herd
        ShouldHandle = new PredicateBuilder<HttpResponseMessage>()
            .Handle<HttpRequestException>()
            .HandleResult(r => r.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
    });
    
    // CIRCUIT BREAKER: Open after 3 failures
    builder.AddCircuitBreaker(new HttpCircuitBreakerStrategyOptions
    {
        FailureRatio = 0.5,           // 50% failure rate
        SamplingDuration = TimeSpan.FromSeconds(10),
        MinimumThroughput = 3,        // Need 3 requests to evaluate
        BreakDuration = TimeSpan.FromSeconds(30)
    });
    
    // TIMEOUT: 10 second limit per request
    builder.AddTimeout(TimeSpan.FromSeconds(10));
});

// ===== USING POLLY DIRECTLY =====
using Polly;
using Polly.Retry;
using Polly.CircuitBreaker;
using Polly.Timeout;

public class ResilientService
{
    private readonly ResiliencePipeline _pipeline;
    private readonly ILogger<ResilientService> _logger;
    
    public ResilientService(ILogger<ResilientService> logger)
    {
        _logger = logger;
        
        // Build a resilience pipeline
        _pipeline = new ResiliencePipelineBuilder()
            .AddRetry(new RetryStrategyOptions
            {
                MaxRetryAttempts = 3,
                Delay = TimeSpan.FromSeconds(1),
                OnRetry = args =>
                {
                    _logger.LogWarning(
                        "Retry attempt {Attempt} after {Delay}ms",
                        args.AttemptNumber,
                        args.RetryDelay.TotalMilliseconds);
                    return ValueTask.CompletedTask;
                }
            })
            .AddCircuitBreaker(new CircuitBreakerStrategyOptions
            {
                FailureRatio = 0.5,
                SamplingDuration = TimeSpan.FromSeconds(10),
                BreakDuration = TimeSpan.FromSeconds(30),
                OnOpened = args =>
                {
                    _logger.LogError("Circuit OPENED! Service unavailable.");
                    return ValueTask.CompletedTask;
                },
                OnClosed = args =>
                {
                    _logger.LogInformation("Circuit CLOSED. Service recovered.");
                    return ValueTask.CompletedTask;
                }
            })
            .AddTimeout(TimeSpan.FromSeconds(5))
            .Build();
    }
    
    public async Task<string> CallExternalServiceAsync()
    {
        // Execute with resilience
        return await _pipeline.ExecuteAsync(async token =>
        {
            // Your actual operation here
            await Task.Delay(100, token);  // Simulate work
            return "Success!";
        });
    }
}

// ===== CIRCUIT BREAKER STATES =====
// CLOSED: Normal operation, requests flow through
// OPEN: Failures exceeded threshold, requests fail fast
// HALF-OPEN: Testing if service recovered, limited requests

// CLOSED -> (failures) -> OPEN -> (wait) -> HALF-OPEN -> (success) -> CLOSED
//                                                     -> (failure) -> OPEN

Console.WriteLine("Resilience patterns configured!");
Console.WriteLine("Retry: 3 attempts with exponential backoff");
Console.WriteLine("Circuit Breaker: Opens after 50% failures");
Console.WriteLine("Timeout: 5 second limit");
```
