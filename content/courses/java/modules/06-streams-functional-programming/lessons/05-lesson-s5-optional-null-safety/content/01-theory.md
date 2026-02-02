---
type: "THEORY"
title: "The Null Problem"
---

Null references are called 'the billion dollar mistake' by their inventor, Tony Hoare. They cause NullPointerExceptions - the most common Java bug.

The problem:
  String name = findUserById(42);  // might return null!
  int length = name.length();       // BOOM! NullPointerException

Traditional defensive coding is ugly:
  String name = findUserById(42);
  if (name != null) {
      int length = name.length();  // Safe, but verbose
  }

Optional<T> is Java's solution - a container that may or may not contain a value. It forces you to handle the 'no value' case explicitly.

Optional<String> name = findUserById(42);
name.ifPresent(n -> IO.println(n.length()));

Optional doesn't eliminate nulls, but it makes 'might be absent' explicit in the API.