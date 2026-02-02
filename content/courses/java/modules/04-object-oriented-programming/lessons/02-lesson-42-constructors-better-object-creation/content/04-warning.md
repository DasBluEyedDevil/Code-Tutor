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
   this(...) or super(...) must be the FIRST statement in constructor -- unless you use flexible constructor bodies (see below).

5. FLEXIBLE CONSTRUCTOR BODIES (JAVA 25):
   Java now allows statements BEFORE super() for validation and transformation:

   public class Employee extends Person {
       public Employee(String name, int age) {
           if (name == null || name.isBlank()) {
               throw new IllegalArgumentException("Name is required");
           }
           if (age <= 0) {
               throw new IllegalArgumentException("Age must be positive");
           }
           super(name, age);  // Call parent constructor AFTER validation
       }
   }

   This is especially useful when you need to validate or transform arguments
   before passing them to the parent constructor.

6. CONSIDER RECORDS FOR DATA CLASSES:
   record Student(String name, int age) {}
   Records generate canonical constructor, compact constructor validation.