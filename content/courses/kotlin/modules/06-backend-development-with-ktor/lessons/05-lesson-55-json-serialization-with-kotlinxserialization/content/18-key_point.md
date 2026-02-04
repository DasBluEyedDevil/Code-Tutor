---
type: "KEY_POINT"
title: "✏️ Quiz Answer Key"
---


**Question 1**: **B) Generates code to convert the class to/from JSON at compile time**

Explanation: @Serializable is a compile-time annotation that generates serializer code. The magic of `call.receive<Book>()` works because the serializer was generated at compile time.

---

**Question 2**: **B) To map a different JSON field name to your Kotlin property**

Explanation: @SerialName allows the JSON field name to differ from your Kotlin property name. Common when working with APIs that use snake_case while Kotlin uses camelCase.

---

**Question 3**: **B) A SerializationException is thrown**

Explanation: By default (ignoreUnknownKeys = false), extra fields cause an error. Set `ignoreUnknownKeys = true` in your JSON configuration to silently ignore them.

---

**Congratulations!** You now have complete control over JSON serialization in your Ktor API! 🎉

