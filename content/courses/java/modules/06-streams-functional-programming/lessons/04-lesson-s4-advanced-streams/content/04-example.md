---
type: "EXAMPLE"
title: "Advanced Stream Operations"
---

Combining advanced operations for complex data processing:

```java
import java.util.*;
import java.util.stream.*;

record Order(int id, String customer, List<String> items, double total) {}

void main() {
    List<Order> orders = List.of(
        new Order(1, "Alice", List.of("Book", "Pen"), 25.0),
        new Order(2, "Bob", List.of("Laptop"), 999.0),
        new Order(3, "Alice", List.of("Coffee", "Tea", "Sugar"), 15.0),
        new Order(4, "Charlie", List.of("Phone", "Case"), 850.0)
    );
    
    // flatMap: Get all unique items across all orders
    Set<String> allItems = orders.stream()
        .flatMap(order -> order.items().stream())
        .collect(Collectors.toSet());
    IO.println("All items: " + allItems);
    
    // reduce: Calculate total revenue
    double totalRevenue = orders.stream()
        .map(Order::total)
        .reduce(0.0, Double::sum);
    IO.println("Total revenue: $" + totalRevenue);  // $1889.0
    
    // groupingBy: Orders by customer
    Map<String, List<Order>> byCustomer = orders.stream()
        .collect(Collectors.groupingBy(Order::customer));
    IO.println("Alice's orders: " + byCustomer.get("Alice").size());  // 2
    
    // groupingBy with sum: Total spent per customer
    Map<String, Double> spentByCustomer = orders.stream()
        .collect(Collectors.groupingBy(
            Order::customer,
            Collectors.summingDouble(Order::total)
        ));
    IO.println("Alice spent: $" + spentByCustomer.get("Alice"));  // $40.0
    
    // partitioningBy: High-value vs regular orders
    Map<Boolean, List<Order>> partitioned = orders.stream()
        .collect(Collectors.partitioningBy(o -> o.total() > 100));
    IO.println("High-value orders: " + partitioned.get(true).size());  // 2
}
```
