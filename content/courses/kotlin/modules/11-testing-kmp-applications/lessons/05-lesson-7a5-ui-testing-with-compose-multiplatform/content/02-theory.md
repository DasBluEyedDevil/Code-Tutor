---
type: "THEORY"
title: "Compose Testing Fundamentals"
---

Compose UI tests use the `compose-ui-test` library:

```kotlin
// build.gradle.kts
val commonTest by getting {
    dependencies {
        implementation(compose.uiTest)
        @OptIn(ExperimentalComposeLibrary::class)
        implementation(compose.uiTestJUnit4)  // JVM/Android
    }
}
```

### Test Structure

```kotlin
class NoteCardTest {
    @get:Rule
    val composeTestRule = createComposeRule()
    
    @Test
    fun `displays note title and content`() {
        // Set content
        composeTestRule.setContent {
            NoteCard(
                note = Note(1, "My Title", "My Content", 0),
                onClick = {}
            )
        }
        
        // Find and assert
        composeTestRule
            .onNodeWithText("My Title")
            .assertIsDisplayed()
        
        composeTestRule
            .onNodeWithText("My Content")
            .assertIsDisplayed()
    }
}
```