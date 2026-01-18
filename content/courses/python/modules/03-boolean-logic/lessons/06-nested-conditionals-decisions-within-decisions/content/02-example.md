---
type: "EXAMPLE"
title: "Code Example"
---

**Expected Output:**
```
--- Access Control ---
Enter password: secret123
✓ Access Granted
Enter your role (admin/user): admin
  → Welcome, Administrator
  → Opening admin dashboard...

--- Discount Calculator ---
Enter cart total: $100
✓ Qualifies for discount (cart >= $50)
Are you a member? (yes/no): yes
  → Member: 20% discount
  → Final total: $80.00
```

```python
# Nested Conditionals: Decisions Within Decisions

# Example 1: User Access Control
print("--- Access Control ---")
password = input("Enter password: ")

if password == "secret123":
    print("✓ Access Granted")
    
    role = input("Enter your role (admin/user): ")
    if role == "admin":
        print("  → Welcome, Administrator")
        print("  → Opening admin dashboard...")
    else:
        print("  → Welcome, User")
        print("  → Opening user dashboard...")
else:
    print("❌ Access Denied")
    print("→ Incorrect password")

print()

# Example 2: Discount Calculator
print("--- Discount Calculator ---")
cart_total = float(input("Enter cart total: $"))

if cart_total >= 50:
    print("✓ Qualifies for discount (cart >= $50)")
    
    is_member_input = input("Are you a member? (yes/no): ")

    if is_member_input.lower() == "yes":
        discount = 0.20  # 20%
        print("  → Member: 20% discount")
    else:
        discount = 0.10  # 10%
        print("  → Non-member: 10% discount")

    final_total = cart_total * (1 - discount)
    print(f"  → Final total: ${final_total:.2f}")
else:
    print("❌ No discount (cart < $50)")
    print(f"→ Final total: ${cart_total:.2f}")
```
