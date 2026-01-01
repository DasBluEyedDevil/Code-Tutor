---
type: "THEORY"
title: "Relationships"
---


### One-to-Many


---



```kotlin
@Entity(tableName = "categories")
data class Category(
    @PrimaryKey(autoGenerate = true)
    val id: Int = 0,
    val name: String,
    val color: String
)

@Entity(
    tableName = "tasks_with_category",
    foreignKeys = [
        ForeignKey(
            entity = Category::class,
            parentColumns = ["id"],
            childColumns = ["categoryId"],
            onDelete = ForeignKey.CASCADE
        )
    ]
)
data class TaskWithCategory(
    @PrimaryKey(autoGenerate = true)
    val id: Int = 0,
    val title: String,
    val categoryId: Int
)

// Query with relationship
data class CategoryWithTasks(
    @Embedded val category: Category,
    @Relation(
        parentColumn = "id",
        entityColumn = "categoryId"
    )
    val tasks: List<TaskWithCategory>
)

@Dao
interface CategoryDao {
    @Transaction
    @Query("SELECT * FROM categories")
    fun getCategoriesWithTasks(): Flow<List<CategoryWithTasks>>
}
```
