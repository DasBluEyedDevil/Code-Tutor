using System;

Console.WriteLine("=== BLAZOR RENDERING MODES (.NET 8) ===");

Console.WriteLine("\n1. STATIC SSR (Server-Side Rendering)");
Console.WriteLine("   Use case: Product catalog, blog posts");
Console.WriteLine("   Code: @rendermode RenderMode.Static");
Console.WriteLine("   Pros: Fast, SEO-friendly, low server load");
Console.WriteLine("   Cons: No interactivity");

// Add other modes...

Console.WriteLine("\n=== COMPARISON TABLE ===");
Console.WriteLine("Feature          | Server  | WASM    | Auto");
Console.WriteLine("-----------------|---------|---------|--------");
// Fill in comparison

Console.WriteLine("\n=== .NET 8 CONFIGURATION ===");
Console.WriteLine("builder.Services.AddRazorComponents()");
// Show configuration