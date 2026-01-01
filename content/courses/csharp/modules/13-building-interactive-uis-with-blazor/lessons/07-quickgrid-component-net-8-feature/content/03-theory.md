---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`<QuickGrid Items="@collection">`**: Main component. Items = IQueryable<T> or IEnumerable<T>. IQueryable is better (database queries optimized!). Renders HTML table.

**`<PropertyColumn Property="@(p => p.Name)" />`**: Column for a property. Lambda selects property. Sortable="true" enables sorting. Format="C2" for currency, "P" for percent.

**`<TemplateColumn>`**: Custom column content. Access item via 'context'. Full control: buttons, badges, conditional styling. Use for actions, custom rendering.

**`Pagination="@state"`**: Enable paging. Create PaginationState with ItemsPerPage. Use <Paginator State="@state" /> to show page controls. Efficient for large datasets!