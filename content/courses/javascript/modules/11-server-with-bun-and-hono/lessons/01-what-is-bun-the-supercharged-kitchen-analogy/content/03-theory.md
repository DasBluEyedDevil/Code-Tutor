---
type: "THEORY"
title: "The Bun Philosophy"
---

Bun is a modern JavaScript runtime designed from the ground up for speed. While Node.js has been the standard for years, Bun solves many of its frustrations.

### 1. Built for Speed (Zig)
Bun is written in **Zig**, a low-level programming language that allows for extreme performance. It uses the **JavaScriptCore** engine (the same one inside Apple's Safari), which is optimized for fast startup times.

### 2. The "Batteries Included" Runtime
In the old days of Node.js, you needed dozens of tools to build a project:
*   **npm/yarn** for packages.
*   **tsc** for TypeScript.
*   **jest/vitest** for testing.
*   **esbuild/webpack** for bundling.

**Bun does all of this in a single binary.** If you have Bun installed, you have a package manager (`bun install`), a test runner (`bun test`), and a bundler (`bun build`) ready to go.

### 3. Native TypeScript & JSX
This is perhaps Bun's best feature for developers. You can run `.ts`, `.tsx`, and even `.jsx` files directly. Bun handles the "transpilation" (converting it to plain JS) in memory at lightning speed, so you don't have to wait for a build step.

### 4. Web-Standard APIs
Bun avoids proprietary APIs where possible. Instead of learning custom "Node-only" ways of doing things, Bun uses the same `fetch`, `Request`, `Response`, and `URL` objects you use in the browser. This makes your skills highly transferable between frontend and backend.
