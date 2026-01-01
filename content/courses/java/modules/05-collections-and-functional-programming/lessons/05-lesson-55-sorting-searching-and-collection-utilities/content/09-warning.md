---
type: "WARNING"
title: "Sorting and Collections Pitfalls"
---

Collections.sort() modifies in place:
List<Integer> nums = List.of(3, 1, 2);
Collections.sort(nums);  // CRASH! List.of() returns immutable list

Use mutable list:
List<Integer> nums = new ArrayList<>(List.of(3, 1, 2));
Collections.sort(nums);  // OK

Comparable vs Comparator:
Custom objects need Comparable or explicit Comparator:
Collections.sort(people, Comparator.comparing(Person::getName));

Null elements cause NullPointerException:
Collections.sort(listWithNulls);  // CRASH!
Use Comparator.nullsFirst() or nullsLast().

Java 21+ Sequenced Collections Compatibility:
If your class implements List and defines getFirst() with a different signature, you'll get a compile error when upgrading to Java 21. The new SequencedCollection interface defines getFirst()/getLast() which List now inherits.