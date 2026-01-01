---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`BenchmarkRunner.Run<T>()`**: Entry point. Runs all [Benchmark] methods in class T. Must run in Release mode!

**`[Benchmark]`**: Marks a method for benchmarking. Method should return something to prevent dead code elimination.

**`[Benchmark(Baseline = true)]`**: This is the baseline. Other results shown as ratio to baseline (1.5x slower, 2x faster, etc.).

**`[MemoryDiagnoser]`**: Class attribute. Tracks memory allocations per operation. Shows Gen0/1/2 GC collections.

**`[RankColumn]`**: Adds ranking column to results. Shows which method is fastest (1st), second (2nd), etc.

**`[Params(10, 100, 1000)]`**: Vary input size. Benchmark runs for each value. Great for seeing how performance scales.

**`[GlobalSetup]`**: Run once before all benchmarks. Use for expensive initialization that shouldn't be measured.

**Mean, Error, StdDev**: Mean is average. Error is confidence interval. StdDev shows consistency. Low StdDev = reliable results.