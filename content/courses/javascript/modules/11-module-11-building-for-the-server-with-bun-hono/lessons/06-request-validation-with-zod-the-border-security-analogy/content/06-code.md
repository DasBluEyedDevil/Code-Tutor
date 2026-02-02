---
type: "EXAMPLE"
title: "Error Response Formatting"
---

When validation fails, raw Zod errors contain detailed information but are not user-friendly. A good API formats these errors into consistent, helpful responses that frontend developers and API consumers can easily understand and display to users. Here is how to transform ZodErrors into clean API responses.

```typescript
// Zod Error Response Formatting - User-Friendly API Errors
import { z } from 'zod';
import { Hono } from 'hono';
import { zValidator } from '@hono/zod-validator';

// UNDERSTANDING ZODERROR STRUCTURE
// When validation fails, ZodError contains an 'errors' array
// Each error has: code, message, path, and possibly other fields

const exampleSchema = z.object({
  name: z.string().min(1),
  email: z.string().email(),
  age: z.number().int().min(18)
});

const badData = { name: '', email: 'invalid', age: 15 };
const result = exampleSchema.safeParse(badData);

if (!result.success) {
  console.log('Raw ZodError:', JSON.stringify(result.error.errors, null, 2));
  // [
  //   { code: 'too_small', path: ['name'], message: 'String must contain at least 1 character(s)' },
  //   { code: 'invalid_string', path: ['email'], message: 'Invalid email' },
  //   { code: 'too_small', path: ['age'], message: 'Number must be greater than or equal to 18' }
  // ]
}

// FORMATTING HELPER FUNCTIONS

// 1. Simple flat format - good for most cases
function formatZodErrors(error: z.ZodError): Record<string, string> {
  const formatted: Record<string, string> = {};
  
  for (const issue of error.errors) {
    const path = issue.path.join('.');
    // Only keep first error per field
    if (!formatted[path]) {
      formatted[path] = issue.message;
    }
  }
  
  return formatted;
}

// Usage
if (!result.success) {
  console.log('Formatted errors:', formatZodErrors(result.error));
  // { name: 'String must contain at least 1 character(s)', email: 'Invalid email', age: '...' }
}

// 2. Array format with field paths - for frontend forms
function formatZodErrorsArray(error: z.ZodError) {
  return error.errors.map(issue => ({
    field: issue.path.join('.'),
    message: issue.message,
    code: issue.code
  }));
}

// 3. Zod's built-in flatten() - very useful!
if (!result.success) {
  const flattened = result.error.flatten();
  console.log('Flattened:', flattened);
  // {
  //   formErrors: [],  // Top-level errors
  //   fieldErrors: {   // Per-field errors
  //     name: ['String must contain at least 1 character(s)'],
  //     email: ['Invalid email'],
  //     age: ['Number must be greater than or equal to 18']
  //   }
  // }
}

// HONO ERROR RESPONSE FACTORY
const app = new Hono();

// Standard API error response structure
interface ApiError {
  success: false;
  error: {
    type: string;
    message: string;
    details?: Array<{
      field: string;
      message: string;
    }>;
  };
}

// Create a reusable validation error handler
function createValidationErrorHandler(target: string) {
  return (result: { success: boolean; error?: z.ZodError }, c: any) => {
    if (!result.success && result.error) {
      const response: ApiError = {
        success: false,
        error: {
          type: 'VALIDATION_ERROR',
          message: `Invalid ${target} data`,
          details: result.error.errors.map(err => ({
            field: err.path.join('.'),
            message: err.message
          }))
        }
      };
      
      return c.json(response, 400);
    }
  };
}

// Use in routes
const userSchema = z.object({
  name: z.string().min(1, 'Name is required'),
  email: z.string().email('Please provide a valid email'),
  password: z.string().min(8, 'Password must be at least 8 characters')
});

app.post(
  '/api/users',
  zValidator('json', userSchema, createValidationErrorHandler('user')),
  async (c) => {
    const user = c.req.valid('json');
    return c.json({ success: true, user }, 201);
  }
);

// I18N CONSIDERATIONS - Internationalized error messages
// Store messages separately for translation

const errorMessages = {
  en: {
    required: (field: string) => `${field} is required`,
    email: 'Please enter a valid email address',
    minLength: (field: string, min: number) => `${field} must be at least ${min} characters`,
    maxLength: (field: string, max: number) => `${field} must be at most ${max} characters`,
    positive: (field: string) => `${field} must be a positive number`,
    integer: (field: string) => `${field} must be a whole number`
  },
  es: {
    required: (field: string) => `${field} es obligatorio`,
    email: 'Por favor ingrese un correo valido',
    minLength: (field: string, min: number) => `${field} debe tener al menos ${min} caracteres`,
    maxLength: (field: string, max: number) => `${field} debe tener como maximo ${max} caracteres`,
    positive: (field: string) => `${field} debe ser un numero positivo`,
    integer: (field: string) => `${field} debe ser un numero entero`
  }
};

type Locale = keyof typeof errorMessages;

// Schema factory with locale support
function createUserSchemaForLocale(locale: Locale) {
  const t = errorMessages[locale];
  
  return z.object({
    name: z.string()
      .min(1, t.required('Name'))
      .max(100, t.maxLength('Name', 100)),
    email: z.string()
      .email(t.email),
    age: z.number()
      .int(t.integer('Age'))
      .positive(t.positive('Age'))
  });
}

// Middleware to set locale from Accept-Language header
app.use('/api/*', async (c, next) => {
  const acceptLanguage = c.req.header('Accept-Language') || 'en';
  const locale = acceptLanguage.startsWith('es') ? 'es' : 'en';
  c.set('locale', locale);
  await next();
});

// Route with i18n validation
app.post('/api/i18n/users', async (c) => {
  const locale = c.get('locale') as Locale;
  const schema = createUserSchemaForLocale(locale);
  
  const body = await c.req.json();
  const result = schema.safeParse(body);
  
  if (!result.success) {
    return c.json({
      success: false,
      errors: formatZodErrorsArray(result.error)
    }, 400);
  }
  
  return c.json({ success: true, user: result.data }, 201);
});

// CONSISTENT ERROR RESPONSE STRUCTURE
// Define a standard format for all API errors

app.onError((err, c) => {
  console.error('Unhandled error:', err);
  
  // Handle Zod errors that slip through
  if (err instanceof z.ZodError) {
    return c.json({
      success: false,
      error: {
        type: 'VALIDATION_ERROR',
        message: 'Request validation failed',
        details: err.errors.map(e => ({
          field: e.path.join('.'),
          message: e.message
        }))
      }
    }, 400);
  }
  
  // Generic error response
  return c.json({
    success: false,
    error: {
      type: 'INTERNAL_ERROR',
      message: process.env.NODE_ENV === 'production' 
        ? 'An unexpected error occurred' 
        : err.message
    }
  }, 500);
});

export default app;
```
