---
type: "KEY_POINT"
title: "Automatic vs Manual Signing"
---


**Automatic Signing Pros:**
- Zero configuration for simple apps
- Handles certificate renewal automatically
- Registers new devices automatically
- Great for getting started quickly

**Automatic Signing Cons:**
- Less control over which profiles are used
- Can cause issues in CI/CD environments
- May regenerate profiles unexpectedly
- Harder to share exact signing setup with team

**Manual Signing Pros:**
- Precise control over certificates and profiles
- Consistent builds across machines
- Required for complex CI/CD setups
- Better for team environments

**Manual Signing Cons:**
- More initial setup work
- Must manually update expired profiles
- Must manually register new devices
- Need to understand iOS signing concepts

**Recommended Approach:**
- Use automatic signing for Debug builds (development)
- Use manual signing for Release builds (distribution)
- This gives convenience during development and control for releases

