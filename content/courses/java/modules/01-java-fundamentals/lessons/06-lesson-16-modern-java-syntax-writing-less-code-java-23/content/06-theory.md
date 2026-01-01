---
type: "THEORY"
title: "Putting It All Together"
---

Here's a complete modern Java 23 program:

import module java.base;

void main() {
    var numbers = List.of(1, 2, 3, 4, 5);
    
    // Process each number
    for (var num : numbers) {
        println("Number: " + num);
    }
    
    // Handle errors with unnamed variable
    try {
        var data = Files.readString(Path.of("data.txt"));
        println(data);
    } catch (IOException _) {
        println("Could not read file");
    }
}

Notice:
- One import for everything
- No class declaration
- No static main
- println() works directly
- _ for unused exception