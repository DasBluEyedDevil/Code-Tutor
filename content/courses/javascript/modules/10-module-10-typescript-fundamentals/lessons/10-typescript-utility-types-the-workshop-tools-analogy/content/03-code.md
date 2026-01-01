---
type: "CODE"
title: "Pick<T, K> and Omit<T, K>"
---

Pick<T, K> creates a type by selecting specific properties from T. Omit<T, K> creates a type by excluding specific properties from T. These are complementary - Pick says 'I want only these', Omit says 'I want everything except these'. They're essential for creating API response types and hiding internal fields.

```typescript
// Full user type with many properties
interface User {
  id: number;
  name: string;
  email: string;
  passwordHash: string;     // Sensitive!
  createdAt: Date;
  updatedAt: Date;
  lastLoginIp: string;      // Sensitive!
  role: 'admin' | 'user';
  preferences: {
    theme: 'light' | 'dark';
    notifications: boolean;
  };
}

// PICK<T, K> - Select only specific properties
// Great for API responses - only expose what's needed

// Public profile - only non-sensitive fields
type PublicProfile = Pick<User, 'id' | 'name' | 'role'>;
// Equivalent to:
// {
//   id: number;
//   name: string;
//   role: 'admin' | 'user';
// }

function getPublicProfile(user: User): PublicProfile {
  // TypeScript ensures we only return picked fields
  return {
    id: user.id,
    name: user.name,
    role: user.role
  };
}

let fullUser: User = {
  id: 1,
  name: 'Alice',
  email: 'alice@test.com',
  passwordHash: 'hashed_password_here',
  createdAt: new Date('2024-01-01'),
  updatedAt: new Date('2024-06-15'),
  lastLoginIp: '192.168.1.1',
  role: 'admin',
  preferences: { theme: 'dark', notifications: true }
};

let publicData = getPublicProfile(fullUser);
console.log(publicData);
// { id: 1, name: 'Alice', role: 'admin' }
// passwordHash, email, IP - all excluded!

// OMIT<T, K> - Exclude specific properties
// Great for hiding sensitive fields

// Remove sensitive fields for logging
type SafeUserLog = Omit<User, 'passwordHash' | 'lastLoginIp'>;
// Has everything EXCEPT passwordHash and lastLoginIp

function logUserActivity(user: User): void {
  // Create safe version for logging
  let safeUser: SafeUserLog = {
    id: user.id,
    name: user.name,
    email: user.email,
    createdAt: user.createdAt,
    updatedAt: user.updatedAt,
    role: user.role,
    preferences: user.preferences
  };
  console.log('User activity:', JSON.stringify(safeUser, null, 2));
}

// API endpoint types
interface BlogPost {
  id: number;
  title: string;
  content: string;
  authorId: number;
  createdAt: Date;
  updatedAt: Date;
  views: number;
  internalNotes: string;  // Admin only!
}

// What authors can create (no id, timestamps, or views)
type CreatePostInput = Omit<BlogPost, 'id' | 'createdAt' | 'updatedAt' | 'views' | 'internalNotes'>;
// { title: string; content: string; authorId: number; }

// What public API returns (no internal notes)
type PublicPost = Omit<BlogPost, 'internalNotes'>;

// Combining Pick and Omit for complex transformations
// Summary = only these fields from full post
type PostSummary = Pick<BlogPost, 'id' | 'title' | 'authorId' | 'createdAt'>;

function getPostSummaries(posts: BlogPost[]): PostSummary[] {
  return posts.map(post => ({
    id: post.id,
    title: post.title,
    authorId: post.authorId,
    createdAt: post.createdAt
  }));
}

let posts: BlogPost[] = [
  { id: 1, title: 'Hello World', content: 'First post...', authorId: 1, 
    createdAt: new Date(), updatedAt: new Date(), views: 100, internalNotes: 'Review needed' }
];

console.log(getPostSummaries(posts));
// [{ id: 1, title: 'Hello World', authorId: 1, createdAt: [Date] }]
```
