---
type: "WARNING"
title: "Common Pitfalls"
---

## Watch Out For These Mistakes!

**Version conflicts**: Different packages might need different versions of the same dependency! Check the build output for warnings. Use `dotnet list package --include-transitive` to see all dependencies.

**System.Text.Json vs Newtonsoft.Json**: System.Text.Json is BUILT-IN (no install needed) and faster, but has stricter defaults. Newtonsoft.Json is more flexible but requires installation. Choose based on your needs.

**Property naming in JSON**: By default, System.Text.Json uses PascalCase. If your JSON uses camelCase, add `[JsonPropertyName("name")]` attribute or use `PropertyNamingPolicy = JsonNamingPolicy.CamelCase` in options.

**Null after deserialization**: If JSON structure doesn't match your class, you get null or default values! Always validate deserialized data. Use the ! operator only when you're SURE data is valid.

**Running commands in wrong folder**: `dotnet add package` must run in the folder containing your .csproj file! Check your current directory first.