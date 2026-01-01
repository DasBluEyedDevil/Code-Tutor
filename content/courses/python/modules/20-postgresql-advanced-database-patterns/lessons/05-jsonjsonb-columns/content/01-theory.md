---
type: "THEORY"
title: "When to Use JSON vs Relational Columns"
---

PostgreSQL's JSON support gives you the flexibility of document databases with the power of relational SQL.

**Use JSON/JSONB for:**
- **Flexible metadata** - User preferences, API responses, audit logs
- **Varying structure** - Different transaction types with different fields
- **Denormalized data** - Caching complex query results
- **External data** - Storing third-party API responses
- **Tags/attributes** - Variable lists of properties

**Stick with relational columns for:**
- **Data you query frequently** - Indexed columns are faster
- **Data with constraints** - CHECK, UNIQUE, FOREIGN KEY
- **Data you aggregate** - SUM, AVG, GROUP BY
- **Core business data** - Account balances, user IDs

**Finance Tracker Examples:**
- **Relational:** `amount`, `account_id`, `user_id`, `transaction_date`
- **JSONB:** `metadata` (merchant info, location, receipt data, tags)
- **JSONB:** `user_preferences` (display settings, notification config)

**Rule of Thumb:** If you need to search, sort, or join on it - make it a column. If it's just stored and occasionally filtered - JSONB is fine.