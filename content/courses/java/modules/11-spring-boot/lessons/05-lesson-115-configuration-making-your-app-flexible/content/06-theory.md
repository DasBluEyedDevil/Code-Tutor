---
type: "THEORY"
title: "Custom application properties"
---

app.name=My Awesome App
app.version=1.0.0
app.email.host=smtp.gmail.com
app.email.port=587

Spring automatically applies these settings!

COMMON PROPERTIES:
- server.port: Change default port (8080)
- spring.datasource.*: Database connection
- spring.jpa.hibernate.ddl-auto: create, update, validate
- logging.level.*: Control log levels