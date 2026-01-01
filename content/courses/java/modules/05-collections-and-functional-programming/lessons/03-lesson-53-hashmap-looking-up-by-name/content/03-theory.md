---
type: "THEORY"
title: "HashMap Syntax and Operations"
---

IMPORT:
import java.util.HashMap;

CREATING:
HashMap<String, Integer> ages = new HashMap<>();
// <KeyType, ValueType>

ADDING/UPDATING:
ages.put("Alice", 25);  // Add Alice → 25
ages.put("Bob", 30);    // Add Bob → 30
ages.put("Alice", 26);  // UPDATE Alice to 26 (overwrites)

GETTING VALUES:
ages.get("Alice");  // 26
ages.get("Bob");    // 30
ages.get("Carol");  // null (not found)

CHECKING:
ages.containsKey("Alice");  // true
ages.containsValue(30);  // true
ages.isEmpty();  // false

REMOVING:
ages.remove("Bob");  // Remove Bob's entry

SIZE:
ages.size();  // 1 (only Alice remains)