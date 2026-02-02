---
type: "EXAMPLE"
title: "Compact vs Traditional: A Side-by-Side Comparison"
---

Here's how the same program looks in compact source file syntax (what you've been using) versus the traditional syntax you'll see in older resources.

```java
// ========== COMPACT (what you write) ==========
void main() {
    double celsius = 25.0;
    double fahrenheit = celsius * 9/5 + 32;
    IO.println(celsius + "C = " + fahrenheit + "F");
}

// ========== TRADITIONAL (what older tutorials show) ==========
package com.example;

public class TemperatureConverter {
    public static void main(String[] args) {
        double celsius = 25.0;
        double fahrenheit = celsius * 9/5 + 32;
        System.out.println(celsius + "C = " + fahrenheit + "F");
    }
}

// Same output -- the compact version focuses on what matters!
```
