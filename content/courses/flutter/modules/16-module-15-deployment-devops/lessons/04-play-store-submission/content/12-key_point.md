---
type: "KEY_POINT"
title: "Rollout Decision Matrix"
---


**When to Increase Rollout:**

- Crash-free users rate stable or improving
- No spike in 1-star reviews mentioning bugs
- ANR rate within normal bounds
- No reports of critical functionality broken
- At least 24-48 hours at current percentage

**When to Halt Rollout:**

- Crash rate increases by more than 1%
- Multiple reports of same critical bug
- Data loss or security issues reported
- Payment/subscription issues
- Core functionality broken

**When to Skip Staged Rollout:**

- Critical security patch (go straight to 100%)
- Fixing a bug from halted rollout (monitor closely)
- Very minor changes (text fixes, asset updates)
- Legally required changes with deadline

**Staged Rollout Timeline:**

| Percentage | Minimum Time | Purpose |
|------------|--------------|----------|
| 5% | 24 hours | Catch critical crashes |
| 10% | 24 hours | Validate on diverse devices |
| 25% | 24 hours | Confirm stability at scale |
| 50% | 24 hours | Build confidence |
| 100% | N/A | Full release |

