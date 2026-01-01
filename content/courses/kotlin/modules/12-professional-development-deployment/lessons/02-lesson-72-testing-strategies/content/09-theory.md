---
type: "THEORY"
title: "Testing Jetpack Compose UI"
---


### Compose Testing Library


**Basic Compose Test**:

### Testing Interactions


---



```kotlin
@Test
fun todoList_addItem_showsInList() {
    composeTestRule.setContent {
        TodoApp()
    }

    // Enter new todo
    composeTestRule.onNodeWithTag("todoInput")
        .performTextInput("Buy groceries")

    // Click add button
    composeTestRule.onNodeWithTag("addButton")
        .performClick()

    // Verify item appears
    composeTestRule.onNodeWithText("Buy groceries")
        .assertIsDisplayed()

    // Verify input is cleared
    composeTestRule.onNodeWithTag("todoInput")
        .assertTextEquals("")
}

@Test
fun todoItem_clickCheckbox_marksAsComplete() {
    composeTestRule.setContent {
        TodoItem(
            todo = Todo(id = 1, text = "Test", completed = false),
            onToggle = { }
        )
    }

    // Initially unchecked
    composeTestRule.onNodeWithTag("checkbox-1")
        .assertIsOff()

    // Click checkbox
    composeTestRule.onNodeWithTag("checkbox-1")
        .performClick()

    // Verify it's checked
    composeTestRule.onNodeWithTag("checkbox-1")
        .assertIsOn()
}
```
