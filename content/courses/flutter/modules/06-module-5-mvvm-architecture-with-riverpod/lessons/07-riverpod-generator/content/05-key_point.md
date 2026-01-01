---
type: "KEY_POINT"
title: "Generator Benefits"
---

Using the Riverpod generator provides significant advantages over manual provider creation:

### Benefit 1: Drastically Less Boilerplate

Compare the code you write:
- **Manual**: 15-20 lines per provider (class + provider declaration)
- **Generated**: 5-10 lines per provider (just the class)

For an app with 30 providers, that is potentially 300+ lines of boilerplate eliminated.

### Benefit 2: Type Safety at Compile Time

With manual providers, type mismatches cause runtime errors. With generated providers, the generator ensures all types match correctly. If there is a problem, you get a build error immediately, not a crash when users run your app.

### Benefit 3: Consistent Naming Convention

The generator enforces a standard naming pattern:
- Class `Counter` always becomes `counterProvider`
- Class `UserProfile` always becomes `userProfileProvider`

No more wondering if someone named it `counterProvider`, `counterNotifier`, or `myCounter`. The pattern is automatic and predictable.

### Benefit 4: Part Files Keep Code Organized

Generated code goes into `.g.dart` files (like `counter.g.dart`). This keeps your source files clean. You write the logic; the generator writes the wiring.

### Benefit 5: IDE Support and Autocomplete

Because providers are generated from classes you define, your IDE understands the structure. You get:
- Autocomplete for provider names
- Go-to-definition jumps to your source code
- Refactoring tools work correctly

### Benefit 6: Future-Proof

When Riverpod updates its internal APIs, you just update the generator package and regenerate. Your code stays the same; only the generated code changes.

### When to Use the Generator

**Use the generator when:**
- Building a medium to large app (10+ providers)
- Working on a team (consistency matters)
- You want compile-time type checking
- You prefer writing less boilerplate

**Consider manual providers when:**
- Building a tiny prototype (3-4 providers)
- Learning Riverpod fundamentals
- You need very custom provider behavior