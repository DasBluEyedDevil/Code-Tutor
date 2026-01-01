---
type: "WARNING"
title: "Common Pitfalls"
---

## Watch Out For These Issues

**Type mismatch**: @bind with `type="number"` requires int/decimal variable, not string! Type mismatch = binding fails silently. Match variable type to input type.

**Nullable types**: `int?` needs special handling! `@bind="nullableInt"` may fail. Use `@bind:get` and `@bind:set` for complex scenarios.

**@bind:event timing**: Default is 'onchange' (on blur). For live updates, use `@bind:event="oninput"`. But be aware of performance in large forms!

**Culture formatting**: `@bind:format="C"` uses current culture! $1,234.56 (US) vs 1.234,56 EUR (DE). Use `@bind:culture` to specify.

**Component binding convention**: For `@bind-Value`, component needs `Value` AND `ValueChanged` (exact naming!). `@bind-Text` needs `Text` and `TextChanged`. No flexibility here.

**Binding to readonly**: Can't @bind to readonly property! Needs get AND set. Use `@bind:get` and `@bind:set` for computed bindings.