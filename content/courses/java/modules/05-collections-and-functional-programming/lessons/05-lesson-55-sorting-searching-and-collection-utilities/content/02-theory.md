---
type: "THEORY"
title: "Collections.sort() - Sorting Lists"
---

IMPORT:
import java.util.Collections;

SORTING NUMBERS:
ArrayList<Integer> numbers = new ArrayList<>();
numbers.add(5);
numbers.add(2);
numbers.add(8);
numbers.add(1);

Collections.sort(numbers);  // Sorts IN PLACE
// Now: [1, 2, 5, 8]

SORTING STRINGS:
ArrayList<String> names = new ArrayList<>();
names.add("Charlie");
names.add("Alice");
names.add("Bob");

Collections.sort(names);  // Alphabetical
// Now: ["Alice", "Bob", "Charlie"]

REVERSE SORT:
Collections.sort(numbers, Collections.reverseOrder());
// Now: [8, 5, 2, 1]