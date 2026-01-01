---
type: "THEORY"
title: "Common Security Patterns"
---


### 1. Public Read, Authenticated Write

**Use case**: Blog posts, public content


### 2. User-Specific Data (Most Common!)

**Use case**: User profiles, private data


### 3. Role-Based Access

**Use case**: Admin panels, moderation


### 4. Validate Data Types

**Use case**: Prevent invalid data


### 5. Subcollections

**Use case**: Comments on posts, nested data




```javascript
match /posts/{postId} {
  allow read: if true;
  allow write: if request.auth != null;

  match /comments/{commentId} {
    allow read: if true;
    allow create: if request.auth != null;
    allow update, delete: if request.auth != null
                           && request.auth.uid == resource.data.userId;
  }
}
```
