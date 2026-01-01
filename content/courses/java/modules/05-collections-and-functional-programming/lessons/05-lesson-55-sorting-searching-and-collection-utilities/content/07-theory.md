---
type: "THEORY"
title: "Using Sequenced Collections (Java 21+)"
---

BEFORE Java 21:
ArrayList<String> list = new ArrayList<>();
list.add("A"); list.add("B"); list.add("C");

String first = list.get(0);  // Must use index
String last = list.get(list.size() - 1);  // Awkward!

AFTER Java 21:
String first = list.getFirst();  // Clean!
String last = list.getLast();    // Clean!

REVERSED VIEW:
List<String> reversed = list.reversed();
for (String s : reversed) {
    System.out.println(s);  // Prints C, B, A
}
// Original list unchanged - reversed is a VIEW

LINKEDHASHSET WITH ORDERING:
LinkedHashSet<String> set = new LinkedHashSet<>();
set.add("X"); set.add("Y"); set.add("Z");
set.addFirst("NEW");  // Moves or adds to front
// Now: [NEW, X, Y, Z]

LINKEDHASHMAP ENTRIES:
LinkedHashMap<String, Integer> map = new LinkedHashMap<>();
map.put("A", 1); map.put("B", 2);
Map.Entry<String, Integer> first = map.firstEntry();  // A=1
Map.Entry<String, Integer> last = map.lastEntry();    // B=2