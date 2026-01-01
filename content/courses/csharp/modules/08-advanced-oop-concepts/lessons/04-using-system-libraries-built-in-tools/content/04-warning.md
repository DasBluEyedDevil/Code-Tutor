---
type: "WARNING"
title: "Common Pitfalls"
---

## Watch Out For These Mistakes!

**String concatenation in loops**: DON'T use `str += x` in loops! Creates new string each iteration (slow). Use StringBuilder for building strings iteratively.

**File path hardcoding**: Don't hardcode paths like `C:\Users\...`! Use `Path.Combine()` for cross-platform compatibility and `Environment.GetFolderPath()` for special folders.

**Not checking File.Exists()**: Calling `File.ReadAllText()` on non-existent file throws! Always check first or wrap in try/catch.

**DateTime timezone issues**: `DateTime.Now` is local time, `DateTime.UtcNow` is UTC. For apps across timezones, use `DateTimeOffset` which stores timezone info!

**Forgetting to dispose resources**: File streams need disposing! Use `using` statement: `using var stream = File.OpenRead(path);` or wrap in try/finally.