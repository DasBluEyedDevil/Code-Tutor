---
type: "EXAMPLE"
title: "Basic Type Annotations"
---

```typescript
// 1. Primitive Types
let username: string = "CyberKnight";
let userAge: number = 42;
let isPremium: boolean = true;

// 2. Type Inference (Smart Labeling)
let score = 100; // TypeScript knows this is a 'number'
// score = "high"; // ERROR! Cannot assign string to number.

// 3. Arrays
// You specify the type of item inside the array
let inventory: string[] = ["Sword", "Shield", "Potion"];
let luckyNumbers: number[] = [7, 11, 21];

// 4. Literal Types (Exact values)
// The variable MUST be one of these exact strings
let theme: "light" | "dark" = "dark";
// theme = "blue"; // ERROR! Only 'light' or 'dark' allowed.

// 5. The 'any' type (The Rule Breaker)
// Try to avoid this!
let mysterious: any = "could be anything";
mysterious = 10;
mysterious = true;

// 6. Functions with Types
function greet(name: string): string {
    return `Hello, ${name}`;
}
```