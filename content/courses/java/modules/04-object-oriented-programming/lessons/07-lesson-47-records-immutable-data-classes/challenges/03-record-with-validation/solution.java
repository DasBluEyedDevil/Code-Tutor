// Solution: Record with Validation
// Define the Age record with compact constructor validation
record Age(int years) {
    Age {
        if (years < 0) {
            throw new IllegalArgumentException("Age cannot be negative");
        }
    }
}

void main() {
    // Create a valid Age
    var age = new Age(25);
    
    // Print the years
    println(age.years());
}