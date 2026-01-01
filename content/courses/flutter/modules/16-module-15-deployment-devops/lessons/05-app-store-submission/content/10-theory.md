---
type: "THEORY"
title: "Phased Releases"
---


**Automatic Phased Release**

Apple's phased release gradually delivers your update to users over 7 days:

**Rollout Schedule:**

| Day | Percentage of Users |
|-----|--------------------|
| 1 | 1% |
| 2 | 2% |
| 3 | 5% |
| 4 | 10% |
| 5 | 20% |
| 6 | 50% |
| 7 | 100% |

**Key Differences from Play Store:**

- Schedule is fixed (cannot set custom percentages)
- Automatic progression (doesn't require manual increases)
- Can pause and resume
- Immediate release option available at any time

**Enabling Phased Release:**

1. Submit your app for review
2. In Version Release section, select:
   - "Automatically release this version"
   - Check "Release update over 7 days using phased release"
3. After approval, rollout begins automatically

**Pausing and Resuming:**

From App Store Connect:
1. Go to your app version
2. Click "Pause Phased Release"
3. Users who already have update keep it
4. New users can still manually update from App Store
5. Click "Resume Phased Release" to continue schedule

**When to Pause:**

- Crash reports spike unexpectedly
- Critical bug reported by early users
- Negative reviews mention same issue
- Need time to prepare a fix

