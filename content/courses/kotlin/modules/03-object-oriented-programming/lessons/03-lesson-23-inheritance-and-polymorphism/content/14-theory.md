---
type: "THEORY"
title: "Exercise 3: Bank Account Hierarchy"
---


**Goal**: Build different types of bank accounts with shared and unique features.

**Requirements**:
1. Open class `BankAccount` with `accountNumber`, `holder`, `balance`
2. Methods: `deposit()`, `withdraw()`, `displayBalance()`
3. Class `SavingsAccount` extends `BankAccount`:
   - Adds `interestRate` property
   - Method `applyInterest()`
   - Withdrawal limit of 3 times per month
4. Class `CheckingAccount` extends `BankAccount`:
   - Adds `overdraftLimit` property
   - Can withdraw beyond balance up to overdraft limit
5. Test all account types

---

