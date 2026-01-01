---
type: "THEORY"
title: "Breaking Down the Syntax"
---

Converting JavaScript to TypeScript step-by-step:

1. **File Renaming** (easiest step):
   - `.js` → `.ts` (JavaScript → TypeScript)
   - `.jsx` → `.tsx` (React files)
   - All valid JavaScript is valid TypeScript!

2. **Add Function Return Types**:
   ```typescript
   // Before
   function getName() {
     return 'Alice';
   }
   
   // After
   function getName(): string {
     return 'Alice';
   }
   ```

3. **Add Parameter Types**:
   ```typescript
   // Before
   function greet(name) {
     return `Hello, ${name}`;
   }
   
   // After  
   function greet(name: string): string {
     return `Hello, ${name}`;
   }
   ```

4. **Create Interfaces for Objects**:
   ```typescript
   // Before
   let user = {
     id: 1,
     name: 'Alice'
   };
   
   // After
   interface User {
     id: number;
     name: string;
   }
   
   let user: User = {
     id: 1,
     name: 'Alice'
   };
   ```

5. **Type Arrays**:
   ```typescript
   // Before
   let numbers = [1, 2, 3];
   
   // After
   let numbers: number[] = [1, 2, 3];
   ```

6. **Union Types for Multiple Return Types**:
   ```typescript
   function getValue(): number | string {
     // Can return either number or string
   }
   ```

7. **Migration Strategy**:
   - Start with strict: false in tsconfig.json
   - Convert one file at a time
   - Focus on functions and interfaces first
   - Gradually enable strict checks
   - Don't try to convert everything at once!