---
type: "WARNING"
title: "Common Pitfalls"
---

Common beginner mistakes:

1. **Mixing up type names**: `String` vs `string`
   - Use lowercase: `string`, `number`, `boolean`
   - Uppercase versions (String, Number, Boolean) are JavaScript wrapper objects - avoid them!

2. **Forgetting array brackets**: `let nums: number` vs `let nums: number[]`
   - `number` is a single number
   - `number[]` is an array of numbers

3. **Type mismatch errors**: `let age: number = '25'`
   - '25' is a string, not a number
   - Remove quotes: `let age: number = 25`

4. **Overusing 'any'**: Using `any` everywhere defeats TypeScript's purpose
   - Only use 'any' as a last resort
   - Better to learn the correct type

5. **Type inference confusion**: "Do I always need type annotations?"
   - No! If you assign immediately, TypeScript infers the type
   - `let x = 5` is the same as `let x: number = 5`
   - Explicit types are good for function parameters and return values