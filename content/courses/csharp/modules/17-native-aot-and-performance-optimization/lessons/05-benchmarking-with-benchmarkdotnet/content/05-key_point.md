---
type: "KEY_POINT"
title: "Benchmarking with BenchmarkDotNet"
---

## Key Takeaways

- **Always benchmark in Release mode** -- `BenchmarkRunner.Run<T>()` must run without the debugger attached and in Release configuration. Debug mode measurements are meaningless due to disabled optimizations.

- **`[Benchmark(Baseline = true)]` establishes the reference** -- other benchmarks are shown as ratios (1.5x slower, 2x faster). This makes results meaningful regardless of hardware differences.

- **`[MemoryDiagnoser]` reveals hidden allocations** -- track Gen0/Gen1/Gen2 GC collections and bytes allocated per operation. Performance is not just speed; excessive allocations cause GC pauses that hurt latency.
