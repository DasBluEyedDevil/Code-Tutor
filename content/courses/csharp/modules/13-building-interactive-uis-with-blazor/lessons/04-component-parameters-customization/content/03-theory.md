---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`[Parameter] public Type Name { get; set; }`**: Standard parameter. Must be public with [Parameter] attribute. Can set default value with = "default". Passed from parent component.

**`EventCallback<T>`**: Parameter for events. T is data type passed to parent. Use InvokeAsync(value) to trigger. EventCallback (no <T>) for no data.

**`RenderFragment`**: Special parameter type for child content. Lets parent pass HTML/components as parameter. Name it 'ChildContent' for default slot.

**`[CascadingParameter]`**: Receives value from CascadingValue ancestor. No need to pass through every level! Useful for themes, user context, etc.