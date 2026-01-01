using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

public record Person(int Id, string Name, int Age);

[MemoryDiagnoser]
[RankColumn]
public class SearchBenchmarks
{
    [Params(100, 1000, 10000)]
    public int Size { get; set; }
    
    private Person[] _people = Array.Empty<Person>();
    private Dictionary<int, Person> _dictionary = new();
    private int _targetId;
    
    [GlobalSetup]
    public void Setup()
    {
        // Generate test data
        _people = Enumerable.Range(1, Size)
            .Select(i => new Person(i, $"Person{i}", 20 + (i % 50)))
            .ToArray();
        
        // Build dictionary for O(1) lookups
        _dictionary = _people.ToDictionary(p => p.Id);
        
        // Search for item in middle (worst-case for linear search)
        _targetId = Size / 2;
    }
    
    [Benchmark(Baseline = true)]
    public Person? LinqSearch()
    {
        return _people.FirstOrDefault(p => p.Id == _targetId);
    }
    
    [Benchmark]
    public Person? ForLoopSearch()
    {
        for (var i = 0; i < _people.Length; i++)
        {
            if (_people[i].Id == _targetId)
                return _people[i];
        }
        return null;
    }
    
    [Benchmark]
    public Person? ArrayFind()
    {
        return Array.Find(_people, p => p.Id == _targetId);
    }
    
    [Benchmark]
    public Person? DictionaryLookup()
    {
        return _dictionary.TryGetValue(_targetId, out var person) ? person : null;
    }
}

// Print analysis
Console.WriteLine("=== Search Performance Analysis ===");
Console.WriteLine();
Console.WriteLine("Benchmark setup:");
Console.WriteLine("- Array sizes: 100, 1000, 10000 elements");
Console.WriteLine("- Search target: middle element (worst case for linear)");
Console.WriteLine("- MemoryDiagnoser: tracks allocations");
Console.WriteLine();
Console.WriteLine("Expected results:");
Console.WriteLine("- LINQ FirstOrDefault: O(n) - enumerator overhead + delegate calls");
Console.WriteLine("- For loop: O(n) - minimal overhead, direct array access");
Console.WriteLine("- Array.Find: O(n) - optimized native code, but still linear");
Console.WriteLine("- Dictionary: O(1) - constant time regardless of size!");
Console.WriteLine();
Console.WriteLine("Memory allocations:");
Console.WriteLine("- LINQ: Allocates enumerator");
Console.WriteLine("- For loop: Zero allocations");
Console.WriteLine("- Array.Find: Zero allocations");
Console.WriteLine("- Dictionary: Zero allocations (after setup)");
Console.WriteLine();
Console.WriteLine("Key insight:");
Console.WriteLine("Dictionary lookup stays CONSTANT while others grow linearly!");
Console.WriteLine("At 10,000 items, Dictionary can be 1000x+ faster.");
Console.WriteLine();
Console.WriteLine("Run with: dotnet run -c Release");

// Uncomment to run actual benchmarks:
// BenchmarkRunner.Run<SearchBenchmarks>();