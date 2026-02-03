---
type: "THEORY"
title: "Common For Loop Patterns"
---

1. COUNT FROM 0 TO N (exclusive)
for (int i = 0; i < 10; i++) {
    // Runs 10 times: i = 0, 1, 2...9
}
This is THE most common pattern in programming!

2. COUNT FROM 1 TO N (inclusive)
for (int i = 1; i <= 10; i++) {
    // Runs 10 times: i = 1, 2, 3...10
}

3. COUNT BACKWARDS
for (int i = 10; i > 0; i--) {
    // Runs 10 times: i = 10, 9, 8...1
}

4. COUNT BY 2s
for (int i = 0; i < 10; i += 2) {
    // Runs 5 times: i = 0, 2, 4, 6, 8
}

5. ITERATE OVER CHARACTERS
String word = "Hello";
for (int i = 0; i < word.length(); i++) {
    IO.println(word.charAt(i));
    // Prints: H e l l o (each on new line)
}