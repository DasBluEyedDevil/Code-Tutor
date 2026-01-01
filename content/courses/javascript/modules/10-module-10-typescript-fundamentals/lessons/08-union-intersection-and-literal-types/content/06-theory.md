---
type: "THEORY"
title: "Complete Syntax Reference"
---

**Union Types (`|`):**
```typescript
type A = string | number | boolean;     // Primitives
type B = Dog | Cat | Bird;              // Object types
type C = 'on' | 'off';                  // Literal types
type D = string | null | undefined;     // Nullable
```

**Intersection Types (`&`):**
```typescript
type A = TypeA & TypeB;                 // Combine types
type B = Base & { extra: string };      // Add properties
type C = A & B & C;                     // Multiple
```

**Literal Types:**
```typescript
type Direction = 'up' | 'down' | 'left' | 'right';
type Digit = 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9;
type Bool = true | false;
```

**Type Narrowing (working with unions):**
```typescript
// typeof guard
if (typeof x === 'string') { /* x is string */ }

// in operator
if ('bark' in pet) { /* pet has bark property */ }

// Discriminated union (tagged union)
if (result.type === 'success') { /* result is SuccessType */ }

// instanceof
if (err instanceof Error) { /* err is Error */ }
```

**Interface vs Type Quick Reference:**
| Feature | Interface | Type Alias |
|---------|-----------|------------|
| Object shapes | Yes | Yes |
| Extends/inheritance | `extends` | `&` |
| Declaration merging | Yes | No |
| Union types | No | Yes |
| Tuple types | No | Yes |
| Primitive aliases | No | Yes |
| Mapped types | No | Yes |