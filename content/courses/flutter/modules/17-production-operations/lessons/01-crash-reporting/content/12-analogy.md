---
type: "ANALOGY"
title: "The Black Box Flight Recorder"
---

Crash reporting is your app's black box flight recorder. When an airplane crashes, investigators recover the black box to understand exactly what happened: altitude, speed, engine status, and pilot actions in the moments before the crash. Without it, they are guessing.

Your app's crash reporter (Sentry, Firebase Crashlytics) works the same way. When your app crashes, it captures the stack trace (what code was running), device information (which phone, OS version, available memory), user context (what screen they were on), and breadcrumbs (the sequence of actions that led to the crash). This turns "the app crashed for some users" into "the app crashes on Samsung Galaxy S21 running Android 14 when the user taps the share button with an empty post."

**The difference between debugging with and without crash reporting is the difference between investigating a plane crash with and without the black box.** One gives you answers in minutes; the other takes days of guesswork.
