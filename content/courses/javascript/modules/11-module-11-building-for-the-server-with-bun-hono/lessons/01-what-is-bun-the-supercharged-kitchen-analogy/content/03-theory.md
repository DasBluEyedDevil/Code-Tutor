---
type: "THEORY"
title: "Breaking Down the Syntax"
---

Understanding Bun fundamentals:

1. **What Makes Bun Special**:
   - Written in Zig for maximum performance
   - Up to 4x faster than Node.js
   - Built-in TypeScript support (no tsconfig needed!)
   - Same code runs on Bun, Node, Deno, and Edge
   - Drop-in replacement for Node.js in most cases

2. **Built-in Tools**:
   - `bun install`: Package manager (faster than npm)
   - `bun run`: Script runner (runs .ts files directly!)
   - `bun test`: Test runner (Jest-compatible)
   - `bun build`: Bundler for production

3. **Bun APIs**:
   - `Bun.file()`: Read/write files
   - `Bun.serve()`: Create HTTP servers
   - `Bun.password`: Hash passwords
   - `Bun.sql()`: SQLite database
   - Standard Web APIs: `fetch`, `Response`, `Request`

4. **TypeScript Just Works**:
   ```typescript
   // app.ts - Run directly with: bun app.ts
   interface User {
     name: string;
     email: string;
   }
   
   const user: User = {
     name: 'Alice',
     email: 'alice@example.com'
   };
   
   console.log(user.name); // No compilation step!
   ```

5. **File Operations**:
   ```javascript
   // Reading files in Bun
   const file = Bun.file('config.json');
   const content = await file.text();  // or .json()
   
   // Writing files
   await Bun.write('output.txt', 'Hello World!');
   ```

6. **Creating Servers**:
   ```javascript
   Bun.serve({
     port: 3000,
     fetch(request) {
       return new Response('Hello from Bun!');
     }
   });
   ```

7. **Running Bun Code**:
   - Save file as `app.ts` or `app.js`
   - Run with: `bun run app.ts`
   - Or simply: `bun app.ts`
   - No browser needed, no compilation needed!