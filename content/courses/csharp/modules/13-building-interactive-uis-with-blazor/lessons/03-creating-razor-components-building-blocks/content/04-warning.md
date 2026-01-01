---
type: "WARNING"
title: "Common Pitfalls"
---

## Watch Out For These Issues

**Filename mismatch**: Component filename MUST match the component name exactly. `Alert.razor` = Alert component. Mismatches cause 'component not found' errors.

**Private parameters fail**: [Parameter] on private property doesn't work! Must be `public string Title { get; set; }`. Private = not visible to parent.

**Mutating parameters directly**: Don't modify [Parameter] properties inside the component! Parent owns them. Use local field: `private int _count; [Parameter] public int InitialCount { set => _count = value; }`

**OnInitialized vs OnParametersSet**: OnInitialized runs once at creation. OnParametersSet runs when parent changes parameters. Use OnParametersSet for parameter-dependent logic.

**Async lifecycle methods**: Use OnInitializedAsync for async data loading, not OnInitialized with .Result (blocks thread!). Return Task, use await.

**Missing @using**: Component not found? Check if you need `@using YourNamespace` at top of file or in _Imports.razor.