---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`Dictionary<TKey, TValue>`**: Two types in angle brackets: TKey (key type) and TValue (value type). Dictionary<string, int> means string keys, int values.

**`.Add(key, value)`**: Adds a key-value pair. The key MUST be unique! Trying to add the same key twice causes an error.

**`dict[key]`**: Get or set a value by its key. dict['Alice'] gets Alice's value. dict['Alice'] = 100 sets it. If key doesn't exist, CRASH!

**`.ContainsKey(key)`**: Check if a key exists BEFORE accessing it! Always check first to avoid crashes: if (dict.ContainsKey('x')) { use dict['x'] }

**`foreach (var pair in dict)`**: Loop through key-value pairs. Each 'pair' has .Key and .Value properties. Order is NOT guaranteed!