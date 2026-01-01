---
type: "KEY_POINT"
title: "static: Shared vs Personal"
---

STATIC = Shared tool (belongs to everyone):

Think of a PUBLIC calculator at school:
- Anyone can use it
- Doesn't belong to any one student
- Same calculator for all

Math.abs(-5)  // Use the shared Math class

NON-STATIC = Personal item (belongs to an instance):

Think of YOUR phone:
- Has YOUR contacts
- Stores YOUR photos
- Different from others' phones

myPhone.makeCall("Mom")  // Your phone, your mom's number

In Java:

// STATIC - shared, no object needed
public static int add(int a, int b) {
    return a + b;  // Same for everyone
}
Calculator.add(2, 3);  // Just call it

// NON-STATIC - instance-specific
public class BankAccount {
    double balance;  // EACH account has its own balance
    
    public void deposit(double amount) {
        balance += amount;  // Modifies THIS account
    }
}
BankAccount aliceAccount = new BankAccount();
aliceAccount.deposit(100);  // Alice's balance changes