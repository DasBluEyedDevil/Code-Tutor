---
type: "EXAMPLE"
title: "Lambdas in Action"
---

Lambdas work with functional interfaces like Comparator, Predicate, Function, and Consumer. Method references (::) provide cleaner syntax when a lambda just calls an existing method.

```java
import java.util.*;
import java.util.function.*;

public class LambdaDemo {
    public static void main(String[] args) {
        // Lambda as Comparator
        List<String> names = new ArrayList<>(List.of("Alice", "Bob", "Charlie", "Dan"));
        names.sort((a, b) -> a.length() - b.length());
        System.out.println(names); // [Bob, Dan, Alice, Charlie]
        
        // Lambda with forEach
        names.forEach(name -> System.out.println("Hello, " + name));
        
        // Method reference
        names.forEach(System.out::println);
        
        // Predicate - filter condition
        Predicate<String> startsWithA = s -> s.startsWith("A");
        System.out.println(startsWithA.test("Alice")); // true
        System.out.println(startsWithA.test("Bob"));   // false
        
        // Function - transform
        Function<String, Integer> getLength = String::length;
        System.out.println(getLength.apply("Hello")); // 5
        
        // Consumer - action
        Consumer<String> shout = s -> System.out.println(s.toUpperCase() + "!");
        shout.accept("hello"); // HELLO!
        
        // Custom functional interface
        Calculator add = (a, b) -> a + b;
        Calculator multiply = (a, b) -> a * b;
        System.out.println(operate(10, 5, add));      // 15
        System.out.println(operate(10, 5, multiply)); // 50
    }
    
    @FunctionalInterface
    interface Calculator {
        int calculate(int a, int b);
    }
    
    static int operate(int a, int b, Calculator calc) {
        return calc.calculate(a, b);
    }
}
```
