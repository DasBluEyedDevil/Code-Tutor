---
type: "EXAMPLE"
title: "Production Configuration"
---

Production configuration separates secrets from settings, uses environment variables for sensitive data, and validates all required configuration at startup.

```csharp
// appsettings.Production.json
// NOTE: This file should NOT contain secrets - only non-sensitive production settings
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore": "Warning"
    }
  },
  "AllowedHosts": "shopflow.example.com;www.shopflow.example.com",
  "Cors": {
    "AllowedOrigins": [
      "https://shopflow.example.com",
      "https://www.shopflow.example.com"
    ]
  },
  "RateLimiting": {
    "EnableRateLimiting": true,
    "PermitLimit": 100,
    "WindowSeconds": 60
  },
  "Cache": {
    "DefaultExpirationMinutes": 30,
    "ProductCacheMinutes": 60
  },
  "HealthChecks": {
    "EnableDetailedErrors": false
  }
}

// Program.cs - Production configuration setup

var builder = WebApplication.CreateBuilder(args);

// Load configuration in order: appsettings.json -> appsettings.{env}.json -> env vars -> Key Vault
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

// In production, add Azure Key Vault for secrets
if (builder.Environment.IsProduction())
{
    var keyVaultUri = builder.Configuration["KeyVault:Uri"];
    if (!string.IsNullOrEmpty(keyVaultUri))
    {
        builder.Configuration.AddAzureKeyVault(
            new Uri(keyVaultUri),
            new DefaultAzureCredential());
    }
}

// Validate required configuration at startup
builder.Services.AddOptions<DatabaseOptions>()
    .Bind(builder.Configuration.GetSection("Database"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddOptions<StripeOptions>()
    .Bind(builder.Configuration.GetSection("Stripe"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddOptions<EmailOptions>()
    .Bind(builder.Configuration.GetSection("Email"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

// DatabaseOptions.cs - Configuration with validation

using System.ComponentModel.DataAnnotations;

public class DatabaseOptions
{
    [Required]
    public string ConnectionString { get; set; } = null!;
    
    [Range(1, 1000)]
    public int MaxPoolSize { get; set; } = 100;
    
    [Range(1, 300)]
    public int CommandTimeoutSeconds { get; set; } = 30;
}

public class StripeOptions
{
    [Required]
    public string SecretKey { get; set; } = null!;
    
    [Required]
    public string WebhookSecret { get; set; } = null!;
    
    [Required]
    public string PublishableKey { get; set; } = null!;
}

public class EmailOptions
{
    [Required]
    [EmailAddress]
    public string FromAddress { get; set; } = null!;
    
    [Required]
    public string ApiKey { get; set; } = null!;
}

// Environment variables for secrets (set in Azure Container Apps)
// These are injected at runtime and never stored in code
/*
  DATABASE__CONNECTIONSTRING=Host=prod-db.postgres.database.azure.com;Database=shopflow;...
  STRIPE__SECRETKEY=sk_live_...
  STRIPE__WEBHOOKSECRET=whsec_...
  STRIPE__PUBLISHABLEKEY=pk_live_...
  EMAIL__APIKEY=SG....
  EMAIL__FROMADDRESS=orders@shopflow.example.com
  APPLICATIONINSIGHTS__CONNECTIONSTRING=InstrumentationKey=...
  KEYVAULT__URI=https://shopflow-prod-kv.vault.azure.net/
*/
```
