# Dual Syntax Pattern for Java Course Content

When presenting Java code examples in lessons, use this structure to show both modern and traditional syntax.

## Pattern

### Modern Java (23+)
```java
void main() {
    println("Hello, World!");
}
```

<details>
<summary>📜 Traditional Syntax (Java 8-21)</summary>

```java
public class HelloWorld {
    public static void main(String[] args) {
        System.out.println("Hello, World!");
    }
}
```

**Why the difference?** Modern Java uses implicit classes (JEP 477) to reduce boilerplate. The traditional syntax is what you'll see in most existing codebases and is required for Java 21 LTS and earlier.

</details>

## Guidelines

1. **Modern First**: Always show the modern, simplified syntax first
2. **Collapsible Traditional**: Put traditional syntax in a `<details>` block
3. **Explain the Difference**: Include a brief explanation of why the syntaxes differ
4. **Practical Advice**: Help learners understand when they'll see each syntax

## Examples for Common Patterns

### Variable Declaration
Modern: `var name = "Alice";`
Traditional: `String name = "Alice";`

### Switch Expressions vs Statements
Modern (Java 14+):
```java
String result = switch (day) {
    case MONDAY, FRIDAY -> "Work hard";
    case SATURDAY, SUNDAY -> "Relax";
    default -> "Midweek";
};
```

Traditional:
```java
String result;
switch (day) {
    case MONDAY:
    case FRIDAY:
        result = "Work hard";
        break;
    case SATURDAY:
    case SUNDAY:
        result = "Relax";
        break;
    default:
        result = "Midweek";
}
```

### Pattern Matching for instanceof
Modern (Java 16+):
```java
if (obj instanceof String s) {
    System.out.println(s.toUpperCase());
}
```

Traditional:
```java
if (obj instanceof String) {
    String s = (String) obj;
    System.out.println(s.toUpperCase());
}
```

## Usage in JSON Content

When adding to lesson contentSections in course.json, use escaped newlines:

```json
{
  "type": "KEY_POINT",
  "title": "Modern vs Traditional Syntax",
  "content": "MODERN (Java 23+):\nvoid main() {\n    println(\"Hello\");\n}\n\nTRADITIONAL (Java 8-21):\npublic class Hello {\n    public static void main(String[] args) {\n        System.out.println(\"Hello\");\n    }\n}"
}
```
