---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **JSONB** is almost always preferred over JSON (binary, indexable, faster reads)
- Use JSONB for **flexible data**: metadata, preferences, external API data
- Keep **frequently queried fields** as regular columns for best performance
- **Operators:** `->` returns JSON, `->>` returns text, `@>` for containment
- **GIN indexes** make JSONB queries fast - use `jsonb_path_ops` for @> only
- **jsonb_set()** updates specific paths without replacing the whole document
- Use **||** to merge JSONB objects, **-** to remove keys
- **Index hot paths** that you query frequently as expression indexes