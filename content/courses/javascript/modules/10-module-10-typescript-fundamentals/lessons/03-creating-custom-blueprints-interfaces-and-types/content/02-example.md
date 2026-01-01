---
type: "EXAMPLE"
title: "Defining the Shape of Data"
---

```typescript
// 1. Defining an Interface
// We use the 'interface' keyword and PascalCase
interface User {
    readonly id: number; // Cannot be changed after creation
    username: string;
    email: string;
    level?: number; // The '?' means this property is OPTIONAL
}

const alice: User = {
    id: 1,
    username: "AliceInCode",
    email: "alice@example.com"
    // level is missing, but that's okay!
};

// alice.id = 2; // ERROR! Cannot assign to 'id' because it is read-only.

// 2. Defining a Type Alias
// Good for unions and simple definitions
type UserRole = "admin" | "moderator" | "guest";

type Point = {
    x: number;
    y: number;
};

// 3. Extending an Interface
interface AdminUser extends User {
    role: "admin";
    permissions: string[];
}

const bob: AdminUser = {
    id: 2,
    username: "BobAdmin",
    email: "bob@server.com",
    role: "admin",
    permissions: ["delete_user", "edit_posts"]
};

// 4. Using types in functions
function login(user: User) {
    console.log(`User ${user.username} is logging in...`);
}
```