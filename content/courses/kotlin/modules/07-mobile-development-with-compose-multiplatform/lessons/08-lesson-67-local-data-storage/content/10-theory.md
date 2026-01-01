---
type: "THEORY"
title: "Migration"
---



---



```kotlin
val MIGRATION_1_2 = object : Migration(1, 2) {
    override fun migrate(database: SupportSQLiteDatabase) {
        database.execSQL("ALTER TABLE tasks ADD COLUMN categoryId INTEGER")
    }
}

val database = Room.databaseBuilder(
    context,
    AppDatabase::class.java,
    "app_database"
)
    .addMigrations(MIGRATION_1_2)
    .build()
```
