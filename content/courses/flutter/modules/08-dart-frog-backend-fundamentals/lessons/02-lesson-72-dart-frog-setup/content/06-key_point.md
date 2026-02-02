---
type: "KEY_POINT"
title: "Key Commands Reference"
---


Here are the essential Dart Frog CLI commands you'll use constantly:

| Command | What It Does |
|---------|-------------|
| `dart_frog create <name>` | Create a new Dart Frog project |
| `dart_frog dev` | Start dev server with hot reload |
| `dart_frog build` | Build for production |
| `dart_frog new route <path>` | Generate a new route file |
| `dart_frog new middleware <path>` | Generate middleware |

**Creating Routes with the CLI**:

Instead of manually creating files:

```bash
dart_frog new route /api/users
```

This creates `routes/api/users/index.dart` with boilerplate code.

**Pro Tips**:

1. **Always use `dart_frog dev` during development** - hot reload saves massive time
2. **Use the CLI to create routes** - ensures correct structure
3. **Check the terminal for errors** - Dart Frog shows helpful error messages

**What's Next?**

In the next lesson, we'll create actual API endpoints - handling different HTTP methods (GET, POST, PUT, DELETE), working with request bodies, and returning JSON responses.

