---
type: "THEORY"
title: "Why SQLite for Development?"
---

**SQLite is the Perfect Development Database**

SQLite offers unique advantages for development and prototyping:

**Zero Configuration:**
- No database server to install or configure
- Just a single file on disk
- Ships with Python (sqlite3 module built-in)

**Perfect for Learning:**
```python
# Your entire database is one file!
import sqlite3
conn = sqlite3.connect('my_app.db')  # That's it!
```

**Great for Prototyping:**
- Quick to set up, quick to tear down
- Easy to reset (just delete the file)
- No credentials or connection strings to manage
- Works everywhere Python runs

**Limitations to Know:**
- **Single writer** - Only one process can write at a time
- **No concurrent writes** - Writes block each other
- **Limited scale** - Best for small/medium datasets
- **No network access** - File-based, not client-server

**When SQLite Works Great:**
- Local development and testing
- Small to medium applications
- Prototypes and MVPs
- Desktop and mobile apps
- Data under a few GB

**Production Considerations:**
SQLite can work in production for read-heavy, single-user applications, but most web apps benefit from PostgreSQL or MySQL in production.