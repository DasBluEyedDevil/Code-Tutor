---
type: "ANALOGY"
title: "State as a Whiteboard"
---

State in Compose is like a whiteboard in a classroom that everyone can see.

**The whiteboard represents your app's state**—numbers, text, lists, any data that can change. When you write something new on the whiteboard (update state), everyone in the classroom immediately sees the change.

**Composables are students watching the whiteboard**—whenever the whiteboard changes, students automatically adjust their behavior based on what they see. A student doesn't need to be told "the number changed"; they're always watching and react automatically.

**Recomposition is like students responding to updates**—when you change "score: 10" to "score: 11" on the whiteboard, the student responsible for displaying the score automatically updates their paper. You don't manually tell each student to update; they're observing the whiteboard and react when it changes.

This is why you never manually update UI in Compose—you update the "whiteboard" (state), and Compose automatically recomposes (updates) the UI to match.
