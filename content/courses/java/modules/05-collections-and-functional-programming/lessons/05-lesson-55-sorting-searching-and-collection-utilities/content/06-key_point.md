---
type: "KEY_POINT"
title: "Java 21: Sequenced Collections (JEP 431)"
---

Java 21 introduced three new interfaces for ordered collections:

SequencedCollection<E> - ordered with first/last access:
- getFirst() / getLast() - get elements
- addFirst(e) / addLast(e) - add elements
- removeFirst() / removeLast() - remove elements
- reversed() - returns reversed view

Implemented by: List, Deque, LinkedHashSet, SortedSet

SequencedSet<E> extends SequencedCollection<E>:
- Same methods, but no duplicates
- addFirst()/addLast() repositions existing elements

Implemented by: LinkedHashSet, TreeSet

SequencedMap<K,V> - ordered key-value pairs:
- firstEntry() / lastEntry()
- pollFirstEntry() / pollLastEntry()
- putFirst(k,v) / putLast(k,v)
- sequencedKeySet() / sequencedValues() / sequencedEntrySet()

Implemented by: LinkedHashMap, TreeMap

Note: HashMap/HashSet do NOT implement these (no order)!