---
type: "THEORY"
title: "Analyzing Crashes in the Dashboard"
---


**Firebase Crashlytics Dashboard**

1. **Crash-Free Users** - Your primary health metric. Aim for 99.5%+ crash-free rate.

2. **Issues List** - Crashes grouped by stack trace similarity:
   - Title: The exception type and message
   - Users/Events: How many people are affected
   - Trend: Is it getting better or worse?

3. **Issue Details** - Click an issue to see:
   - Full stack trace with source mapping
   - Device breakdown (which phones crash most)
   - OS version breakdown
   - App version breakdown
   - Breadcrumbs and logs
   - Custom keys you set

**Sentry Dashboard**

1. **Issues** - Similar to Crashlytics but with:
   - First/Last seen timestamps
   - Assignee tracking
   - Status management (resolved, ignored)

2. **Discover** - Query your error data:
   - Filter by tag, user, environment
   - Build custom dashboards
   - Set up alerts

3. **Performance** - Transaction tracing:
   - See slow API calls
   - Identify performance regressions

**Prioritization Strategy**

| Priority | Criteria |
|----------|----------|
| P0 (Critical) | >1% users affected, app unusable |
| P1 (High) | >0.1% users affected, key feature broken |
| P2 (Medium) | <0.1% users, workaround exists |
| P3 (Low) | Edge cases, rare devices |

