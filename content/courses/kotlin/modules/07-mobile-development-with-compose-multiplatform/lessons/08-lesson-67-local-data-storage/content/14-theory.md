---
type: "THEORY"
title: "Solution 2"
---



---



```kotlin
import androidx.datastore.preferences.core.stringSetPreferencesKey

class FavoritesRepository(private val context: Context) {
    private val FAVORITES_KEY = stringSetPreferencesKey("favorites")

    val favorites: Flow<Set<String>> = context.dataStore.data
        .map { preferences ->
            preferences[FAVORITES_KEY] ?: emptySet()
        }

    suspend fun addFavorite(itemId: String) {
        context.dataStore.edit { preferences ->
            val currentFavorites = preferences[FAVORITES_KEY]?.toMutableSet() ?: mutableSetOf()
            currentFavorites.add(itemId)
            preferences[FAVORITES_KEY] = currentFavorites
        }
    }

    suspend fun removeFavorite(itemId: String) {
        context.dataStore.edit { preferences ->
            val currentFavorites = preferences[FAVORITES_KEY]?.toMutableSet() ?: mutableSetOf()
            currentFavorites.remove(itemId)
            preferences[FAVORITES_KEY] = currentFavorites
        }
    }

    suspend fun toggleFavorite(itemId: String) {
        context.dataStore.edit { preferences ->
            val currentFavorites = preferences[FAVORITES_KEY]?.toMutableSet() ?: mutableSetOf()
            if (currentFavorites.contains(itemId)) {
                currentFavorites.remove(itemId)
            } else {
                currentFavorites.add(itemId)
            }
            preferences[FAVORITES_KEY] = currentFavorites
        }
    }
}
```
