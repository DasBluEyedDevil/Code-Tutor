---
type: "THEORY"
title: "Measuring Build Performance"
---


### Key Metrics

| Metric | Good | Needs Work |
|--------|------|------------|
| Configuration | < 5s | > 10s |
| Clean build | < 2min | > 5min |
| Incremental | < 20s | > 60s |
| Cache hit rate | > 80% | < 50% |

### Build Scan Reports

```bash
# Create detailed report
./gradlew build --scan

# Report shows:
# - Task execution timeline
# - Cache hit/miss rates
# - Configuration time
# - Dependency download time
# - Memory usage
```

---

