using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

// TODO: Create Person record (Id, Name, Age)

// TODO: Create SearchBenchmarks class
// - [MemoryDiagnoser]
// - [Params(100, 1000, 10000)] for Size property
// - [GlobalSetup] to initialize data based on Size
// - Array of Person
// - Dictionary<int, Person>
// - Target ID to find

// Benchmark methods:
// - LinqSearch (Baseline = true)
// - ForLoopSearch
// - ArrayFind
// - DictionaryLookup

public class SearchBenchmarks
{
    // TODO: Implement benchmarks
}

// Print analysis
Console.WriteLine("=== Search Performance Analysis ===");
Console.WriteLine("Expected results:");
Console.WriteLine("- LINQ FirstOrDefault: O(n) - scans until found");
Console.WriteLine("- For loop: O(n) - similar but less overhead");
Console.WriteLine("- Array.Find: O(n) - optimized but still linear");
Console.WriteLine("- Dictionary: O(1) - constant time lookup!");

// BenchmarkRunner.Run<SearchBenchmarks>();