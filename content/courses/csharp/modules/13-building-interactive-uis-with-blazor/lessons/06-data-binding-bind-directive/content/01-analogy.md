---
type: "ANALOGY"
title: "Understanding the Concept"
---

Data binding is like a two-way mirror:

ONE-WAY (display only):
C# variable → @variable → Shows in UI
If variable changes, UI updates

TWO-WAY (@bind):
C# variable ⟷ @bind ⟷ Input field
Variable changes → UI updates
User types → Variable updates
It's AUTOMATIC!

Without @bind:
<input value="@name" @oninput="e => name = e.Value.ToString()" />

With @bind:
<input @bind="name" />

Much simpler! Blazor handles sync automatically.

Think: @bind = 'Keep C# variable and UI input perfectly in sync, both ways!'