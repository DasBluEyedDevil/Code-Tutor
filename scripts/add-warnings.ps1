# Add WARNING sections to Java Collections lessons
$file = "C:\Users\dasbl\Downloads\Code-Tutor\content\courses\java\course.json"
$content = Get-Content $file -Raw -Encoding UTF8

# 1. Arrays lesson - epoch-3-lesson-1
$arraysOld = @'
"content": "Creating an array:\n\n// Method 1: Declare size, fill later\nint[] numbers = new int[5];\nnumbers[0] = 10;\nnumbers[1] = 20;\n\n// Method 2: Declare with values\nint[] numbers = {10, 20, 30, 40, 50};\n\nAccessing elements:\nSystem.out.println(numbers[0]);  // 10\nSystem.out.println(numbers[4]);  // 50\n\nArray length:\nnumbers.length  // 5\n\nLooping through array:\nfor (int i = 0; i < numbers.length; i++) {\n    System.out.println(numbers[i]);\n}"
            }
          ],
          "challenges": [
'@

$arraysNew = @'
"content": "Creating an array:\n\n// Method 1: Declare size, fill later\nint[] numbers = new int[5];\nnumbers[0] = 10;\nnumbers[1] = 20;\n\n// Method 2: Declare with values\nint[] numbers = {10, 20, 30, 40, 50};\n\nAccessing elements:\nSystem.out.println(numbers[0]);  // 10\nSystem.out.println(numbers[4]);  // 50\n\nArray length:\nnumbers.length  // 5\n\nLooping through array:\nfor (int i = 0; i < numbers.length; i++) {\n    System.out.println(numbers[i]);\n}"
            },
            {
              "type": "WARNING",
              "title": "Common Array Pitfalls",
              "content": "ArrayIndexOutOfBoundsException:\nint[] arr = new int[5];\narr[5] = 10;  // CRASH! Valid indexes are 0-4\n\nArrays start at 0, so arr[arr.length] is ALWAYS invalid.\n\nFixed size cannot change:\nint[] arr = new int[3];\n// Cannot add a 4th element - use ArrayList instead!\n\nDefault values:\n- int[]: all zeros\n- boolean[]: all false\n- Object[]: all null (NullPointerException risk!)\n\nFor dynamic sizing, prefer ArrayList over arrays."
            }
          ],
          "challenges": [
'@

$content = $content.Replace($arraysOld, $arraysNew)

# 2. ArrayList lesson - epoch-3-lesson-2
$arraylistOld = @'
"content": "ADDING:\n- add(item) \u2192 adds to end\n- add(index, item) \u2192 inserts at position\n\nREMOVING:\n- remove(index) \u2192 removes at position\n- remove(object) \u2192 removes first occurrence\n- clear() \u2192 removes all\n\nACCESSING:\n- get(index) \u2192 retrieves element\n- set(index, item) \u2192 replaces element\n\nQUERYING:\n- size() \u2192 number of elements\n- isEmpty() \u2192 true if empty\n- contains(item) \u2192 true if found\n- indexOf(item) \u2192 position of item (-1 if not found)\n\nLOOPING:\nfor (int i = 0; i < list.size(); i++) {\n    System.out.println(list.get(i));\n}\n\n// Enhanced for loop (easier!)\nfor (String item : list) {\n    System.out.println(item);\n}"
            }
          ],
          "challenges": [
            {
              "type": "FREE_CODING",
              "id": "epoch-3-lesson-2-todo",
'@

$arraylistNew = @'
"content": "ADDING:\n- add(item) \u2192 adds to end\n- add(index, item) \u2192 inserts at position\n\nREMOVING:\n- remove(index) \u2192 removes at position\n- remove(object) \u2192 removes first occurrence\n- clear() \u2192 removes all\n\nACCESSING:\n- get(index) \u2192 retrieves element\n- set(index, item) \u2192 replaces element\n\nQUERYING:\n- size() \u2192 number of elements\n- isEmpty() \u2192 true if empty\n- contains(item) \u2192 true if found\n- indexOf(item) \u2192 position of item (-1 if not found)\n\nLOOPING:\nfor (int i = 0; i < list.size(); i++) {\n    System.out.println(list.get(i));\n}\n\n// Enhanced for loop (easier!)\nfor (String item : list) {\n    System.out.println(item);\n}"
            },
            {
              "type": "WARNING",
              "title": "ArrayList Gotchas",
              "content": "IndexOutOfBoundsException:\nArrayList<String> list = new ArrayList<>();\nlist.get(0);  // CRASH! List is empty\nAlways check size() before accessing by index.\n\nRemove while iterating (ConcurrentModificationException):\nfor (String s : list) {\n    if (condition) list.remove(s);  // CRASH!\n}\nUse removeIf() instead:\nlist.removeIf(s -> s.startsWith(\"test\"));\n\nPrimitive remove() confusion:\nArrayList<Integer> nums = new ArrayList<>();\nnums.add(1); nums.add(2);\nnums.remove(1);  // Removes at INDEX 1, not value 1!\nnums.remove(Integer.valueOf(1));  // Removes VALUE 1\n\nJava 21+ Sequenced Collections:\nArrayList now supports getFirst(), getLast(), addFirst(), reversed()."
            }
          ],
          "challenges": [
            {
              "type": "FREE_CODING",
              "id": "epoch-3-lesson-2-todo",
'@

$content = $content.Replace($arraylistOld, $arraylistNew)

# 3. HashMap lesson - epoch-3-lesson-3 - already has warning-like content in THEORY
$hashmapOld = @'
"content": "HashMap<String, Integer> map = new HashMap<>();\nmap.put(\"Alice\", 25);\nmap.put(\"Bob\", 30);\nmap.put(\"Alice\", 26);  // OVERWRITES previous Alice value!\n\nSystem.out.println(map.get(\"Alice\"));  // 26 (not 25)\nSystem.out.println(map.size());  // 2 (not 3)\n\nEach KEY can only appear ONCE.\nIf you put() with an existing key, it REPLACES the value.\n\nBut VALUES can be duplicated:\nmap.put(\"Carol\", 30);  // Same value as Bob - totally fine!"
            }
          ],
          "challenges": [
'@

$hashmapNew = @'
"content": "HashMap<String, Integer> map = new HashMap<>();\nmap.put(\"Alice\", 25);\nmap.put(\"Bob\", 30);\nmap.put(\"Alice\", 26);  // OVERWRITES previous Alice value!\n\nSystem.out.println(map.get(\"Alice\"));  // 26 (not 25)\nSystem.out.println(map.size());  // 2 (not 3)\n\nEach KEY can only appear ONCE.\nIf you put() with an existing key, it REPLACES the value.\n\nBut VALUES can be duplicated:\nmap.put(\"Carol\", 30);  // Same value as Bob - totally fine!"
            },
            {
              "type": "WARNING",
              "title": "HashMap Pitfalls",
              "content": "NullPointerException with get():\nHashMap<String, Integer> map = new HashMap<>();\nint value = map.get(\"missing\");  // CRASH! get() returns null, auto-unboxing fails\n\nUse getOrDefault() instead:\nint value = map.getOrDefault(\"missing\", 0);\n\nMutable keys are dangerous:\nIf you modify a key object after insertion, you cannot find it again!\n\nNo ordering guarantee:\nHashMap does NOT maintain insertion order.\nUse LinkedHashMap for insertion order.\nUse TreeMap for sorted key order.\n\nJava 21+ Note:\nHashMap does NOT implement SequencedMap (no ordering).\nLinkedHashMap does implement SequencedMap with getFirst(), getLast()."
            }
          ],
          "challenges": [
'@

$content = $content.Replace($hashmapOld, $hashmapNew)

# 4. LinkedList lesson - epoch-3-lesson-4
$linkedlistOld = @'
"content": "```java\nA QUEUE is FIFO (First In, First Out) like a line at a store:\n\nLinkedList<String> queue = new LinkedList<>();\n\n// People join the line (add to end)\nqueue.addLast(\"Alice\");\nqueue.addLast(\"Bob\");\nqueue.addLast(\"Carol\");\n\n// Serve customers (remove from front)\nString first = queue.removeFirst();  // \"Alice\"\nString second = queue.removeFirst(); // \"Bob\"\n\n// Who's next?\nString next = queue.getFirst();  // \"Carol\" (peek without removing)\n\nLinkedList is PERFECT for queues because:\n- addLast() is O(1)\n- removeFirst() is O(1)\nBoth operations are FAST!\n```"
            }
          ],
          "challenges": [
'@

$linkedlistNew = @'
"content": "```java\nA QUEUE is FIFO (First In, First Out) like a line at a store:\n\nLinkedList<String> queue = new LinkedList<>();\n\n// People join the line (add to end)\nqueue.addLast(\"Alice\");\nqueue.addLast(\"Bob\");\nqueue.addLast(\"Carol\");\n\n// Serve customers (remove from front)\nString first = queue.removeFirst();  // \"Alice\"\nString second = queue.removeFirst(); // \"Bob\"\n\n// Who's next?\nString next = queue.getFirst();  // \"Carol\" (peek without removing)\n\nLinkedList is PERFECT for queues because:\n- addLast() is O(1)\n- removeFirst() is O(1)\nBoth operations are FAST!\n```"
            },
            {
              "type": "WARNING",
              "title": "LinkedList Pitfalls",
              "content": "NoSuchElementException:\nLinkedList<String> list = new LinkedList<>();\nlist.getFirst();  // CRASH! List is empty\n\nUse peekFirst() for null-safe access:\nString first = list.peekFirst();  // Returns null if empty\n\nRandom access is SLOW:\nlist.get(500);  // Must traverse 500 nodes! O(n)\nUse ArrayList if you need fast random access.\n\nMemory overhead:\nEach element has prev/next pointers - more memory than ArrayList.\n\nJava 21+ Sequenced Collections:\nLinkedList implements SequencedCollection.\nNew methods: reversed() returns reversed view."
            }
          ],
          "challenges": [
'@

$content = $content.Replace($linkedlistOld, $linkedlistNew)

# 5. Sorting lesson - epoch-3-lesson-5
$sortingOld = @'
"content": "All collections implement common interfaces:\n\nCollection (interface)\n  |\n  \u251c\u2500 List (interface)\n  \u2502   \u251c\u2500 ArrayList (class)\n  \u2502   \u2514\u2500 LinkedList (class)\n  \u2502\n  \u251c\u2500 Set (interface) - No duplicates\n  \u2502   \u251c\u2500 HashSet (class)\n  \u2502   \u2514\u2500 TreeSet (class) - Sorted\n  \u2502\n  \u2514\u2500 Queue (interface)\n      \u2514\u2500 LinkedList (class)\n\nMap (separate hierarchy)\n  \u251c\u2500 HashMap (class)\n  \u251c\u2500 LinkedHashMap (maintains order)\n  \u2514\u2500 TreeMap (sorted by key)\n\nCommon methods ALL collections share:\n- add(element)\n- remove(element)\n- size()\n- isEmpty()\n- clear()\n- contains(element)"
            },
