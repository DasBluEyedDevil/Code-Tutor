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
        if (!_inventory.CheckStock(productId, quantity))
            return false;
        
        return _payment.ProcessPayment(price * quantity);
    }
}

public class OrderServiceTests
{
    [Fact]
    public void PlaceOrder_StockAvailablePaymentSucceeds_ReturnsTrue()
    {
        var mockInventory = new Mock<IInventoryService>();
        var mockPayment = new Mock<IPaymentService>();
        
        mockInventory.Setup(i => i.CheckStock("PROD1", 2)).Returns(true);
        mockPayment.Setup(p => p.ProcessPayment(199.98m)).Returns(true);
        
        var service = new OrderService(mockInventory.Object, mockPayment.Object);
        
        bool result = service.PlaceOrder("PROD1", 2, 99.99m);
        
        Assert.True(result);
        mockInventory.Verify(i => i.CheckStock("PROD1", 2), Times.Once);
        mockPayment.Verify(p => p.ProcessPayment(199.98m), Times.Once);
    }
    
    [Fact]
    public void PlaceOrder_NoStock_ReturnsFalse()
    {
        var mockInventory = new Mock<IInventoryService>();
        var mockPayment = new Mock<IPaymentService>();
        
        mockInventory.Setup(i => i.CheckStock(It.IsAny<string>(), It.IsAny<int>())).Returns(false);
        
        var service = new OrderService(mockInventory.Object, mockPayment.Object);
        
        bool result = service.PlaceOrder("PROD1", 100, 99.99m);
        
        Assert.False(result);
        mockPayment.Verify(p => p.ProcessPayment(It.IsAny<decimal>()), Times.Never);
    }
    
    [Fact]
    public void PlaceOrder_PaymentFails_ReturnsFalse()
    {
        var mockInventory = new Mock<IInventoryService>();
        var mockPayment = new Mock<IPaymentService>();
        
        mockInventory.Setup(i => i.CheckStock("PROD1", 1)).Returns(true);
        mockPayment.Setup(p => p.ProcessPayment(It.IsAny<decimal>())).Returns(false);
        
        var service = new OrderService(mockInventory.Object, mockPayment.Object);
        
        bool result = service.PlaceOrder("PROD1", 1, 50m);
        
        Assert.False(result);
    }
}

Console.WriteLine("OrderService tests with mocks defined!");
Console.WriteLine("Tests cover: success, no stock, payment failure");