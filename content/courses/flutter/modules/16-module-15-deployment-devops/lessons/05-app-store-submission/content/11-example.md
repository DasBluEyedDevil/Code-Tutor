---
type: "EXAMPLE"
title: "Monitoring Your Release"
---


Post-release monitoring and response:



```text
## Key Metrics to Track

### App Store Connect Analytics
Location: App Analytics -> Metrics

1. Crash Rate:
   - Check Crashes -> Crash Rate
   - Compare to previous versions
   - Target: <1% crash rate

2. Retention:
   - Day 1, Day 7, Day 30 retention
   - Compare to previous versions
   - Drops may indicate issues

3. App Store Performance:
   - Impressions
   - Product Page Views
   - Conversion Rate (views to downloads)
   - Source of downloads

### Xcode Organizer
Location: Window -> Organizer -> Crashes

- Real-time crash reports
- Symbolicated stack traces
- Filter by version
- Group by root cause

## Response Playbook

### Scenario: High Crash Rate Detected

Actions:
1. Pause phased release immediately
2. Analyze crash reports in Organizer
3. Identify root cause and affected devices
4. Prepare hotfix build
5. Test thoroughly on affected devices
6. Submit with expedited review request
7. When approved, release immediately (skip phased)

### Scenario: Negative Review Pattern

Actions:
1. Categorize feedback themes
2. Reply to reviews professionally
3. Prioritize fixes based on frequency
4. Consider pausing if issue is critical
5. Communicate fix timeline in responses

## Phased Release Controls

In App Store Connect:

Pause: App -> Version -> Phased Release -> Pause
Resume: Same location -> Resume Phased Release  
Release to All: Same location -> Release Update to All Users

## Health Monitoring Checklist (Daily during rollout)

[ ] Check crash rate vs previous version
[ ] Read new 1-star and 2-star reviews
[ ] Monitor support inbox
[ ] Check TestFlight for regression reports
[ ] Verify backend services are handling load
[ ] Compare conversion rate to baseline
```
