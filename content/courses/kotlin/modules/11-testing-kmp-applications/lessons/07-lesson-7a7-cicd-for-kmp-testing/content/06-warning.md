---
type: "WARNING"
title: "Common CI Pitfalls"
---

### Pitfall 1: Running all tests on every commit

```yaml
# ❌ Expensive - runs iOS tests on every commit
on: push
jobs:
  ios-tests:
    runs-on: macos-14
    steps:
      - run: ./gradlew :shared:iosTest

# ✅ Smart - JVM tests on commits, iOS only on PRs
on:
  push:
    branches: [main]
  pull_request:
jobs:
  jvm-tests:     # Every commit
    runs-on: ubuntu-latest
    ...
  ios-tests:     # PRs only
    if: github.event_name == 'pull_request'
    runs-on: macos-14
    ...
```

### Pitfall 2: No test caching

```yaml
# ❌ Slow - downloads dependencies every time
- run: ./gradlew test

# ✅ Fast - uses Gradle caching
- uses: gradle/gradle-build-action@v3
  with:
    cache-read-only: ${{ github.ref != 'refs/heads/main' }}
- run: ./gradlew test
```

### Pitfall 3: Flaky tests not handled

```kotlin
// ❌ Flaky test blocks CI
@Test
fun `test that sometimes fails`() {
    // Depends on timing/network/order
}

// ✅ Retry flaky tests (temporary fix while you fix the test)
@RepeatedTest(3)
fun `test that sometimes fails`() {
    // Will pass if 1 of 3 attempts succeeds
}
```

### Pitfall 4: No test result artifacts

```yaml
# ❌ Can't debug failures
- run: ./gradlew test

# ✅ Upload results even on failure
- run: ./gradlew test
- uses: actions/upload-artifact@v4
  if: always()  # Key: upload even on failure
  with:
    name: test-results
    path: '**/build/reports/tests/'
```