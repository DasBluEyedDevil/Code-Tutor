---
type: "THEORY"
title: "flatMap: Flattening Nested Structures"
---

When you have nested collections or need to expand elements, flatMap() is your tool. It maps each element to a stream, then flattens all streams into one.

map() vs flatMap():

// map: one-to-one transformation
List<String> words = List.of("hello", "world");
words.stream().map(String::toUpperCase);  // Stream of "HELLO", "WORLD"

// flatMap: one-to-many, then flatten
List<List<Integer>> nested = List.of(
    List.of(1, 2),
    List.of(3, 4, 5)
);
nested.stream()
    .flatMap(List::stream)  // Flatten to: 1, 2, 3, 4, 5
    .forEach(System.out::println);

// Common use: split words into characters
"hello world".chars()  // IntStream of character codes

List.of("hello", "world").stream()
    .flatMap(word -> word.chars().mapToObj(c -> (char) c))
    .forEach(System.out::print);  // helloworld