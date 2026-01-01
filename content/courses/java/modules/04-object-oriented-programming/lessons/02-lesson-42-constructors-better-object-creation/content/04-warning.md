---
type: "WARNING"
title: "Common Constructor Pitfalls"
---

COMMON MISTAKES TO AVOID:

1. ADDING RETURN TYPE:
   public void Student(String name) { }  // This is a METHOD, not constructor!
   CORRECT: public Student(String name) { }  // No return type

2. FORGETTING this WITH SAME PARAMETER NAMES:
   public Student(String name) {
       name = name;  // Does NOTHING! Assigns parameter to itself
   }
   CORRECT: this.name = name;

3. NOT CALLING super() IN SUBCLASS:
   If parent has no default constructor, you MUST call super(...) first.

4. CONSTRUCTOR CHAINING MISTAKES:
   this(...) or super(...) must be the FIRST statement in constructor.

5. JAVA 22+ FEATURE - STATEMENTS BEFORE super():
   Java 22 allows statements before super() for validation/transformation.
   Pre-Java 22: super() must always be first line.

6. CONSIDER RECORDS FOR DATA CLASSES:
   record Student(String name, int age) {}
   Records generate canonical constructor, compact constructor validation.