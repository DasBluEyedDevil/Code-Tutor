---
type: "THEORY"
title: "Looping Through a HashMap"
---

You can iterate over KEYS, VALUES, or ENTRIES:

HashMap<String, Integer> scores = new HashMap<>();
scores.put("Alice", 95);
scores.put("Bob", 87);
scores.put("Carol", 92);

// METHOD 1: Loop through keys
for (String name : scores.keySet()) {
    System.out.println(name + ": " + scores.get(name));
}

// METHOD 2: Loop through entries (KEY-VALUE pairs)
for (Map.Entry<String, Integer> entry : scores.entrySet()) {
    System.out.println(entry.getKey() + ": " + entry.getValue());
}

// METHOD 3: Loop through values only
for (Integer score : scores.values()) {
    System.out.println(score);
}

Note: HashMap does NOT maintain order (use LinkedHashMap for order)