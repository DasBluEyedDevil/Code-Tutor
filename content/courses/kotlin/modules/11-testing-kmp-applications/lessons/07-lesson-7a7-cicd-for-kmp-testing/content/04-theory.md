---
type: "THEORY"
title: "Test Reporting and Coverage"
---

### Generating Coverage Reports

```kotlin
// build.gradle.kts
plugins {
    id("org.jetbrains.kotlinx.kover") version "0.7.6"
}

koverReport {
    defaults {
        xml { onCheck = true }
        html { onCheck = true }
    }
}
```

### Uploading to Codecov

```yaml
# In your workflow
- name: Generate coverage report
  run: ./gradlew koverXmlReport

- name: Upload coverage to Codecov
  uses: codecov/codecov-action@v4
  with:
    token: ${{ secrets.CODECOV_TOKEN }}
    files: '**/build/reports/kover/report.xml'
    fail_ci_if_error: true
```

### PR Comments with Test Results

```yaml
- name: Publish Test Results
  uses: EnricoMi/publish-unit-test-result-action@v2
  if: always()
  with:
    files: '**/build/test-results/**/*.xml'
    comment_mode: 'always'
```