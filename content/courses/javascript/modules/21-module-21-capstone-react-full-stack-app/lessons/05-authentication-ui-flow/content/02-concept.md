---
type: "THEORY"
title: "Form Validation & Error Handling"
---

Login and registration forms need to validate input before sending to the server.

**Client-side Validation**
- Email format check
- Password strength requirements
- Required field checks
- Display clear error messages

**Common Validation Rules**
```typescript
interface ValidationErrors {
  email?: string;
  password?: string;
  name?: string;
  [key: string]: string | undefined;
}

const validateEmail = (email: string): boolean => {
  return /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email);
};

const validatePassword = (password: string): boolean => {
  return password.length >= 8;
};

const validateRegistration = (email: string, password: string, name: string): ValidationErrors => {
  const errors: ValidationErrors = {};
  if (!validateEmail(email)) errors.email = 'Invalid email format';
  if (!validatePassword(password)) errors.password = 'Password must be at least 8 characters';
  if (name.trim().length < 2) errors.name = 'Name must be at least 2 characters';
  return errors;
};
```

**Server Error Handling**
When the API returns an error, display it to the user:
- 400 Bad Request: Show field-specific errors
- 401 Unauthorized: Show "Invalid credentials"
- 409 Conflict: Show "Email already exists"
- 500 Server Error: Show generic message + support contact