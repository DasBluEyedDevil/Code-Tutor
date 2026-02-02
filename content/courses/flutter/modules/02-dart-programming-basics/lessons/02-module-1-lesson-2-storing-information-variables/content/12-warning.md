---
type: "WARNING"
title: "Variable Pitfalls to Avoid"
---


**Watch out for these common traps:**

1. **Confusing text numbers with actual numbers**
   - `var age = '25';` - This is TEXT, not a number!
   - `var age = 25;` - This is an actual number you can do math with

2. **Forgetting to declare variables first**
   - ❌ `name = 'Alex';` (Error - where did `name` come from?)
   - ✅ `var name = 'Alex';` (Correct - we're creating it)

3. **Declaring a variable twice**
   - ❌ `var age = 25; var age = 30;` (Error!)
   - ✅ `var age = 25; age = 30;` (Correct - updating existing)

4. **Using variables before they exist**
   - The variable must be created BEFORE you use it
   - Dart reads top-to-bottom!

5. **Type confusion with `var`**
   - Once Dart decides a variable's type, you can't change it
   - `var x = 5;` then `x = 'hello';` = ERROR!

