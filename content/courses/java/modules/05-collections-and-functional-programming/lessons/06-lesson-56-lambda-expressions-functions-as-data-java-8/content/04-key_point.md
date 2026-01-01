---
type: "KEY_POINT"
title: "Method References"
---

When a lambda just calls an existing method, use a method reference:

// Lambda that calls a method
list.forEach(s -> System.out.println(s));

// Method reference (cleaner!)
list.forEach(System.out::println);

FOUR TYPES OF METHOD REFERENCES:

1. Static method: ClassName::staticMethod
   Function<String, Integer> parse = Integer::parseInt;
   // Same as: s -> Integer.parseInt(s)

2. Instance method of a particular object: object::method
   String prefix = "Hello, ";
   Function<String, String> greeter = prefix::concat;
   // Same as: s -> prefix.concat(s)

3. Instance method of an arbitrary object: ClassName::method
   Function<String, String> upper = String::toUpperCase;
   // Same as: s -> s.toUpperCase()

4. Constructor reference: ClassName::new
   Supplier<ArrayList<String>> listMaker = ArrayList::new;
   // Same as: () -> new ArrayList<String>()

Method references are cleaner when the lambda just delegates to a method.