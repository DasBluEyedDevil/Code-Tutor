---
type: "EXAMPLE"
title: "Recommended Release Strategy"
---


A phased approach to releasing your Flutter app:



```text
## Phase 1: Internal Testing (Week 1-2)

Upload to internal testing track:
1. Test on various device configurations
2. Verify all features work on real devices
3. Check analytics and crash reporting integration
4. Validate in-app purchases with test cards
5. Fix critical issues quickly (no review delay)

Testers: Development team, QA, stakeholders

## Phase 2: Closed Testing (Week 3-4)

Promote to closed testing track:
1. Invite beta testers from signup list
2. Create feedback mechanisms (in-app form, email)
3. Monitor crash-free users percentage
4. Track key metrics: retention, engagement
5. Gather qualitative feedback on UX

Testers: 100-1,000 external users

## Phase 3: Open Testing (Optional - Week 5-6)

If needed for larger scale testing:
1. Open to public with "Early Access" label
2. Iterate based on broader feedback
3. Monitor reviews (visible but labeled as early access)
4. Build early user base before launch

## Phase 4: Production (Week 6+)

Full release:
1. Start with 5-10% staged rollout
2. Monitor crash rate and ANR rate
3. Watch for spike in negative reviews
4. Gradually increase to 100% over 3-7 days
5. If issues found: Halt rollout, fix, resume

## Rollout Timeline Example:
- Day 1: 5% of users
- Day 2: 10% if metrics stable
- Day 3: 25% if metrics stable
- Day 5: 50% if metrics stable
- Day 7: 100% full release
```
