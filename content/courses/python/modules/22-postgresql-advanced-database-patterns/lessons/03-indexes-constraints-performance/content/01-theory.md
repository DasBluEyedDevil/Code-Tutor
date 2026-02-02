---
type: "THEORY"
title: "Constraints: Data Integrity"
---

Constraints ensure your data stays valid at the database level - the last line of defense:

**PRIMARY KEY** - Unique identifier, never NULL
```sql
id SERIAL PRIMARY KEY
-- or
PRIMARY KEY (user_id, account_id)  -- Composite key
```

**FOREIGN KEY** - References another table
```sql
user_id INTEGER REFERENCES users(id) ON DELETE CASCADE
-- ON DELETE options: CASCADE, SET NULL, RESTRICT, NO ACTION
```

**UNIQUE** - No duplicate values
```sql
email VARCHAR(255) UNIQUE
-- or table-level:
UNIQUE (user_id, account_name)  -- Unique combination
```

**NOT NULL** - Value required
```sql
name VARCHAR(100) NOT NULL
```

**CHECK** - Custom validation
```sql
amount DECIMAL(12,2) CHECK (amount != 0),
account_type VARCHAR(20) CHECK (account_type IN ('checking', 'savings', 'credit'))
```

**DEFAULT** - Auto-fill if not provided
```sql
created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
is_active BOOLEAN DEFAULT true
```