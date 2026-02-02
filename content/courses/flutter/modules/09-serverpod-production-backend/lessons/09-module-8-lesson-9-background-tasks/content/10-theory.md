---
type: "THEORY"
title: "Email Sending in Background"
---

Email is a perfect candidate for background processing. Users should not wait for SMTP servers, and failed emails should be retried.

**Why Background Email Matters:**

1. **Speed**: SMTP connections can take 500ms-3s
2. **Reliability**: Email servers are occasionally unavailable
3. **Retries**: Transient failures should be retried automatically
4. **Rate Limiting**: Some providers limit emails per minute
5. **Tracking**: You want to record send status

**Email Architecture:**

```
+-------------+     +---------------+     +----------------+
|  Endpoint   | --> |  Email Queue  | --> |  Email Worker  |
| (instant    |     | (persistent   |     | (sends with    |
|  return)    |     |  storage)     |     |  retries)      |
+-------------+     +---------------+     +--------+-------+
                                                   |
                                                   v
                                          +--------+-------+
                                          |  SMTP/API      |
                                          |  (SendGrid,    |
                                          |   SES, etc.)   |
                                          +----------------+
```

**Best Practices:**

1. **Use a Database Queue**: Persist emails so they survive restarts
2. **Template Rendering**: Render templates before queuing
3. **Idempotency**: Track sent emails to prevent duplicates
4. **Backoff**: Exponential backoff on failures
5. **Dead Letter Queue**: Capture permanently failed emails
6. **Rate Limiting**: Respect provider limits

