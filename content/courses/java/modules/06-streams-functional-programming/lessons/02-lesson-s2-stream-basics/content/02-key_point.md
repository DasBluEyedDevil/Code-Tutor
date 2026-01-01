---
type: "KEY_POINT"
title: "Creating Streams"
---

There are multiple ways to create streams:

From Collections:
  List<String> list = List.of("a", "b", "c");
  Stream<String> stream = list.stream();

From Arrays:
  String[] arr = {"a", "b", "c"};
  Stream<String> stream = Arrays.stream(arr);

Using Stream.of():
  Stream<String> stream = Stream.of("a", "b", "c");

Using Stream.generate() (infinite):
  Stream<Double> randoms = Stream.generate(Math::random);

Using Stream.iterate() (infinite):
  Stream<Integer> evens = Stream.iterate(0, n -> n + 2);

From files:
  Stream<String> lines = Files.lines(Path.of("file.txt"));

Primitive streams (avoid boxing overhead):
  IntStream ints = IntStream.range(1, 100);  // 1 to 99
  IntStream inclusive = IntStream.rangeClosed(1, 100);  // 1 to 100