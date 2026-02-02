---
type: "THEORY"
title: "Performance Budgets and Alerts"
---


**What is a Performance Budget?**

A performance budget sets limits on key metrics. When a metric exceeds its budget, you get alerted and can investigate before users are affected.

**Setting Performance Budgets**

| Metric | Budget | Rationale |
|--------|--------|------------|
| App cold start | <3 seconds | Users expect quick launch |
| Screen transition | <300ms | Feels instant |
| API response (P95) | <500ms | 95% should be fast |
| Slow frame rate | <5% | Smooth experience |
| Frozen frames | 0% | Never acceptable |

**Firebase Performance Alerts**

In Firebase Console, set up alerts for:

1. **Threshold Alerts** - Notify when metric exceeds value
2. **Regression Alerts** - Notify when performance degrades vs. baseline
3. **Anomaly Alerts** - Notify on unusual patterns

**Implementing Local Performance Checks**

You can also check budgets in your app:

```dart
class PerformanceBudget {
  static const maxColdStartMs = 3000;
  static const maxApiResponseMs = 500;
  static const maxSlowFramePercent = 5.0;
  
  static bool isColdStartWithinBudget(int ms) => ms <= maxColdStartMs;
  static bool isApiResponseWithinBudget(int ms) => ms <= maxApiResponseMs;
  static bool isFrameRateWithinBudget(double slowPercent) => 
      slowPercent <= maxSlowFramePercent;
}
```

**Continuous Monitoring Strategy**

1. **Baseline** - Establish current performance metrics
2. **Budget** - Set acceptable thresholds
3. **Alert** - Get notified when budgets are exceeded
4. **Investigate** - Drill down to root cause
5. **Fix** - Implement optimization
6. **Verify** - Confirm improvement in production
7. **Repeat** - Continuously monitor and improve

**Acting on Performance Data**

When you see a problem:

1. Check which app version introduced the regression
2. Filter by device/OS to find affected segments
3. Look at custom attributes for patterns
4. Review recent code changes in that area
5. Add more specific traces if needed
6. Fix, deploy, and verify improvement

