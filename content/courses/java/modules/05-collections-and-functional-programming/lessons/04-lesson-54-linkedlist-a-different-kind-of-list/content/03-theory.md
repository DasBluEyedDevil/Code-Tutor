---
type: "THEORY"
title: "LinkedList Syntax"
---

IMPORT:
import java.util.LinkedList;

CREATING:
LinkedList<String> names = new LinkedList<>();

LinkedList has ALL ArrayList methods:
names.add("Alice");
names.add("Bob");
names.get(0);  // "Alice"
names.remove(1);  // Remove Bob

PLUS special methods for ends:
names.addFirst("First");  // Add to beginning
names.addLast("Last");    // Add to end
names.getFirst();  // Get first element
names.getLast();   // Get last element
names.removeFirst();  // Remove first
names.removeLast();   // Remove last

These operations are O(1) - VERY FAST!