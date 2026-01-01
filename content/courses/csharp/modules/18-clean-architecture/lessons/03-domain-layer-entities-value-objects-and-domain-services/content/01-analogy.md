---
type: "ANALOGY"
title: "Entities vs Value Objects"
---

Understanding the difference between Entities and Value Objects is crucial for Domain-Driven Design and Clean Architecture. Let's use a relatable analogy: People vs Money.

**ENTITIES ARE LIKE PEOPLE:**

Think about two identical twins - same height, same weight, same hair color, same everything visible. Are they the same person? Absolutely not! Even if every attribute matches, they are distinct individuals with their own unique identity. You track them by WHO they are, not WHAT they look like.

In software, an Entity is the same way:
- A Customer with ID=42 is different from Customer with ID=43, even if they have the same name
- An Order with ID=1001 is a specific, trackable order throughout its lifecycle
- A Product with ID=500 maintains its identity even when you change its price

Entities have:
- A unique identifier (ID) that never changes
- A lifecycle - they're created, modified, and potentially deleted
- Identity equality - two entities are equal only if they have the same ID

**VALUE OBJECTS ARE LIKE MONEY:**

Now think about two $20 bills in your wallet. Are they the same? For all practical purposes, YES! You don't care which specific $20 bill you use to pay for lunch. A $20 is a $20 is a $20. You don't track individual bills; you track the amount.

In software, a Value Object works the same way:
- Money(20, "USD") equals Money(20, "USD") - always!
- Address("123 Main St", "Seattle", "WA") equals another identical Address
- Email("john@example.com") is interchangeable with an identical Email

Value Objects have:
- No unique identifier - they ARE their attributes
- Immutability - once created, they never change (you create a new one instead)
- Value equality - two value objects are equal if all their properties are equal

**THE PRACTICAL DIFFERENCE:**

Scenario: A customer moves to a new address.

- Customer (Entity): Same customer, same ID. You UPDATE the customer's address reference.
- Address (Value Object): You don't modify the old address. You CREATE a new Address and assign it to the customer.

Why? Because addresses don't have identity. "123 Main St" isn't a specific thing you're tracking - it's just a value. The customer IS a specific thing you're tracking.

**REMEMBER:**
- Entity = tracked by ID (like people - WHO they are)
- Value Object = defined by attributes (like money - WHAT it is)

Think: 'If I swapped one for an identical copy, would it matter? If yes, it's an Entity. If no, it's a Value Object.'