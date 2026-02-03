---
type: "THEORY"
title: "Enhanced For Loop (for-each)"
---

The EASIEST way to iterate through collections:

ArrayList<String> fruits = new ArrayList<>();
fruits.add("Apple");
fruits.add("Banana");
fruits.add("Cherry");

// Traditional for loop
for (int i = 0; i < fruits.size(); i++) {
    IO.println(fruits.get(i));
}

// Enhanced for loop (for-each)
for (String fruit : fruits) {  // Read as: "for each fruit in fruits"
    IO.println(fruit);
}

Benefits:
- Cleaner syntax
- No index errors
- Works with ANY collection

Works with HashMap too:
HashMap<String, Integer> ages = new HashMap<>();
for (String key : ages.keySet()) {
    IO.println(key + ": " + ages.get(key));
}