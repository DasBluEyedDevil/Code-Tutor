---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`do { } while (condition);`**: Structure: 'do' keyword, then { code }, then 'while (condition)' and SEMICOLON. The semicolon after while is REQUIRED for do-while!

**`Runs at least once`**: The code block executes FIRST, THEN the condition is checked. Even if the condition is false, the code runs one time!

**`Condition at the end`**: The while (condition); comes AFTER the closing brace. If condition is true, loop back to 'do'. If false, continue after the loop.

**`Don't forget the semicolon!`**: while (condition); - the semicolon at the end is MANDATORY for do-while! Forgetting it causes a compiler error.

**`Perfect use cases`**: Input validation (ask once, validate, re-ask if invalid), menu systems (show menu at least once), retry logic (try at least once, retry on failure).