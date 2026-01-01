---
type: "EXAMPLE"
title: "Handling Account Conflicts"
---

When a user tries to link an external login whose email already exists, you need to handle the conflict carefully to prevent account takeover.

```csharp
// ===== HANDLING EMAIL CONFLICTS DURING OAUTH SIGN-IN =====

public class OAuthSignInHandler
{
    private readonly ShopFlowDbContext _db;
    private readonly ILogger<OAuthSignInHandler> _logger;
    
    public OAuthSignInHandler(ShopFlowDbContext db, ILogger<OAuthSignInHandler> logger)
    {
        _db = db;
        _logger = logger;
    }
    
    public async Task<SignInResult> HandleExternalLoginAsync(
        string provider,
        string providerKey,
        string? email,
        string? name,
        bool emailVerified)
    {
        // Step 1: Check if this external login already exists
        var existingLogin = await _db.ExternalLogins
            .Include(el => el.User)
            .FirstOrDefaultAsync(el => 
                el.Provider == provider && 
                el.ProviderKey == providerKey);
        
        if (existingLogin != null)
        {
            // Known user - just sign them in
            _logger.LogInformation(
                "Existing user {UserId} signed in via {Provider}",
                existingLogin.UserId, provider);
            
            return SignInResult.Success(existingLogin.User);
        }
        
        // Step 2: New external login - check for email conflicts
        if (!string.IsNullOrEmpty(email))
        {
            var existingUserWithEmail = await _db.Users
                .Include(u => u.ExternalLogins)
                .FirstOrDefaultAsync(u => u.Email == email);
            
            if (existingUserWithEmail != null)
            {
                // Email conflict! An account with this email exists.
                // This could be:
                // 1. The same person wanting to add another login method
                // 2. Someone trying to hijack another user's account
                
                if (!emailVerified)
                {
                    // DANGER: Unverified email from external provider
                    // Do NOT auto-link - require explicit verification
                    _logger.LogWarning(
                        "Blocked unverified {Provider} login for existing email {Email}",
                        provider, email);
                    
                    return SignInResult.RequiresEmailVerification(
                        "This email is already registered. Please sign in with your " +
                        "existing method and link this provider from account settings.");
                }
                
                // Email IS verified by provider
                // Options based on your security policy:
                
                // OPTION A: Still require explicit linking (most secure)
                return SignInResult.RequiresLinking(
                    existingUserWithEmail.Id,
                    $"An account with email {email} already exists. " +
                    $"Sign in with your existing method to link {provider}.");
                
                // OPTION B: Auto-link if provider verifies email (convenient but riskier)
                // Uncomment below to enable:
                /*
                var newLogin = new ExternalLogin
                {
                    UserId = existingUserWithEmail.Id,
                    Provider = provider,
                    ProviderKey = providerKey,
                    ProviderDisplayName = name,
                    CreatedAt = DateTime.UtcNow
                };
                _db.ExternalLogins.Add(newLogin);
                await _db.SaveChangesAsync();
                
                _logger.LogInformation(
                    "Auto-linked verified {Provider} to existing user {UserId}",
                    provider, existingUserWithEmail.Id);
                
                return SignInResult.Success(existingUserWithEmail);
                */
            }
        }
        
        // Step 3: Completely new user - create account
        var newUser = new User
        {
            Email = email ?? $"{providerKey}@{provider.ToLower()}.external",
            DisplayName = name ?? "User",
            CreatedAt = DateTime.UtcNow,
            LastLoginAt = DateTime.UtcNow,
            ExternalLogins = new List<ExternalLogin>
            {
                new ExternalLogin
                {
                    Provider = provider,
                    ProviderKey = providerKey,
                    ProviderDisplayName = name,
                    CreatedAt = DateTime.UtcNow
                }
            }
        };
        
        _db.Users.Add(newUser);
        await _db.SaveChangesAsync();
        
        _logger.LogInformation(
            "Created new user {UserId} via {Provider}",
            newUser.Id, provider);
        
        return SignInResult.Success(newUser);
    }
}

public class SignInResult
{
    public bool Succeeded { get; init; }
    public User? User { get; init; }
    public string? ErrorMessage { get; init; }
    public int? ExistingUserId { get; init; }
    public SignInResultType Type { get; init; }
    
    public static SignInResult Success(User user) => new()
    {
        Succeeded = true,
        User = user,
        Type = SignInResultType.Success
    };
    
    public static SignInResult RequiresLinking(int existingUserId, string message) => new()
    {
        Succeeded = false,
        ExistingUserId = existingUserId,
        ErrorMessage = message,
        Type = SignInResultType.RequiresLinking
    };
    
    public static SignInResult RequiresEmailVerification(string message) => new()
    {
        Succeeded = false,
        ErrorMessage = message,
        Type = SignInResultType.RequiresEmailVerification
    };
}

public enum SignInResultType
{
    Success,
    RequiresLinking,
    RequiresEmailVerification,
    Failed
}
```
