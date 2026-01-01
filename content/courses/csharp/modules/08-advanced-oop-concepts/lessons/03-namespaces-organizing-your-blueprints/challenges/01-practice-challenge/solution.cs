using System;
using System.Collections.Generic;
using MyStore.Models;
using MyStore.Services;

namespace MyStore.Models
{
    class Product
    {
        public string Name;
        public decimal Price;
    }
}

namespace MyStore.Services
{
    using MyStore.Models;
    
    class ShoppingCart
    {
        public List<Product> Items = new List<Product>();
        
        public void AddItem(Product p)
        {
            Items.Add(p);
            Console.WriteLine("Added: " + p.Name);
        }
        
        public decimal GetTotal()
        {
            decimal total = 0;
            foreach (Product item in Items)
            {
                total += item.Price;
            }
            return total;
        }
    }
}

namespace MyStore
{
    class Program
    {
        static void Main()
        {
            Product p1 = new Product() { Name = "Laptop", Price = 999.99m };
            Product p2 = new Product() { Name = "Mouse", Price = 29.99m };
            
            ShoppingCart cart = new ShoppingCart();
            cart.AddItem(p1);
            cart.AddItem(p2);
            
            Console.WriteLine("Total: $" + cart.GetTotal());
        }
    }
}