---
type: "EXAMPLE"
title: "Release Testing Checklist"
---


Create a testing checklist before store submission:



```markdown
# Release Testing Checklist

## Build Verification
- [ ] Build completes without errors
- [ ] APK/AAB size is reasonable (compare to previous release)
- [ ] Correct version code and version name
- [ ] Correct application ID (especially with flavors)

## Installation Testing
- [ ] Installs successfully on physical device
- [ ] App icon displays correctly
- [ ] App name displays correctly
- [ ] No debug banner visible

## Functional Testing
- [ ] App launches without crash
- [ ] Login/authentication works
- [ ] API calls succeed (correct production URLs)
- [ ] Push notifications work
- [ ] In-app purchases work (if applicable)
- [ ] Deep links work
- [ ] All major features functional

## Performance Testing
- [ ] App startup is fast
- [ ] Scrolling is smooth
- [ ] No memory leaks during extended use
- [ ] Network requests are responsive

## Edge Cases
- [ ] Works offline (if applicable)
- [ ] Handles network errors gracefully
- [ ] Works after process death/restart
- [ ] Orientation changes work correctly

## Analytics & Monitoring
- [ ] Analytics events fire correctly
- [ ] Crash reporting is connected
- [ ] Remote config loads (if using)
```
