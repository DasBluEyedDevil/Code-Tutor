using Xunit;
using Microsoft.EntityFrameworkCore;
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
        // Implement
    }
    
    public List<Product> GetInStock()
    {
        // Implement: return products where InStock = true
        return new List<Product>();
    }
    
    public List<Product> GetByPriceRange(decimal min, decimal max)
    {
        // Implement: return products in price range
        return new List<Product>();
    }
}

public class ProductRepositoryIntegrationTests : IDisposable
{
    private readonly ProductDbContext _context;
    private readonly ProductRepository _repo;
    
    public ProductRepositoryIntegrationTests()
    {
        // Setup in-memory database
        var options = new DbContextOptionsBuilder<ProductDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        _context = new ProductDbContext(options);
        _repo = new ProductRepository(_context);
    }
    
    [Fact]
    public void AddProduct_ValidProduct_PersistsToDatabase()
    {
        // Write test
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }
}