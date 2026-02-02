---
type: "EXAMPLE"
title: "Records in Action"
---

Records provide compact constructors for validation, automatic accessor methods, and built-in toString/equals/hashCode. Use them for immutable data holders like DTOs and value objects.

```java
// Define a simple record
public record Student(String name, int grade, double gpa) {
    // Compact constructor for validation
    public Student {
        if (grade < 1 || grade > 12) {
            throw new IllegalArgumentException("Grade must be 1-12");
        }
        if (gpa < 0.0 || gpa > 4.0) {
            throw new IllegalArgumentException("GPA must be 0.0-4.0");
        }
    }
    
    // Custom method
    public boolean isHonorRoll() {
        return gpa >= 3.5;
    }
}

// Using the record
void main() {
    // Create records
    var alice = new Student("Alice", 10, 3.8);
    var bob = new Student("Bob", 11, 3.2);

    // Access components
    IO.println(alice.name());  // Alice
    IO.println(alice.gpa());   // 3.8

    // Use custom methods
    IO.println(alice.isHonorRoll());  // true
    IO.println(bob.isHonorRoll());    // false

    // Automatic toString
    IO.println(alice);  // Student[name=Alice, grade=10, gpa=3.8]

    // Automatic equals
    var aliceCopy = new Student("Alice", 10, 3.8);
    IO.println(alice.equals(aliceCopy));  // true

    // Use in collections
    var students = List.of(alice, bob);
    for (var student : students) {
        IO.println(student.name() + ": " + student.gpa());
    }
}
```
