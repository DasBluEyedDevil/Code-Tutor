---
type: "KEY_POINT"
title: "Putting It All Together"
---

public static void main(String[] args)
  |      |     |    |     └─ Parameter: array of Strings
  |      |     |    └─ Name: main (special name, program entry)
  |      |     └─ Return type: void (doesn't return anything)
  |      └─ static: Can be called without creating an object
  └─ public: Must be accessible from anywhere (JVM needs to call it)

public static int add(int a, int b)
  |      |     |   |   └─ Parameters
  |      |     |   └─ Name
  |      |     └─ Return type: int (returns an integer)
  |      └─ static: Utility method, no object needed
  └─ public: Other classes can use this

private void updateDisplay()
  |      |    └─ Name
  |      └─ Return type: void (performs action, no return)
  └─ private: Only THIS class can call this

QUICK REFERENCE:
- void: Returns nothing
- int/String/etc: Returns that type
- public: Everyone can access
- private: Only this class can access
- static: Class-level, no object needed
- (no static): Instance-level, needs object