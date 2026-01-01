---
type: "EXAMPLE"
title: "Code Example"
---

**Expected Output:**
```
✓ User is logged in
  → Granting admin access
  → Showing admin dashboard

Cart Total: $75
✓ Qualifies for discount (cart >= $50)
  → Member: 20% discount
  → Final total: $60.00

=== Roller Coaster Safety Check ===
✓ Height requirement met (>= 48")
  ✓ Ticket verified
    ✓ Safety cleared
    → APPROVED: You may ride!

Age: 16, Student: True
Category: Student, Price: $10

=== Activity Planner ===
✓ Temperature is comfortable
  ✓ Weather is dry
    → Recommendation: Walk during lunch break
```

```python
# Nested Conditionals: Decisions Within Decisions

# Example 1: Basic Nesting - User Access Control
is_logged_in = True
is_admin = True

if is_logged_in:
    print("✓ User is logged in")
    
    # This inner check only runs if logged in
    if is_admin:
        print("  → Granting admin access")
        print("  → Showing admin dashboard")
    else:
        print("  → Regular user access")
        print("  → Showing user dashboard")
else:
    print("❌ Not logged in")
    print("→ Redirecting to login page")

print()

# Example 2: Discount Calculator (Layered Eligibility)
cart_total = 75
is_member = True

print(f"Cart Total: ${cart_total}")

if cart_total >= 50:  # First condition: minimum purchase
    print("✓ Qualifies for discount (cart >= $50)")
    
    if is_member:  # Second condition: checked only if first is True
        discount = 0.20  # 20% member discount
        print("  → Member: 20% discount")
    else:
        discount = 0.10  # 10% non-member discount
        print("  → Non-member: 10% discount")
    
    final_total = cart_total * (1 - discount)
    print(f"  → Final total: ${final_total:.2f}")
else:
    print("❌ No discount (cart < $50)")
    print(f"→ Final total: ${cart_total}")

print()

# Example 3: Three-Level Nesting - Ride Safety Check
height = 52  # inches
has_ticket = True
is_pregnant = False

print("=== Roller Coaster Safety Check ===")

if height >= 48:
    print("✓ Height requirement met (>= 48\")")
    
    if has_ticket:
        print("  ✓ Ticket verified")
        
        if not is_pregnant:
            print("    ✓ Safety cleared")
            print("    → APPROVED: You may ride!")
        else:
            print("    ❌ Safety concern: pregnancy")
            print("    → DENIED: For your safety")
    else:
        print("  ❌ No ticket found")
        print("  → Please purchase a ticket")
else:
    print("❌ Height requirement not met")
    print(f"→ You are {height}\", need 48\" minimum")

print()

# Example 4: Age & Student Status (Nested elif)
age = 16
is_student = True

if age < 13:
    price = 8
    category = "Child"
else:
    # Nested decision for 13+ age group
    if is_student:
        price = 10
        category = "Student"
    else:
        if age < 65:
            price = 15
            category = "Adult"
        else:
            price = 12
            category = "Senior"

print(f"Age: {age}, Student: {is_student}")
print(f"Category: {category}, Price: ${price}")

print()

# Example 5: Weather Activity Planner
temperature = 75
is_raining = False
is_weekend = True

print("=== Activity Planner ===")

if temperature > 60:
    print("✓ Temperature is comfortable")
    
    if not is_raining:
        print("  ✓ Weather is dry")
        
        if is_weekend:
            print("    ✓ It's the weekend!")
            print("    → Recommendation: Go to the park!")
        else:
            print("    → Recommendation: Walk during lunch break")
    else:
        print("  ❌ It's raining")
        print("  → Recommendation: Indoor activities")
else:
    print("❌ Too cold outside")
    print("→ Recommendation: Stay indoors")
```
