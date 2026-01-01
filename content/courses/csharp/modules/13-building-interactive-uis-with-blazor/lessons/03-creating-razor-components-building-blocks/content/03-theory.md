---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`[Parameter]`**: Makes property a component parameter. Can be set from parent: <MyComponent Title="Hello" />. Must be public property with [Parameter] attribute.

**`OnInitialized()`**: Lifecycle method called once when component created. Use for initialization, loading data. Override with 'protected override void OnInitialized()'.

**`<ComponentName />`**: Use component in parent. Self-closing if no child content. Pass parameters as attributes: <Alert Message="text" />.

**`Component file structure`**: .razor file with: HTML markup at top, @code block at bottom. Component name MUST match filename! Alert.razor contains Alert component.