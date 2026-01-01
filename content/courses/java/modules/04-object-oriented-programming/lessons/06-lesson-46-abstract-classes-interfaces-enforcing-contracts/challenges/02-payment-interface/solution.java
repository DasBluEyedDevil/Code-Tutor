// Solution: Payment Interface
// This demonstrates interfaces defining contracts

interface Payable {
    // Interface method - no body, must be implemented
    double calculatePayment();
}

class Employee implements Payable {
    double salary;
    
    // Constructor
    public Employee(double salary) {
        this.salary = salary;
    }
    
    // Implement the interface method
    @Override
    public double calculatePayment() {
        return salary;
    }
}

class Contractor implements Payable {
    double hourlyRate;
    int hoursWorked;
    
    // Constructor
    public Contractor(double hourlyRate, int hoursWorked) {
        this.hourlyRate = hourlyRate;
        this.hoursWorked = hoursWorked;
    }
    
    // Implement the interface method
    @Override
    public double calculatePayment() {
        return hourlyRate * hoursWorked;
    }
}