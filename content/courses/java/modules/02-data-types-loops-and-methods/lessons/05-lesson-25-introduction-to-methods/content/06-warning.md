---
type: "WARNING"
title: "Common Method Mistakes"
---

Avoid these frequent errors:

1. RETURNING FROM VOID METHOD:
// WRONG: void methods cannot return a value
public static void sayHi() {
    return "Hi";  // COMPILE ERROR!
}

2. FORGETTING TO RETURN:
// WRONG: Missing return statement
public static int add(int a, int b) {
    int sum = a + b;
    // Forgot to return sum!  COMPILE ERROR
}

3. WRONG RETURN TYPE:
// WRONG: Method says int, but returns double
public static int divide(int a, int b) {
    return a / (double) b;  // COMPILE ERROR!
}

4. USING VOID RETURN VALUE:
// WRONG: void has no value to store
public static void printHi() { System.out.println("Hi"); }
String result = printHi();  // COMPILE ERROR!

5. CONFUSING METHOD WITH CONSTRUCTOR:
// This is a METHOD named Student (returns void)
public static void Student() { }

// This is a CONSTRUCTOR (no return type at all)
public Student() { }

6. ALWAYS USE @Override WHEN OVERRIDING:
// Without @Override, typos create new methods!
@Override
public String toString() { ... }  // Safe - compiler checks!