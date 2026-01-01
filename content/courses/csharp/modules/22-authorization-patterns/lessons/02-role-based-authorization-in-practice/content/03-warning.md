---
type: "WARNING"
title: "Role Anti-Patterns"
---

## Anti-Pattern 1: Role Explosion

The most common role-based authorization mistake is creating too many granular roles. You start with Admin, Manager, and User. Then someone needs 'ReadOnlyManager' access, so you add that. Then 'ManagerWithoutDelete'. Then 'PowerUser'. Before you know it, you have 47 roles and no one understands which role grants which permission. This is called 'role explosion' and it defeats the entire purpose of role-based access control.

**The Problem:** Each new edge case spawns a new role. Maintenance becomes a nightmare. Users get assigned to multiple overlapping roles with conflicting permissions. New features require updating dozens of role definitions.

**The Solution:** Keep roles coarse-grained and aligned with business functions (Admin, Seller, Customer). Use claims for fine-grained permissions. If you need 'ManagerWithDeleteAccess', add a 'CanDelete' claim rather than a new role.

## Anti-Pattern 2: Hardcoded Role Checks Everywhere

Scattering `if (user.IsInRole("Admin"))` checks throughout your codebase creates a maintenance nightmare. When role names change or requirements evolve, you must find and update dozens of scattered checks. Worse, different developers might use different strings ('Admin' vs 'Administrator' vs 'admin'), creating subtle bugs.

**The Problem:** Role checking logic is duplicated across controllers, services, and views. No single source of truth for what each role can do. Typos in role strings cause silent failures.

**The Solution:** Define roles as constants (ShopFlowRoles.Admin). Use policies that encapsulate role requirements. Centralize authorization logic in handlers rather than spreading checks across the codebase.

## Anti-Pattern 3: Storing Roles Only in JWT

Some developers store roles only in the JWT token and never check the database. This seems efficient - the token contains everything needed for authorization. But it creates a serious security gap: if you revoke a user's admin role, their existing JWT tokens still contain the Admin claim until they expire.

**The Problem:** Role changes do not take effect until the user's token expires. A fired admin retains access for hours or days. Demoted users maintain elevated privileges until re-authentication.

**The Solution:** For sensitive operations, verify roles against the database rather than trusting the JWT alone. Implement token revocation lists or short token lifetimes with refresh tokens. Cache role lookups with short TTLs for performance.

## Anti-Pattern 4: Not Handling Role Hierarchy

Treating roles as completely independent ignores the natural hierarchy in most organizations. An Admin should automatically have all Manager permissions. A Manager should have all Employee permissions. But if you check `IsInRole("Manager")` explicitly, Admins fail the check despite having superior access.

**The Problem:** Admins get 'access denied' on Manager-level features. You end up with awkward `IsInRole("Admin") || IsInRole("Manager")` checks everywhere. Higher-level roles require explicit grants of lower-level permissions.

**The Solution:** Define role hierarchy in your policy configuration. Use policies like 'RequireManagerLevel' that accept Admin OR Manager. Consider claims-based authorization where higher roles automatically receive lower-level claims.

## Anti-Pattern 5: Role Names as Magic Strings

Using role names directly in authorization attributes creates fragile code:

```csharp
// BAD: Magic string, easy to typo
[Authorize(Roles = "Adimn")] // Typo goes unnoticed until runtime

// GOOD: Compile-time safety
[Authorize(Roles = ShopFlowRoles.Admin)]
```

**The Problem:** Typos in role names are not caught at compile time. Renaming a role requires finding all string occurrences. No IDE support for refactoring.

**The Solution:** Always use constants for role names. Define them in a single location and reference them everywhere. The compiler will catch typos and refactoring tools will work correctly.