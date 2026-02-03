---
type: "ANALOGY"
title: "Type Mistakes That Cost Companies Millions"
---

## Real-World Type Disasters

Choosing the wrong data type might seem like a minor decision, but history is full of costly examples.

**The Patriot Missile Bug (1991)**
A timing calculation used a floating-point representation that accumulated rounding errors over time. After 100 hours of operation, the error grew large enough that the missile defense system failed to intercept an incoming missile, resulting in 28 deaths. The fix? Using a more precise numeric type.

**The Ariane 5 Explosion (1996)**
A 64-bit floating-point number was converted to a 16-bit signed integer. The value exceeded the maximum the smaller type could hold, causing an overflow that led to a $370 million rocket explosion just 37 seconds after launch.

**Y2K: The Two-Digit Year Problem**
To save memory (expensive in the 1960s-70s), programmers stored years as two digits. When 2000 approached, systems couldn't distinguish 1900 from 2000. The global remediation effort cost an estimated $300 billion.

**Banking Rounding Errors**
Banks processing millions of transactions per day have discovered that using `double` instead of `decimal` can cause pennies to appear or disappear due to floating-point rounding. These errors compound over time and create audit nightmares.

**The Lesson for ShopFlow**
Every type decision in our e-commerce platform matters. Using `decimal` for money, proper nullable types for optional fields, and appropriate ID types prevents subtle bugs that could affect financial accuracy and customer trust.