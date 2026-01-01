---
type: "WARNING"
title: "Stream API Pitfalls"
---

Streams can only be consumed ONCE:
Stream<String> stream = names.stream();
stream.count();  // OK
stream.forEach(System.out::println);  // IllegalStateException!

Lazy evaluation surprises:
names.stream().peek(System.out::println);  // Prints NOTHING!
Peek only runs when terminal operation is called.

Collectors.toList() vs Stream.toList() (Java 16+):
.collect(Collectors.toList())  // Mutable list
.toList()  // IMMUTABLE list - cannot modify!

Parallel stream ordering:
Parallel streams may not preserve order. Use forEachOrdered() if order matters.

Java 22+ Stream Gatherers (Preview):
New gather() operation for custom intermediate operations.
Built-in gatherers: Gatherers.fold(), windowFixed(), scan().