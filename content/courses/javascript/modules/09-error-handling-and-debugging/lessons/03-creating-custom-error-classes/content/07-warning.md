---
type: "WARNING"
title: "Custom Error Pitfalls"
---

### 1. Forgetting `super()`
If you define a constructor but forget to call `super(message)`, JavaScript will throw an error immediately before your code even has a chance to crash. `super()` is required to initialize the parent `Error` object.

### 2. Forgetting `.name`
If you don't set `this.name = "MyError"`, your error will show up in the console as just `Error: message`. This makes it much harder to debug because you can't tell at a glance which custom class triggered the problem.

### 3. Inheritance Overload
Don't create a massive tree of error types (e.g., `DatabaseError` -> `MySQLError` -> `MySQLConnectionError` -> `MySQLConnectionTimeoutError`). 
*   **Fix:** Keep your error hierarchy shallow (usually just 1 or 2 levels). Use custom properties (like `code: 'TIMEOUT'`) to differentiate minor details instead of creating dozens of new classes.

### 4. Throwing vs Returning
Always use the `throw` keyword with your custom errors. If you just `return new ValidationError()`, it’s just a normal object and won't trigger any `catch` blocks.
