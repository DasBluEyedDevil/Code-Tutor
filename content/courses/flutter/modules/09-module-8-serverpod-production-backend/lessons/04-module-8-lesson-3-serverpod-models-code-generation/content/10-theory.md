---
type: "THEORY"
title: "Running serverpod generate"
---

After defining your models in YAML, you must generate the Dart code.

**The Generate Command:**

```bash
# Navigate to your server project
cd my_project_server

# Run the generator
serverpod generate
```

**What Happens During Generation:**

1. **Parses YAML files**: Reads all .yaml files in protocol/

2. **Validates definitions**: Checks for errors (typos, invalid types, etc.)

3. **Generates server models**: Creates Dart classes in lib/src/generated/

4. **Generates client models**: Creates Dart classes in my_project_client/

5. **Generates protocol**: Creates the API contract

6. **Updates database migrations**: Prepares SQL for schema changes

**When to Run Generate:**

Run `serverpod generate` after:
- Adding a new .yaml model file
- Modifying an existing model
- Adding or changing endpoints
- Any change to the protocol/ folder

**Pro Tip:** Many developers set up file watchers to auto-run generate on save.

