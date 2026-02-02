---
type: "KEY_POINT"
title: "Essential Admin Customizations"
---

**Must-Know Options:**

| Option | Purpose |
|--------|--------|
| `list_display` | Columns in list view |
| `list_filter` | Sidebar filters |
| `search_fields` | Searchable fields |
| `ordering` | Default sort order |
| `list_per_page` | Pagination size |
| `readonly_fields` | Non-editable fields |
| `fieldsets` | Grouped form fields |
| `inlines` | Related object editors |
| `actions` | Bulk operations |
| `list_select_related` | Query optimization |

**Performance Tips:**
- Always set `list_select_related` for ForeignKey fields
- Use `list_prefetch_related` for ManyToMany
- Limit `list_display` to essential columns
- Be careful with method columns that hit the database