---
type: "EXAMPLE"
title: "The Bun All-in-One Runtime"
---

Bun is unique because it's a **runtime**, a **package manager**, and a **test runner** all in one. In 2025, it is the fastest way to run JavaScript and TypeScript.

```typescript
// 1. Native TypeScript Support
// In Bun, you don't need to compile. Just run `bun app.ts`
interface User {
  id: number;
  username: string;
}

const user: User = { id: 1, username: "Bunny" };
console.log(`Hello, ${user.username}!`);

// 2. High-Performance File I/O
// Bun provides a much faster and simpler File API than Node.js
// const file = Bun.file("data.txt");
// const text = await file.text(); // Simple, right?

// 3. Built-in HTTP Server
// You don't even need a library like Express to start a fast server
/*
Bun.serve({
  port: 3000,
  fetch(req) {
    return new Response("Welcome to the Bun Server!");
  },
});
*/

// 4. Using Web Standards
// Bun uses standard fetch, Request, and Response objects out of the box
const response = new Response("JSON response", {
  headers: { "Content-Type": "application/json" },
});
console.log("Response status:", response.status);

// 5. Blazing Fast Environment Variables
// No need for 'dotenv' package. Bun reads .env files automatically.
const apiKey = Bun.env.API_KEY || "default_key";
console.log(`API Key loaded: ${apiKey}`);
```