'@

$sortingNew = @'
"content": "All collections implement common interfaces:\n\nCollection (interface)\n  |\n  \u251c\u2500 List (interface)\n  \u2502   \u251c\u2500 ArrayList (class)\n  \u2502   \u2514\u2500 LinkedList (class)\n  \u2502\n  \u251c\u2500 Set (interface) - No duplicates\n  \u2502   \u251c\u2500 HashSet (class)\n  \u2502   \u2514\u2500 TreeSet (class) - Sorted\n  \u2502\n  \u2514\u2500 Queue (interface)\n      \u2514\u2500 LinkedList (class)\n\nMap (separate hierarchy)\n  \u251c\u2500 HashMap (class)\n  \u251c\u2500 LinkedHashMap (maintains order)\n  \u2514\u2500 TreeMap (sorted by key)\n\nCommon methods ALL collections share:\n- add(element)\n- remove(element)\n- size()\n- isEmpty()\n- clear()\n- contains(element)"
            },
            {
              "type": "WARNING",
              "title": "Sorting and Collections Pitfalls",
              "content": "Collections.sort() modifies in place:\nList<Integer> nums = List.of(3, 1, 2);\nCollections.sort(nums);  // CRASH! List.of() returns immutable list\n\nUse mutable list:\nList<Integer> nums = new ArrayList<>(List.of(3, 1, 2));\nCollections.sort(nums);  // OK\n\nComparable vs Comparator:\nCustom objects need Comparable or explicit Comparator:\nCollections.sort(people, Comparator.comparing(Person::getName));\n\nNull elements cause NullPointerException:\nCollections.sort(listWithNulls);  // CRASH!\nUse Comparator.nullsFirst() or nullsLast().\n\nJava 21+ Sequenced Collections:\nNew interfaces: SequencedCollection, SequencedSet, SequencedMap.\nNew methods: getFirst(), getLast(), reversed()."
            },
