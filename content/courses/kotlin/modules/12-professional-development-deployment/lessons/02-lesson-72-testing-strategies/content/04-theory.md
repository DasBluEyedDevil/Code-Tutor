---
type: "THEORY"
title: "Testing Pyramid"
---


### The Right Balance


**Unit Tests (70%)**:
- Test individual functions/classes in isolation
- Fast (milliseconds)
- Easy to write and maintain
- Run on every code change

**Integration Tests (20%)**:
- Test multiple components together
- Medium speed (seconds)
- Test real interactions

**E2E Tests (10%)**:
- Test entire user flows
- Slow (minutes)
- Fragile (UI changes break tests)
- Only for critical paths

---



```kotlin
       /\
      /  \     E2E Tests (UI)
     /    \    10% - Slow, expensive, brittle
    /------\
   /        \  Integration Tests
  /          \ 20% - Medium speed, test components together
 /------------\
/              \ Unit Tests
----------------  70% - Fast, cheap, test individual functions
```
