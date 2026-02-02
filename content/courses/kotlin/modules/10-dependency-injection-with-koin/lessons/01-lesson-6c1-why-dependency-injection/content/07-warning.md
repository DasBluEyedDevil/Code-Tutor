---
type: "WARNING"
title: "Common DI Misconceptions"
---

### ❌ "DI is only for testing"
Testing is a benefit, but DI also improves modularity, reusability, and team collaboration.

### ❌ "DI adds complexity"
DI *manages* existing complexity. Your app already has dependencies - DI makes them explicit and organized.

### ❌ "Just use singletons"
Singletons create hidden global state, make testing hard, and can cause memory leaks. DI provides controlled singleton behavior when needed.

### ❌ "I'll add DI later"
Retrofitting DI is painful. Start with it from the beginning - even for small projects.