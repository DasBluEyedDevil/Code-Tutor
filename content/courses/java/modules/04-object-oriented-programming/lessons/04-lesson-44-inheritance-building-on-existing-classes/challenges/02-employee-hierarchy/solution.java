// Solution: Employee Hierarchy
// This demonstrates inheritance with Person as parent class

class Person {
    String name;
    int age;
    
    // Constructor
    public Person(String name, int age) {
        this.name = name;
        this.age = age;
    }
}

class Employee extends Person {
    double salary;
    
    // Constructor calls super to initialize Person fields
    public Employee(String name, int age, double salary) {
        super(name, age);  // Call Person constructor
        this.salary = salary;
    }
    
    // Getter for salary
    public double getSalary() {
        return salary;
    }
}