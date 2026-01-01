---
type: "THEORY"
title: "What Identity Provides"
---

## Core Identity Services

ASP.NET Core Identity provides a comprehensive set of services that handle all aspects of user management. Understanding these services is essential for building secure authentication systems.

### UserManager<TUser>

UserManager is your primary interface for managing users. It provides methods for the complete user lifecycle:

**User Creation and Management:**
- `CreateAsync(user, password)` - Creates a new user with a hashed password
- `UpdateAsync(user)` - Saves changes to user properties
- `DeleteAsync(user)` - Removes a user from the system
- `FindByIdAsync(id)` - Retrieves a user by their unique identifier
- `FindByEmailAsync(email)` - Finds a user by email address
- `FindByNameAsync(username)` - Finds a user by username

**Password Management:**
- `CheckPasswordAsync(user, password)` - Verifies a password without signing in
- `ChangePasswordAsync(user, current, new)` - Changes password with verification
- `ResetPasswordAsync(user, token, new)` - Resets password using a token
- `GeneratePasswordResetTokenAsync(user)` - Creates a password reset token

**Email and Phone Confirmation:**
- `GenerateEmailConfirmationTokenAsync(user)` - Creates email verification token
- `ConfirmEmailAsync(user, token)` - Marks email as confirmed
- `IsEmailConfirmedAsync(user)` - Checks email confirmation status

### SignInManager<TUser>

SignInManager handles the authentication process itself, managing how users prove their identity:

**Sign-In Operations:**
- `PasswordSignInAsync(username, password, isPersistent, lockoutOnFailure)` - Full sign-in with lockout support
- `SignInAsync(user, isPersistent)` - Signs in a user directly (after custom validation)
- `SignOutAsync()` - Signs out the current user
- `RefreshSignInAsync(user)` - Refreshes the authentication cookie

**Two-Factor Authentication:**
- `GetTwoFactorAuthenticationUserAsync()` - Gets user pending 2FA
- `TwoFactorSignInAsync(provider, code, isPersistent, rememberClient)` - Completes 2FA sign-in

### RoleManager<TRole>

RoleManager provides role-based access control capabilities:

**Role Operations:**
- `CreateAsync(role)` - Creates a new role
- `DeleteAsync(role)` - Removes a role
- `RoleExistsAsync(roleName)` - Checks if a role exists
- `FindByNameAsync(roleName)` - Retrieves a role by name

**User-Role Assignment (via UserManager):**
- `AddToRoleAsync(user, roleName)` - Assigns user to a role
- `RemoveFromRoleAsync(user, roleName)` - Removes user from role
- `GetRolesAsync(user)` - Gets all roles for a user
- `IsInRoleAsync(user, roleName)` - Checks role membership

## Identity Database Tables

Identity creates several tables in your database to store authentication data:

| Table | Purpose |
|-------|--------|
| AspNetUsers | User accounts with properties |
| AspNetRoles | Available roles |
| AspNetUserRoles | Many-to-many user-role assignments |
| AspNetUserClaims | Custom claims attached to users |
| AspNetRoleClaims | Claims attached to roles |
| AspNetUserLogins | External login provider associations |
| AspNetUserTokens | Tokens for password reset, 2FA, etc. |

When you customize table names as shown in the example, these become Users, Roles, UserRoles, etc. The schema remains the same; only the names change.

## Token Providers

The `AddDefaultTokenProviders()` call registers token generators used for:

- **Password Reset** - Time-limited tokens for resetting forgotten passwords
- **Email Confirmation** - Tokens sent to verify email addresses
- **Two-Factor Authentication** - TOTP and other 2FA token generation
- **Phone Number Confirmation** - SMS verification tokens

These tokens are cryptographically secure, time-limited, and single-use, making them safe for sensitive operations like password resets.