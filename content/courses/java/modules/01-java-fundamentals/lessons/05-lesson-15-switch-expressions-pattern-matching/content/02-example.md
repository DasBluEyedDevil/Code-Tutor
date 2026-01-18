---
type: "EXAMPLE"
title: "Modern Switch Expressions"
---

Modern Java uses the arrow (`->`) syntax for cleaner switches.

```java
import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        System.out.println("Enter a day of the week:");
        String day = scanner.nextLine().toUpperCase();

        String dayType = switch (day) {
            case "MONDAY", "TUESDAY", "WEDNESDAY", "THURSDAY", "FRIDAY" -> "Weekday";
            case "SATURDAY", "SUNDAY" -> "Weekend";
            default -> "Invalid day";
        };

        System.out.println(day + " is a " + dayType);
    }
}
```

### Expected Output

```
Enter a day of the week:
MONDAY
MONDAY is a Weekday
```

### Key Differences
*   **Arrow (`->`)**: Replaces the colon (`:`).
*   **No `break` needed**: The code does NOT fall through.
*   **Returns a value**: The result is assigned directly to `dayType`.
