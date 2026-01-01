---
type: "THEORY"
title: "Adding Spring Security to Your Project"
---

Step 1: Add dependency to pom.xml:

<dependency>
    <groupId>org.springframework.boot</groupId>
    <artifactId>spring-boot-starter-security</artifactId>
</dependency>

That's it! Spring Security is now active.

DEFAULT BEHAVIOR (automatic):
- ALL endpoints require authentication
- Default user: 'user'
- Random password: Printed in console on startup
- Login form at /login

Console output:
Using generated security password: a1b2c3d4-e5f6-7890-abcd-ef1234567890

Now every API call needs authentication!