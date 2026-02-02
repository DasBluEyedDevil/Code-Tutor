---
type: "ANALOGY"
title: "The Escape Room for Special Characters"
---

Imagine you're writing a message that will be displayed on a digital sign, but certain characters like '<' and '>' control the sign's formatting. To show these literally, you need to 'escape' them with special codes.

Regular expressions have their own special characters: `.`, `*`, `?`, `+`, `^`, `$`, `{`, `}`, `(`, `)`, `|`, `[`, `]`, and `\`. If you want to search for these characters literally, you must escape them with a backslash. `RegExp.escape()` does this automatically, saving you from bugs and security issues.