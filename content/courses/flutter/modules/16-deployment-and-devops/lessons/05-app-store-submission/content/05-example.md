---
type: "EXAMPLE"
title: "Setting Up TestFlight"
---


Configuring TestFlight for your Flutter app:



```text
## Internal Testing Setup

1. App Store Connect -> Your App -> TestFlight
2. Click "Internal Testing" in sidebar
3. Click "+" to create group (or use default)
4. Name your group (e.g., "Core Team", "QA")
5. Add testers from your ASC team members
6. Select build to test
7. Testers receive email with TestFlight invite

## External Testing Setup

1. TestFlight -> External Testing
2. Click "+" to create new group
3. Configure group:
   - Group name: "Beta Testers"
   - Enable/disable public link
   - Set tester limit if using public link
4. Add build to group (triggers review)
5. Fill in "What to Test" description
6. Wait for beta review (24-48 hours)
7. Once approved, invite testers:
   - Enter email addresses manually
   - Import CSV with emails
   - Share public TestFlight link

## Tester Information Collection

Optionally require testers to provide:
- Email address (always required)
- First and last name
- Device type and iOS version (automatic)

## Build Metadata

For each build, you can set:
- What to Test: Instructions for testers
- Test Information: Login credentials, test data
- Review Notes: For beta review team

## Public Link Distribution

Public TestFlight link format:
https://testflight.apple.com/join/XXXXXXXX

Benefits:
- No need to collect emails upfront
- Testers self-register
- Set maximum tester limit (1-10,000)

Considerations:
- Anyone with link can join
- Harder to control who's testing
- Good for open betas, bad for NDA products
```
