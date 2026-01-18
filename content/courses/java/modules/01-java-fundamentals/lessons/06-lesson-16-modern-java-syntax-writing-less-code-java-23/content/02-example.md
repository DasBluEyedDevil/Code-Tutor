---
type: "EXAMPLE"
title: "Modern Main Method"
---

Java 21+ lets you write programs without class declarations and simplifies the main method.

### Old Way (Java 8-20)
```java
public class Main {
    public static void main(String[] args) {
        System.out.println("Hello, World!");
    }
}
```

### New Way (Java 21+)
```java
void main() {
    System.out.println("Hello, World!");
}
```

### Key Changes
*   **No `public class`**: Java automatically creates one for you.
*   **No `static`**: The main method can be an instance method.
*   **No `String[] args`**: You can omit the arguments if you don't need them.
*   **`println()`**: (Preview feature in some versions) You can sometimes skip `System.out`.

*Note: For this course, we will stick to `System.out.println` to be safe, but we will use the simplified `void main()` in examples where appropriate.*
