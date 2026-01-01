---
type: "THEORY"
title: "Access Modifiers: public, private, protected"
---

Java has keywords that control WHO can access your code:

1. PUBLIC - Anyone can access
   public int speed;  // Any code anywhere can read/write

2. PRIVATE - Only THIS class can access
   private int speed;  // Only methods inside Car can access

3. PROTECTED - This class + subclasses (we'll learn later)

4. DEFAULT (no keyword) - Same package only

BEST PRACTICE:
- Fields: ALWAYS private
- Methods: public if others need them, private if internal only