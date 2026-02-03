---
type: "KEY_POINT"
title: "Access Modifier Rules"
---

## Key Takeaways

- **Default to `private` for fields, `public` for the API surface** -- fields should be private with public properties. Methods that other code calls are public; internal helpers are private.

- **`protected` is for inheritance** -- derived classes can access protected members, but outside code cannot. Use it sparingly -- prefer interfaces over deep inheritance hierarchies.

- **Always specify access modifiers explicitly** -- C# defaults to `private` for class members, but writing it out makes your intent clear and prevents mistakes.
