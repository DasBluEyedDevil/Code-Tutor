Console.WriteLine("=== HYBRIDCACHE (MODERN CACHING) ===");

Console.WriteLine("\n--- SETUP ---");
// TODO: Show builder.Services.AddHybridCache() configuration
// builder.Services...

Console.WriteLine("\n--- USAGE ---");
// TODO: Show how to use _cache.GetOrCreateAsync<T>(key, factory)
// var data = await _cache...

Console.WriteLine("\n--- TAGGING ---");
// TODO: Show how to add tags and use RemoveByTagAsync
// await _cache.RemoveByTagAsync("...");

Console.WriteLine("\n--- STAMPEDE PROTECTION ---");
// TODO: Briefly explain how HybridCache handles multiple concurrent requests for the same key
// Console.WriteLine("Protection: ...");

Console.WriteLine("\n--- COMPARISON ---");
// TODO: State one advantage of HybridCache over IDistributedCache
// Console.WriteLine("Advantage: ...");