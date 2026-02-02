---
type: "KEY_POINT"
title: "Golden Test Best Practices"
---


**1. Consistent Environment:**
- Use fixed screen size with `tester.view.physicalSize`
- Avoid dynamic content (dates, random IDs)
- Mock images and network data

**2. Organization:**
```
test/
  goldens/
    product_card.png
    checkout_screen.png
  widgets/
    product_card_test.dart
```

**3. CI/CD Considerations:**
- Golden files must be committed to git
- May differ across platforms (use tags for platform-specific goldens)
- Review golden diffs carefully in PRs

