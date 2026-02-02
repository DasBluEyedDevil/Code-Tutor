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
    IO.println(isEven.test(4));  // true
    IO.println(isEven.test(7));  // false
    
    // Function: transforming
    Function<String, String> toUpper = s -> s.toUpperCase();
    IO.println(toUpper.apply("hello"));  // HELLO
    
    // Consumer: performing actions
    List<String> names = List.of("Alice", "Bob", "Charlie");
    names.forEach(name -> IO.println("Hi, " + name));
    
    // Supplier: generating values
    Supplier<UUID> idGenerator = () -> UUID.randomUUID();
    IO.println(idGenerator.get());
    
    // Combining predicates
    Predicate<Integer> isPositive = n -> n > 0;
    Predicate<Integer> isEvenAndPositive = isEven.and(isPositive);
    IO.println(isEvenAndPositive.test(4));   // true
    IO.println(isEvenAndPositive.test(-4));  // false
}
```
