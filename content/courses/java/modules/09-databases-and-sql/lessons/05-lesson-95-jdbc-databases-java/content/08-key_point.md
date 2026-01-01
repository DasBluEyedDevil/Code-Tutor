---
type: "KEY_POINT"
title: "JDBC Best Practices"
---

1. ALWAYS USE TRY-WITH-RESOURCES
   - Auto-closes connections, statements, resultsets
   - Prevents resource leaks

2. USE PREPAREDSTATEMENT, NOT STATEMENT
   - Security (prevents SQL injection)
   - Performance (pre-compiled)

3. DON'T CONCATENATE SQL WITH USER INPUT
   - Use ? placeholders
   - Set parameters with setString(), setInt(), etc.

4. USE CONNECTION POOLING IN PRODUCTION
   - Libraries: HikariCP, Apache DBCP
   - Reuse connections (expensive to create)

5. HANDLE EXCEPTIONS PROPERLY
   - Log errors
   - Don't expose SQL details to users

6. CLOSE RESOURCES IN FINALLY OR USE TRY-WITH-RESOURCES
   - Connection leaks kill performance