---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`EditForm component`**: Blazor form with validation. Model="@object" binds form. OnValidSubmit fires when valid. InputText, InputNumber are built-in inputs with validation.

**`Create vs Update pattern`**: Use boolean flag (isEditing). If true: PUT existing. If false: POST new. Same form, different API call based on context.

**`SaveChangesAsync()`**: EF Core persists changes to database. Call after Add/Update/Remove. Returns number of affected rows. Always await!

**`Reload after changes`**: After Create/Update/Delete, reload data from API. Ensures UI shows latest database state. Call LoadProducts() after operations.