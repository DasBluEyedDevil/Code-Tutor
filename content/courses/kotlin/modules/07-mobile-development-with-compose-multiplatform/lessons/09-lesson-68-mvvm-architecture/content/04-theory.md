---
type: "THEORY"
title: "ViewModel Lifecycle"
---


### Lifecycle Scope


**Lifecycle**:

### ViewModelScope


---



```kotlin
class UserViewModel(private val repository: UserRepository) : ViewModel() {
    private val _users = MutableStateFlow<List<User>>(emptyList())
    val users: StateFlow<List<User>> = _users.asStateFlow()

    fun loadUsers() {
        viewModelScope.launch {
            // Automatically cancelled when ViewModel is cleared
            val result = repository.getUsers()
            _users.value = result
        }
    }

    override fun onCleared() {
        super.onCleared()
        // viewModelScope is automatically cancelled here
    }
}
```
