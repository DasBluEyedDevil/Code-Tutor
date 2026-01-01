---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`try { risky code }`**: The 'try' block contains code that MIGHT fail. If any line throws an exception, execution immediately jumps to the catch block.

**`catch (ExceptionType ex) { handle }`**: 'catch' runs ONLY if an exception occurs in the try block. ExceptionType specifies what kind of error to catch. 'ex' is a variable holding error details.

**`catch (Exception ex) when (condition)`**: Exception FILTER! The catch block only runs if BOTH the exception type matches AND the 'when' condition is true. The filter is evaluated BEFORE unwinding the stack, preserving the original StackTrace!

**`Multiple catch blocks`**: You can have multiple catch blocks for different exception types! They're checked in order. Put specific exceptions first, generic (Exception) last.

**`ex.Message`**: The exception object 'ex' has properties: Message (error description), StackTrace (where error occurred). Useful for debugging!