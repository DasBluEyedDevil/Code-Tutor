---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Compose Multiplatform UI testing uses Compose Testing library** with semantics-based assertions. Find elements by text, content description, or test tags: `onNodeWithText("Login").performClick()`.

**UI tests validate composable behavior without platform dependencies**—test that buttons appear, clicks trigger callbacks, state updates re-render correctly. These tests run in a fake Compose environment.

**Prefer testing ViewModels over UI when possible**—ViewModels are faster to test and cover business logic. Save UI tests for interaction flows that require composable lifecycle and state management.
