---
type: "LEGACY_COMPARISON"
title: "The Shift from Node to Bun"
---

If you've used Node.js before, here is how Bun changes your workflow in 2025.

| Feature | The Node.js Way (Legacy) | The Bun Way (Modern) |
| :--- | :--- | :--- |
| **Running TS** | Install `tsx` or compile with `tsc` first. | `bun run app.ts` (Native support). |
| **Packages** | `npm install` (Often slow with large `node_modules`). | `bun install` (Extremely fast, uses global cache). |
| **Environment** | Install `dotenv`, then `require('dotenv').config()`. | `.env` files are loaded automatically. |
| **APIs** | Use `fs.readFile` with callbacks or promises. | Use `Bun.file()` for high-performance I/O. |
| **Testing** | Install `Jest` or `Vitest` and configure them. | Use `bun test` (Built-in and Vitest-compatible). |

### Why the change?
Node.js was built in a different era (2009). Bun was built for the modern era of high-speed cloud deployments, edge computing, and TypeScript-first development. While Node.js is still very reliable, Bun offers a developer experience that is significantly faster and requires much less configuration.