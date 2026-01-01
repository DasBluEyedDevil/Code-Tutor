---
type: "THEORY"
title: "The Problem: Passing Behavior Around"
---

Imagine you want to sort a list of names by length instead of alphabetically:

// Traditional way: Create a whole class just to pass behavior
class LengthComparator implements Comparator<String> {
    public int compare(String a, String b) {
        return a.length() - b.length();
    }
}
Collections.sort(names, new LengthComparator());

// Or use anonymous class (still verbose)
Collections.sort(names, new Comparator<String>() {
    public int compare(String a, String b) {
        return a.length() - b.length();
    }
});

That's A LOT of code just to say: "sort by length"!

Java 8 introduced LAMBDA EXPRESSIONS to pass behavior in a concise way:

Collections.sort(names, (a, b) -> a.length() - b.length());

One line instead of 5+!