---
type: "CODE"
title: "Schema Composition"
---

Real-world APIs deal with complex nested structures, arrays of objects, optional fields with defaults, and data that needs transformation. Zod provides powerful composition tools to build these complex schemas from simpler parts. Think of it like LEGO blocks - you build complex structures by combining simple pieces.

```typescript
// Zod Schema Composition - Building Complex Structures
import { z } from 'zod';

// NESTED OBJECTS - Objects inside objects
const addressSchema = z.object({
  street: z.string().min(1),
  city: z.string().min(1),
  zipCode: z.string().regex(/^\d{5}(-\d{4})?$/, 'Invalid ZIP code'),
  country: z.string().length(2)  // ISO country code
});

const companySchema = z.object({
  name: z.string(),
  address: addressSchema,  // Nested object!
  founded: z.number().int().min(1800).max(new Date().getFullYear())
});

// ARRAYS - Lists of items
const tagsSchema = z.array(z.string());
const numbersSchema = z.array(z.number()).min(1).max(10);  // 1-10 items

// Array of objects - very common in APIs
const orderItemSchema = z.object({
  productId: z.string().uuid(),
  quantity: z.number().int().positive(),
  price: z.number().positive()
});

const orderSchema = z.object({
  orderId: z.string().uuid(),
  customerId: z.string().uuid(),
  items: z.array(orderItemSchema).min(1, 'Order must have at least one item'),
  total: z.number().positive()
});

// OPTIONAL FIELDS - Field may or may not exist
const profileSchema = z.object({
  username: z.string(),
  bio: z.string().optional(),           // string | undefined
  website: z.string().url().optional(),
  avatarUrl: z.string().url().nullable() // string | null (different from optional!)
});

// DEFAULT VALUES - Provide value if missing
const settingsSchema = z.object({
  theme: z.enum(['light', 'dark']).default('light'),
  notifications: z.boolean().default(true),
  language: z.string().default('en'),
  itemsPerPage: z.number().int().min(10).max(100).default(25)
});

const inputSettings = {};  // Empty input
const settings = settingsSchema.parse(inputSettings);
console.log(settings);
// { theme: 'light', notifications: true, language: 'en', itemsPerPage: 25 }

// TRANSFORMS - Convert data during validation
const dateStringSchema = z.string()
  .transform((str) => new Date(str));  // string -> Date

const lowercaseEmailSchema = z.string()
  .email()
  .transform((email) => email.toLowerCase());  // Normalize email

const trimmedStringSchema = z.string()
  .transform((s) => s.trim())  // Remove whitespace
  .pipe(z.string().min(1));    // Then validate non-empty

// Parse comma-separated string into array
const csvToArraySchema = z.string()
  .transform((str) => str.split(',').map(s => s.trim()))
  .pipe(z.array(z.string()));

const tags = csvToArraySchema.parse('javascript, typescript, zod');
console.log(tags);  // ['javascript', 'typescript', 'zod']

// COERCION - Convert types automatically
const coercedNumber = z.coerce.number();  // '42' -> 42
const coercedBoolean = z.coerce.boolean(); // 'true' -> true
const coercedDate = z.coerce.date();       // '2024-01-01' -> Date

console.log(coercedNumber.parse('123'));     // 123 (number)
console.log(coercedBoolean.parse('false'));  // false (boolean)

// UNIONS - Value can be one of several types
const stringOrNumber = z.union([z.string(), z.number()]);
console.log(stringOrNumber.parse('hello'));  // 'hello'
console.log(stringOrNumber.parse(42));       // 42

// DISCRIMINATED UNIONS - Powerful pattern for API responses
const apiResponseSchema = z.discriminatedUnion('status', [
  z.object({
    status: z.literal('success'),
    data: z.object({
      id: z.number(),
      name: z.string()
    })
  }),
  z.object({
    status: z.literal('error'),
    error: z.object({
      code: z.string(),
      message: z.string()
    })
  })
]);

type ApiResponse = z.infer<typeof apiResponseSchema>;

// TypeScript knows the shape based on 'status'
function handleResponse(response: ApiResponse) {
  if (response.status === 'success') {
    console.log(response.data.name);  // TypeScript knows data exists
  } else {
    console.log(response.error.message);  // TypeScript knows error exists
  }
}

// EXTENDING AND MERGING SCHEMAS
const baseUserSchema = z.object({
  id: z.string().uuid(),
  email: z.string().email(),
  createdAt: z.string().datetime()
});

// Extend adds new fields
const adminUserSchema = baseUserSchema.extend({
  role: z.literal('admin'),
  permissions: z.array(z.string())
});

// Merge combines two schemas
const profileInfoSchema = z.object({
  displayName: z.string(),
  avatarUrl: z.string().url().optional()
});

const fullUserSchema = baseUserSchema.merge(profileInfoSchema);

// PICK AND OMIT - Select specific fields
const createUserInput = baseUserSchema.omit({ id: true, createdAt: true });
// Only { email: string }

const publicUserInfo = baseUserSchema.pick({ id: true, email: true });
// Only { id: string; email: string }

// PARTIAL - Make all fields optional
const updateUserSchema = baseUserSchema.partial();
// All fields are now optional for PATCH requests

// REQUIRED - Make all fields required
const strictUserSchema = baseUserSchema.partial().required();
// Undo partial - all fields required again

type CreateUser = z.infer<typeof createUserInput>;
type UpdateUser = z.infer<typeof updateUserSchema>;
```
