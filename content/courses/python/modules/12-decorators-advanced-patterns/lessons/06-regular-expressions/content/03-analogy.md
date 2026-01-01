---
type: "ANALOGY"
title: "Syntax Breakdown"
---

**Pattern syntax quick reference:**

**Special characters:**
```
.   Any character (except newline)
\d  Digit [0-9]
\D  Not digit [^0-9]
\w  Word char [a-zA-Z0-9_]
\W  Not word char
\s  Whitespace [ \t\n\r\f\v]
\S  Not whitespace
```

**Quantifiers:**
```
*      0 or more
+      1 or more
?      0 or 1 (optional)
{3}    Exactly 3
{2,5}  Between 2 and 5
{2,}   2 or more
```

**Anchors:**
```
^   Start of string
$   End of string
\b  Word boundary
```

**Groups:**
```
(...)   Capturing group
(?:...)  Non-capturing group
|       OR (alternation)
```

**Character classes:**
```
[abc]    Any of a, b, c
[a-z]    Range (a through z)
[^abc]   NOT a, b, or c
```

**Escaping special chars:**
```
\.  Literal dot
\$  Literal dollar sign
\*  Literal asterisk
Use \ before special characters
```