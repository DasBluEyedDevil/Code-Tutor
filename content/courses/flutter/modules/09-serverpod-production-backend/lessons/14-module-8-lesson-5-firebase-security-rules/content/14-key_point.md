---
type: "KEY_POINT"
title: "Answer Key"
---


### Answer 1: B
**Correct**: Hackers can modify client code to bypass client-side checks

Since Flutter apps run on the user's device, hackers can decompile your app, modify the code, and bypass any client-side security checks. Security rules run on Firebase servers (which hackers can't access), making them the only reliable security layer.

### Answer 2: B
**Correct**: It allows anyone (including unauthenticated users) to write data

`allow write: if true` means "allow anyone to write data, no questions asked." This is extremely dangerous in production - anyone could delete your entire database, inject malicious data, or fill your storage quota.

### Answer 3: B
**Correct**: To prevent invalid data that could break your app

Without validation, users could write `{ title: 123, likes: "hello", createdAt: null }` which would break your app when it tries to display a string title or count numeric likes. Validation ensures data matches your expected schema.

