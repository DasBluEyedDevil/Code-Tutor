---
type: "CODE"
title: "Zod Schema Basics"
---

Zod provides primitive validators for all JavaScript types, plus powerful combinators for complex structures. The key insight is that a Zod schema serves two purposes: runtime validation and compile-time type inference. When you define a schema, you get both validation logic and a TypeScript type for free.

```typescript
// Zod Schema Basics - Foundation of API Validation
import { z } from 'zod';

// PRIMITIVE TYPES
// Zod has validators for all JavaScript primitives

const stringSchema = z.string();
const numberSchema = z.number();
const booleanSchema = z.boolean();
const dateSchema = z.date();
const bigintSchema = z.bigint();
const symbolSchema = z.symbol();
const undefinedSchema = z.undefined();
const nullSchema = z.null();

// STRING VALIDATORS WITH BUILT-IN REFINEMENTS
const emailSchema = z.string().email();           // Must be valid email
const urlSchema = z.string().url();               // Must be valid URL
const uuidSchema = z.string().uuid();             // Must be valid UUID
const minLengthSchema = z.string().min(3);        // At least 3 characters
const maxLengthSchema = z.string().max(100);      // At most 100 characters
const lengthSchema = z.string().length(10);       // Exactly 10 characters
const regexSchema = z.string().regex(/^[A-Z]+$/); // Must match pattern
const trimmedSchema = z.string().trim();          // Trims whitespace
const lowercaseSchema = z.string().toLowerCase(); // Converts to lowercase

// NUMBER VALIDATORS
const positiveSchema = z.number().positive();     // > 0
const negativeSchema = z.number().negative();     // < 0
const intSchema = z.number().int();               // Must be integer
const minSchema = z.number().min(0);              // >= 0
const maxSchema = z.number().max(100);            // <= 100
const multipleOfSchema = z.number().multipleOf(5); // Must be divisible by 5

// OBJECT SCHEMAS - The Foundation of API Validation
const userSchema = z.object({
  id: z.number().int().positive(),
  name: z.string().min(1).max(100),
  email: z.string().email(),
  age: z.number().int().min(0).max(150).optional(),
  isActive: z.boolean().default(true)
});

// TWO WAYS TO VALIDATE: parse() vs safeParse()

// 1. parse() - Throws ZodError if validation fails
try {
  const validUser = userSchema.parse({
    id: 1,
    name: 'Alice',
    email: 'alice@example.com'
  });
  console.log('Valid user:', validUser);
  // validUser has type { id: number; name: string; email: string; age?: number; isActive: boolean }
} catch (error) {
  if (error instanceof z.ZodError) {
    console.error('Validation failed:', error.errors);
  }
}

// 2. safeParse() - Returns result object, never throws
const result = userSchema.safeParse({
  id: 'not-a-number',  // Wrong type!
  name: '',            // Too short!
  email: 'invalid'     // Not an email!
});

if (result.success) {
  console.log('Valid:', result.data);
} else {
  console.log('Invalid:', result.error.errors);
  // [
  //   { path: ['id'], message: 'Expected number, received string' },
  //   { path: ['name'], message: 'String must contain at least 1 character(s)' },
  //   { path: ['email'], message: 'Invalid email' }
  // ]
}

// TYPE INFERENCE - Get TypeScript types from schemas
type User = z.infer<typeof userSchema>;
// Equivalent to:
// type User = {
//   id: number;
//   name: string;
//   email: string;
//   age?: number | undefined;
//   isActive: boolean;
// }

// Now you can use the inferred type throughout your codebase
function createUser(userData: User): void {
  console.log(`Creating user: ${userData.name}`);
}

// PRACTICAL EXAMPLE: API Request Body Validation
const createPostSchema = z.object({
  title: z.string().min(1, 'Title is required').max(200, 'Title too long'),
  content: z.string().min(10, 'Content must be at least 10 characters'),
  tags: z.array(z.string()).min(1, 'At least one tag required').max(5, 'Maximum 5 tags'),
  publishedAt: z.string().datetime().optional()
});

type CreatePostInput = z.infer<typeof createPostSchema>;

// Simulating incoming API request
const requestBody = {
  title: 'Learning Zod',
  content: 'Zod is a powerful validation library for TypeScript...',
  tags: ['typescript', 'validation']
};

const validatedPost = createPostSchema.safeParse(requestBody);
if (validatedPost.success) {
  console.log('Post is valid:', validatedPost.data);
} else {
  console.log('Validation errors:', validatedPost.error.flatten());
}
```
