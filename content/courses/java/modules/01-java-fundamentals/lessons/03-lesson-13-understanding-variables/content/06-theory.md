---
type: "THEORY"
title: "The var Keyword (Java 10+)"
---

Starting with Java 10, you can use 'var' for LOCAL VARIABLE TYPE INFERENCE.

Instead of writing the type explicitly:

String name = "Alice";
int age = 25;
ArrayList<String> names = new ArrayList<String>();

You can let Java figure out the type:

var name = "Alice";          // Java knows it's a String
var age = 25;                 // Java knows it's an int
var names = new ArrayList<String>();  // Java knows the type!

WHY USE var?
1. Less typing (especially for long type names)
2. Cleaner code when the type is obvious
3. Required for some advanced features (like anonymous classes)

WHEN TO USE var:
✓ When the type is obvious from the right side
✓ For complex generic types: var map = new HashMap<String, List<Integer>>();
✓ In for-each loops: for (var item : list) { ... }

WHEN NOT TO USE var:
✗ When the type isn't obvious: var result = getResult();  // What type is this?
✗ For method parameters or return types (not allowed)
✗ For class fields (not allowed)

IMPORTANT: var is NOT like JavaScript's var - the type is still fixed at compile time!
var age = 25;
age = "hello";  // ERROR! age is still an int