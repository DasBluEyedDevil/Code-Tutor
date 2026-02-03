---
type: "WARNING"
title: "Common Access Modifier Mistakes"
---

Avoid these frequent errors:

1. MAKING EVERYTHING PUBLIC:
// BAD: Exposes internal implementation
public class User {
    public String password;  // Anyone can read/modify!
}

// GOOD: Use private with getters
private String password;
public boolean checkPassword(String input) { ... }

2. CALLING NON-STATIC FROM STATIC:
// WRONG: Cannot access instance field from static method
public class Example {
    String name;
    public static void main(String[] args) {
        IO.println(name);  // COMPILE ERROR!
    }
}

3. FORGETTING main() MUST BE public static:
// WRONG: JVM cannot find entry point
private static void main(String[] args) { }  // ERROR
static void main(String[] args) { }  // Package-private, fails

4. ACCESSING PRIVATE FROM ANOTHER CLASS:
// In class A:
private void helper() { }

// In class B:
A obj = new A();
obj.helper();  // COMPILE ERROR: helper has private access

5. USING DEFAULT (PACKAGE-PRIVATE) ACCIDENTALLY:
// No modifier = package-private, NOT public!
void myMethod() { }  // Only accessible within same package
public void myMethod() { }  // Accessible everywhere