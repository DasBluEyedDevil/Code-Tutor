// Solution: Create Encapsulated Student
// This demonstrates encapsulation with validation in setters

public class Student {
    // Private fields
    private String name;
    private int grade;
    
    // Constructor
    public Student(String name, int grade) {
        this.name = name;
        // Validate grade in constructor too
        if (grade >= 0 && grade <= 100) {
            this.grade = grade;
        }
    }
    
    // Getter for name
    public String getName() {
        return name;
    }
    
    // Getter for grade
    public int getGrade() {
        return grade;
    }
    
    // Setter for grade with validation (0-100 only)
    public void setGrade(int g) {
        if (g >= 0 && g <= 100) {
            this.grade = g;
        }
        // If invalid, grade remains unchanged
    }
}