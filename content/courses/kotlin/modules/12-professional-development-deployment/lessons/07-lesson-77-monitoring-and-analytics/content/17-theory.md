---
type: "THEORY"
title: "Quiz Answers"
---


**Question 1: B) INFO**

Logging levels:
- **ERROR**: Exceptions, failures
- **WARN**: Potential issues
- **INFO**: Important business events ✅
- **DEBUG**: Detailed execution (dev only)

Business events = INFO level

---

**Question 2: B) Tracks and reports application errors**

Sentry:
- Captures all exceptions
- Groups similar errors
- Shows stack traces
- Sends alerts
- Tracks error trends

Essential for production apps!

---

**Question 3: C) Easier to parse and analyze**

JSON logs enable:
- Searching by field
- Aggregation and counting
- Automated analysis
- Integration with log tools

Text logs are hard to parse.

---

**Question 4: A) Application Performance Monitoring**

APM tools (New Relic, Datadog):
- Track request times
- Monitor database queries
- Identify bottlenecks
- Alert on slow performance

---

**Question 5: C) 503 Service Unavailable**

Health check status codes:
- **200 OK**: All systems healthy
- **503 Service Unavailable**: Critical dependency down ✅
- **500**: Code error (not appropriate here)

Load balancers remove unhealthy instances.

---

