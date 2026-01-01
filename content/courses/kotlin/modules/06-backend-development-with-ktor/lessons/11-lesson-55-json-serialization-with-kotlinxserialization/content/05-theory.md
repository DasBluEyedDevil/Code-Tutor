---
type: "THEORY"
title: "🎨 Customizing Field Names"
---


### Using @SerialName

Sometimes your Kotlin naming doesn't match the JSON format you need:


**JSON representation:**

**Why use @SerialName?**
- ✅ Match external API naming conventions (snake_case vs camelCase)
- ✅ Keep Kotlin code idiomatic (camelCase)
- ✅ Avoid breaking changes when refactoring

---



```json
{
  "id": 1,
  "user_name": "alice",
  "email_address": "alice@example.com",
  "created_at": "2024-11-13"
}
```
