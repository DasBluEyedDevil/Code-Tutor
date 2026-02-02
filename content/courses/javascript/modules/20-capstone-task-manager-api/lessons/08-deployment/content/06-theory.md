---
type: "THEORY"
title: "Monitoring & Logging in Production"
---

What to monitor and log in production:

**Error Tracking:**
- Uncaught exceptions
- Database connection errors
- API errors (500, 503, etc)
- Authentication failures

**Performance Metrics:**
- Response time per endpoint
- Database query time
- Request count and throughput
- Memory usage
- CPU usage

**Application Health:**
- Database connectivity
- Dependent service health (Redis, external APIs)
- Cache hit/miss rates
- Queue depth and processing time

**User Activity (Privacy-Conscious):**
- Active users count
- API endpoint usage patterns
- Error rates by endpoint
- Task creation/completion rates

**Recommended Tools:**
- **Sentry** - Error tracking with context and sourcemaps
- **DataDog** - Full observability (metrics, logs, APM)
- **LogRocket** - Session replay and console logs
- **New Relic** - APM and infrastructure monitoring
- **Grafana** - Custom dashboards and alerting

**Health Check Endpoint:**
Implement a `/health` endpoint that the hosting platform monitors. If it fails, the platform can restart your app.

**Structured Logging:**
Log as JSON for easy parsing:
```json
{
  "timestamp": "2024-01-15T10:30:45.123Z",
  "level": "error",
  "message": "Database connection failed",
  "userId": "user_123",
  "endpoint": "POST /api/tasks",
  "duration_ms": 5000,
  "error": "ECONNREFUSED"
}
```