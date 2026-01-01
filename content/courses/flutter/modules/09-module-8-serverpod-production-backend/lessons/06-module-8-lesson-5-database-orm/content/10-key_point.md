---
type: "KEY_POINT"
title: "The id Field is Special"
---

When you add `table: table_name` to your protocol definition, Serverpod automatically adds an `id` field of type `int?`. This is your primary key.

**Important id Behaviors:**

1. **Before Insert**: The `id` is `null` because the database has not assigned one yet

2. **After Insert**: The `id` is set to the auto-generated value from PostgreSQL

3. **For Updates**: The `id` must NOT be null - you cannot update a record that does not exist in the database

4. **For Delete**: You need the `id` to identify which record to remove

**Example:**

```dart
// Creating a new user - id is null
final newUser = User(
  email: 'john@example.com',
  username: 'john',
  createdAt: DateTime.now(),
  isActive: true,
  isVerified: false,
  postCount: 0,
  followerCount: 0,
);

print(newUser.id); // null

// After inserting, the returned user has an id
final savedUser = await User.db.insertRow(session, newUser);
print(savedUser.id); // 1 (or whatever PostgreSQL assigned)
```

