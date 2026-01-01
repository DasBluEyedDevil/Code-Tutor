---
type: "THEORY"
title: "Breaking Down the Syntax"
---

Let's break down TypeScript's basic types:

1. **String Type**: `let name: string = 'Alice'`
   - For text data
   - Use single quotes, double quotes, or backticks
   - Can't assign numbers or booleans to string variables

2. **Number Type**: `let age: number = 25`
   - For all numeric data (integers and decimals)
   - JavaScript/TypeScript doesn't distinguish between int and float
   - Can't assign strings or booleans to number variables

3. **Boolean Type**: `let isActive: boolean = true`
   - Only two values: `true` or `false`
   - Used for yes/no, on/off logic
   - Can't assign strings or numbers (even 0 and 1)

4. **Array Types**: `let numbers: number[] = [1, 2, 3]`
   - Square brackets `[]` after the type
   - All elements must be the same type
   - Alternative syntax: `Array<number>`

5. **Type Inference**: TypeScript is smart!
   - If you assign a value immediately, TypeScript guesses the type
   - `let x = 5` → TypeScript knows x is a number
   - Still type-safe, just less typing!

6. **The 'any' Type**: Last resort!
   - Turns off type checking for that variable
   - Defeats the purpose of TypeScript
   - Use only when absolutely necessary (like external APIs)