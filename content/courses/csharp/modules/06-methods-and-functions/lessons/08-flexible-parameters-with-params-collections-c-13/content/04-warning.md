---
type: "WARNING"
title: "Common Pitfalls"
---

**params must be the last parameter!** You can't have `void Method(params string[] items, int count)` - params must come last in the parameter list.

**Don't combine params with ref/out/in!** Modifiers like `ref`, `out`, and `in` cannot be used with params parameters.

**Requires .NET 9 / C# 13!** The enhanced params collections feature requires C# 13 and .NET 9. Older projects still need `params T[]` arrays.

**No default values for params!** You cannot write `params string[] items = null` - params parameters cannot have default values.

**Overload resolution changes:** With multiple collection types supported, be careful about overload ambiguity. The compiler picks based on argument types.