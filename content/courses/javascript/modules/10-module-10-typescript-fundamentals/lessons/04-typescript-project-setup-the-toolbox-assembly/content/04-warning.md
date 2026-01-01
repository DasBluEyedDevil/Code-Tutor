---
type: "WARNING"
title: "Common Pitfalls"
---

Common setup mistakes:

1. **Wrong tsconfig.json location**:
   - Must be in project root (not inside src/)
   - Must be named exactly 'tsconfig.json' (case-sensitive)

2. **JSON syntax errors**:
   ```json
   {
     "target": "ES2024",  // ERROR: No trailing comma on last item!
   }
   ```
   Solution: Remove trailing commas in JSON files

3. **Running .ts files directly**: `node src/index.ts`
   - Node.js can't run TypeScript!
   - Either compile first: `npx tsc` then `node dist/index.js`
   - Or use ts-node: `npx ts-node src/index.ts`

4. **Forgetting to install TypeScript**:
   - Must run `npm install -D typescript` first
   - Check with `npx tsc --version`

5. **Wrong folder structure**:
   - TypeScript files should be in src/
   - If you put them elsewhere, update `rootDir` in tsconfig.json

6. **Confusion about compilation**:
   - TypeScript → JavaScript (compilation/transpilation)
   - Types are removed during compilation
   - The .js output has no type information
   - Type checking happens at compile-time only

7. **Not using strict mode**:
   - `"strict": false` allows many unsafe patterns
   - Always use `"strict": true` for maximum safety
   - You're learning TypeScript to catch bugs - use all its power!