---
type: "THEORY"
title: "Modern Java: Text Blocks for Multi-line Strings"
---

Since Java 15, you can write multi-line strings using TEXT BLOCKS:

OLD WAY (messy, hard to read):
String json = "{\n" +
    "  \"name\": \"Alice\",\n" +
    "  \"age\": 20\n" +
    "}";

MODERN WAY (clean, readable):
String json = """
    {
        "name": "Alice",
        "age": 20
    }
    """;

Text blocks use triple quotes """ to start and end. Everything between them is treated as-is, including line breaks and indentation.

Perfect for:
• JSON data
• SQL queries
• HTML/XML
• Any multi-line text

Key rules:
1. Opening """ must be followed by a newline
2. Closing """ determines the indentation baseline
3. Java automatically removes common leading whitespace