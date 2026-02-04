---
type: KEY_POINT
---

- Structure data with model classes (e.g., `Note` with `id`, `title`, `content`, `createdAt`) before building UI
- Use a `StatefulWidget` to manage the list of notes with `setState()` for add, edit, and delete operations
- `FloatingActionButton` triggers the "add note" flow; `ListView.builder` renders the note list efficiently
- `showDialog` or `Navigator.push` to a form screen collects user input for new or edited notes
- Combine buttons, forms, gestures, and state management in one project to practice the full interaction lifecycle
