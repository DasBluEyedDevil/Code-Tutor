---
type: "WARNING"
title: "Common Mistakes to Avoid"
---

**Mistake 1: Forgetting to Run Generate**

After changing YAML files, you MUST run `serverpod generate`. Otherwise:
- New models won't exist
- Changed fields won't update
- You'll get confusing errors

**Mistake 2: Incorrect YAML Syntax**

```yaml
# WRONG - Missing colon after fields
fields
  name: String

# CORRECT
fields:
  name: String
```

```yaml
# WRONG - String default without nested quotes
fields:
  status: String, default='pending'

# CORRECT
fields:
  status: String, default="'pending'"
```

**Mistake 3: Editing Generated Files**

Never edit files in generated/ folders. Your changes WILL be lost.

**Mistake 4: Mismatched Package Versions**

If server and client packages are out of sync, you'll get serialization errors. Always regenerate both by running `serverpod generate` from the server folder.

**Mistake 5: Circular Relations**

```yaml
# This can cause issues
class: User
fields:
  posts: List<Post>  # User has posts

class: Post
fields:
  author: User       # Post has user
  # Both trying to include each other!
```

Use explicit include statements when fetching to avoid infinite loops.

