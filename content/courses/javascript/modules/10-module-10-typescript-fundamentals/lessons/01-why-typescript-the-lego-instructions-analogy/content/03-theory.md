---
type: "THEORY"
title: "Breaking Down the Syntax"
---

Let's break down the TypeScript syntax:

1. **Parameter Types**: `a: number`
   - The colon `:` means "this variable should be"
   - `number` is the type we're expecting
   - Think of it as a label on a LEGO piece

2. **Return Type**: `: number` after the parentheses
   - Tells TypeScript what type of value the function returns
   - Optional but recommended for clarity

3. **Type Checking**: TypeScript analyzes your code
   - Happens BEFORE you run the code (compile-time)
   - Catches type mismatches and shows red squiggly lines in your editor
   - Your code won't compile if types don't match

4. **Benefits**:
   - Catch bugs early (before runtime)
   - Better autocomplete in your editor
   - Self-documenting code (types show intent)
   - Safer refactoring (TypeScript tells you what breaks)