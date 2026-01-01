---
type: "EXAMPLE"
title: "Using var in Practice"
---

The var keyword enables local type inference, reducing boilerplate while maintaining type safety. It's especially useful for complex generic types and in for-each loops where the type is obvious from context.

```java
public class VarExample {
    public static void main(String[] args) {
        // Instead of: String message = "Hello, World!";
        var message = "Hello, World!";
        
        // Instead of: int count = 10;
        var count = 10;
        
        // Instead of: double price = 19.99;
        var price = 19.99;
        
        // Especially useful for complex types:
        // Instead of: ArrayList<String> names = new ArrayList<String>();
        var names = new ArrayList<String>();
        names.add("Alice");
        names.add("Bob");
        
        // In for-each loops:
        for (var name : names) {
            System.out.println(name);
        }
        
        // With Map:
        var scores = new HashMap<String, Integer>();
        scores.put("Alice", 95);
        scores.put("Bob", 87);
        
        for (var entry : scores.entrySet()) {
            System.out.println(entry.getKey() + ": " + entry.getValue());
        }
    }
}
```
