---
type: "EXAMPLE"
title: "ShopFlow Infrastructure Layer"
---

The Infrastructure layer implements all the interfaces defined in Application using concrete technologies like Entity Framework Core, SendGrid, Stripe, etc.

```csharp
// ===== SHOPFLOW INFRASTRUCTURE LAYER =====

// ========== DATABASE CONTEXT ==========

namespace ShopFlow.Infrastructure.Data;

public class AppDbContext : DbContext, IUnitOfWork
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply all configurations from this assembly
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AppDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }

    // Implementing IUnitOfWork
    public override async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        // Add audit fields
        foreach (var entry in ChangeTracker.Entries<Entity>())
        {
            if (entry.State == EntityState.Modified)
            {
                entry.Entity.SetModifiedAt(DateTime.UtcNow);
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}


// ========== ENTITY CONFIGURATIONS ==========

namespace ShopFlow.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Description)
            .HasMaxLength(2000);

        // Configure Value Object (Money) as owned entity
        builder.OwnsOne(p => p.Price, priceBuilder =>
        {
            priceBuilder.Property(m => m.Amount)
                .HasColumnName("Price")
                .HasPrecision(18, 2);
            
            priceBuilder.Property(m => m.Currency)
                .HasColumnName("Currency")
                .HasMaxLength(3);
        });

        builder.Property(p => p.StockQuantity)
            .IsRequired();

        builder.Property(p => p.Status)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        // Index for common queries
        builder.HasIndex(p => p.Status);
        builder.HasIndex(p => p.Name);
    }
}


// ========== REPOSITORIES ==========

namespace ShopFlow.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> GetByIdAsync(
        int id, 
        CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Product>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Product>> GetByCategoryAsync(
        int categoryId, 
        CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .AsNoTracking()
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(
        Product product, 
        CancellationToken cancellationToken = default)
    {
        await _context.Products.AddAsync(product, cancellationToken);
    }

    public Task UpdateAsync(
        Product product, 
        CancellationToken cancellationToken = default)
    {
        _context.Products.Update(product);
        return Task.CompletedTask;
    }

    public async Task<bool> ExistsAsync(
        int id, 
        CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .AnyAsync(p => p.Id == id, cancellationToken);
    }
}


// ========== EXTERNAL SERVICES ==========

namespace ShopFlow.Infrastructure.Services;

public class SendGridEmailService : IEmailService
{
    private readonly ISendGridClient _client;
    private readonly ILogger<SendGridEmailService> _logger;
    private readonly EmailSettings _settings;

    public SendGridEmailService(
        ISendGridClient client,
        IOptions<EmailSettings> settings,
        ILogger<SendGridEmailService> logger)
    {
        _client = client;
        _settings = settings.Value;
        _logger = logger;
    }

    public async Task SendOrderConfirmationAsync(
        int orderId,
        string customerEmail,
        string customerName,
        CancellationToken cancellationToken = default)
    {
        var message = new SendGridMessage
        {
            From = new EmailAddress(_settings.FromEmail, _settings.FromName),
            Subject = $"Order #{orderId} Confirmation",
            TemplateId = _settings.OrderConfirmationTemplateId
        };

        message.AddTo(new EmailAddress(customerEmail, customerName));
        message.SetTemplateData(new { order_id = orderId, customer_name = customerName });

        try
        {
            var response = await _client.SendEmailAsync(message, cancellationToken);
            
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning(
                    "Failed to send order confirmation email for order {OrderId}", 
                    orderId);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, 
                "Error sending order confirmation email for order {OrderId}", 
                orderId);
            throw;
        }
    }
}


namespace ShopFlow.Infrastructure.Services;

public class StripePaymentGateway : IPaymentGateway
{
    private readonly PaymentIntentService _paymentIntentService;
    private readonly ILogger<StripePaymentGateway> _logger;

    public StripePaymentGateway(
        PaymentIntentService paymentIntentService,
        ILogger<StripePaymentGateway> logger)
    {
        _paymentIntentService = paymentIntentService;
        _logger = logger;
    }

    public async Task<PaymentResult> ProcessPaymentAsync(
        int orderId,
        Money amount,
        string paymentMethodId,
        CancellationToken cancellationToken = default)
    {
        var options = new PaymentIntentCreateOptions
        {
            Amount = (long)(amount.Amount * 100), // Stripe uses cents
            Currency = amount.Currency.ToLowerInvariant(),
            PaymentMethod = paymentMethodId,
            Confirm = true,
            Metadata = new Dictionary<string, string>
            {
                { "order_id", orderId.ToString() }
            }
        };

        try
        {
            var intent = await _paymentIntentService.CreateAsync(
                options, 
                cancellationToken: cancellationToken);

            return new PaymentResult(
                intent.Status == "succeeded",
                intent.Id,
                intent.Status == "succeeded" ? null : "Payment not confirmed");
        }
        catch (StripeException ex)
        {
            _logger.LogError(ex, "Stripe payment failed for order {OrderId}", orderId);
            return new PaymentResult(false, null, ex.Message);
        }
    }
}
```
