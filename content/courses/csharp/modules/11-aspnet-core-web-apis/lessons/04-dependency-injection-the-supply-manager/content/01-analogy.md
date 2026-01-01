---
type: "ANALOGY"
title: "Understanding the Concept"
---

Imagine a chef in a restaurant:

BAD WAY (creating dependencies yourself):
• Chef grows vegetables
• Chef raises chickens
• Chef makes plates
• Chef builds oven
• THEN cooks!

GOOD WAY (dependencies provided):
• Kitchen manager PROVIDES ingredients
• Kitchen manager PROVIDES tools
• Chef just COOKS!

That's DEPENDENCY INJECTION (DI)!

Dependency = Something your code needs to work (database, logger, email service)

Instead of creating dependencies yourself:
• You DECLARE what you need (interface or class type)
• ASP.NET Core PROVIDES it (injects it)
• You just USE it!

Benefits:
• TESTABLE - Swap real database for fake one in tests
• FLEXIBLE - Change implementations easily
• CLEAN - No 'new' everywhere!

Think: DI = 'Don't create what you need. Ask for it, and it will be provided!'