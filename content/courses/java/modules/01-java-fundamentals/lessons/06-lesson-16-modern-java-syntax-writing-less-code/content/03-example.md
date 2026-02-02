---
type: "EXAMPLE"
title: "Before and After: A Complete Comparison"
---

The compact source file syntax (JEP 512, finalized in Java 25) dramatically reduces boilerplate for beginners. Here's a side-by-side comparison showing how much cleaner modern Java code can be.

```java
// ========== OLD WAY (Java 8-21 LTS) ==========
package com.example;

public class TemperatureConverter {
    public static void main(String[] args) {
        double celsius = 25.0;
        double fahrenheit = celsius * 9/5 + 32;
        System.out.println(celsius + "C = " + fahrenheit + "F");
    }
}

// ========== NEW WAY (Java 25+) ==========
void main() {
    double celsius = 25.0;
    double fahrenheit = celsius * 9/5 + 32;
    IO.println(celsius + "C = " + fahrenheit + "F");
}

// Same output, 60% less code!
```
