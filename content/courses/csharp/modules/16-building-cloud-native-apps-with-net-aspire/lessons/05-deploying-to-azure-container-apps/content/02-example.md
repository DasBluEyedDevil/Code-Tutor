---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// ===== STEP 1: Prepare Your AppHost =====
// AppHost/Program.cs - Production-ready configuration

var builder = DistributedApplication.CreateBuilder(args);

// Infrastructure - Aspire maps these to Azure services
var cache = builder.AddRedis("cache");
var db = builder.AddPostgres("postgres")
    .AddDatabase("catalogdb");

// Services
var api = builder.AddProject<Projects.CatalogApi>("api")
    .WithReference(cache)
    .WithReference(db)
    .WithExternalHttpEndpoints();  // Expose to internet

builder.AddProject<Projects.WebApp>("webapp")
    .WithReference(api)
    .WithExternalHttpEndpoints();

builder.Build().Run();

// ===== STEP 2: Initialize Azure Developer CLI =====
// Run in terminal (from solution folder):

// Initialize azd (one time)
// > azd init
//   - Creates azure.yaml
//   - Creates .azure/ folder

// ===== azure.yaml (auto-generated) =====
// name: my-aspire-app
// services:
//   app:
//     project: ./MyApp.AppHost/MyApp.AppHost.csproj
//     host: containerapp

// ===== STEP 3: Deploy! =====
// > azd up
//
// This command:
// 1. Builds all your projects as containers
// 2. Creates Azure Container Registry
// 3. Pushes containers to registry
// 4. Creates Azure Container Apps Environment
// 5. Creates managed Redis (Azure Cache for Redis)
// 6. Creates managed Postgres (Azure Database)
// 7. Deploys your containers
// 8. Configures networking/service discovery
// 9. Sets up connection strings

// ===== STEP 4: Environment Configuration =====
// appsettings.Production.json - Override for production
{
    "Logging": {
        "LogLevel": {
            "Default": "Warning",
            "Microsoft.Hosting.Lifetime": "Information"
        }
    }
}

// ===== STEP 5: GitHub Actions CI/CD =====
// .github/workflows/deploy.yml

// name: Deploy to Azure
// 
// on:
//   push:
//     branches: [main]
// 
// jobs:
//   deploy:
//     runs-on: ubuntu-latest
//     steps:
//       - uses: actions/checkout@v4
//       
//       - name: Install azd
//         uses: Azure/setup-azd@v1
//       
//       - name: Log in with Azure (federated)
//         run: azd auth login --no-prompt
//         env:
//           AZURE_CLIENT_ID: ${{ vars.AZURE_CLIENT_ID }}
//           AZURE_TENANT_ID: ${{ vars.AZURE_TENANT_ID }}
//           AZURE_SUBSCRIPTION_ID: ${{ vars.AZURE_SUBSCRIPTION_ID }}
//       
//       - name: Deploy
//         run: azd up --no-prompt
//         env:
//           AZURE_ENV_NAME: ${{ vars.AZURE_ENV_NAME }}

// ===== USEFUL AZD COMMANDS =====
// azd init          - Initialize project
// azd up            - Deploy everything
// azd deploy        - Deploy code only (faster)
// azd down          - Delete all Azure resources
// azd monitor       - Open Azure Portal monitoring
// azd env list      - List environments (dev, staging, prod)
// azd env select    - Switch environment

// ===== ASPIRE MANIFEST =====
// Aspire generates a manifest.json describing your app
// Run: dotnet run --project AppHost -- --publisher manifest

Console.WriteLine("Deployment steps:");
Console.WriteLine("1. azd init      - Initialize Azure config");
Console.WriteLine("2. azd up        - Deploy to Azure!");
Console.WriteLine("3. azd monitor   - View in Azure Portal");
Console.WriteLine("");
Console.WriteLine("Aspire maps to Azure services:");
Console.WriteLine("  AddRedis -> Azure Cache for Redis");
Console.WriteLine("  AddPostgres -> Azure Database for PostgreSQL");
Console.WriteLine("  AddProject -> Azure Container App");
```
