---
type: "THEORY"
title: "Characters That Get Escaped"
---

RegExp.escape() escapes all characters that have special meaning in regular expressions:

| Character | Regex Meaning | Escaped Form |
|-----------|--------------|---------------|
| `.` | Any character | `\.` |
| `*` | Zero or more | `\*` |
| `+` | One or more | `\+` |
| `?` | Zero or one | `\?` |
| `^` | Start of string | `\^` |
| `$` | End of string | `\$` |
| `{` `}` | Quantifier | `\{` `\}` |
| `(` `)` | Group | `\(` `\)` |
| `[` `]` | Character class | `\[` `\]` |
| `\|` | Alternation | `\\|` |
| `\` | Escape char | `\\` |

The method also escapes some characters for safety in various regex contexts (like `/` for regex literals).