---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// AZURE APP SERVICE DEPLOYMENT

// 1. Install Azure CLI
// Download from: https://aka.ms/installazurecli

// 2. Login to Azure
az login

// 3. Create Resource Group
az group create --name MyAppRG --location eastus

// 4. Create App Service Plan
az appservice plan create \
  --name MyAppPlan \
  --resource-group MyAppRG \
  --sku B1 \
  --is-linux

// 5. Create Web App
az webapp create \
  --name MyUniqueAppName \
  --resource-group MyAppRG \
  --plan MyAppPlan \
  --runtime "DOTNET|9.0"

// 6. Deploy from Git
az webapp deployment source config \
  --name MyUniqueAppName \
  --resource-group MyAppRG \
  --repo-url https://github.com/user/repo \
  --branch main \
  --manual-integration

// OR DEPLOY VIA VISUAL STUDIO
// Right-click project → Publish → Azure → App Service
// Follow wizard!

// CONNECTION STRING IN AZURE
// App Service → Configuration → Connection Strings
// Add: Name=DefaultConnection, Value=your-sql-connection

// ENVIRONMENT VARIABLES
// Configuration → Application Settings
// Add settings (API keys, secrets)

// AZURE SQL DATABASE
az sql server create \
  --name myserver123 \
  --resource-group MyAppRG \
  --location eastus \
  --admin-user sqladmin \
  --admin-password YourPassword123!

az sql db create \
  --name MyDatabase \
  --server myserver123 \
  --resource-group MyAppRG \
  --service-objective S0

// Update connection string in App Service
// Server=myserver123.database.windows.net;
// Database=MyDatabase;
// User Id=sqladmin;
// Password=YourPassword123!;

// MONITORING
// Azure Portal → App Service → Monitoring → Logs
// View application logs, errors, performance
```
