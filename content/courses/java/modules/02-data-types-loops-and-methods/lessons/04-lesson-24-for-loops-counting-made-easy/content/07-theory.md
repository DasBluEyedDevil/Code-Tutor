---
type: "THEORY"
title: "Modern Java: The Enhanced For Loop (for-each)"
---

When you just want to process each item in a collection, the ENHANCED FOR LOOP (also called for-each) is cleaner:

TRADITIONAL FOR LOOP:
String[] names = {"Alice", "Bob", "Charlie"};
for (int i = 0; i < names.length; i++) {
    IO.println(names[i]);
}

ENHANCED FOR LOOP (simpler!):
for (String name : names) {
    IO.println(name);
}

Read this as: "for each name in names, do..."

With 'var' (Java 10+), it's even cleaner:
for (var name : names) {
    IO.println(name);
}

Benefits of enhanced for:
• No index variable needed
• No risk of off-by-one errors
• Clearer intent: "process each item"
• Works with arrays AND collections (like ArrayList)