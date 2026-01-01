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
    // Add 4 more products
};

// Extract names
var productNames = products.Select(p => /* transform */);

// Calculate discounted prices
var discountedPrices = products.Select(p => /* transform */);

// Create summary objects
var summaries = products.Select(p => new
{
    // Properties
});

// Names of affordable products
var affordableNames = products
    .Where(p => /* filter */)
    .Select(p => /* transform */);