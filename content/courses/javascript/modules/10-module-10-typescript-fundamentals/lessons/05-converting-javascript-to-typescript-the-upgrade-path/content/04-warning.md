---
type: "WARNING"
title: "Common Pitfalls"
---

Common migration mistakes:

1. **Trying to convert everything at once**:
   - This is overwhelming and error-prone
   - Convert one file or module at a time
   - Start with core utilities, then work outward

2. **Using 'any' everywhere**:
   ```typescript
   function process(data: any): any {  // Bad!
     // ...
   }
   ```
   - This defeats the purpose of TypeScript
   - Take time to figure out the correct types
   - Use 'any' only as a temporary placeholder

3. **Ignoring type errors**:
   - TypeScript errors are there to help!
   - Don't use `@ts-ignore` to suppress them
   - Fix the underlying issue instead

4. **Not updating tests**:
   - If you have .test.js files, convert them to .test.ts
   - Add types to test data and assertions

5. **Forgetting about third-party types**:
   - Many libraries need type definitions
   - Install them: `npm install -D @types/lodash`
   - Check DefinitelyTyped for available types

6. **Over-typing simple code**:
   ```typescript
   // Over-typed (unnecessary)
   let name: string = 'Alice';
   
   // Better (type inference)
   let name = 'Alice';  // TypeScript knows it's a string
   ```
   - Let TypeScript infer obvious types
   - Add explicit types for function parameters and returns

7. **Not communicating with team**:
   - Migration affects everyone
   - Set coding standards together
   - Pair program on tricky conversions
   - Document your migration strategy

Remember: Migration is a gradual process. It's okay to have both .js and .ts files in your project during the transition!