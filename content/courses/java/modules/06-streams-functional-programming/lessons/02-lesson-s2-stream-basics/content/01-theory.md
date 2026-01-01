---
type: "THEORY"
title: "What Are Streams?"
---

A Stream is a sequence of elements that supports functional-style operations. Unlike collections, streams:

1. DON'T STORE DATA - they process elements from a source
2. ARE LAZY - operations execute only when needed
3. CAN BE INFINITE - generate elements on demand
4. ARE CONSUMED ONCE - you cannot reuse a stream after a terminal operation

Think of streams as a pipeline: data flows through, gets transformed, and produces a result.

// Collection: data at rest
List<String> names = List.of("Alice", "Bob", "Charlie");

// Stream: data in motion
names.stream()
     .filter(name -> name.startsWith("A"))
     .map(String::toUpperCase)
     .forEach(System.out::println);  // ALICE

Streams enable declarative programming - you say WHAT you want, not HOW to do it.