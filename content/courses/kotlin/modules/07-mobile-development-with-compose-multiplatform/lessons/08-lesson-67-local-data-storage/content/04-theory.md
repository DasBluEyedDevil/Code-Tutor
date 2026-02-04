---
type: "THEORY"
title: "Room Database (Android-Only)"
---

> **Android-only:** Room is an Android library. It works in `androidMain` but NOT in `commonMain`. For cross-platform persistence, use **SQLDelight** (covered in Module 08: Persistence with SQLDelight).

### Entity (Table)


### Type Converters


### DAO (Data Access Object)


### Database


---



```kotlin
// androidMain -- Room is Android only
import android.content.Context
import androidx.room.Database
import androidx.room.Room
import androidx.room.RoomDatabase
import androidx.room.TypeConverters

@Database(
    entities = [Task::class],
    version = 1,
    exportSchema = false
)
@TypeConverters(Converters::class)
abstract class AppDatabase : RoomDatabase() {
    abstract fun taskDao(): TaskDao

    companion object {
        @Volatile
        private var INSTANCE: AppDatabase? = null

        fun getDatabase(context: Context): AppDatabase {
            return INSTANCE ?: synchronized(this) {
                val instance = Room.databaseBuilder(
                    context.applicationContext,
                    AppDatabase::class.java,
                    "app_database"
                )
                    .fallbackToDestructiveMigration()  // For development only!
                    .build()

                INSTANCE = instance
                instance
            }
        }
    }
}
```
