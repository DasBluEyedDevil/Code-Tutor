---
type: "THEORY"
title: "Project-Wide Type Checking"
---

For entire projects, create a `jsconfig.json`:

```json
{
  "compilerOptions": {
    "checkJs": true,
    "strict": true,
    "target": "ESNext",
    "module": "ESNext",
    "moduleResolution": "bundler"
  },
  "include": ["src/**/*.js"],
  "exclude": ["node_modules"]
}
```

**Key options:**
- `checkJs: true` - Check all .js files
- `strict: true` - Enable all strict checks
- `target/module: ESNext` - Use latest JavaScript features

Bun respects `jsconfig.json`, so your IDE and runtime are in sync.