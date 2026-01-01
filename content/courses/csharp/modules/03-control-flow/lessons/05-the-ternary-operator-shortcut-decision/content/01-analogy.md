---
type: "ANALOGY"
title: "Understanding the Concept"
---

Sometimes you need a simple if-else decision that just assigns a value. Writing a full if-else block feels like overkill:

```
string message;
if (score >= 50) {
    message = "Pass";
} else {
    message = "Fail";
}
```

C# has a SHORTCUT called the TERNARY OPERATOR (ternary means 'three parts'):

```
string message = (score >= 50) ? "Pass" : "Fail";
```

One line! It works like this:

(condition) ? valueIfTrue : valueIfFalse

Think of the ? as 'if this is true' and the : as 'otherwise'. It's like asking a yes/no question:

'Is the score 50 or higher? If YES, assign "Pass". If NO, assign "Fail".'

The ternary operator is PERFECT for simple assignments but can get messy if overused. If your logic is complex, stick with regular if-else for readability!