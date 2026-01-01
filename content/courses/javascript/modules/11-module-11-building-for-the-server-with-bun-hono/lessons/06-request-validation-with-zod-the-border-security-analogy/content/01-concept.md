---
type: "CONCEPT"
title: "Why Validate API Input?"
---

Imagine you are building a border checkpoint for a country. Anyone can approach your border, but you must verify their documents before letting them through. You check passports, visas, and declarations. Without this checkpoint, anyone could enter with forged papers, dangerous items, or false identities.

Your API endpoints are exactly like border checkpoints. Data arrives from the outside world - users, mobile apps, third-party services - and you have no control over what they send. They might send malformed JSON, wrong data types, missing fields, or even malicious payloads designed to exploit your system.

Validation at the API boundary is your first line of defense. It catches problems early, before bad data reaches your business logic or database. Here is why this matters:

**Security**: Without validation, attackers can send SQL injection strings, oversized payloads that crash your server, or specially crafted data that exploits vulnerabilities. Proper validation sanitizes and rejects malicious input before it causes damage.

**Data Integrity**: Your database schema expects specific types and formats. If someone sends a string where you expect a number, your application crashes or corrupts data. Validation ensures only clean, correctly-typed data enters your system.

**TypeScript Integration**: JavaScript is dynamically typed at runtime. Even if TypeScript compiles your code correctly, incoming JSON has type 'any'. Zod bridges this gap by validating runtime data and inferring TypeScript types from schemas, giving you end-to-end type safety.

**Developer Experience**: Good validation provides clear error messages that help API consumers fix their requests. Instead of cryptic database errors, users get helpful feedback like 'email must be a valid email address'.

**Why Zod?**: Zod is a TypeScript-first schema validation library. Unlike older libraries like Joi or Yup, Zod was built for TypeScript from the ground up. It infers types automatically, has zero dependencies, and integrates seamlessly with modern frameworks like Hono.