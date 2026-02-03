---
type: "KEY_POINT"
title: "Why Databases Over Files"
---

## Key Takeaways

- **Databases provide indexed, transactional, concurrent access** -- text files require reading everything to find one record. Databases use indexes for instant lookups, transactions for safety, and locking for multi-user access.

- **SQL databases are the default for business applications** -- PostgreSQL (open-source), SQL Server (enterprise), and SQLite (embedded) are the most common choices. Start with SQLite for learning, scale to PostgreSQL for production.

- **Files are still useful for configuration and logging** -- not everything belongs in a database. Config files (appsettings.json), logs, and static assets are better as files.
