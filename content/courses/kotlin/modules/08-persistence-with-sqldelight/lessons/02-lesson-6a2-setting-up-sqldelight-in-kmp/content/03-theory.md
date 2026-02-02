---
type: "THEORY"
title: "Database Configuration"
---

### Step 2: Configure the Database

**In shared/build.gradle.kts:**
```kotlin
sqldelight {
    databases {
        create("AppDatabase") {
            packageName.set("com.example.app.data")
            
            // Generate suspend functions (recommended)
            generateAsync.set(true)
        }
    }
}
```

### Configuration Options

| Option | Description |
|--------|-------------|
| `packageName` | Package for generated code |
| `generateAsync` | Generate suspend functions |
| `schemaOutputDirectory` | Where to save schema files |
| `verifyMigrations` | Validate migration files |
| `deriveSchemaFromMigrations` | Build schema from migrations |

### Advanced Configuration
```kotlin
sqldelight {
    databases {
        create("AppDatabase") {
            packageName.set("com.example.app.data")
            generateAsync.set(true)
            
            // For schema export (useful for inspection)
            schemaOutputDirectory.set(file("src/commonMain/sqldelight/schema"))
            
            // Verify migrations match schema
            verifyMigrations.set(true)
        }
    }
}
```