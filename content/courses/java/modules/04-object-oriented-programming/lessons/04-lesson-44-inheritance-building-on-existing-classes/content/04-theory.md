---
type: "THEORY"
title: "The 'super' Keyword"
---

'super' refers to the PARENT class:

1. CALLING PARENT CONSTRUCTOR:
   public Dog(String name, int age) {
       super(name, age);  // Must be FIRST line
   }

2. CALLING PARENT METHOD:
   public void eat() {
       super.eat();  // Call Animal's eat() first
       IO.println("Dog is satisfied.");
   }

3. ACCESSING PARENT FIELD:
   IO.println(super.name);  // Usually just use 'name'

NOTE: If you don't call super(...) explicitly, Java tries to call
the parent's no-argument constructor automatically.