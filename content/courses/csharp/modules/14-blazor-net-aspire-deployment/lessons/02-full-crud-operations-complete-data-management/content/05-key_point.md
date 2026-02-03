---
type: "KEY_POINT"
title: "Full CRUD with Blazor Forms"
---

## Key Takeaways

- **`EditForm` provides built-in validation** -- bind to a model with `Model="@product"`, handle `OnValidSubmit`, and use `InputText`, `InputNumber` for type-safe form inputs with automatic validation.

- **Create and Update share the same form** -- use a boolean flag (`isEditing`) to toggle between POST (create) and PUT (update). This avoids duplicating form markup and validation logic.

- **Reload data after every mutation** -- after creating, updating, or deleting, re-fetch the list from the API. This ensures the UI always reflects the current database state.
