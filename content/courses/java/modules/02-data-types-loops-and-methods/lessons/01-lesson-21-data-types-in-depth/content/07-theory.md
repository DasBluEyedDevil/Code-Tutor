---
type: "THEORY"
title: "String Formatting with .formatted()"
---

When combining variables with strings, you have several options:

1. STRING CONCATENATION (basic, but messy):
String msg = "Hello, " + name + "! You are " + age + " years old.";

2. String.format() (traditional):
String msg = String.format("Hello, %s! You are %d years old.", name, age);

3. .formatted() (modern, Java 15+):
String msg = "Hello, %s! You are %d years old.".formatted(name, age);

The .formatted() method is cleaner because the template comes first!

Combining text blocks with .formatted():

String template = """
    Dear %s,
    Your order #%d is ready.
    Total: $%.2f
    """;
String email = template.formatted("Alice", 12345, 99.99);

Format specifiers:
• %s = String (or any object)
• %d = Integer (whole number)
• %f = Floating point (decimal)
• %.2f = Decimal with 2 decimal places
• %n = Newline (platform-independent)