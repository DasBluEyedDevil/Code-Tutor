---
type: "THEORY"
title: "Common Stream Operations"
---

FILTER - Keep elements matching condition
List<Integer> evens = numbers.stream()
    .filter(n -> n % 2 == 0)
    .collect(Collectors.toList());

MAP - Transform each element
List<Integer> lengths = names.stream()
    .map(String::length)
    .collect(Collectors.toList());

SORTED - Sort elements
List<String> sorted = names.stream()
    .sorted()  // Natural order
    .collect(Collectors.toList());

List<String> byLength = names.stream()
    .sorted(Comparator.comparing(String::length))
    .collect(Collectors.toList());

DISTINCT - Remove duplicates
List<Integer> unique = numbers.stream()
    .distinct()
    .collect(Collectors.toList());

LIMIT and SKIP - Pagination
List<String> page = names.stream()
    .skip(10)      // Skip first 10
    .limit(5)      // Take next 5
    .collect(Collectors.toList());

FLATMAP - Flatten nested streams
List<String> allWords = sentences.stream()
    .flatMap(s -> Arrays.stream(s.split(" ")))
    .collect(Collectors.toList());