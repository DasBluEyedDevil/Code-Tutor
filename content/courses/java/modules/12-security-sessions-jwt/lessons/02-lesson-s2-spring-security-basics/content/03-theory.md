---
type: "THEORY"
title: "Adding Spring Security"
---

Add the dependency:

<!-- Maven -->
<dependency>
    <groupId>org.springframework.boot</groupId>
    <artifactId>spring-boot-starter-security</artifactId>
</dependency>

With ZERO configuration, Spring Security:
- Requires authentication for ALL endpoints
- Generates a random password (check console logs)
- Provides /login and /logout pages
- Enables CSRF protection
- Adds security headers

Console output:
Using generated security password: 8f3a7b2c-4d5e-6f7g-8h9i-0j1k2l3m4n5o

Default username: user

This is intentionally strict. You customize from a secure baseline rather than adding security later.