---
type: "KEY_POINT"
title: "Code-First Database Design"
---

## Key Takeaways

- **Data Annotations configure the database from C# attributes** -- `[Required]` maps to NOT NULL, `[MaxLength(50)]` maps to VARCHAR(50), `[Key]` marks the primary key. Simple configurations go on properties.

- **Fluent API in `OnModelCreating()` handles complex configurations** -- relationships, composite keys, indexes, and multi-property constraints are cleaner with the fluent API than with attributes.

- **Navigation properties define relationships** -- `public List<Order> Orders { get; set; }` on a Customer creates a one-to-many relationship. EF Core generates the foreign key automatically.
