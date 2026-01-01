// Define the Product record
public record Product(string Name, decimal Price, string Category);

// Create products
var laptop = new Product("Laptop", 999.99m, "Electronics");
var coffee = new Product("Coffee", 12.99m, "Food");
var book = new Product("Book", 24.99m, "Books");

// Display products
Console.WriteLine(laptop);
Console.WriteLine(coffee);
Console.WriteLine(book);

// Create discounted laptop using 'with'
var discountedLaptop = laptop with { Price = 899.99m };
Console.WriteLine("Discounted: " + discountedLaptop);

// Compare two identical products
var anotherCoffee = new Product("Coffee", 12.99m, "Food");
Console.WriteLine("Are they equal? " + (coffee == anotherCoffee));  // True!