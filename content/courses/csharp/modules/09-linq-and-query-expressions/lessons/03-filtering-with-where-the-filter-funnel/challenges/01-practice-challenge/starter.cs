using System;
using System.Collections.Generic;
using System.Linq;

class Product
{
    public string Name;
    public decimal Price;
    public string Category;
    public int Stock;
}

List<Product> products = new List<Product>
{
    new Product { Name = "Laptop", Price = 999, Category = "Electronics", Stock = 5 },
    new Product { Name = "Shirt", Price = 25, Category = "Clothing", Stock = 50 },
    // Add 4 more products
};

// Filter 1: Under $50
var affordable = products.Where(p => /* condition */);

// Filter 2: Electronics in stock
var availableElectronics = products.Where(p => /* condition */);

// Filter 3: Books OR expensive
var booksOrExpensive = products.Where(p => /* condition */);

// Filter 4: Out of stock
var outOfStock = products.Where(p => /* condition */);