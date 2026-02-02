---
type: "EXAMPLE"
title: "Running Integration Tests"
---




```bash
# Run on connected device/emulator
flutter test integration_test/app_test.dart

# Run on specific device
flutter test integration_test/app_test.dart -d chrome
flutter test integration_test/app_test.dart -d emulator-5554

# Run with performance profiling
flutter drive \
  --driver=test_driver/integration_test.dart \
  --target=integration_test/app_test.dart \
  --profile
```
