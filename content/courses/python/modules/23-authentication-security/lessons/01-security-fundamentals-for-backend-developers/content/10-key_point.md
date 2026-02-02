---
type: "KEY_POINT"
title: "Security Checklist for Every Feature"
---

Before shipping any feature, ask yourself:

1. **Authentication**: Does this require a logged-in user?
2. **Authorization**: Can users only access their own data?
3. **Input Validation**: Is all input validated and sanitized?
4. **Output Encoding**: Is output properly escaped?
5. **Encryption**: Is sensitive data encrypted at rest and in transit?
6. **Logging**: Are security events being logged?
7. **Rate Limiting**: Is this endpoint protected from abuse?
8. **Error Handling**: Do error messages leak sensitive information?