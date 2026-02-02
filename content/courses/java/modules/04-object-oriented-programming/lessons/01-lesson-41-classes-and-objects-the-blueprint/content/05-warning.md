---
type: "WARNING"
title: "Common Classes and Objects Pitfalls"
---

COMMON MISTAKES TO AVOID:

1. FORGETTING new KEYWORD:
   Student alice;  // Only declares a variable (null)
   alice.name = "Alice";  // NullPointerException!
   CORRECT: Student alice = new Student();

2. CONFUSING CLASS WITH OBJECT:
   Student.name = "Alice";  // ERROR! Cannot access fields on the class
   CORRECT: Student s = new Student(); s.name = "Alice";

3. PUBLIC FIELDS (BAD PRACTICE):
   Directly accessing fields like myCar.speed = -500 allows invalid data.
   Use encapsulation (private fields + getters/setters) instead.

4. FORGETTING TO INITIALIZE FIELDS:
   Fields have default values (0, null, false), but relying on them causes bugs.
   Always initialize fields explicitly via constructors.

5. ALTERNATIVE FOR DATA CLASSES - RECORDS:
   For simple data classes, consider using records:
   record Student(String name, int age) {}
   Records auto-generate constructor, getters, equals, hashCode, toString.