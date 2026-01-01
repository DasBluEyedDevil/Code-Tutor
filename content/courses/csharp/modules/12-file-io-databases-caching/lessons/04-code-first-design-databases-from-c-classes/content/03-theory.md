---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`Data Annotations`**: [Required], [MaxLength], [Column], [Key] - Attributes on properties configure database! [Required] = NOT NULL. [MaxLength(50)] = VARCHAR(50).

**`Navigation properties`**: public List<Order> Orders - Represents relationship! Customer has many Orders. EF creates foreign key automatically.

**`OnModelCreating()`**: Fluent API configuration. More powerful than attributes! Configure relationships, indexes, constraints. Override in DbContext.

**`HasMany().WithOne()`**: Fluent API for 1-to-Many relationship. HasMany(c => c.Orders) on Customer. WithOne(o => o.Customer) on Order. Defines both sides!