---
type: "WARNING"
title: "Common Pitfalls"
---

### Pitfall 1: Over-Mocking

```kotlin
// ❌ Testing the mock, not the code
@Test
fun `getUser returns user`() {
    val mock = mock<UserRepository> {
        everySuspend { getUser("1") } returns User("1", "John")
    }
    
    val result = mock.getUser("1")  // You're just testing Mokkery!
    assertEquals("John", result.name)
}

// ✅ Test actual code using the mock
@Test
fun `ViewModel shows user name`() {
    val mock = mock<UserRepository> {
        everySuspend { getUser(any()) } returns User("1", "John")
    }
    
    val viewModel = UserViewModel(mock)  // Test real code
    assertEquals("John", viewModel.state.value.userName)
}
```

### Pitfall 2: Verification Obsession

```kotlin
// ❌ Testing implementation, not behavior
@Test
fun `addNote works`() {
    viewModel.addNote("Title", "Content")
    
    verify { repo.add("Title", "Content") }
    verify { repo.getAll() }  // Who cares?
    verify { state.update(any()) }  // Implementation detail!
}

// ✅ Test the outcome
@Test
fun `addNote appears in list`() {
    viewModel.addNote("Title", "Content")
    
    assertTrue(viewModel.state.value.notes.any { it.title == "Title" })
}
```