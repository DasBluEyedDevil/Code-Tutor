// Solution: Product with Validation
// This demonstrates encapsulation with price validation

public class Product {
    // Private fields
    private String name;
    private double price;
    
    // Constructor
    public Product(String name, double price) {
        this.name = name;
        // Only set price if positive
        if (price > 0) {
            this.price = price;
        }
    }
    
    // Getter for name
    public String getName() {
        return name;
    }
    
    // Getter for price
    public double getPrice() {
        return price;
    }
    
    // Setter for price with validation (must be > 0)
    public void setPrice(double p) {
        if (p > 0) {
            this.price = p;
        }
        // If invalid, price remains unchanged
    }
}