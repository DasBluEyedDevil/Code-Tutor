---
type: "EXAMPLE"
title: "Implementing Least Privilege in Database Access"
---

**Creating restricted database users:**

```python
# Least Privilege Database Setup
# This shows the SQL commands conceptually - run via psql or migration

DATABASE_SETUP_SQL = """
-- Create separate roles for different purposes

-- Application user: Limited write access
CREATE USER app_user WITH PASSWORD 'secure_password_here';
GRANT SELECT, INSERT, UPDATE ON ALL TABLES IN SCHEMA public TO app_user;
GRANT USAGE, SELECT ON ALL SEQUENCES IN SCHEMA public TO app_user;
-- Note: No DELETE, no DROP, no CREATE

-- Read-only user for reports and analytics
CREATE USER report_user WITH PASSWORD 'another_secure_password';
GRANT SELECT ON ALL TABLES IN SCHEMA public TO report_user;
-- Cannot modify any data

-- Migration user: Full access (use sparingly)
CREATE USER migration_user WITH PASSWORD 'very_secure_password';
GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO migration_user;
-- Only used during deployments
"""

# Python application: Use appropriate user for each operation
class DatabaseConfig:
    """Use different database users for different operations"""
    
    # Regular application queries
    APP_DATABASE_URL = "postgresql://app_user:password@localhost/financetracker"
    
    # Read-only reporting queries 
    REPORT_DATABASE_URL = "postgresql://report_user:password@localhost/financetracker"
    
    # Migrations only (CI/CD pipeline)
    MIGRATION_DATABASE_URL = "postgresql://migration_user:password@localhost/financetracker"

def get_connection_for_operation(operation_type: str) -> str:
    """Return appropriate database URL based on operation"""
    connections = {
        'read': DatabaseConfig.REPORT_DATABASE_URL,
        'write': DatabaseConfig.APP_DATABASE_URL,
        'migrate': DatabaseConfig.MIGRATION_DATABASE_URL
    }
    return connections.get(operation_type, DatabaseConfig.APP_DATABASE_URL)

# Example: Using least privilege
print("Least Privilege Database Access:")
print(f"For reading reports: {get_connection_for_operation('read')}")
print(f"For app operations: {get_connection_for_operation('write')}")
print(f"For migrations: {get_connection_for_operation('migrate')}")
```
