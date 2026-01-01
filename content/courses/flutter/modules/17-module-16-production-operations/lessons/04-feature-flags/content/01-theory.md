---
type: "THEORY"
title: "What Are Feature Flags?"
---


**The Power of Controlled Releases**

Feature flags (also called feature toggles) let you control which features are enabled in your app without deploying new code. This simple concept transforms how you ship software.

**Key Use Cases**

1. **Gradual Rollouts** - Release features to 1% of users, then 10%, then 100%
2. **A/B Testing** - Show different variants to measure which performs better
3. **Kill Switches** - Instantly disable problematic features in production
4. **User Targeting** - Enable features for specific user segments
5. **Beta Programs** - Let early adopters try new features first

**Why Feature Flags Matter**

| Without Feature Flags | With Feature Flags |
|----------------------|--------------------|
| Ship and pray | Ship with confidence |
| Rollback = new release | Instant disable |
| All-or-nothing releases | Gradual rollouts |
| Guess what users want | Measure with A/B tests |
| Wait for app store review | Instant activation |

**The Feature Flag Lifecycle**

```
Create Flag → Develop Feature → Deploy (flag off) → Enable Gradually → Full Release → Remove Flag
```

**Types of Feature Flags**

1. **Release Flags** - Short-lived, control new feature rollout
2. **Experiment Flags** - Temporary, for A/B testing
3. **Ops Flags** - Kill switches for operational control
4. **Permission Flags** - Enable premium features for paid users

**Best Practices**

- Always have a default value (usually disabled)
- Clean up flags after full rollout
- Log which flag values users receive
- Test both enabled and disabled states
- Document what each flag controls

