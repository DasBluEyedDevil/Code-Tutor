---
type: "THEORY"
title: "How It Works"
---

### Under the Hood

1. **Initial emission**: Flow emits current query results immediately
2. **Table listeners**: SQLDelight watches the tables in your query
3. **Automatic re-query**: When any watched table changes, query re-runs
4. **New emission**: Updated results are emitted to collectors

```kotlin
// This query watches the Note table
getAllNotes:
SELECT * FROM Note;

// Any change to Note table triggers re-emission:
noteQueries.insertNote(...)  // → Flow emits new list
noteQueries.updateNote(...)  // → Flow emits new list
noteQueries.deleteNote(...)  // → Flow emits new list
```

### Multi-Table Queries

Flows watch ALL tables in the query:

```sql
getNotesWithCategories:
SELECT Note.*, Category.name AS category_name
FROM Note
LEFT JOIN Category ON Note.category_id = Category.id;
```

```kotlin
// This Flow re-emits when either Note OR Category changes!
fun observeNotesWithCategories(): Flow<List<GetNotesWithCategories>> {
    return queries.getNotesWithCategories()
        .asFlow()
        .mapToList(Dispatchers.IO)
}
```