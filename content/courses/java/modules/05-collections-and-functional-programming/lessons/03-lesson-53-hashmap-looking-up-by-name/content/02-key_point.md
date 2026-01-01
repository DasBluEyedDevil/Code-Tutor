---
type: "KEY_POINT"
title: "HashMap is Like a Dictionary"
---

ENGLISH DICTIONARY:
- Look up "apple" (KEY)
- Get definition: "a round fruit..." (VALUE)
- Don't need to know page number
- Direct lookup!

JAVA HASHMAP:
HashMap<String, String> phonebook = new HashMap<>();
phonebook.put("Alice", "555-1234");  // KEY → VALUE
phonebook.put("Bob", "555-5678");

String aliceNumber = phonebook.get("Alice");  // "555-1234"

Fast lookup by KEY, not index!