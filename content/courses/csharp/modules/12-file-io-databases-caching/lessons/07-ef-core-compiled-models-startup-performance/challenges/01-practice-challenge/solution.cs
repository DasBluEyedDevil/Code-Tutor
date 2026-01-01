Console.WriteLine("=== EF CORE COMPILED MODELS ===");
Console.WriteLine("Pre-build your EF model for 60-80% faster startup!");

Console.WriteLine("\n--- OPTION 1: Traditional CLI Approach ---");
Console.WriteLine("Run this CLI command:");
Console.WriteLine(@"
  dotnet ef dbcontext optimize \
    --output-dir CompiledModels \
    --namespace MyApp.CompiledModels
");
Console.WriteLine("This generates C# files:");
Console.WriteLine("  CompiledModels/");
Console.WriteLine("    AppDbContextModel.cs      (main model)");
Console.WriteLine("    ProductEntityType.cs       (per entity)");
Console.WriteLine("    OrderEntityType.cs");
Console.WriteLine("    CustomerEntityType.cs");

Console.WriteLine("\n--- OPTION 2: EF Core 9 Auto-Compiled Models (RECOMMENDED) ---");
Console.WriteLine("Step 1: Install MSBuild task package:");
Console.WriteLine(@"
  dotnet add package Microsoft.EntityFrameworkCore.Tasks --version 9.0.0
");
Console.WriteLine("Step 2: Add to your .csproj file:");
Console.WriteLine(@"
  <PropertyGroup>
    <EFOptimizeContext>true</EFOptimizeContext>
  </PropertyGroup>
");
Console.WriteLine("Step 3: Just build! Model auto-regenerates when entities change.");
Console.WriteLine("  dotnet build");

Console.WriteLine("\n--- Configure DbContext ---");
Console.WriteLine(@"
public class AppDbContext : DbContext
{
    protected override void OnConfiguring(
        DbContextOptionsBuilder options)
    {
        options
            // Use pre-built model!
            .UseModel(CompiledModels.AppDbContextModel.Instance)
            .UseSqlServer(connectionString);
    }
}
");

Console.WriteLine("\n--- PERFORMANCE IMPACT ---");
Console.WriteLine("Large models see significant startup improvements:");
Console.WriteLine("+---------------------------+---------------+");
Console.WriteLine("| Method                    | Startup       |");
Console.WriteLine("+---------------------------+---------------+");
Console.WriteLine("| Runtime model building    | Baseline      |");
Console.WriteLine("| Compiled model            | 60-80% FASTER |");
Console.WriteLine("+---------------------------+---------------+");
Console.WriteLine("The larger your model, the bigger the savings!");

Console.WriteLine("\n--- WHEN TO USE ---");
Console.WriteLine("USE Compiled Models when:");
Console.WriteLine("  + Large models (100+ entities)");
Console.WriteLine("  + Microservices (scaling needs fast cold starts)");
Console.WriteLine("  + Serverless (Azure Functions, AWS Lambda)");
Console.WriteLine("  + Container orchestration (Kubernetes pods)");

Console.WriteLine("\nSKIP Compiled Models when:");
Console.WriteLine("  - Small models (< 50 entities)");
Console.WriteLine("  - Startup time not critical");

Console.WriteLine("\n--- LIMITATIONS ---");
Console.WriteLine("  - Global Query Filters NOT compatible");
Console.WriteLine("  - Lazy-loading proxies NOT supported");
Console.WriteLine("  - Must rebuild after entity changes (auto-compile handles this!)");