'@

$content = $content.Replace($sortingOld, $sortingNew)

# 6. Lambda lesson - epoch-3-lesson-6
$lambdaOld = @'
"content": "When a lambda just calls an existing method, use a method reference:\n\n// Lambda that calls a method\nlist.forEach(s -> System.out.println(s));\n\n// Method reference (cleaner!)\nlist.forEach(System.out::println);\n\nFOUR TYPES OF METHOD REFERENCES:\n\n1. Static method: ClassName::staticMethod\n   Function<String, Integer> parse = Integer::parseInt;\n   // Same as: s -> Integer.parseInt(s)\n\n2. Instance method of a particular object: object::method\n   String prefix = \"Hello, \";\n   Function<String, String> greeter = prefix::concat;\n   // Same as: s -> prefix.concat(s)\n\n3. Instance method of an arbitrary object: ClassName::method\n   Function<String, String> upper = String::toUpperCase;\n   // Same as: s -> s.toUpperCase()\n\n4. Constructor reference: ClassName::new\n   Supplier<ArrayList<String>> listMaker = ArrayList::new;\n   // Same as: () -> new ArrayList<String>()\n\nMethod references are cleaner when the lambda just delegates to a method."
            },
'@

$lambdaNew = @'
"content": "When a lambda just calls an existing method, use a method reference:\n\n// Lambda that calls a method\nlist.forEach(s -> System.out.println(s));\n\n// Method reference (cleaner!)\nlist.forEach(System.out::println);\n\nFOUR TYPES OF METHOD REFERENCES:\n\n1. Static method: ClassName::staticMethod\n   Function<String, Integer> parse = Integer::parseInt;\n   // Same as: s -> Integer.parseInt(s)\n\n2. Instance method of a particular object: object::method\n   String prefix = \"Hello, \";\n   Function<String, String> greeter = prefix::concat;\n   // Same as: s -> prefix.concat(s)\n\n3. Instance method of an arbitrary object: ClassName::method\n   Function<String, String> upper = String::toUpperCase;\n   // Same as: s -> s.toUpperCase()\n\n4. Constructor reference: ClassName::new\n   Supplier<ArrayList<String>> listMaker = ArrayList::new;\n   // Same as: () -> new ArrayList<String>()\n\nMethod references are cleaner when the lambda just delegates to a method."
            },
            {
              "type": "WARNING",
              "title": "Lambda Expression Pitfalls",
              "content": "Capturing mutable variables:\nint count = 0;\nlist.forEach(s -> count++);  // COMPILE ERROR!\nVariables captured by lambdas must be effectively final.\n\nUse AtomicInteger for counters:\nAtomicInteger count = new AtomicInteger(0);\nlist.forEach(s -> count.incrementAndGet());\n\nShadowing parameters:\n(String s) -> { String s = \"test\"; }  // COMPILE ERROR!\nParameter names cannot be reused inside lambda body.\n\nReturn type ambiguity:\nComparator<String> c = (a, b) -> { a.length() - b.length(); };  // MISSING RETURN!\nWith braces, you need explicit return:\nComparator<String> c = (a, b) -> { return a.length() - b.length(); };\n\nException handling:\nLambdas cannot throw checked exceptions unless functional interface declares them."
            },
