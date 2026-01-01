---
type: "WARNING"
title: "Common Pitfalls"
---

## Watch Out For These!

**KeyNotFoundException**: Accessing dict["key"] when "key" doesn't exist throws an exception! ALWAYS use ContainsKey() first, or use TryGetValue() for safer access.

**Duplicate key exception**: Calling .Add() with a key that already exists crashes! Use dict[key] = value instead if you want to update OR add.

**Keys are case-sensitive (for strings)**: "Alice" and "alice" are DIFFERENT keys! If you need case-insensitive matching, use StringComparer.OrdinalIgnoreCase when creating the dictionary.

**No guaranteed order**: Don't rely on items being in any particular order when looping. If you need order, consider SortedDictionary or sort the keys yourself.

**Null keys**: String keys cannot be null - attempting to use null as a key throws ArgumentNullException!