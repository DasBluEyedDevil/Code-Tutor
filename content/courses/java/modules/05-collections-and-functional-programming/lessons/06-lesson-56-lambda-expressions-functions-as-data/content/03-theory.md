---
type: "THEORY"
title: "Functional Interfaces"
---

Lambdas can only be used where a FUNCTIONAL INTERFACE is expected.

A functional interface has exactly ONE abstract method:

@FunctionalInterface
interface Calculator {
    int calculate(int a, int b);  // ONE abstract method
}

// Use lambda to implement it
Calculator add = (a, b) -> a + b;
Calculator multiply = (a, b) -> a * b;

System.out.println(add.calculate(5, 3));      // 8
System.out.println(multiply.calculate(5, 3)); // 15

COMMON BUILT-IN FUNCTIONAL INTERFACES:

1. Predicate<T> - takes T, returns boolean
   Predicate<String> isEmpty = s -> s.isEmpty();

2. Function<T, R> - takes T, returns R
   Function<String, Integer> length = s -> s.length();

3. Consumer<T> - takes T, returns nothing
   Consumer<String> printer = s -> System.out.println(s);

4. Supplier<T> - takes nothing, returns T
   Supplier<Double> random = () -> Math.random();

5. Comparator<T> - takes two T, returns int
   Comparator<String> byLength = (a, b) -> a.length() - b.length();