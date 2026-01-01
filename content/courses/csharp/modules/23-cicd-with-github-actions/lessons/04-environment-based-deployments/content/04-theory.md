---
type: "THEORY"
title: "Deployment Strategies"
---

## Rolling Deployment

Rolling deployment gradually replaces old instances with new ones. If you have 10 application instances, a rolling deployment might update 2 at a time, waiting for each batch to become healthy before proceeding. This maintains capacity throughout the deployment but means both old and new versions run simultaneously during the rollout.

**Advantages:**
- Zero downtime
- Gradual rollout limits blast radius
- Easy rollback by stopping the rollout

**Disadvantages:**
- Version mixing during deployment (old and new instances handle requests)
- Database schema changes must be backward compatible
- Longer deployment time compared to blue-green

**Best for:** Stateless applications with backward-compatible changes, where some version mixing is acceptable.

## Blue-Green Deployment

Blue-green deployment maintains two identical production environments. Blue runs the current version, green runs the new version. Traffic switches atomically from blue to green once the new version is verified. If problems arise, traffic switches back instantly.

**Advantages:**
- Instant rollback (just switch traffic back)
- No version mixing during deployment
- Easy to verify new version before serving traffic

**Disadvantages:**
- Requires double infrastructure (or ability to provision quickly)
- Database migrations must work with both versions
- Stateful applications may lose in-flight requests during switch

**Best for:** Critical applications where instant rollback is essential and version mixing is unacceptable.

## Canary Deployment

Canary deployment routes a small percentage of traffic to the new version while monitoring for issues. If metrics look good, traffic gradually shifts until the new version handles 100%. Named after the canary in a coal mine, this strategy detects problems before they affect most users.

**Advantages:**
- Minimal user impact if problems occur
- Real production traffic validates the deployment
- Metrics-based decisions about proceeding

**Disadvantages:**
- Complex traffic routing configuration
- Requires robust monitoring and alerting
- Slower full rollout than blue-green

**Best for:** High-traffic applications where you want real-world validation before full rollout.

## Feature Flags

Feature flags decouple deployment from release. Code ships to production but remains inactive until the flag is enabled. This enables:

- Deploy anytime, release when ready
- Gradual feature rollout to user segments
- Instant disable without deployment
- A/B testing and experimentation

```csharp
if (await _featureManager.IsEnabledAsync("NewCheckoutFlow"))
{
    return View("CheckoutV2");
}
return View("Checkout");
```

Feature flags complement deployment strategies by controlling what features are active regardless of what code is deployed.

## Choosing a Strategy

| Factor | Rolling | Blue-Green | Canary |
|--------|---------|------------|--------|
| Rollback speed | Slow (must re-deploy) | Instant | Fast (shift traffic) |
| Infrastructure cost | Low | High (2x) | Medium |
| Version mixing | Yes | No | Controlled |
| Implementation complexity | Low | Medium | High |
| Database migration support | Requires compatibility | Requires compatibility | Requires compatibility |

Most teams use rolling deployments for non-critical services and blue-green or canary for production-critical systems. Database migrations always require the most careful planning regardless of deployment strategy.