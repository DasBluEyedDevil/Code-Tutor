---
type: "THEORY"
title: "If Statement Syntax"
---

The basic structure:

if (condition) {
    // Code to run if condition is TRUE
}

Real example:

int age = 20;
if (age >= 18) {
    System.out.println("You can vote!");
}

Breaking it down:
- 'if' is the keyword
- (age >= 18) is the CONDITION being tested
- '>=' means 'greater than or equal to'
- If the condition is TRUE, the code inside { } runs
- If the condition is FALSE, the code is skipped

In this example, since age is 20, and 20 >= 18 is TRUE, it prints "You can vote!"