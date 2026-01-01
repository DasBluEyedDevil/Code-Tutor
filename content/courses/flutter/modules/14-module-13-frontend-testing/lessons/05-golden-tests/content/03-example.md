---
type: "EXAMPLE"
title: "Running Golden Tests"
---




```bash
# Generate/update golden files
flutter test --update-goldens

# Run tests and compare against goldens
flutter test

# Run specific golden test
flutter test test/widgets/product_card_test.dart --update-goldens
```
