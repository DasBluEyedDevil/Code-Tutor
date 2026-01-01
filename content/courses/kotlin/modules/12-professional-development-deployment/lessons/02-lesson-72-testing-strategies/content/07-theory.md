---
type: "THEORY"
title: "MockK - Powerful Mocking"
---


### Why Mock?

**Problem**: Testing a service that depends on a database:


To test `UserService`, we don't want to:
- Set up a real database
- Insert test data
- Clean up after tests
- Deal with slow I/O operations

**Solution**: Mock the database!


### Basic Mocking


### Advanced Mocking

**Relaxed Mocks** (return default values):

**Spy** (real object with partial mocking):

**Capture Arguments**:

---



```kotlin
@Test
fun `verify method was called with specific arguments`() {
    val mockRepo = mockk<UserRepository>(relaxed = true)
    val service = UserService(mockRepo)

    val slot = slot<User>()

    service.updateUser(User(1, "John", "john@example.com"))

    verify { mockRepo.update(capture(slot)) }

    assertEquals("John", slot.captured.name)
}
```
