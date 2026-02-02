---
type: "THEORY"
title: "Firebase Analytics Overview"
---


**Why Firebase Analytics?**

Firebase Analytics (part of Google Analytics for Firebase) is the most popular choice for Flutter apps:

- **Free** - No usage limits, unlike many competitors
- **Automatic Events** - Many events tracked without any code
- **Deep Integration** - Works seamlessly with other Firebase products
- **Audience Segmentation** - Create user groups based on behavior
- **BigQuery Export** - Free raw data export for advanced analysis

**Automatic Events**

Firebase tracks these without any code:

| Event | Description |
|-------|-------------|
| first_open | First time user opens the app |
| session_start | User begins a new session |
| app_update | User updates to a new version |
| os_update | User updates their operating system |
| app_remove | User uninstalls the app (Android only) |
| in_app_purchase | User completes a purchase |

**Enhanced Measurement**

With minimal setup, Firebase can also track:

- Screen views (with NavigatorObserver)
- User engagement time
- App exceptions (crashes)
- Notification interactions

**Dashboard Metrics**

1. **Active Users** - Daily, weekly, monthly active users
2. **Engagement** - Sessions, screen views, session duration
3. **Retention** - Day 1, Day 7, Day 30 retention cohorts
4. **Revenue** - If using in-app purchases or ads

