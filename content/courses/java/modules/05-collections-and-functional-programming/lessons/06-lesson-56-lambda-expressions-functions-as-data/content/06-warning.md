---
type: "WARNING"
title: "Lambda Expression Pitfalls"
---

Capturing mutable variables:
int count = 0;
list.forEach(s -> count++);  // COMPILE ERROR!
Variables captured by lambdas must be effectively final.

Use AtomicInteger for counters:
AtomicInteger count = new AtomicInteger(0);
list.forEach(s -> count.incrementAndGet());

Shadowing parameters:
(String s) -> { String s = "test"; }  // COMPILE ERROR!
Parameter names cannot be reused inside lambda body.

Return type ambiguity:
Comparator<String> c = (a, b) -> { a.length() - b.length(); };  // MISSING RETURN!
With braces, you need explicit return:
Comparator<String> c = (a, b) -> { return a.length() - b.length(); };

Exception handling:
Lambdas cannot throw checked exceptions unless functional interface declares them.