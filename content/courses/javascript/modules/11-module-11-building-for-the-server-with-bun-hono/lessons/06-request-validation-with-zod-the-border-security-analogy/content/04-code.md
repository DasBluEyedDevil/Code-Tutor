---
type: "EXAMPLE"
title: "Custom Validations"
---

Sometimes built-in validators are not enough. You need custom business logic - password confirmation must match, dates must be in the future, or a field depends on another field value. Zod provides refine() for simple validations and superRefine() for complex scenarios with multiple error paths.

```typescript
// Zod Custom Validations - Beyond Built-in Rules
import { z } from 'zod';

// REFINE - Add custom validation logic
// Returns true if valid, false if invalid

const passwordSchema = z.string()
  .min(8, 'Password must be at least 8 characters')
  .refine(
    (password) => /[A-Z]/.test(password),
    { message: 'Password must contain at least one uppercase letter' }
  )
  .refine(
    (password) => /[a-z]/.test(password),
    { message: 'Password must contain at least one lowercase letter' }
  )
  .refine(
    (password) => /[0-9]/.test(password),
    { message: 'Password must contain at least one number' }
  )
  .refine(
    (password) => /[!@#$%^&*]/.test(password),
    { message: 'Password must contain at least one special character (!@#$%^&*)' }
  );

// Test password validation
const weakPassword = passwordSchema.safeParse('weak');
console.log(weakPassword.success);  // false
if (!weakPassword.success) {
  console.log(weakPassword.error.errors[0].message);
  // 'Password must be at least 8 characters'
}

const strongPassword = passwordSchema.safeParse('SecurePass123!');
console.log(strongPassword.success);  // true

// REFINE ON OBJECTS - Cross-field validation
const registrationSchema = z.object({
  email: z.string().email(),
  password: z.string().min(8),
  confirmPassword: z.string()
}).refine(
  (data) => data.password === data.confirmPassword,
  {
    message: 'Passwords do not match',
    path: ['confirmPassword']  // Error appears on this field
  }
);

const badRegistration = registrationSchema.safeParse({
  email: 'user@example.com',
  password: 'SecurePass123',
  confirmPassword: 'DifferentPass456'
});

if (!badRegistration.success) {
  console.log(badRegistration.error.errors);
  // [{ path: ['confirmPassword'], message: 'Passwords do not match' }]
}

// DATE RANGE VALIDATION
const dateRangeSchema = z.object({
  startDate: z.coerce.date(),
  endDate: z.coerce.date()
}).refine(
  (data) => data.endDate > data.startDate,
  {
    message: 'End date must be after start date',
    path: ['endDate']
  }
);

// FUTURE DATE VALIDATION
const futureDateSchema = z.coerce.date().refine(
  (date) => date > new Date(),
  { message: 'Date must be in the future' }
);

// SUPERREFINE - For complex multi-error validations
const advancedPasswordSchema = z.string().superRefine((password, ctx) => {
  // ctx.addIssue() allows adding multiple errors
  
  if (password.length < 8) {
    ctx.addIssue({
      code: z.ZodIssueCode.custom,
      message: 'Password must be at least 8 characters',
      fatal: true  // Stop further checks if this fails
    });
    return;  // Early return on fatal error
  }
  
  const checks = [
    { test: /[A-Z]/, message: 'Missing uppercase letter' },
    { test: /[a-z]/, message: 'Missing lowercase letter' },
    { test: /[0-9]/, message: 'Missing number' },
    { test: /[!@#$%^&*]/, message: 'Missing special character' }
  ];
  
  // Add ALL failing checks as separate errors
  for (const check of checks) {
    if (!check.test.test(password)) {
      ctx.addIssue({
        code: z.ZodIssueCode.custom,
        message: check.message
      });
    }
  }
});

// Test - will show ALL errors at once
const weakResult = advancedPasswordSchema.safeParse('abc');
if (!weakResult.success) {
  console.log('All password errors:');
  weakResult.error.errors.forEach(err => console.log(`  - ${err.message}`));
  // - Password must be at least 8 characters
}

// CONDITIONAL VALIDATION WITH SUPERREFINE
const paymentSchema = z.object({
  method: z.enum(['credit_card', 'paypal', 'bank_transfer']),
  cardNumber: z.string().optional(),
  paypalEmail: z.string().email().optional(),
  bankAccount: z.string().optional()
}).superRefine((data, ctx) => {
  if (data.method === 'credit_card' && !data.cardNumber) {
    ctx.addIssue({
      code: z.ZodIssueCode.custom,
      message: 'Card number is required for credit card payments',
      path: ['cardNumber']
    });
  }
  
  if (data.method === 'paypal' && !data.paypalEmail) {
    ctx.addIssue({
      code: z.ZodIssueCode.custom,
      message: 'PayPal email is required for PayPal payments',
      path: ['paypalEmail']
    });
  }
  
  if (data.method === 'bank_transfer' && !data.bankAccount) {
    ctx.addIssue({
      code: z.ZodIssueCode.custom,
      message: 'Bank account is required for bank transfers',
      path: ['bankAccount']
    });
  }
});

// CUSTOM ERROR MESSAGES - Various approaches
const userSchema = z.object({
  // Inline message as second argument
  name: z.string().min(1, 'Name cannot be empty'),
  
  // Object with message property
  email: z.string().email({ message: 'Please enter a valid email address' }),
  
  // Multiple messages for different validations
  age: z.number({
    required_error: 'Age is required',
    invalid_type_error: 'Age must be a number'
  }).int('Age must be a whole number')
    .min(18, 'Must be at least 18 years old')
    .max(120, 'Age seems unrealistic')
});

// REUSABLE CUSTOM VALIDATORS
const isValidPhoneNumber = (phone: string) => {
  const phoneRegex = /^\+?[1-9]\d{1,14}$/;  // E.164 format
  return phoneRegex.test(phone.replace(/[\s-]/g, ''));
};

const phoneSchema = z.string()
  .transform(phone => phone.replace(/[\s-]/g, ''))  // Remove spaces and dashes
  .refine(isValidPhoneNumber, {
    message: 'Invalid phone number. Use format: +1234567890'
  });

// ASYNC VALIDATION - For database lookups, API calls, etc.
const uniqueEmailSchema = z.string().email().refine(
  async (email) => {
    // Simulate database check
    const existingEmails = ['taken@example.com', 'used@example.com'];
    await new Promise(resolve => setTimeout(resolve, 100));  // Simulate delay
    return !existingEmails.includes(email);
  },
  { message: 'This email is already registered' }
);

// Must use parseAsync for async refinements
async function validateEmail(email: string) {
  const result = await uniqueEmailSchema.safeParseAsync(email);
  console.log(result.success ? 'Email available' : 'Email taken');
}

validateEmail('new@example.com');  // Email available
validateEmail('taken@example.com'); // Email taken
```
