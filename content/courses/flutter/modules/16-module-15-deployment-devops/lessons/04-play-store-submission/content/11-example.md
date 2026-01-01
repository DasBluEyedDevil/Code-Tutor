---
type: "EXAMPLE"
title: "Managing Staged Rollouts"
---


Handling different rollout scenarios:



```text
## Scenario 1: Successful Rollout

Starting rollout:
1. Release -> Production -> Create new release
2. Upload signed .aab file
3. Set "Rollout percentage" to 5%
4. Click "Save" then "Review release" then "Start rollout"

Increasing rollout:
1. Production -> Releases -> In progress rollout
2. Click "Increase rollout"
3. Set new percentage (e.g., 20%)
4. Click "Update"

Completing rollout:
1. Set rollout to 100%
2. Release is now fully available

## Scenario 2: Problem Detected

Symptoms: Crash rate spiked from 0.5% to 3%

Halting rollout:
1. Production -> Releases -> In progress rollout
2. Click "Halt rollout"
3. Confirm halt
4. No new users receive the update
5. Users who already updated keep the version

Post-halt actions:
1. Analyze crash reports in Android Vitals
2. Identify root cause
3. Create fix and upload new version
4. Start new staged rollout from 5%

## Scenario 3: Critical Bug - Full Rollback

If you need to get users back to old version:
1. Cannot force users to downgrade
2. Instead: Upload NEWER version with fix
3. Set rollout to 100% (urgent fix)
4. Or: Use staged rollout even for fix

## Monitoring Dashboard Locations:
- Crash reports: Quality -> Android Vitals -> Crashes
- ANR reports: Quality -> Android Vitals -> ANRs  
- Ratings: Quality -> Ratings and reviews
- Install metrics: Statistics -> Install
```
