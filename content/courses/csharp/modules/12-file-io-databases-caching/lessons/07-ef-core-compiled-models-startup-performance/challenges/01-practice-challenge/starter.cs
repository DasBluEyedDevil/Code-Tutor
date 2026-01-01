Console.WriteLine("=== EF CORE COMPILED MODELS ===");

Console.WriteLine("\nOPTION 1: Traditional CLI Approach");
// TODO: Print the 'dotnet ef dbcontext optimize' command string
// Console.WriteLine("Command: ...");

Console.WriteLine("\nOPTION 2: EF Core 9 Auto-Compiled Models");
// TODO: Print the package name and .csproj property needed for auto-optimization
// Console.WriteLine("Package: ...");
// Console.WriteLine("Property: <EFOptimizeContext>...</EFOptimizeContext>");

Console.WriteLine("\nSTEP 3: Configure DbContext");
// TODO: Show how to use .UseModel() in OnConfiguring
/*
protected override void OnConfiguring(DbContextOptionsBuilder options)
{
    options.UseModel(...);
}
*/

Console.WriteLine("\nPERFORMANCE IMPACT:");
// TODO: Print the expected startup speed improvement percentage
// Console.WriteLine("Startup is ... faster!");

Console.WriteLine("\nWHEN TO USE:");
// TODO: List 2-3 use cases (e.g., microservices, large models)
// Console.WriteLine("- ...");

Console.WriteLine("\nLIMITATIONS:");
// TODO: Mention at least one limitation (e.g., Global Query Filters)
// Console.WriteLine("- ...");