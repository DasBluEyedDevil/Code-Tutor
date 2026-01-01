---
type: "KEY_POINT"
title: "✏️ Quiz Answer Key"
---


**Question 1**: **B) It runs the Ktor application as a standalone server inside your program**

Explanation: `embeddedServer` starts Ktor as an embedded server (no external Tomcat/Jetty needed). Your application *is* the server.

---

**Question 2**: **C) A path parameter that captures a dynamic value from the URL**

Explanation: `{id}` is a path parameter placeholder. When someone visits `/api/users/42`, the value `42` is captured and accessible via `call.parameters["id"]`.

---

**Question 3**: **B) ContentNegotiation with kotlinx.serialization**

Explanation: ContentNegotiation handles content type negotiation (JSON, XML, etc.), and kotlinx.serialization provides the actual JSON conversion. Together, they enable automatic Kotlin object ↔ JSON transformation.

---

**Congratulations!** You've set up your first Ktor project and built a working server. You're now officially a backend developer! 🎉

