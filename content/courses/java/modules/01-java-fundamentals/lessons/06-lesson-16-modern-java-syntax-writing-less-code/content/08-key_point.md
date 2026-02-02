---
type: "KEY_POINT"
title: "Summary: Java 25 Features for Clean Code"
---

Here's a quick reference of the features covered in this lesson:

COMPACT SOURCE FILES (JEP 512):
void main() {
    IO.println("Hello!");
}
No class declaration, no static, no String[] args.

UNNAMED VARIABLES (JEP 456):
catch (Exception _) { ... }
for (var _ : list) { ... }
Use _ when a variable is required but unused.

MODULE IMPORTS (JEP 476):
import module java.base;
One line imports all standard library classes.

These features work together to make Java clean and focused on your logic. As your programs grow larger (Module 04 onwards), you'll naturally add more structure -- but you'll always understand WHY that structure exists.