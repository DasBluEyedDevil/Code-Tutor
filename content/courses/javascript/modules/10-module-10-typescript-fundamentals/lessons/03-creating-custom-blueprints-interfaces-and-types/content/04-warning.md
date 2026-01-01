---
type: "WARNING"
title: "Common Pitfalls"
---

Common mistakes with interfaces:

1. **Forgetting required properties**:
   ```typescript
   let user: User = { id: 1 }; // ERROR: Missing username, email, isActive
   ```
   Solution: Include ALL required properties

2. **Wrong property names**:
   ```typescript
   let user: User = { 
     id: 1, 
     userName: 'alice' // ERROR: Should be 'username' not 'userName'
   };
   ```
   Solution: Match property names exactly (case-sensitive!)

3. **Extra properties**:
   ```typescript
   let user: User = {
     id: 1,
     username: 'alice',
     email: 'alice@example.com',
     isActive: true,
     age: 25 // ERROR: 'age' doesn't exist in User interface
   };
   ```
   Solution: Only include properties defined in the interface

4. **Confusing `?` placement**: `string?` is wrong, `property?: string` is correct

5. **Interface vs Type confusion**:
   - Both can define object shapes
   - Interfaces can be extended and merged
   - Types can do unions and complex types
   - For simple object shapes, use either one - they're interchangeable!