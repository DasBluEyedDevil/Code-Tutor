---
type: "WARNING"
title: "Common Mistakes"
---

**Ignoring nullable warnings**: Those compiler warnings exist to prevent NullReferenceException! Don't just suppress them - fix the underlying issue by adding null checks.

**Overusing the ! operator**: `name!.Length` tells the compiler 'trust me, this isn't null'. But if you're wrong, you still crash! Only use `!` when you genuinely know something isn't null and the compiler just can't prove it.

**Forgetting nullable is compile-time only**: Nullable reference types don't change runtime behavior - they're just compiler hints. A `string?` variable that's actually not null will still work, and a `string` that somehow becomes null will still crash.

**Not enabling nullable in existing projects**: In .NET 6+, new projects have nullable enabled by default (`<Nullable>enable</Nullable>` in .csproj). Older projects may need to enable it manually to get these safety benefits.