---
type: "THEORY"
title: "ArrayList Syntax and Operations"
---

IMPORT:
import java.util.ArrayList;

CREATING:
ArrayList<String> names = new ArrayList<>();
// <String> = type parameter (ArrayList of Strings)

ADDING ELEMENTS:
names.add("Alice");  // ["Alice"]
names.add("Bob");    // ["Alice", "Bob"]
names.add("Carol");  // ["Alice", "Bob", "Carol"]

ACCESSING:
names.get(0);  // "Alice" (like array[0])
names.get(1);  // "Bob"

SIZE:
names.size();  // 3 (NOT .length like arrays)

REMOVING:
names.remove(1);  // Remove "Bob"
// Now: ["Alice", "Carol"] - automatically shifts!

CHECKING:
names.contains("Alice");  // true
names.isEmpty();  // false