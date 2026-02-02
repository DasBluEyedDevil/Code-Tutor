---
type: "THEORY"
title: "Staged Rollouts"
---


**What is a Staged Rollout?**

A staged rollout releases your app update to a percentage of users, allowing you to catch issues before they affect everyone.

**How It Works:**

1. Upload new version to production track
2. Set initial rollout percentage (e.g., 5%)
3. Google randomly selects that percentage of users to receive update
4. Monitor metrics for 24-48 hours
5. Increase percentage or halt if issues found
6. Continue until reaching 100%

**Key Metrics to Monitor:**

| Metric | Healthy Threshold | Action if Exceeded |
|--------|------------------|--------------------|
| Crash-free users | > 99% | Halt rollout immediately |
| ANR rate | < 0.5% | Investigate and consider halting |
| User rating | Stable or improving | Monitor feedback |
| Uninstalls | Below baseline | Check for UX regression |

**Benefits of Staged Rollouts:**

1. **Risk Mitigation:** Issues affect fewer users
2. **Faster Detection:** Easier to spot problems in crash reports
3. **Easy Rollback:** Halt distribution before damage spreads
4. **A/B Comparison:** Compare old vs new version metrics

**Staged Rollout Limitations:**

- Only for updates, not new app launches
- Users who got update cannot downgrade
- Percentage is approximate (not exact)
- Cannot target specific users or regions

