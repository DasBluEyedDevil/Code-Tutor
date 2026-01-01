---
type: "THEORY"
title: "Or require authentication"
---

✓ Disable debug mode
logging.level.root=INFO  # Not DEBUG

✓ Don't show SQL queries
spring.jpa.show-sql=false

✓ Validate database schema only
spring.jpa.hibernate.ddl-auto=validate  # NOT create-drop!

✓ Use environment variables for secrets