---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// Install: dotnet add package Moq
using Moq;
using Xunit;

// ===== INTERFACES (Dependencies) =====
public interface IEmailService
{
    bool SendEmail(string to, string subject, string body);
}

public interface IUserRepository
{
    User GetById(int id);
    void Save(User user);
}

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public bool IsVerified { get; set; }
}

// ===== CLASS UNDER TEST =====
public class UserService
{
    private readonly IUserRepository _repo;
    private readonly IEmailService _email;
    
    public UserService(IUserRepository repo, IEmailService email)
    {
        _repo = repo;
        _email = email;
    }
    
    public bool VerifyUser(int userId)
    {
        var user = _repo.GetById(userId);
        if (user == null) return false;
        
        user.IsVerified = true;
        _repo.Save(user);
        _email.SendEmail(user.Email, "Verified!", "Your account is verified.");
        
        return true;
    }
}

// ===== TESTS WITH MOCKS =====
public class UserServiceTests
{
    [Fact]
    public void VerifyUser_ValidUser_SetsVerifiedAndSendsEmail()
    {
        // ARRANGE - Create mocks
        var mockRepo = new Mock<IUserRepository>();
        var mockEmail = new Mock<IEmailService>();
        
        var testUser = new User { Id = 1, Email = "test@test.com" };
        
        // Setup mock behavior
        mockRepo.Setup(r => r.GetById(1)).Returns(testUser);
        mockEmail.Setup(e => e.SendEmail(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>()
        )).Returns(true);
        
        // Create service with mocks
        var service = new UserService(mockRepo.Object, mockEmail.Object);
        
        // ACT
        bool result = service.VerifyUser(1);
        
        // ASSERT
        Assert.True(result);
        Assert.True(testUser.IsVerified);  // User was modified
        
        // Verify mock was called correctly
        mockRepo.Verify(r => r.Save(testUser), Times.Once);
        mockEmail.Verify(e => e.SendEmail(
            "test@test.com",
            "Verified!",
            It.IsAny<string>()
        ), Times.Once);
    }
    
    [Fact]
    public void VerifyUser_UserNotFound_ReturnsFalse()
    {
        var mockRepo = new Mock<IUserRepository>();
        var mockEmail = new Mock<IEmailService>();
        
        // Setup: GetById returns null
        mockRepo.Setup(r => r.GetById(999)).Returns((User)null);
        
        var service = new UserService(mockRepo.Object, mockEmail.Object);
        
        bool result = service.VerifyUser(999);
        
        Assert.False(result);
        // Verify Save was NEVER called
        mockRepo.Verify(r => r.Save(It.IsAny<User>()), Times.Never);
    }
}

Console.WriteLine("Mock examples defined!");
Console.WriteLine("Key Moq methods:");
Console.WriteLine("  mock.Setup(x => x.Method()).Returns(value)");
Console.WriteLine("  mock.Verify(x => x.Method(), Times.Once)");
Console.WriteLine("  mock.Object - get the mocked instance");
```
