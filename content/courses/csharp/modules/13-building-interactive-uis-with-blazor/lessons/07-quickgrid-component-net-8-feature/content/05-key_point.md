---
type: "KEY_POINT"
title: "QuickGrid for Data Tables"
---

## Key Takeaways

- **`<QuickGrid Items="@data">` renders a data table** -- pass `IQueryable<T>` for server-side query optimization or `IEnumerable<T>` for in-memory data. QuickGrid handles sorting and pagination.

- **`<PropertyColumn>` for simple data, `<TemplateColumn>` for custom rendering** -- PropertyColumn auto-generates the column from a lambda. TemplateColumn gives full control for buttons, badges, and conditional formatting.

- **Built-in pagination with `PaginationState`** -- set `ItemsPerPage` and pair with `<Paginator>` for automatic paging. Efficient for large datasets without loading everything into memory.
