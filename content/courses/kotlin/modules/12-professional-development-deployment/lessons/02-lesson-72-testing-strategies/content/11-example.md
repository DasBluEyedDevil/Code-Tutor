---
type: "EXAMPLE"
title: "Code Coverage"
---


### Measuring Coverage with JaCoCo


**Run Coverage**:

**View Report**:
Open `build/reports/jacoco/test/html/index.html`

### Coverage Metrics

**What's Good Coverage?**:
- **80%+**: Excellent
- **60-80%**: Good
- **40-60%**: Needs improvement
- **<40%**: Risky

**Important**: 100% coverage ≠ bug-free code. Focus on testing critical paths.

---



```bash
./gradlew test jacocoTestReport
```
