using Moq;
using Xunit;

public interface IInventoryService
{
    bool CheckStock(string productId, int quantity);
}

public interface IPaymentService
{
    bool ProcessPayment(decimal amount);
}

public class OrderService
{
    private readonly IInventoryService _inventory;
    private readonly IPaymentService _payment;
    
    public OrderService(IInventoryService inventory, IPaymentService payment)
    {
        _inventory = inventory;
        _payment = payment;
    }
    
    public bool PlaceOrder(string productId, int quantity, decimal price)
    {
        // Implement: check stock, then process payment
        // Return true only if BOTH succeed
        return false; // TODO
    }
}

public class OrderServiceTests
{
    [Fact]
    public void PlaceOrder_StockAvailablePaymentSucceeds_ReturnsTrue()
    {
        // Arrange - create mocks
        var mockInventory = new Mock<IInventoryService>();
        var mockPayment = new Mock<IPaymentService>();
        
        // Setup mock behavior
        mockInventory.Setup(i => i.CheckStock("PROD1", 2)).Returns(true);
        // Setup payment mock...
        
        var service = new OrderService(mockInventory.Object, mockPayment.Object);
        
        // Act
        bool result = service.PlaceOrder("PROD1", 2, 99.99m);
        
        // Assert
        Assert.True(result);
        // Verify both services were called...
    }
    
    // Add more tests for failure cases!
}