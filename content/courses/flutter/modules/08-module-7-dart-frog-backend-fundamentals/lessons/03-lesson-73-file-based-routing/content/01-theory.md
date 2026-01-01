---
type: "THEORY"
title: "How File-Based Routing Works"
---


Dart Frog uses **file-based routing** - the file path in your `routes/` folder becomes the URL path for your API.

**The Rule is Simple**:
- File path = URL path
- No configuration needed
- Create a file, get an endpoint

**Examples**:

| File Path | URL Endpoint |
|-----------|-------------|
| `routes/index.dart` | GET `/` |
| `routes/users.dart` | GET `/users` |
| `routes/hello.dart` | GET `/hello` |
| `routes/api/status.dart` | GET `/api/status` |

**Why This is Brilliant**:
1. **No Router Configuration**: Unlike Express or other frameworks, you don't configure routes manually
2. **Visual Structure**: Looking at your folder = looking at your API
3. **Instant Endpoints**: New file = new endpoint immediately
4. **Familiar Pattern**: If you know Next.js, this feels right at home

