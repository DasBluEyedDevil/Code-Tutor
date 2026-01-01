---
type: "WARNING"
title: "Security Best Practices"
---

## Critical Security Practices for Authentication Endpoints

### Never Reveal User Existence

**The Mistake:** Returning different error messages for 'email not found' versus 'wrong password'. Attackers can use this to enumerate valid email addresses in your system.

**Example of Bad Practice:**
```csharp
// DON'T DO THIS!
if (user is null)
    return BadRequest("User not found");
if (!passwordValid)
    return BadRequest("Incorrect password");
```

**The Risk:** An attacker tries logging in with 'admin@company.com'. If they get 'User not found', they know that email does not exist. If they get 'Incorrect password', they know the email IS valid and can now focus on cracking just the password.

**The Fix:** Always return the same generic message:
```csharp
// CORRECT - same message for both cases
if (user is null || !passwordValid)
    return Unauthorized("Invalid email or password");
```

This applies to registration too - if a user tries to register with an existing email, do not say 'email already registered'. Instead, pretend registration succeeded and send an email to the existing user saying 'someone tried to create an account with your email'.

### Implement Rate Limiting

**The Mistake:** Allowing unlimited login attempts from the same IP or for the same account.

**The Risk:** Even with account lockout after 5 failed attempts, an attacker can try 5 passwords on millions of accounts, or use distributed IPs to bypass IP-based limits.

**The Fix:** Implement multiple layers of rate limiting:
```csharp
// In Program.cs
builder.Services.AddRateLimiter(options =>
{
    // Per-IP limit for login endpoint
    options.AddFixedWindowLimiter("login", opt =>
    {
        opt.PermitLimit = 10;
        opt.Window = TimeSpan.FromMinutes(1);
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = 2;
    });
});

// Apply to endpoint
group.MapPost("/login", Login)
    .RequireRateLimiting("login");
```

Combine with:
- Account lockout (Identity's built-in feature)
- CAPTCHA after several failed attempts
- Increasing delays between attempts (exponential backoff)
- Alerting on distributed attack patterns

### Always Use HTTPS

**The Mistake:** Running authentication endpoints over HTTP, even in development.

**The Risk:** Credentials sent over HTTP can be intercepted by anyone on the network. This includes coffee shop WiFi, corporate networks, and ISPs.

**The Fix:** Force HTTPS everywhere:
```csharp
// In Program.cs
app.UseHttpsRedirection();

// Configure cookies for HTTPS only
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

// Reject non-HTTPS in production
if (!app.Environment.IsDevelopment())
{
    app.Use(async (context, next) =>
    {
        if (!context.Request.IsHttps)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("HTTPS required");
            return;
        }
        await next();
    });
}
```

### Validate Input Thoroughly

**The Mistake:** Trusting client-side validation or only validating on the happy path.

**The Risk:** Attackers bypass client-side validation easily. Malformed input can cause unexpected behavior, SQL injection, or denial of service.

**The Fix:** Validate everything server-side:
```csharp
public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        // Email: format, length, not empty
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(256);
        
        // Password: complexity requirements
        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(14)
            .Matches("[A-Z]")
            .Matches("[a-z]")
            .Matches("[0-9]")
            .Matches("[^a-zA-Z0-9]");
        
        // Names: prevent script injection
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(100)
            .Matches("^[a-zA-Z\\s'-]+$")
            .WithMessage("Name contains invalid characters");
    }
}
```

### Log Security Events

**The Mistake:** Not logging authentication events, or logging sensitive data like passwords.

**The Risk:** Without logs, you cannot detect attacks, investigate breaches, or meet compliance requirements. Logging passwords creates a massive security hole.

**The Fix:** Log security events without sensitive data:
```csharp
// Log successful login
_logger.LogInformation(
    "User logged in: {UserId} from IP {IpAddress} at {Timestamp}",
    user.Id,
    httpContext.Connection.RemoteIpAddress,
    DateTime.UtcNow);

// Log failed attempts (for detecting attacks)
_logger.LogWarning(
    "Failed login attempt for email {Email} from IP {IpAddress}",
    request.Email,  // OK to log
    httpContext.Connection.RemoteIpAddress);

// NEVER log passwords!
// _logger.LogDebug("Login with password: {Password}", request.Password); // NEVER!
```

Consider sending alerts for:
- Multiple failed logins to the same account
- Logins from new locations
- Logins at unusual times
- Password changes