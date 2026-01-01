---
type: "WARNING"
title: "Critical JDBC Security and Resource Management"
---

SECURITY RISKS:
- SQL Injection was the #1 web vulnerability in 2023
- NEVER concatenate user input into SQL strings
- ALWAYS use PreparedStatement with ? placeholders

RESOURCE LEAKS:
- Unclosed connections exhaust database pool
- Always use try-with-resources (Java 7+)
- Close in order: ResultSet, Statement, Connection

CONNECTION POOL EXHAUSTION:
- Creating connections is expensive (100-500ms each)
- Use HikariCP or similar pool in production
- Set reasonable pool size (10-20 connections typical)

CREDENTIAL EXPOSURE:
- Never hardcode database passwords in source code
- Use environment variables or secrets management
- Rotate credentials regularly