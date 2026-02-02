---
type: "WARNING"
title: "Clean Architecture Pitfalls"
---

### Pitfall 1: Over-Layering Small Apps

```
❌ For a 3-screen app:
UserUseCase → UserRepository → UserLocalDataSource → UserDao
                             → UserRemoteDataSource → UserApi

✅ Start simpler:
UserViewModel → UserRepository (combines local + remote)
```

### Pitfall 2: Anemic Domain Models

```kotlin
// ❌ Domain model is just data - logic elsewhere
data class User(val id: String, val birthDate: LocalDate)

class UserUtils {
    fun isAdult(user: User): Boolean = // logic here
}

// ✅ Domain model encapsulates behavior
data class User(val id: String, val birthDate: LocalDate) {
    fun isAdult(): Boolean = birthDate.yearsUntil(Clock.System.now()) >= 18
}
```

### Pitfall 3: Leaking Framework Types

```kotlin
// ❌ SQLDelight/Room types in domain
interface NoteRepository {
    fun getNotes(): Query<NoteEntity> // Framework type!
}

// ✅ Domain types only
interface NoteRepository {
    fun observeNotes(): Flow<List<Note>> // Pure Kotlin
}
```

### Pitfall 4: Mapper Explosion

```kotlin
// ❌ 20 mapper files for simple conversions
UserDtoToEntityMapper
UserEntityToDomainMapper
UserDomainToUiMapper

// ✅ Extension functions in appropriate places
fun UserDto.toEntity(): UserEntity = ...
fun UserEntity.toDomain(): User = ...
```