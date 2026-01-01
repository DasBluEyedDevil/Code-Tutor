---
type: "EXAMPLE"
title: "Complete Audit Trail Implementation"
---

**Full audit logging for the Finance Tracker:**

```python
import asyncpg
import asyncio
import json
from typing import Optional

async def setup_audit_system():
    """Create comprehensive audit trail system."""
    conn = await asyncpg.connect(
        'postgresql://finance_user:secure_password@localhost/finance_tracker'
    )
    
    # Audit log table
    await conn.execute('''
        CREATE TABLE IF NOT EXISTS audit_log (
            id SERIAL PRIMARY KEY,
            table_name VARCHAR(100) NOT NULL,
            record_id INTEGER NOT NULL,
            action VARCHAR(10) NOT NULL CHECK (action IN ('INSERT', 'UPDATE', 'DELETE')),
            old_values JSONB,
            new_values JSONB,
            changed_fields TEXT[],
            changed_by INTEGER,
            changed_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
            ip_address INET,
            user_agent TEXT
        );
        
        CREATE INDEX IF NOT EXISTS idx_audit_table_record
        ON audit_log (table_name, record_id);
        
        CREATE INDEX IF NOT EXISTS idx_audit_changed_at
        ON audit_log (changed_at DESC);
    ''')
    
    # Generic audit trigger function
    await conn.execute('''
        CREATE OR REPLACE FUNCTION audit_trigger_func()
        RETURNS TRIGGER AS $$
        DECLARE
            old_json JSONB;
            new_json JSONB;
            changed TEXT[];
            key TEXT;
        BEGIN
            IF TG_OP = 'INSERT' THEN
                new_json := to_jsonb(NEW);
                INSERT INTO audit_log (table_name, record_id, action, new_values)
                VALUES (TG_TABLE_NAME, NEW.id, 'INSERT', new_json);
                
            ELSIF TG_OP = 'UPDATE' THEN
                old_json := to_jsonb(OLD);
                new_json := to_jsonb(NEW);
                
                -- Find changed fields
                SELECT array_agg(key) INTO changed
                FROM jsonb_each(old_json) AS o(key, value)
                JOIN jsonb_each(new_json) AS n(key, value) USING (key)
                WHERE o.value IS DISTINCT FROM n.value;
                
                INSERT INTO audit_log 
                    (table_name, record_id, action, old_values, new_values, changed_fields)
                VALUES (TG_TABLE_NAME, NEW.id, 'UPDATE', old_json, new_json, changed);
                
            ELSIF TG_OP = 'DELETE' THEN
                old_json := to_jsonb(OLD);
                INSERT INTO audit_log (table_name, record_id, action, old_values)
                VALUES (TG_TABLE_NAME, OLD.id, 'DELETE', old_json);
            END IF;
            
            RETURN COALESCE(NEW, OLD);
        END;
        $$ LANGUAGE plpgsql;
    ''')
    
    # Apply trigger to accounts table
    await conn.execute('''
        DROP TRIGGER IF EXISTS audit_accounts ON accounts;
        CREATE TRIGGER audit_accounts
            AFTER INSERT OR UPDATE OR DELETE ON accounts
            FOR EACH ROW EXECUTE FUNCTION audit_trigger_func();
    ''')
    
    print("Audit system created!")
    
    # Demo: Make changes and view audit log
    await conn.execute('''
        UPDATE accounts SET balance = balance + 100 WHERE id = 1
    ''')
    
    audit_entries = await conn.fetch('''
        SELECT action, changed_fields, old_values->>'balance' as old_balance,
               new_values->>'balance' as new_balance, changed_at
        FROM audit_log
        WHERE table_name = 'accounts'
        ORDER BY changed_at DESC
        LIMIT 5
    ''')
    
    print("\nRecent audit entries:")
    for entry in audit_entries:
        print(f"  {entry['action']}: balance {entry['old_balance']} -> {entry['new_balance']}")
    
    await conn.close()

asyncio.run(setup_audit_system())
```
