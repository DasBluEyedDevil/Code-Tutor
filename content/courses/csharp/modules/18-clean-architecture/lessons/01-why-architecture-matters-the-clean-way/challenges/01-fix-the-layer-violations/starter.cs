// PROBLEM: This code has Clean Architecture violations!
// Fix the issues to follow proper layering.

// Current structure (all in one file for the challenge):
namespace ShopFlow;

// This class is in the Domain layer
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    
    // VIOLATION: Domain depends on Infrastructure!
    private readonly SqlConnection _connection;
    
    public void Save()
    {
        // Direct database access in domain entity
        _connection.Execute("INSERT INTO Products...");
    }
}

// This class is in the Application layer
public class ProductService
{
    // VIOLATION: Depends on concrete implementation!
    private readonly SqlProductRepository _repository;
    
    public Product GetProduct(int id)
    {
        return _repository.GetById(id);
    }
}