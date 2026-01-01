---
type: "THEORY"
title: "Audit Trail Pattern"
---

Track who changed what and when - essential for financial applications:

**Basic Audit Fields:**
```sql
created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
created_by INTEGER REFERENCES users(id),
updated_by INTEGER REFERENCES users(id)
```

**Auto-update trigger:**
```sql
CREATE OR REPLACE FUNCTION update_updated_at()
RETURNS TRIGGER AS $$
BEGIN
    NEW.updated_at = CURRENT_TIMESTAMP;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_transactions_updated
    BEFORE UPDATE ON transactions
    FOR EACH ROW
    EXECUTE FUNCTION update_updated_at();
```

**Full Audit Log (separate table):**
```sql
CREATE TABLE audit_log (
    id SERIAL PRIMARY KEY,
    table_name VARCHAR(50),
    record_id INTEGER,
    action VARCHAR(10),  -- INSERT, UPDATE, DELETE
    old_values JSONB,
    new_values JSONB,
    changed_by INTEGER,
    changed_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
```

**Finance Tracker Requirements:**
- Track every balance change
- Know who made each transaction
- Audit trail for compliance
- Ability to reconstruct history