---
type: "WARNING"
title: "HashMap Pitfalls"
---

NullPointerException with get():
HashMap<String, Integer> map = new HashMap<>();
int value = map.get("missing");  // CRASH! get() returns null, auto-unboxing fails

Use getOrDefault() instead:
int value = map.getOrDefault("missing", 0);

Mutable keys are dangerous:
If you modify a key object after insertion, you cannot find it again!

No ordering guarantee:
HashMap does NOT maintain insertion order.
Use LinkedHashMap for insertion order.
Use TreeMap for sorted key order.

Java 21+ Note:
HashMap does NOT implement SequencedMap (no ordering).
LinkedHashMap does implement SequencedMap with getFirst(), getLast().