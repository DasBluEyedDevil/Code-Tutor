---
type: "THEORY"
title: "ConfigureAwait Deep Dive"
---

## ConfigureAwait(false) - When and Why?

**What does it do?**
By default, after an await, execution resumes on the original context (UI thread, ASP.NET request context). `ConfigureAwait(false)` says 'I don't need to resume on the original context.'

**When to use ConfigureAwait(false):**
- **Library code**: Always! You don't know if your code will be called from UI apps
- **Internal helper methods**: When you don't need UI thread access
- **Performance-critical code**: Avoids context-switch overhead

**When NOT to use it:**
- **UI code**: You NEED to be on UI thread to update controls
- **ASP.NET Core**: No synchronization context, so it's a no-op anyway
- **Code that accesses HttpContext**: Needs the request context

```csharp
// Library code - use ConfigureAwait(false)
public async Task<string> FetchDataAsync()
{
    var response = await httpClient
        .GetAsync(url)
        .ConfigureAwait(false);  // Don't capture context!
        
    return await response.Content
        .ReadAsStringAsync()
        .ConfigureAwait(false);  // On EVERY await!
}

// UI code - DON'T use it
private async void Button_Click(object sender, EventArgs e)
{
    string data = await FetchDataAsync();
    label.Text = data;  // Needs UI thread!
}
```

**Modern guidance:** Use ConfigureAwait(false) in library code. For app code in ASP.NET Core (no sync context), it doesn't matter.