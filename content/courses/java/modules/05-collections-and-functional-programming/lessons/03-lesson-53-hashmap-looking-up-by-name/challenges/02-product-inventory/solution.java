// Solution: Product Inventory
// This demonstrates HashMap with containsKey and default values

import java.util.HashMap;

public class Inventory {
    // HashMap to store product -> quantity mappings
    HashMap<String, Integer> stock;
    
    // Constructor initializes the HashMap
    public Inventory() {
        stock = new HashMap<>();
    }
    
    // Add a product with quantity
    public void addProduct(String product, int quantity) {
        stock.put(product, quantity);
    }
    
    // Get stock quantity (returns 0 if product not found)
    public int getStock(String product) {
        // Use getOrDefault to return 0 if product doesn't exist
        return stock.getOrDefault(product, 0);
    }
    
    // Check if product exists
    public boolean hasProduct(String product) {
        return stock.containsKey(product);
    }
}