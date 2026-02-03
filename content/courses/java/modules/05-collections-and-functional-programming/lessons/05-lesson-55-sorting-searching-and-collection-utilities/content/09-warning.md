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

Sequenced Collections Compatibility (since Java 21):
If your class implements List and defines getFirst() with a different signature, you may get a compile error. The SequencedCollection interface (added in Java 21) defines getFirst()/getLast() which List now inherits. Watch for this when working with legacy code.