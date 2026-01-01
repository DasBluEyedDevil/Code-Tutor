---
type: "WARNING"
title: "LinkedList Pitfalls"
---

NoSuchElementException:
LinkedList<String> list = new LinkedList<>();
list.getFirst();  // CRASH! List is empty

Use peekFirst() for null-safe access:
String first = list.peekFirst();  // Returns null if empty

Random access is SLOW:
list.get(500);  // Must traverse 500 nodes! O(n)
Use ArrayList if you need fast random access.

Memory overhead:
Each element has prev/next pointers - more memory than ArrayList.

Java 21+ Sequenced Collections:
LinkedList implements SequencedCollection.
New methods: reversed() returns reversed view.