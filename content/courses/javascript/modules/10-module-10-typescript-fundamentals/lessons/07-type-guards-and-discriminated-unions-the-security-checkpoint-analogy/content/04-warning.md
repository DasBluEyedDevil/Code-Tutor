---
type: "WARNING"
title: "Common Pitfalls"
---

Common mistakes with type guards:

1. **typeof null === 'object'**:
   ```typescript
   let value: object | null = null;
   
   // Wrong - null passes this check!
   if (typeof value === 'object') {
     // value could still be null here
   }
   
   // Correct - check for null first
   if (value !== null && typeof value === 'object') {
     // value is definitely an object
   }
   ```

2. **Forgetting to handle all union members**:
   ```typescript
   type Status = 'pending' | 'approved' | 'rejected';
   
   function handleStatus(status: Status): void {
     if (status === 'pending') { ... }
     if (status === 'approved') { ... }
     // Forgot 'rejected'!
   }
   ```
   Use exhaustiveness checking with `never`.

3. **Wrong discriminant property type**:
   ```typescript
   // Wrong - 'type' is just string, not specific
   interface Circle { type: string; radius: number }
   
   // Correct - literal type
   interface Circle { type: 'circle'; radius: number }
   ```

4. **Type guard returns wrong thing**:
   ```typescript
   // Wrong - returns boolean, but no narrowing
   function isDog(pet: Pet): boolean {
     return 'bark' in pet;
   }
   
   // Correct - 'pet is Dog' enables narrowing
   function isDog(pet: Pet): pet is Dog {
     return 'bark' in pet;
   }
   ```

5. **Not understanding narrowing scope**:
   - Type narrowing only works within the if block
   - After the block, type reverts to original union
   - Use early returns for cleaner code

6. **Overcomplicating simple checks**:
   ```typescript
   // Overcomplicated
   function isDefined<T>(value: T | undefined): value is T {
     return value !== undefined;
   }
   
   // Often simpler inline
   if (value !== undefined) { ... }
   ```