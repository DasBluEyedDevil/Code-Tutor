---
type: "THEORY"
title: "Understanding Generics: Type Parameters"
---

The <String> in ArrayList<String> is a TYPE PARAMETER (generics).

WITHOUT GENERICS (old Java):
ArrayList list = new ArrayList();  // Raw type - AVOID!
list.add("hello");
list.add(123);  // No error! Mixed types = bugs later
String s = (String) list.get(1);  // CRASH at runtime!

WITH GENERICS:
ArrayList<String> list = new ArrayList<String>();
list.add("hello");
list.add(123);  // Compile error! Caught early
String s = list.get(0);  // No cast needed

DIAMOND OPERATOR (Java 7+):
ArrayList<String> names = new ArrayList<>();  // <> infers type
// Compiler knows it's ArrayList<String> from left side

MULTIPLE TYPE PARAMETERS:
HashMap<String, Integer> ages = new HashMap<>();
// K=String (key), V=Integer (value)

BOUNDED TYPES (advanced):
List<? extends Number> nums;  // Any Number subtype (Integer, Double...)
List<? super Integer> items;  // Integer or its supertypes