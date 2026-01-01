---
type: "THEORY"
title: "Functional Interfaces"
---

A lambda can only be used where Java expects a FUNCTIONAL INTERFACE - an interface with exactly one abstract method.

Java provides many built-in functional interfaces in java.util.function:

Predicate<T>: Takes T, returns boolean
  Predicate<String> isEmpty = s -> s.isEmpty();
  isEmpty.test("");  // true

Function<T, R>: Takes T, returns R
  Function<String, Integer> length = s -> s.length();
  length.apply("hello");  // 5

Consumer<T>: Takes T, returns nothing
  Consumer<String> printer = s -> System.out.println(s);
  printer.accept("hello");  // prints: hello

Supplier<T>: Takes nothing, returns T
  Supplier<Double> random = () -> Math.random();
  random.get();  // 0.7234...

These are the building blocks of functional programming in Java.