// FIXED: Proper Clean Architecture structure

// ===== DOMAIN LAYER =====
namespace ShopFlow.Domain.Entities;

public class Product
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    
    // Domain has NO infrastructure dependencies
    // Only contains business logic and validation
    
    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice < 0)
            throw new DomainException("Price cannot be negative");
        Price = newPrice;
    }
}

// ===== APPLICATION LAYER =====
namespace ShopFlow.Application.Interfaces;

// Interface defined in Application layer
public interface IProductRepository
{
    Product? GetById(int id);
    void Save(Product product);
}

namespace ShopFlow.Application.Services;

public class ProductService
{
    // Depends on INTERFACE, not concrete implementation
    private readonly IProductRepository _repository;
    
    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }
    
    public Product? GetProduct(int id)
    {
        return _repository.GetById(id);
    }
}

// ===== INFRASTRUCTURE LAYER =====
namespace ShopFlow.Infrastructure.Repositories;

// Concrete implementation in Infrastructure
public class SqlProductRepository : IProductRepository
{
    private readonly DbContext _context;
    
    public SqlProductRepository(DbContext context)
    {
        _context = context;
    }
    
    public Product? GetById(int id)
    {
        return _context.Products.Find(id);
    }
    
    public void Save(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
    }
}