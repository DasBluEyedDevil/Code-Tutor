---
type: "THEORY"
title: "Breaking Down the Syntax"
---

Let's break down type guards and discriminated unions:

1. **typeof Type Guards**:
   ```typescript
   if (typeof value === 'string') {
     // value is string here
   }
   ```
   - Works for: string, number, boolean, function, object, undefined
   - Most common type guard

2. **Array.isArray Type Guard**:
   ```typescript
   if (Array.isArray(data)) {
     // data is an array here
   }
   ```
   - Specifically checks for arrays
   - typeof returns 'object' for arrays!

3. **Discriminated Unions**:
   - Add a common property with literal types
   - Usually called 'kind', 'type', or 'tag'
   - Switch on this property to narrow types
   ```typescript
   interface A { kind: 'a'; propA: string }
   interface B { kind: 'b'; propB: number }
   type AB = A | B;
   ```

4. **Custom Type Guards** (type predicates):
   ```typescript
   function isDog(pet: Pet): pet is Dog {
     return 'bark' in pet;
   }
   ```
   - Return type: `paramName is Type`
   - Returns boolean, but narrows type when true

5. **'in' Operator**:
   ```typescript
   if ('propertyName' in object) {
     // object has propertyName here
   }
   ```
   - Checks if property exists
   - Narrows type to those with that property

6. **Nullish Checks**:
   ```typescript
   if (value !== null && value !== undefined) {
     // value is non-nullable here
   }
   ```
   - Removes null/undefined from type
   - Also works: `if (value) { ... }` (for truthy check)

7. **Exhaustiveness Checking**:
   - Use `never` type in default case
   - TypeScript errors if you forget a case
   - Great for switch statements on unions