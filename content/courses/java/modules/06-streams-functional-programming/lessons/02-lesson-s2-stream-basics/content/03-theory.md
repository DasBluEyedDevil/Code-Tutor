---
type: "THEORY"
title: "Core Stream Operations"
---

Stream operations are either INTERMEDIATE (return a stream) or TERMINAL (produce a result).

INTERMEDIATE OPERATIONS (lazy, chainable):

filter(Predicate): Keep elements matching condition
  .filter(n -> n > 0)

map(Function): Transform each element
  .map(String::toUpperCase)

sorted(): Sort elements
  .sorted()  // natural order
  .sorted(Comparator.reverseOrder())

distinct(): Remove duplicates
  .distinct()

limit(n): Take first n elements
  .limit(5)

skip(n): Skip first n elements
  .skip(10)

TERMINAL OPERATIONS (trigger execution):

forEach(Consumer): Process each element
  .forEach(System.out::println)

count(): Count elements
  long count = stream.count();

collect(): Gather results into collection
  List<String> list = stream.collect(Collectors.toList());

findFirst(), findAny(): Get an element
  Optional<String> first = stream.findFirst();