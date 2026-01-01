using Xunit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public bool InStock { get; set; }
}

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options) 
        : base(options) { }
    
    public DbSet<Product> Products { get; set; }
}

public class ProductRepository
{
    private readonly ProductDbContext _context;
    
    public ProductRepository(ProductDbContext context)
    {
        _context = context;
    }
    
    public void AddProduct(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
    }
    
    public List<Product> GetInStock()
    {
        return _context.Products.Where(p => p.InStock).ToList();
    }
    
    public List<Product> GetByPriceRange(decimal min, decimal max)
    {
        return _context.Products
            .Where(p => p.Price >= min && p.Price <= max)
            .ToList();
    }
}

public class ProductRepositoryIntegrationTests : IDisposable
{
    private readonly ProductDbContext _context;
    private readonly ProductRepository _repo;
    
    public ProductRepositoryIntegrationTests()
    {
        var options = new DbContextOptionsBuilder<ProductDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        _context = new ProductDbContext(options);
        _repo = new ProductRepository(_context);
    }
    
    [Fact]
    public void AddProduct_ValidProduct_PersistsToDatabase()
    {
        var product = new Product { Name = "Laptop", Price = 999m, InStock = true };
        
        _repo.AddProduct(product);
        
        var dbProduct = _context.Products.First();
        Assert.Equal("Laptop", dbProduct.Name);
        Assert.Equal(999m, dbProduct.Price);
    }
    
    [Fact]
    public void GetInStock_MixedProducts_ReturnsOnlyInStock()
    {
        _repo.AddProduct(new Product { Name = "A", Price = 10, InStock = true });
        _repo.AddProduct(new Product { Name = "B", Price = 20, InStock = false });
        _repo.AddProduct(new Product { Name = "C", Price = 30, InStock = true });
        
        var inStock = _repo.GetInStock();
        
        Assert.Equal(2, inStock.Count);
        Assert.All(inStock, p => Assert.True(p.InStock));
    }
    
    [Fact]
    public void GetByPriceRange_ProductsExist_FiltersCorrectly()
    {
        _repo.AddProduct(new Product { Name = "Cheap", Price = 10, InStock = true });
        _repo.AddProduct(new Product { Name = "Mid", Price = 50, InStock = true });
        _repo.AddProduct(new Product { Name = "Expensive", Price = 100, InStock = true });
        
        var result = _repo.GetByPriceRange(20, 80);
        
        Assert.Single(result);
        Assert.Equal("Mid", result[0].Name);
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }
}

Console.WriteLine("Integration tests with in-memory DB defined!");
Console.WriteLine("Tests: AddProduct, GetInStock, GetByPriceRange");