---
type: "WARNING"
title: "Production Monitoring Best Practices"
---


**Do's**

1. **Log at Appropriate Levels**
   - DEBUG: Development-only details
   - INFO: Normal operations (request completed, user action)
   - WARNING: Recoverable issues (retry succeeded, deprecated usage)
   - ERROR: Failures requiring attention
   - FATAL: System cannot continue

2. **Include Context in Logs**
```dart
// Good: Includes actionable context
session.log(
  'Payment failed for user ${userId}, amount ${amount}, error: $error',
  level: LogLevel.error,
);

// Bad: No context
session.log('Payment failed', level: LogLevel.error);
```

3. **Monitor Key Metrics**
   - Response time (p50, p95, p99)
   - Error rate
   - Database connection pool utilization
   - Memory usage trends

4. **Set Up Alerts for**
   - Error rate exceeding threshold
   - Response time degradation
   - Database connection exhaustion
   - Memory pressure

5. **Regularly Review Logs**
   - Daily review of ERROR level logs
   - Weekly review of WARNING patterns
   - Monthly analysis of performance trends

**Don'ts**

1. **Don't Log Sensitive Data**
```dart
// BAD: Logging sensitive information
session.log('User login: password=${password}');
session.log('Payment: card=${cardNumber}');

// GOOD: Redact sensitive fields
session.log('User login: userId=${userId}');
session.log('Payment: cardLast4=${cardNumber.substring(cardNumber.length - 4)}');
```

2. **Don't Over-Log in Production**
   - Debug logs can fill storage quickly
   - Configure log levels per environment
   - Use sampling for high-volume endpoints

3. **Don't Ignore Warnings**
   - Warnings often precede errors
   - Track warning trends over time
   - Investigate repeated warnings

4. **Don't Forget to Clean Up Old Logs**
   - Set retention policies
   - Archive important logs before deletion
   - Monitor storage usage

5. **Don't Rely Solely on Logs**
   - Combine with metrics and traces
   - Use structured data for aggregation
   - Implement health checks

**Security Considerations**

- Restrict Insights dashboard access
- Use strong authentication
- Audit who accesses production logs
- Encrypt log storage and transmission
- Comply with data retention regulations (GDPR, etc.)

