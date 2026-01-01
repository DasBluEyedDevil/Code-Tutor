---
type: "CONCEPT"
title: "Built-in Type Transformers"
---

Imagine you're in a workshop with specialized tools. Instead of building every tool from scratch, you have power tools that transform raw materials into exactly what you need. TypeScript's utility types work the same way - they're built-in type transformers that take an existing type and produce a new, modified version.

Why does TypeScript provide these utility types? Because developers kept writing the same type transformations over and over: making properties optional, picking certain fields, making things readonly. Rather than having everyone reinvent these wheels, TypeScript includes them as standard tools.

The pattern is consistent: each utility type takes one or more type parameters (like function arguments) and returns a transformed type. For example, `Partial<User>` takes your User type and returns a new type where every property is optional. This reduces boilerplate dramatically - what would take 10 lines of manual type definition becomes a single word.

These utilities also increase type safety. When you use `Readonly<Config>`, TypeScript enforces immutability at compile time. When you use `Pick<User, 'id' | 'name'>`, you get exactly those fields and nothing else. The compiler catches mistakes that would otherwise slip through.

Think of utility types as the 'verbs' of the type system - they DO something to your types. `Partial` makes optional, `Required` makes required, `Pick` selects, `Omit` excludes, `Readonly` freezes. Master these verbs and you can express complex type relationships concisely.