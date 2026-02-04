---
type: KEY_POINT
---

- Use `ListView.builder` with `reverse: true` so new messages appear at the bottom and older messages load as users scroll up
- Distinguish sent vs. received messages with alignment (right/left), color, and bubble shape using `Align` and `Container`
- Group consecutive messages from the same sender to avoid repeating the avatar and name on every bubble
- The input field should support multi-line text, a send button, and clear itself after sending a message
- Scroll to the latest message automatically when a new message arrives using `ScrollController.animateTo()`
