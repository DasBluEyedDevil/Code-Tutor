---
type: "THEORY"
title: "⚠️ HashMap Keys Must Be Unique"
---

HashMap<String, Integer> map = new HashMap<>();
map.put("Alice", 25);
map.put("Bob", 30);
map.put("Alice", 26);  // OVERWRITES previous Alice value!

System.out.println(map.get("Alice"));  // 26 (not 25)
System.out.println(map.size());  // 2 (not 3)

Each KEY can only appear ONCE.
If you put() with an existing key, it REPLACES the value.

But VALUES can be duplicated:
map.put("Carol", 30);  // Same value as Bob - totally fine!