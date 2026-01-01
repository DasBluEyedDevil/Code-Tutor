---
type: "EXAMPLE"
title: "Code Example"
---

See the code example above demonstrating Code Example.

```javascript
// TypeScript 5.7 Basic Types (2024-2025)

// 1. STRING - Text data
let playerName: string = 'Alice';
let greeting: string = "Hello, world!";
let message: string = `Welcome, ${playerName}!`; // Template literal

console.log(message); // 'Welcome, Alice!'

// 2. NUMBER - Numeric data (integers and decimals)
let score: number = 100;
let health: number = 95.5;
let temperature: number = -5;

console.log('Score:', score); // 100

// 3. BOOLEAN - True or false
let isGameOver: boolean = false;
let hasWon: boolean = true;
let isLoggedIn: boolean = score > 50;

console.log('Game over?', isGameOver); // false

// 4. ARRAYS - Lists of the same type
let scores: number[] = [100, 95, 87, 92];
let names: string[] = ['Alice', 'Bob', 'Charlie'];
let flags: boolean[] = [true, false, true];

console.log('First score:', scores[0]);   // 100
console.log('All names:', names);         // ['Alice', 'Bob', 'Charlie']

// Alternative array syntax (both work the same)
let moreScores: Array<number> = [88, 92, 76];

// 5. TYPE INFERENCE - TypeScript guesses the type
let autoString = 'TypeScript is smart!'; // TypeScript knows this is a string
let autoNumber = 42;                      // TypeScript knows this is a number
let autoBool = true;                      // TypeScript knows this is a boolean

// autoString = 123; // ERROR: Can't assign number to string variable

// 6. ANY - Escape hatch (use sparingly!)
let anything: any = 'I can be anything';
anything = 42;        // OK
anything = true;      // OK
anything = [1, 2, 3]; // OK
// Using 'any' turns off type checking - avoid it when possible!

console.log('Anything:', anything);
```
