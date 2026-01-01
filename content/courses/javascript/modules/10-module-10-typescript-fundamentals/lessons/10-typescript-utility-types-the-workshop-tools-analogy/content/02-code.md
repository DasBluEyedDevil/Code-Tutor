---
type: "CODE"
title: "Partial<T> and Required<T>"
---

Partial<T> transforms all properties of T into optional properties. This is incredibly useful for update operations where you might only change some fields. Required<T> does the opposite - it makes all properties required, even those marked with '?'. These two utilities are mirrors of each other.

```typescript
// Original type with mix of required and optional
interface User {
  id: number;
  name: string;
  email: string;
  avatar?: string;    // Optional
  bio?: string;       // Optional
}

// PARTIAL<T> - Makes ALL properties optional
type PartialUser = Partial<User>;
// Equivalent to:
// {
//   id?: number;
//   name?: string;
//   email?: string;
//   avatar?: string;
//   bio?: string;
// }

// Real use case: Update operations
function updateUser(userId: number, updates: Partial<User>): User {
  // Fetch existing user (simulated)
  let existingUser: User = {
    id: userId,
    name: 'Alice',
    email: 'alice@example.com'
  };
  
  // Merge updates - only provided fields are changed
  return { ...existingUser, ...updates };
}

// You can update just one field
let updated1 = updateUser(1, { name: 'Alicia' });
console.log(updated1);
// { id: 1, name: 'Alicia', email: 'alice@example.com' }

// Or multiple fields
let updated2 = updateUser(1, { email: 'new@email.com', bio: 'Developer' });
console.log(updated2);
// { id: 1, name: 'Alice', email: 'new@email.com', bio: 'Developer' }

// REQUIRED<T> - Makes ALL properties required
type RequiredUser = Required<User>;
// Equivalent to:
// {
//   id: number;
//   name: string;
//   email: string;
//   avatar: string;   // No longer optional!
//   bio: string;      // No longer optional!
// }

// Real use case: Form validation - ensure all fields filled before submit
interface RegistrationForm {
  username?: string;
  email?: string;
  password?: string;
  confirmPassword?: string;
}

// After validation, we know all fields are present
type ValidatedForm = Required<RegistrationForm>;

function submitRegistration(form: ValidatedForm): void {
  // TypeScript knows all fields are present
  console.log(`Registering ${form.username} with email ${form.email}`);
  // No need for optional chaining or null checks!
}

// Validate and submit
let formData: RegistrationForm = {
  username: 'alice',
  email: 'alice@test.com',
  password: 'secure123',
  confirmPassword: 'secure123'
};

// Check all required fields before calling submitRegistration
if (formData.username && formData.email && formData.password && formData.confirmPassword) {
  submitRegistration(formData as ValidatedForm);
}
// Output: Registering alice with email alice@test.com

// Combining Partial with intersection for patch operations
interface Article {
  id: number;
  title: string;
  content: string;
  published: boolean;
}

// Patch = some fields from Article, plus metadata
type ArticlePatch = Partial<Article> & { updatedAt: Date };

function patchArticle(id: number, patch: ArticlePatch): void {
  console.log(`Patching article ${id} at ${patch.updatedAt}`);
  if (patch.title) console.log(`New title: ${patch.title}`);
  if (patch.published !== undefined) console.log(`Published: ${patch.published}`);
}

patchArticle(42, { title: 'New Title', updatedAt: new Date() });
// Patching article 42 at [current date]
// New title: New Title
```
