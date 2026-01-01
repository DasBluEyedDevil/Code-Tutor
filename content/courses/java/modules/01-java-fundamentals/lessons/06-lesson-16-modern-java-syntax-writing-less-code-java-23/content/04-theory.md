---
type: "THEORY"
title: "JEP 456: Unnamed Variables with _"
---

Sometimes you MUST declare a variable but don't need it:

// Forced to name the exception even if unused:
try {
    riskyOperation();
} catch (Exception e) {  // 'e' is never used!
    System.out.println("Something went wrong");
}

// Forced to name element in enhanced for-loop:
for (var item : List.of(1, 2, 3, 4, 5)) {  // 'item' never used!
    doSomething();
}

Java 23 introduces the unnamed variable '_':

try {
    riskyOperation();
} catch (Exception _) {  // Clearly signals: unused
    println("Something went wrong");
}

// Enhanced for-loop where we just need iterations:
for (var _ : List.of(1, 2, 3, 4, 5)) {  // Run 5 times
    doSomething();
}

// Lambda parameter that's unused:
list.forEach(_ -> count++);

Note: Traditional for-loops (for int i = 0; i < 5; i++)
still need a regular counter since _ cannot be read.

Benefits:
- Clearly communicates intent
- No compiler warnings about unused variables
- Cleaner code