---
type: "EXAMPLE"
title: "Lambdas in Action"
---

Lambdas work with functional interfaces like Comparator, Predicate, Function, and Consumer. Method references (::) provide cleaner syntax when a lambda just calls an existing method.

```java
import module java.base;

@FunctionalInterface
interface Calculator {
    int calculate(int a, int b);
}

int operate(int a, int b, Calculator calc) {
    return calc.calculate(a, b);
}

void main() {
    // Lambda as Comparator
    var names = new ArrayList<>(List.of("Alice", "Bob", "Charlie", "Dan"));
    names.sort((a, b) -> a.length() - b.length());
    IO.println(names); // [Bob, Dan, Alice, Charlie]

    // Lambda with forEach
    names.forEach(name -> IO.println("Hello, " + name));

    // Method reference
    names.forEach(IO::println);

    // Predicate - filter condition
    Predicate<String> startsWithA = s -> s.startsWith("A");
    IO.println(startsWithA.test("Alice")); // true
    IO.println(startsWithA.test("Bob"));   // false

    // Function - transform
    Function<String, Integer> getLength = String::length;
    IO.println(getLength.apply("Hello")); // 5

    // Consumer - action
    Consumer<String> shout = s -> IO.println(s.toUpperCase() + "!");
    shout.accept("hello"); // HELLO!

    // Custom functional interface
    Calculator add = (a, b) -> a + b;
    Calculator multiply = (a, b) -> a * b;
    IO.println(operate(10, 5, add));      // 15
    IO.println(operate(10, 5, multiply)); // 50
}
```
