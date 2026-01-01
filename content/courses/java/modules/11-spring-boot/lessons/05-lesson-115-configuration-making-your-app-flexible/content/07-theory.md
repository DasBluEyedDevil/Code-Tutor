---
type: "THEORY"
title: "application.yml - The Modern Alternative"
---

YAML format is more readable for complex configs:

Location: src/main/resources/application.yml

spring:
  datasource:
    url: jdbc:mysql://localhost:3306/mydb
    username: root
    password: secret
  jpa:
    hibernate:
      ddl-auto: update
    show-sql: true

server:
  port: 8080

app:
  name: My Awesome App
  version: 1.0.0
  email:
    host: smtp.gmail.com
    port: 587
    username: user@example.com

YAML BENEFITS:
✓ Hierarchical structure (easier to read)
✓ No repetition of prefixes
✓ Supports lists and complex objects
✓ Preferred for modern Spring Boot apps

You can use EITHER .properties OR .yml (not both!)