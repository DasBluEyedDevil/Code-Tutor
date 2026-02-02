---
type: "THEORY"
title: "Putting It All Together"
---

Here's a complete modern Java 25 program:

import module java.base;

void main() {
    var numbers = List.of(1, 2, 3, 4, 5);

    // Process each number
    for (var num : numbers) {
        IO.println("Number: " + num);
    }

    // Handle errors with unnamed variable
    try {
        var data = Files.readString(Path.of("data.txt"));
        IO.println(data);
    } catch (IOException _) {
        IO.println("Could not read file");
    }
}

Notice:
- One import for everything
- No class declaration
- No static main
- IO.println() for console output (the IO class is in java.lang, auto-imported)
- _ for unused exception