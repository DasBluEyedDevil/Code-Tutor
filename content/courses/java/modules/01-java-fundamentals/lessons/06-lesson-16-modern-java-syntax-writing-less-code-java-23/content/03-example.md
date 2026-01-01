---
type: "EXAMPLE"
title: "Before and After: A Complete Comparison"
---

The new implicit main syntax dramatically reduces boilerplate for beginners. Here's a side-by-side comparison showing how much cleaner Java 23 code can be.

```java
// ========== OLD WAY (Java 8-22) ==========
package com.example;

public class TemperatureConverter {
    public static void main(String[] args) {
        double celsius = 25.0;
        double fahrenheit = celsius * 9/5 + 32;
        System.out.println(celsius + "C = " + fahrenheit + "F");
    }
}

// ========== NEW WAY (Java 23+) ==========
void main() {
    double celsius = 25.0;
    double fahrenheit = celsius * 9/5 + 32;
    println(celsius + "C = " + fahrenheit + "F");
}

// Same output, 60% less code!
```
