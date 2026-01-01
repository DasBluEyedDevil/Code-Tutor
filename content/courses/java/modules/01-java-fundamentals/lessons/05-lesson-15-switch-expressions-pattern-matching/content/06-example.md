---
type: "EXAMPLE"
title: "Modern Switch and Pattern Matching in Action"
---

Switch expressions with arrow syntax eliminate fall-through bugs and return values directly. Pattern matching for instanceof combines type checking and casting. Guards with 'when' add conditional logic to patterns.

```java
public class ModernJavaDemo {
    public static void main(String[] args) {
        // Switch expression with arrow syntax
        String day = "WEDNESDAY";
        String activity = switch (day) {
            case "MONDAY", "TUESDAY", "WEDNESDAY", "THURSDAY", "FRIDAY" -> "Work";
            case "SATURDAY" -> "Relax";
            case "SUNDAY" -> "Prepare for week";
            default -> "Unknown";
        };
        System.out.println(day + ": " + activity);
        
        // Pattern matching for instanceof
        Object[] items = {"Hello", 42, 3.14, true};
        for (Object item : items) {
            if (item instanceof String s) {
                System.out.println("String with " + s.length() + " chars");
            } else if (item instanceof Integer n) {
                System.out.println("Integer: " + n);
            } else if (item instanceof Double d) {
                System.out.println("Double: " + d);
            } else {
                System.out.println("Other: " + item);
            }
        }
        
        // Pattern matching in switch
        for (Object item : items) {
            String description = switch (item) {
                case String s when s.length() > 10 -> "Long string";
                case String s -> "String: " + s;
                case Integer i when i < 0 -> "Negative";
                case Integer i -> "Positive: " + i;
                case Double d -> "Double: " + d;
                case Boolean b -> "Boolean: " + b;
                case null -> "null";
                default -> "Unknown";
            };
            System.out.println(description);
        }
    }
}
```
