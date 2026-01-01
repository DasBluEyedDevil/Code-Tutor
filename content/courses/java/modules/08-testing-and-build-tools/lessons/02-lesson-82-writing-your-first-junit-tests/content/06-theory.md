---
type: "THEORY"
title: "⚠️ Don't Test Everything - Test What Matters"
---

❌ Don't test:
- Getters/setters (trivial code)
- External libraries (already tested)
- UI code (use different techniques)

✓ DO test:
- Business logic
- Complex calculations
- Edge cases (empty lists, null values, boundaries)
- Error handling

AIM FOR: 70-80% code coverage
NOT: 100% (diminishing returns, wastes time)