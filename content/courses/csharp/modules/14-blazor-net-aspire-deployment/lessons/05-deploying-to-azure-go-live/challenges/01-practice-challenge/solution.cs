Console.WriteLine("═══════════════════════════════════════════");
Console.WriteLine("  AZURE DEPLOYMENT COMPLETE GUIDE");
Console.WriteLine("═══════════════════════════════════════════\n");

Console.WriteLine("PHASE 1: PREPARE APPLICATION");
Console.WriteLine("\n1.1 Update Connection Strings");
Console.WriteLine("  Code: var connString = Environment.GetEnvironmentVariable(\"ConnectionString\");");
Console.WriteLine("  Don't hardcode in appsettings.json!");

Console.WriteLine("\n1.2 Add Health Check");
Console.WriteLine("  Code: app.MapHealthChecks(\"/health\");");
Console.WriteLine("  Azure uses this to verify app is running");

Console.WriteLine("\n1.3 Configure Logging");
Console.WriteLine("  Code: builder.Logging.AddAzureWebAppDiagnostics();");
Console.WriteLine("  Sends logs to Azure Portal");

Console.WriteLine("\n\nPHASE 2: AZURE CLI DEPLOYMENT");
Console.WriteLine("\n2.1 Login to Azure");
Console.WriteLine("  $ az login");
Console.WriteLine("  Opens browser, authenticate with Microsoft account");

Console.WriteLine("\n2.2 Create Resource Group");
Console.WriteLine("  $ az group create --name MyAppRG --location eastus");
Console.WriteLine("  Container for all resources");

Console.WriteLine("\n2.3 Create App Service Plan");
Console.WriteLine("  $ az appservice plan create \\");
Console.WriteLine("      --name MyAppPlan \\");
Console.WriteLine("      --resource-group MyAppRG \\");
Console.WriteLine("      --sku B1");
Console.WriteLine("  B1 = Basic tier ($55/month)");

Console.WriteLine("\n2.4 Create Web App");
Console.WriteLine("  $ az webapp create \\");
Console.WriteLine("      --name MyUniqueAppName123 \\");
Console.WriteLine("      --resource-group MyAppRG \\");
Console.WriteLine("      --plan MyAppPlan \\");
Console.WriteLine("      --runtime \"DOTNET|8.0\"");
Console.WriteLine("  URL: https://myuniqueappname123.azurewebsites.net");

Console.WriteLine("\n2.5 Deploy via Git");
Console.WriteLine("  $ az webapp deployment source config \\");
Console.WriteLine("      --name MyUniqueAppName123 \\");
Console.WriteLine("      --resource-group MyAppRG \\");
Console.WriteLine("      --repo-url https://github.com/user/repo \\");
Console.WriteLine("      --branch main");
Console.WriteLine("  Azure pulls from GitHub, builds, deploys!");

Console.WriteLine("\n\nPHASE 3: CONFIGURE PRODUCTION SETTINGS");
Console.WriteLine("\n3.1 Set Connection String");
Console.WriteLine("  Azure Portal → App Service → Configuration → Connection Strings");
Console.WriteLine("  Name: DefaultConnection");
Console.WriteLine("  Value: Server=...;Database=...;User Id=...;Password=...;");
Console.WriteLine("  Type: SQLAzure");

Console.WriteLine("\n3.2 Configure App Settings");
Console.WriteLine("  Configuration → Application Settings");
Console.WriteLine("  Add: API_KEY, EMAIL_SERVICE_URL, etc.");

Console.WriteLine("\n3.3 Enable HTTPS Only");
Console.WriteLine("  Settings → Configuration → General Settings");
Console.WriteLine("  HTTPS Only: On");
Console.WriteLine("  Redirects HTTP → HTTPS automatically");

Console.WriteLine("\n3.4 Configure Custom Domain (Optional)");
Console.WriteLine("  Custom Domains → Add custom domain");
Console.WriteLine("  Domain: www.myapp.com");
Console.WriteLine("  Add DNS CNAME record");

Console.WriteLine("\n\nPHASE 4: MONITORING & MAINTENANCE");
Console.WriteLine("\n4.1 View Logs");
Console.WriteLine("  Monitoring → Log stream");
Console.WriteLine("  Real-time application logs");

Console.WriteLine("\n4.2 Set Up Alerts");
Console.WriteLine("  Monitoring → Alerts → New alert rule");
Console.WriteLine("  Alert if: Response time > 2s, Error rate > 5%");

Console.WriteLine("\n4.3 Monitor Performance");
Console.WriteLine("  Application Insights → Performance");
Console.WriteLine("  Track requests, dependencies, exceptions");

Console.WriteLine("\n4.4 Scale Up/Out");
Console.WriteLine("  Scale Up: Bigger machine (more CPU/RAM)");
Console.WriteLine("  Scale Out: More instances (load balancing)");

Console.WriteLine("\n═══════════════════════════════════════════");
Console.WriteLine("✓ App deployed to Azure!");
Console.WriteLine("✓ Accessible worldwide");
Console.WriteLine("✓ Secure (HTTPS, secrets in Key Vault)");
Console.WriteLine("✓ Monitored (logs, alerts, Application Insights)");
Console.WriteLine("\n🎉 YOU'RE LIVE ON THE INTERNET! 🎉");