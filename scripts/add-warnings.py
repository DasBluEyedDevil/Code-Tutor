#!/usr/bin/env python3
"""Add WARNING sections to Java Collections lessons using regex replacement."""
import re
import sys

def main():
    file_path = r"C:\Users\dasbl\Downloads\Code-Tutor\content\courses\java\course.json"

    # Read file as text
    with open(file_path, 'r', encoding='utf-8') as f:
        content = f.read()

    # WARNING sections for each lesson (as JSON strings with proper escaping)
    warnings = {
        'epoch-3-lesson-1': r''',
            {
              "type": "WARNING",
              "title": "Common Array Pitfalls",
              "content": "ArrayIndexOutOfBoundsException:\nint[] arr = new int[5];\narr[5] = 10;  // CRASH! Valid indexes are 0-4\n\nArrays start at 0, so arr[arr.length] is ALWAYS invalid.\n\nFixed size cannot change:\nint[] arr = new int[3];\n// Cannot add a 4th element - use ArrayList instead!\n\nDefault values:\n- int[]: all zeros\n- boolean[]: all false\n- Object[]: all null (NullPointerException risk!)\n\nFor dynamic sizing, prefer ArrayList over arrays."
            }''',
        'epoch-3-lesson-2': r''',
            {
              "type": "WARNING",
              "title": "ArrayList Gotchas",
              "content": "IndexOutOfBoundsException:\nArrayList<String> list = new ArrayList<>();\nlist.get(0);  // CRASH! List is empty\nAlways check size() before accessing by index.\n\nRemove while iterating (ConcurrentModificationException):\nfor (String s : list) {\n    if (condition) list.remove(s);  // CRASH!\n}\nUse removeIf() instead:\nlist.removeIf(s -> s.startsWith(\"test\"));\n\nPrimitive remove() confusion:\nArrayList<Integer> nums = new ArrayList<>();\nnums.add(1); nums.add(2);\nnums.remove(1);  // Removes at INDEX 1, not value 1!\nnums.remove(Integer.valueOf(1));  // Removes VALUE 1\n\nJava 21+ Sequenced Collections:\nArrayList now supports getFirst(), getLast(), addFirst(), reversed()."
            }''',
        'epoch-3-lesson-3': r''',
            {
              "type": "WARNING",
              "title": "HashMap Pitfalls",
              "content": "NullPointerException with get():\nHashMap<String, Integer> map = new HashMap<>();\nint value = map.get(\"missing\");  // CRASH! get() returns null, auto-unboxing fails\n\nUse getOrDefault() instead:\nint value = map.getOrDefault(\"missing\", 0);\n\nMutable keys are dangerous:\nIf you modify a key object after insertion, you cannot find it again!\n\nNo ordering guarantee:\nHashMap does NOT maintain insertion order.\nUse LinkedHashMap for insertion order.\nUse TreeMap for sorted key order.\n\nJava 21+ Note:\nHashMap does NOT implement SequencedMap (no ordering).\nLinkedHashMap does implement SequencedMap with getFirst(), getLast()."
            }''',
        'epoch-3-lesson-4': r''',
            {
              "type": "WARNING",
              "title": "LinkedList Pitfalls",
              "content": "NoSuchElementException:\nLinkedList<String> list = new LinkedList<>();\nlist.getFirst();  // CRASH! List is empty\n\nUse peekFirst() for null-safe access:\nString first = list.peekFirst();  // Returns null if empty\n\nRandom access is SLOW:\nlist.get(500);  // Must traverse 500 nodes! O(n)\nUse ArrayList if you need fast random access.\n\nMemory overhead:\nEach element has prev/next pointers - more memory than ArrayList.\n\nJava 21+ Sequenced Collections:\nLinkedList implements SequencedCollection.\nNew methods: reversed() returns reversed view."
            }''',
        'epoch-3-lesson-5': r''',
            {
              "type": "WARNING",
              "title": "Sorting and Collections Pitfalls",
              "content": "Collections.sort() modifies in place:\nList<Integer> nums = List.of(3, 1, 2);\nCollections.sort(nums);  // CRASH! List.of() returns immutable list\n\nUse mutable list:\nList<Integer> nums = new ArrayList<>(List.of(3, 1, 2));\nCollections.sort(nums);  // OK\n\nComparable vs Comparator:\nCustom objects need Comparable or explicit Comparator:\nCollections.sort(people, Comparator.comparing(Person::getName));\n\nNull elements cause NullPointerException:\nCollections.sort(listWithNulls);  // CRASH!\nUse Comparator.nullsFirst() or nullsLast().\n\nJava 21+ Sequenced Collections:\nNew interfaces: SequencedCollection, SequencedSet, SequencedMap.\nNew methods: getFirst(), getLast(), reversed()."
            }''',
        'epoch-3-lesson-6': r''',
            {
              "type": "WARNING",
              "title": "Lambda Expression Pitfalls",
              "content": "Capturing mutable variables:\nint count = 0;\nlist.forEach(s -> count++);  // COMPILE ERROR!\nVariables captured by lambdas must be effectively final.\n\nUse AtomicInteger for counters:\nAtomicInteger count = new AtomicInteger(0);\nlist.forEach(s -> count.incrementAndGet());\n\nShadowing parameters:\n(String s) -> { String s = \"test\"; }  // COMPILE ERROR!\nParameter names cannot be reused inside lambda body.\n\nReturn type ambiguity:\nComparator<String> c = (a, b) -> { a.length() - b.length(); };  // MISSING RETURN!\nWith braces, you need explicit return:\nComparator<String> c = (a, b) -> { return a.length() - b.length(); };\n\nException handling:\nLambdas cannot throw checked exceptions unless functional interface declares them."
            }''',
        'epoch-3-lesson-7': r''',
            {
              "type": "WARNING",
              "title": "Stream API Pitfalls",
              "content": "Streams can only be consumed ONCE:\nStream<String> stream = names.stream();\nstream.count();  // OK\nstream.forEach(System.out::println);  // IllegalStateException!\n\nLazy evaluation surprises:\nnames.stream().peek(System.out::println);  // Prints NOTHING!\nPeek only runs when terminal operation is called.\n\nCollectors.toList() vs Stream.toList() (Java 16+):\n.collect(Collectors.toList())  // Mutable list\n.toList()  // IMMUTABLE list - cannot modify!\n\nParallel stream ordering:\nParallel streams may not preserve order. Use forEachOrdered() if order matters.\n\nJava 22+ Stream Gatherers (Preview):\nNew gather() operation for custom intermediate operations.\nBuilt-in gatherers: Gatherers.fold(), windowFixed(), scan()."
            }'''
    }

    # Search patterns for each lesson's contentSections closing bracket
    # We look for the last section in contentSections, then add WARNING before the closing ]
    patterns = {
        'epoch-3-lesson-1': (
            r'("id": "epoch-3-lesson-1"[^}]*"contentSections": \[[^\]]*"type": "THEORY"[^}]*"title": "Array Syntax"[^}]*\})\s*(\])',
            r'\1' + warnings['epoch-3-lesson-1'] + r'\n          \2'
        ),
        'epoch-3-lesson-2': (
            r'("id": "epoch-3-lesson-2"[^}]*"contentSections": \[[^\]]*"type": "KEY_POINT"[^}]*"title": "Common ArrayList Methods"[^}]*\})\s*(\])',
            r'\1' + warnings['epoch-3-lesson-2'] + r'\n          \2'
        ),
        'epoch-3-lesson-3': (
            r'("id": "epoch-3-lesson-3"[^}]*"contentSections": \[[^\]]*"title": "[^"]*HashMap Keys Must Be Unique"[^}]*\})\s*(\])',
            r'\1' + warnings['epoch-3-lesson-3'] + r'\n          \2'
        ),
        'epoch-3-lesson-4': (
            r'("id": "epoch-3-lesson-4"[^}]*"contentSections": \[[^\]]*"title": "[^"]*Queue Example[^"]*"[^}]*\})\s*(\])',
            r'\1' + warnings['epoch-3-lesson-4'] + r'\n          \2'
        ),
        'epoch-3-lesson-5': (
            r'("id": "epoch-3-lesson-5"[^}]*"contentSections": \[[^\]]*"title": "[^"]*Leaderboard[^"]*"[^}]*\})\s*(\])',
            r'\1' + warnings['epoch-3-lesson-5'] + r'\n          \2'
        ),
        'epoch-3-lesson-6': (
            r'("id": "epoch-3-lesson-6"[^}]*"contentSections": \[[^\]]*"title": "Method References"[^}]*\})\s*(\])',
            r'\1' + warnings['epoch-3-lesson-6'] + r'\n          \2'
        ),
        'epoch-3-lesson-7': (
            r'("id": "epoch-3-lesson-7"[^}]*"contentSections": \[[^\]]*"title": "Best Practices for Streams"[^}]*\})\s*(\])',
            r'\1' + warnings['epoch-3-lesson-7'] + r'\n          \2'
        )
    }

    added = 0
    for lesson_id, (pattern, replacement) in patterns.items():
        new_content, count = re.subn(pattern, replacement, content, flags=re.DOTALL)
        if count > 0:
            content = new_content
            print(f"Added WARNING to {lesson_id}")
            added += 1
        else:
            print(f"Pattern not found for {lesson_id}")

    # Write back
    with open(file_path, 'w', encoding='utf-8', newline='\n') as f:
        f.write(content)

    print(f"\nTotal: Added {added} WARNING sections")

if __name__ == '__main__':
    main()
