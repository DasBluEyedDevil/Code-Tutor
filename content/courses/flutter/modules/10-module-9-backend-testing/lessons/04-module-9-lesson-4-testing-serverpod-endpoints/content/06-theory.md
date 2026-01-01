---
type: "THEORY"
title: "Mocking Sessions and Dependencies"
---

Sometimes you need to mock parts of the session for unit tests. Serverpod supports this pattern while still allowing integration tests when needed.

Key mocking scenarios:

1. **External APIs**: Mock HTTP clients for third-party services
2. **Email Services**: Mock email sending to avoid sending real emails
3. **File Storage**: Mock cloud storage for faster tests
4. **Time-sensitive logic**: Mock DateTime.now() for predictable tests