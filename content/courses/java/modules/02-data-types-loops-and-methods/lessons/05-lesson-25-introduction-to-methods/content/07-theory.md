---
type: "THEORY"
title: "Preview: Methods as Values (Method References)"
---

Here's something powerful you'll explore more in the Streams module:

In Java, methods can be passed around as VALUES, not just called!

TRADITIONAL: Call methods directly
void printNumber(int n) {
    IO.println(n);
}
for (int i = 1; i <= 5; i++) {
    printNumber(i);  // Call the method
}

MODERN: Pass the method itself using ::
List<Integer> numbers = List.of(1, 2, 3, 4, 5);
numbers.forEach(System.out::println);  // Pass the method!

The :: syntax creates a "method reference" - a way to refer to a method without calling it. The forEach then calls it for each item.

This enables a powerful style called FUNCTIONAL PROGRAMMING:
• Pass methods to other methods
• Transform data with map, filter, reduce
• Write concise, expressive code

Don't worry if this seems advanced - we'll cover it fully in the Streams module. For now, just know that methods in Java are more flexible than they first appear!