'@

$content = $content.Replace($lambdaOld, $lambdaNew)

# 7. Streams lesson - epoch-3-lesson-7
$streamsOld = @'
"content": "DO:\n- Use streams for transforming collections\n- Chain operations for readability\n- Use method references when possible\n- Use parallel streams for CPU-intensive operations on large data\n\nDON'T:\n- Use streams for simple loops (overkill)\n- Modify external state in stream operations\n- Reuse streams (they can only be consumed once)\n- Use parallel streams for I/O operations\n\nPARALLEL STREAMS:\nList<String> result = names.parallelStream()\n    .filter(n -> n.length() > 3)\n    .map(String::toUpperCase)\n    .collect(Collectors.toList());\n\nNote: Parallel streams split work across CPU cores.\nOnly use for CPU-bound operations on large datasets!\n\nWHEN TO USE STREAMS:\n- Filtering, mapping, reducing collections\n- When you want declarative, readable code\n- When operations can be parallelized\n\nWHEN NOT TO USE:\n- Simple iteration with side effects\n- When performance is critical and data is small\n- When you need to modify the original collection"
            }
          ],
          "challenges": [
'@

$streamsNew = @'
"content": "DO:\n- Use streams for transforming collections\n- Chain operations for readability\n- Use method references when possible\n- Use parallel streams for CPU-intensive operations on large data\n\nDON'T:\n- Use streams for simple loops (overkill)\n- Modify external state in stream operations\n- Reuse streams (they can only be consumed once)\n- Use parallel streams for I/O operations\n\nPARALLEL STREAMS:\nList<String> result = names.parallelStream()\n    .filter(n -> n.length() > 3)\n    .map(String::toUpperCase)\n    .collect(Collectors.toList());\n\nNote: Parallel streams split work across CPU cores.\nOnly use for CPU-bound operations on large datasets!\n\nWHEN TO USE STREAMS:\n- Filtering, mapping, reducing collections\n- When you want declarative, readable code\n- When operations can be parallelized\n\nWHEN NOT TO USE:\n- Simple iteration with side effects\n- When performance is critical and data is small\n- When you need to modify the original collection"
            },
            {
              "type": "WARNING",
              "title": "Stream API Pitfalls",
              "content": "Streams can only be consumed ONCE:\nStream<String> stream = names.stream();\nstream.count();  // OK\nstream.forEach(System.out::println);  // IllegalStateException!\n\nLazy evaluation surprises:\nnames.stream().peek(System.out::println);  // Prints NOTHING!\nPeek only runs when terminal operation is called.\n\nCollectors.toList() vs Stream.toList() (Java 16+):\n.collect(Collectors.toList())  // Mutable list\n.toList()  // IMMUTABLE list - cannot modify!\n\nParallel stream ordering:\nParallel streams may not preserve order. Use forEachOrdered() if order matters.\n\nJava 22+ Stream Gatherers (Preview):\nNew gather() operation for custom intermediate operations.\nBuilt-in gatherers: Gatherers.fold(), windowFixed(), scan()."
            }
          ],
          "challenges": [
'@

$content = $content.Replace($streamsOld, $streamsNew)

# Write the file without BOM
$utf8NoBom = New-Object System.Text.UTF8Encoding $false
[System.IO.File]::WriteAllText($file, $content, $utf8NoBom)

Write-Host "Successfully added WARNING sections to all 7 lessons!"
