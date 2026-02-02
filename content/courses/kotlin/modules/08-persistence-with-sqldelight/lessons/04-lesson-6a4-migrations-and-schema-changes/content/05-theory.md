---
type: "THEORY"
title: "Enabling Migration Verification"
---

### Gradle Configuration

```kotlin
sqldelight {
    databases {
        create("AppDatabase") {
            packageName.set("com.example.app.db")
            
            // Verify migrations produce the same schema
            verifyMigrations.set(true)
            
            // Generate schema files for inspection
            schemaOutputDirectory.set(file("src/commonMain/sqldelight/schema"))
        }
    }
}
```

### What Verification Does

1. Creates database using only `.sq` files (fresh install)
2. Creates database using migrations (upgrade path)
3. Compares resulting schemas
4. Fails build if they don't match

### Running Verification

```bash
./gradlew verifySqlDelightMigration
```

If verification fails:
```
> Migration verification failed:
> Table 'Note' has different columns:
>   Expected: [id, title, content, color]
>   Actual:   [id, title, content]
```