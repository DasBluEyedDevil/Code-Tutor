---
type: "THEORY"
title: "The Problem: Tight Coupling"
---

Consider this common pattern in applications:

```kotlin
class NotesViewModel {
    // ❌ Hard-coded dependencies
    private val database = AppDatabase.getInstance()
    private val apiClient = ApiClient()
    private val preferences = UserPreferences()
    
    fun loadNotes() {
        val notes = database.notesDao().getAll()
        // ...
    }
}
```

**Problems with this approach:**

1. **Untestable**: You can't substitute a fake database for testing
2. **Rigid**: Changing the database implementation affects this class
3. **Hidden dependencies**: Readers can't see what this class needs without reading the code
4. **Lifecycle issues**: Who creates and destroys these objects?
5. **Singletons everywhere**: `getInstance()` patterns lead to global state