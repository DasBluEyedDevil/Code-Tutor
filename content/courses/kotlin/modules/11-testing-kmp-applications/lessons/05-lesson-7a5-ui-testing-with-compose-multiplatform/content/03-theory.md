---
type: "THEORY"
title: "Finding Nodes"
---

### Finders

```kotlin
// By text
onNodeWithText("Submit")
onAllNodesWithText("Item")

// By content description (accessibility)
onNodeWithContentDescription("Delete note")

// By test tag
onNodeWithTag("note_title_input")

// By semantic property
onNode(hasText("Submit") and hasClickAction())
onNode(isDialog())
onNode(hasScrollAction())

// Combine matchers
onNode(
    hasText("Delete") and
    hasAncestor(hasTestTag("note_card"))
)
```

### Adding Test Tags

```kotlin
@Composable
fun NoteCard(note: Note, onClick: () -> Unit) {
    Card(
        modifier = Modifier
            .testTag("note_card_${note.id}")  // Unique tag
            .clickable(onClick = onClick)
    ) {
        Text(
            text = note.title,
            modifier = Modifier.testTag("note_title")
        )
    }
}
```