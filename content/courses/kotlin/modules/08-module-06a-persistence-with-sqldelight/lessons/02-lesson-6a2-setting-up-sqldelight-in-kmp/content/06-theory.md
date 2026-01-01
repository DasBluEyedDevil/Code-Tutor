---
type: "THEORY"
title: "Project Structure"
---

### SQL File Location

SQLDelight expects `.sq` files in a specific location:

```
shared/
├── src/
│   ├── commonMain/
│   │   ├── kotlin/
│   │   │   └── com/example/app/
│   │   │       └── data/
│   │   │           └── Repository.kt
│   │   └── sqldelight/
│   │       └── com/example/app/data/
│   │           ├── Note.sq
│   │           └── Category.sq
│   ├── androidMain/
│   │   └── kotlin/
│   │       └── Database.android.kt
│   └── iosMain/
│       └── kotlin/
│           └── Database.ios.kt
└── build.gradle.kts
```

### Important Notes

1. **sqldelight directory**: Must be at same level as `kotlin` directory
2. **Package path**: Should match the `packageName` in your gradle config
3. **File extension**: Use `.sq` for schema and queries, `.sqm` for migrations