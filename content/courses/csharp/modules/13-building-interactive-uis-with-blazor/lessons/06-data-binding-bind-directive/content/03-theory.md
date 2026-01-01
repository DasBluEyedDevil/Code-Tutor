---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`@bind="variable"`**: Two-way binding. Variable ↔ input. User types → variable updates. Variable changes → input updates. Automatic sync!

**`@bind:event="oninput"`**: Control when binding updates. 'oninput' = every keystroke. 'onchange' = on blur (default for text inputs). Choose based on performance needs.

**`@bind:format`**: Format display value. 'C2' = currency. 'yyyy-MM-dd' = date format. 'P' = percentage. Format doesn't change underlying value, just display.

**`@bind-PropertyName`**: Component two-way binding. Requires: [Parameter] Property AND [Parameter] PropertyChanged EventCallback. @bind-Count binds to Count parameter.