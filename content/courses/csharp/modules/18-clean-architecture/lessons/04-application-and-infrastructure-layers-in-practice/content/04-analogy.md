---
type: "ANALOGY"
title: "Wiring It All Together"
---

## Dependency Injection Configuration in Program.cs

The final piece of Clean Architecture is wiring everything together using Dependency Injection. This happens in the Presentation layer's Program.cs, where we register all our services.

```csharp
// ===== PROGRAM.CS - WIRING CLEAN ARCHITECTURE =====

// File: src/ShopFlow.API/Program.cs

using ShopFlow.Application;
using ShopFlow.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// ========== LAYER-SPECIFIC REGISTRATIONS ==========

// Application Layer services
builder.Services.AddApplicationServices();

// Infrastructure Layer services (includes Database, External APIs)
builder.Services.AddInfrastructureServices(builder.Configuration);

// Presentation Layer services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

var app = builder.Build();

// ... middleware configuration ...

app.Run();


// ========== APPLICATION LAYER EXTENSION ==========

// File: src/ShopFlow.Application/DependencyInjection.cs

namespace ShopFlow.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        // MediatR for CQRS
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });

        // FluentValidation
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        // AutoMapper
        services.AddAutoMapper(typeof(DependencyInjection).Assembly);

        // Pipeline behaviors
        services.AddTransient(
            typeof(IPipelineBehavior<,>), 
            typeof(ValidationBehavior<,>));
        
        services.AddTransient(
            typeof(IPipelineBehavior<,>), 
            typeof(LoggingBehavior<,>));

        return services;
    }
}


// ========== INFRASTRUCTURE LAYER EXTENSION ==========

// File: src/ShopFlow.Infrastructure/DependencyInjection.cs

namespace ShopFlow.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // ===== DATABASE =====
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                sqlOptions => sqlOptions.MigrationsAssembly(
                    typeof(AppDbContext).Assembly.FullName)));

        // Register DbContext as IUnitOfWork
        services.AddScoped<IUnitOfWork>(provider => 
            provider.GetRequiredService<AppDbContext>());

        // ===== REPOSITORIES =====
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        // ===== EXTERNAL SERVICES =====
        
        // Email (SendGrid)
        services.Configure<EmailSettings>(
            configuration.GetSection("Email"));
        services.AddScoped<ISendGridClient>(_ => 
            new SendGridClient(configuration["Email:ApiKey"]));
        services.AddScoped<IEmailService, SendGridEmailService>();

        // Payments (Stripe)
        StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"];
        services.AddScoped<PaymentIntentService>();
        services.AddScoped<IPaymentGateway, StripePaymentGateway>();

        // Date/Time abstraction
        services.AddSingleton<IDateTimeService, DateTimeService>();

        // Current user (from HttpContext)
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        return services;
    }
}


// ========== USING THE SERVICES ==========

// File: src/ShopFlow.API/Controllers/ProductsController.cs

namespace ShopFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<CreateProductResult>> Create(
        CreateProductCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(
            nameof(GetById), 
            new { id = result.ProductId }, 
            result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var query = new GetProductQuery(id);
        var result = await _mediator.Send(query, cancellationToken);
        
        return result is null 
            ? NotFound() 
            : Ok(result);
    }
}


/*
========== THE FLOW AT RUNTIME ==========

1. Request arrives: POST /api/products with JSON body

2. Controller receives CreateProductCommand (model binding)

3. MediatR routes to CreateProductHandler

4. Handler uses injected IProductRepository
   - At runtime, this is ProductRepository (EF Core)
   - Handler doesn't know it's EF Core!

5. Handler creates Product entity (Domain)

6. Repository saves to database

7. Handler returns DTO (Application)

8. Controller returns HTTP response (Presentation)

========== TESTING BENEFITS ==========

Unit test for CreateProductHandler:
- Mock IProductRepository (no database needed)
- Mock IUnitOfWork
- Test pure business logic

Integration test:
- Use real EF Core with in-memory or test database
- Test the whole flow
*/
```
