---
type: "KEY_POINT"
title: "Common Shorthands"
---


Here are the most common enum shorthands you'll use in Flutter:

| Verbose | Shorthand | Context |
|---------|-----------|-------------------------------|
| MainAxisAlignment.center | .center | Column/Row |
| CrossAxisAlignment.start | .start | Column/Row |
| TextAlign.center | .center | Text widget |
| FontWeight.bold | .bold | TextStyle |
| BoxFit.cover | .cover | Image/BoxDecoration |
| Axis.vertical | .vertical | Scrollable widgets |
| Alignment.center | .center | Container |

**Important Note:** `Colors.blue` does **NOT** have a shorthand because `Colors` is a class with static constants, not an enum. You must still write `Colors.blue`, not `.blue`.

