---
type: "ANALOGY"
title: "Understanding the Concept"
---

Imagine a bank vault. You can't just walk in and grab money - you need to go through a teller who validates your request, checks your balance, and controls access!

PROPERTIES are like that teller. Instead of exposing fields directly (public string Name;), you use properties with get and set to control HOW data is accessed and modified.

Why? VALIDATION and SECURITY!
• Check if age is valid (0-120) before storing
• Make data read-only (get only, no set)
• Calculate values on the fly (FullName = FirstName + LastName)

Properties look like fields when you use them, but they're actually methods in disguise! This is called ENCAPSULATION - hiding implementation details and controlling access.