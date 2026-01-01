---
type: "THEORY"
title: "💻 Queue Example with LinkedList"
---

```java
A QUEUE is FIFO (First In, First Out) like a line at a store:

LinkedList<String> queue = new LinkedList<>();

// People join the line (add to end)
queue.addLast("Alice");
queue.addLast("Bob");
queue.addLast("Carol");

// Serve customers (remove from front)
String first = queue.removeFirst();  // "Alice"
String second = queue.removeFirst(); // "Bob"

// Who's next?
String next = queue.getFirst();  // "Carol" (peek without removing)

LinkedList is PERFECT for queues because:
- addLast() is O(1)
- removeFirst() is O(1)
Both operations are FAST!
```