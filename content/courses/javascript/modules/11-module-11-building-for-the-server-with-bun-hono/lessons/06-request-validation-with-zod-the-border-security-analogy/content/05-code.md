---
type: "CODE"
title: "Integrating with Hono"
---

The @hono/zod-validator package provides seamless integration between Zod schemas and Hono route handlers. It validates request body, query parameters, URL parameters, and headers automatically. When validation fails, it returns a 400 error with detailed messages. When validation succeeds, your handler receives fully typed data.

```typescript
// Zod Integration with Hono - Type-Safe API Validation
import { Hono } from 'hono';
import { zValidator } from '@hono/zod-validator';
import { z } from 'zod';

const app = new Hono();

// DEFINE YOUR SCHEMAS
const createUserSchema = z.object({
  name: z.string().min(1).max(100),
  email: z.string().email(),
  password: z.string().min(8),
  age: z.number().int().min(18).optional()
});

const updateUserSchema = createUserSchema.partial();  // All fields optional

const userIdParamSchema = z.object({
  id: z.string().uuid()
});

const paginationQuerySchema = z.object({
  page: z.coerce.number().int().min(1).default(1),
  limit: z.coerce.number().int().min(1).max(100).default(20),
  sortBy: z.enum(['name', 'email', 'createdAt']).default('createdAt'),
  order: z.enum(['asc', 'desc']).default('desc')
});

const searchQuerySchema = z.object({
  q: z.string().min(1).optional(),
  category: z.string().optional(),
  minPrice: z.coerce.number().min(0).optional(),
  maxPrice: z.coerce.number().min(0).optional()
}).refine(
  (data) => !data.minPrice || !data.maxPrice || data.minPrice <= data.maxPrice,
  { message: 'minPrice must be less than or equal to maxPrice', path: ['minPrice'] }
);

// VALIDATE REQUEST BODY
app.post(
  '/api/users',
  zValidator('json', createUserSchema),  // Validates body
  async (c) => {
    // c.req.valid('json') returns typed, validated data
    const userData = c.req.valid('json');
    // userData has type: { name: string; email: string; password: string; age?: number }
    
    // Safe to use - already validated!
    console.log(`Creating user: ${userData.name} <${userData.email}>`);
    
    return c.json({
      message: 'User created',
      user: {
        id: crypto.randomUUID(),
        name: userData.name,
        email: userData.email
        // Note: Don't return password!
      }
    }, 201);
  }
);

// VALIDATE URL PARAMETERS
app.get(
  '/api/users/:id',
  zValidator('param', userIdParamSchema),  // Validates :id param
  async (c) => {
    const { id } = c.req.valid('param');
    // id is guaranteed to be a valid UUID string
    
    // Simulate database lookup
    const user = { id, name: 'Alice', email: 'alice@example.com' };
    
    return c.json(user);
  }
);

// VALIDATE QUERY PARAMETERS
app.get(
  '/api/users',
  zValidator('query', paginationQuerySchema),  // Validates query string
  async (c) => {
    const { page, limit, sortBy, order } = c.req.valid('query');
    // All values are typed and have defaults applied
    
    console.log(`Fetching page ${page}, ${limit} items, sorted by ${sortBy} ${order}`);
    
    return c.json({
      page,
      limit,
      total: 100,
      users: []
    });
  }
);

// COMBINE MULTIPLE VALIDATORS
app.put(
  '/api/users/:id',
  zValidator('param', userIdParamSchema),
  zValidator('json', updateUserSchema),
  async (c) => {
    const { id } = c.req.valid('param');
    const updates = c.req.valid('json');
    
    console.log(`Updating user ${id} with:`, updates);
    
    return c.json({
      message: 'User updated',
      userId: id,
      updates
    });
  }
);

// VALIDATE HEADERS
const authHeaderSchema = z.object({
  authorization: z.string().regex(/^Bearer .+$/, 'Invalid authorization header')
});

app.get(
  '/api/protected',
  zValidator('header', authHeaderSchema),
  async (c) => {
    const headers = c.req.valid('header');
    const token = headers.authorization.replace('Bearer ', '');
    
    return c.json({ message: 'Access granted', token: token.substring(0, 10) + '...' });
  }
);

// CUSTOM ERROR HANDLING
// By default, zValidator returns 400 with error details
// You can customize this behavior:

const customErrorHandler = (result: { success: boolean; error?: z.ZodError }, c: any) => {
  if (!result.success) {
    return c.json({
      success: false,
      error: 'Validation failed',
      details: result.error?.errors.map(err => ({
        field: err.path.join('.'),
        message: err.message
      }))
    }, 400);
  }
};

app.post(
  '/api/products',
  zValidator('json', z.object({
    name: z.string().min(1),
    price: z.number().positive(),
    quantity: z.number().int().min(0)
  }), customErrorHandler),  // Third argument is error handler
  async (c) => {
    const product = c.req.valid('json');
    return c.json({ created: product }, 201);
  }
);

// COMBINING WITH OTHER MIDDLEWARE
import { jwt } from 'hono/jwt';
import { cors } from 'hono/cors';

app.use('/api/*', cors());
app.use('/api/protected/*', jwt({ secret: 'your-secret' }));

app.post(
  '/api/protected/posts',
  zValidator('json', z.object({
    title: z.string().min(1).max(200),
    content: z.string().min(10)
  })),
  async (c) => {
    const post = c.req.valid('json');
    const jwtPayload = c.get('jwtPayload');  // From JWT middleware
    
    return c.json({
      post,
      author: jwtPayload.sub
    }, 201);
  }
);

// TYPE INFERENCE FROM SCHEMAS TO HANDLERS
// Extract types for use elsewhere in your app

type CreateUserInput = z.infer<typeof createUserSchema>;
type UpdateUserInput = z.infer<typeof updateUserSchema>;
type PaginationQuery = z.infer<typeof paginationQuerySchema>;

// Use in service layer
function createUserInDatabase(userData: CreateUserInput) {
  // Type-safe function signature
  console.log(`Inserting ${userData.email} into database`);
}

export default app;
```
