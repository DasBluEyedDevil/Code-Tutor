---
type: "ANALOGY"
title: "SQLDelight as a Translator"
---

SQLDelight is like having a professional translator between you and a foreign partner.

**You speak SQL** (the database's native language), which is precise and widely understood. You write exactly what you want: "SELECT all users WHERE age > 18".

**Your Kotlin code speaks... well, Kotlin**—it wants type-safe objects, not raw strings and manual parsing. It wants a function that returns `List<User>`, not something that might return anything or crash.

**SQLDelight is the translator**—it reads your SQL, understands both languages perfectly, and generates Kotlin functions that bridge the gap. It ensures the SQL you wrote and the Kotlin code you use stay in sync. If you ask for a column that doesn't exist, the translator catches it immediately (at compile-time).

Unlike ORMs that try to make you forget SQL exists (and often generate inefficient queries), SQLDelight embraces SQL while giving you Kotlin type safety. You write explicit SQL, and SQLDelight ensures your Kotlin code matches it perfectly.
