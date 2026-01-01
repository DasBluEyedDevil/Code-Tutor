---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`string name`**: Non-nullable reference type. Compiler expects this to NEVER be null. You'll get warnings if you try to assign null.

**`string? name`**: Nullable reference type. The `?` says 'this might be null'. Compiler will warn you to check before using it.

**`name?.Length`**: Null-conditional operator. If name is null, returns null instead of crashing. Safe way to access members!

**`name ?? "default"`**: Null-coalescing operator. If name is null, use the value after `??`. Great for providing fallback values.

**`name ??= "value"`**: Null-coalescing assignment (C# 8+). Only assigns if the variable is currently null. Clean and concise!

**`name!`**: Null-forgiving operator. Tells compiler 'trust me, it's not null'. Use sparingly - it bypasses safety checks!

**`is string actualName`**: Pattern matching with type check. If name is not null, assigns it to `actualName` and enters the block. Modern and readable!