---
type: "THEORY"
title: "Kill Switches for Emergency Control"
---


**What is a Kill Switch?**

A kill switch is a feature flag that can instantly disable a feature in production without deploying new code. It's your emergency brake for problematic features.

**When You Need Kill Switches**

1. **Critical bugs discovered** - Feature causes crashes or data loss
2. **Security vulnerabilities** - Exploit discovered in new feature
3. **Third-party failures** - Payment provider or API goes down
4. **Performance issues** - Feature overloads servers
5. **Business reasons** - Legal or compliance issues

**Kill Switch Best Practices**

1. **Default to safe** - Kill switch should default to feature OFF
2. **Test the switch** - Verify it actually disables the feature
3. **Fast activation** - Configure low fetch interval for emergencies
4. **Graceful degradation** - Show helpful message when disabled
5. **Alert on activation** - Know when someone flips the switch

**Kill Switch Architecture**

```
Firebase Console
      |
      v
Feature Flag Service
      |
      v
Kill Switch Check
      |
   /     \
Enabled   Disabled
  |          |
  v          v
Feature   Fallback UI
```

**Fallback Strategies**

1. **Hide the feature** - Remove button/menu item entirely
2. **Show maintenance message** - "Feature temporarily unavailable"
3. **Redirect** - Send user to alternative flow
4. **Graceful degradation** - Show cached/static data
5. **Queue for later** - Store action, execute when restored

**Kill Switch Response Time**

To minimize time between flipping switch and users seeing change:

1. Enable real-time Remote Config updates
2. Reduce minimum fetch interval during incidents
3. Consider checking critical flags on each screen load
4. Have on-call process for emergency flag changes

