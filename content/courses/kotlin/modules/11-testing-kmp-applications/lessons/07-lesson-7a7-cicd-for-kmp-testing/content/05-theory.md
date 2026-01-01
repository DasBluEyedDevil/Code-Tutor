---
type: "THEORY"
title: "Parallelizing Tests"
---

### Gradle Configuration

```kotlin
// gradle.properties
org.gradle.parallel=true
org.gradle.caching=true
kotlin.incremental=true

// Run tests in parallel within JVM
tasks.withType<Test> {
    maxParallelForks = Runtime.getRuntime().availableProcessors()
}
```

### Split Tests Across CI Jobs

```yaml
jobs:
  test-matrix:
    runs-on: ubuntu-latest
    strategy:
      matrix:
        shard: [1, 2, 3, 4]
    steps:
      - uses: actions/checkout@v4
      
      - name: Run tests (shard ${{ matrix.shard }})
        run: |
          ./gradlew :shared:jvmTest \
            --tests "*Test" \
            -Dtest.shard=${{ matrix.shard }} \
            -Dtest.totalShards=4
```

### Shard Implementation

```kotlin
// Custom test filter for sharding
tasks.withType<Test> {
    val shard = System.getProperty("test.shard")?.toIntOrNull()
    val totalShards = System.getProperty("test.totalShards")?.toIntOrNull()
    
    if (shard != null && totalShards != null) {
        filter {
            includeTest("*") { desc ->
                desc.hashCode().mod(totalShards) == shard - 1
            }
        }
    }
}
```