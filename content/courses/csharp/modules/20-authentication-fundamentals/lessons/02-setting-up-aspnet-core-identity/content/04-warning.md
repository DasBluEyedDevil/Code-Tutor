---
type: "WARNING"
title: "Identity Security Considerations"
---

## Critical Security Mistakes to Avoid

### Weak Password Policies

**The Mistake:** Using Identity's default password requirements or disabling them for 'user convenience'. The defaults require only 6 characters with one non-alphanumeric character - far too weak for modern security.

**The Risk:** Weak passwords are easily cracked through brute force or dictionary attacks. A 6-character password can be cracked in seconds with modern hardware. Password dumps from breached sites provide attackers with common password patterns.

**The Fix:** Enforce strong password policies from day one:
```csharp
options.Password.RequiredLength = 12;  // Minimum 12 characters
options.Password.RequireDigit = true;
options.Password.RequireLowercase = true;
options.Password.RequireUppercase = true;
options.Password.RequireNonAlphanumeric = true;
options.Password.RequiredUniqueChars = 4;
```

Consider also implementing password breach checking using HaveIBeenPwned API to reject known compromised passwords.

### Disabled Account Lockout

**The Mistake:** Disabling account lockout to avoid support tickets from locked-out users, or setting lockout thresholds too high (50+ attempts).

**The Risk:** Without lockout, attackers can attempt unlimited password guesses against an account. Even rate limiting at the network level can be bypassed with distributed attacks or slow-and-steady attempts.

**The Fix:** Enable lockout with reasonable thresholds:
```csharp
options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
options.Lockout.MaxFailedAccessAttempts = 5;
options.Lockout.AllowedForNewUsers = true;
```

Combine with progressive lockout (longer durations for repeated lockouts) and notification to users when lockout occurs.

### Exposing Internal User IDs

**The Mistake:** Using Identity's GUID-based user IDs directly in URLs, APIs, or client-side code: `/api/users/a1b2c3d4-e5f6-...`

**The Risk:** While GUIDs are not sequential, exposing internal identifiers enables user enumeration (attackers can try to access other users' data by guessing IDs) and violates the principle of minimal information exposure.

**The Fix:** Use separate public identifiers:
```csharp
public class ApplicationUser : IdentityUser
{
    // Internal GUID used by Identity
    // Id property inherited from IdentityUser
    
    // Public identifier for APIs and URLs
    public string PublicId { get; set; } = Nanoid.Generate(size: 12);
}
```

Or use username/email for lookups instead of exposing any ID.

### Skipping Email Confirmation

**The Mistake:** Setting `RequireConfirmedEmail = false` to simplify the registration flow.

**The Risk:** Attackers can register accounts with emails they do not own, potentially:
- Impersonating legitimate users
- Claiming email addresses before real owners
- Bypassing email-based account recovery
- Sending spam or phishing from your platform

**The Fix:** Always require email confirmation:
```csharp
options.SignIn.RequireConfirmedEmail = true;
options.SignIn.RequireConfirmedAccount = true;
```

Implement a smooth confirmation flow with clear user guidance and resend functionality.

### Storing Sensitive Data in Claims

**The Mistake:** Adding sensitive information to claims because claims are convenient to access throughout the application.

**The Risk:** Claims are stored in authentication cookies or JWT tokens. If cookies are HttpOnly (good), the data is still transmitted on every request. If tokens are exposed client-side (SPAs), the data is visible to JavaScript and potentially attackers.

**The Fix:** Only store non-sensitive, necessary data in claims:
- User ID (for lookups)
- Roles (for authorization)
- Display name (for UI)

Keep sensitive data in the database and query when needed:
- Social Security Numbers
- Financial information
- Medical data
- Internal notes