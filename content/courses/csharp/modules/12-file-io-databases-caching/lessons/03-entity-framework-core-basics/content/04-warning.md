---
type: "WARNING"
title: "EF Core Setup Pitfalls"
---

## Watch Out For These Issues!

**EnsureCreated() vs Migrations**: `Database.EnsureCreated()` creates the database but does NOT create migrations. Once you start using migrations (`dotnet ef migrations add`), do NOT use EnsureCreated -- they conflict! EnsureCreated is fine for learning and prototyping, but production apps should always use migrations.

**Missing NuGet packages**: EF Core requires TWO packages: the core package (`Microsoft.EntityFrameworkCore`) AND a database provider (`Microsoft.EntityFrameworkCore.Sqlite`, `.SqlServer`, `.Npgsql` for PostgreSQL). Forgetting the provider gives cryptic errors about "no database provider configured."

**Connection string in source code**: Hardcoding `"Data Source=app.db"` in `OnConfiguring()` is fine for learning, but never do this in production! Use `IConfiguration` to read from appsettings.json or environment variables. Secrets (like passwords) belong in User Secrets or Azure Key Vault.

**DbContext lifetime**: DbContext is designed to be short-lived. In ASP.NET Core, register it with `AddDbContext<T>()` (Scoped lifetime by default -- one per HTTP request). Never make DbContext a Singleton -- it will accumulate tracked entities and eventually consume all memory.
