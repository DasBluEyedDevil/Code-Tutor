---
type: "EXAMPLE"
title: "Practical Lambda Examples"
---

Here are real-world uses of lambdas with functional interfaces:

```java
import java.util.function.*;
import java.util.*;

void main() {
    // Predicate: filtering
    Predicate<Integer> isEven = n -> n % 2 == 0;
    System.out.println(isEven.test(4));  // true
    System.out.println(isEven.test(7));  // false
    
    // Function: transforming
    Function<String, String> toUpper = s -> s.toUpperCase();
    System.out.println(toUpper.apply("hello"));  // HELLO
    
    // Consumer: performing actions
    List<String> names = List.of("Alice", "Bob", "Charlie");
    names.forEach(name -> System.out.println("Hi, " + name));
    
    // Supplier: generating values
    Supplier<UUID> idGenerator = () -> UUID.randomUUID();
    System.out.println(idGenerator.get());
    
    // Combining predicates
    Predicate<Integer> isPositive = n -> n > 0;
    Predicate<Integer> isEvenAndPositive = isEven.and(isPositive);
    System.out.println(isEvenAndPositive.test(4));   // true
    System.out.println(isEvenAndPositive.test(-4));  // false
}
```
