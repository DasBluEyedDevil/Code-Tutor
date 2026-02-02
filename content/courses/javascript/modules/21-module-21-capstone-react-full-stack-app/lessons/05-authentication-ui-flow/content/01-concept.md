---
type: "THEORY"
title: "Authentication Context Setup"
---

Authentication state needs to be accessible throughout your app without prop drilling. React Context is perfect for this.

**Why Context for Auth?**
- Store user identity and tokens globally
- Avoid passing auth state through every component
- Centralize login/logout logic
- Enable automatic login on page refresh via localStorage

**Context Structure**
Your auth context should expose:
- `user`: Current authenticated user (or null)
- `token`: JWT token for API requests
- `isLoading`: Whether auth check is in progress
- `login(email, password)`: Authenticate user
- `register(email, password, name)`: Create new account
- `logout()`: Clear auth state

**Types for Auth**
```typescript
interface User {
  id: string;
  email: string;
  name: string;
  createdAt: string;
}

interface AuthContextType {
  user: User | null;
  token: string | null;
  isLoading: boolean;
  login: (email: string, password: string) => Promise<void>;
  register: (email: string, password: string, name: string) => Promise<void>;
  logout: () => void;
}
```