---
type: "THEORY"
title: "The Evolution of Navigation"
---


You've learned two navigation approaches:
1. **Basic Navigation**: `Navigator.push(MaterialPageRoute(...))`
2. **Named Routes**: `Navigator.pushNamed('/route')`

Both work, but they're **imperative** - you tell Flutter exactly what to do, step by step.

**Problem with imperative navigation:**
- Hard to handle deep links (`myapp://product/123`)
- Hard to sync URL bar on web
- Hard to manage complex navigation state
- Difficult to test

**Solution: Declarative Navigation with GoRouter!**

Think of it like building with LEGO blocks:
- **Imperative**: "Take this block, put it here, now take that block..."
- **Declarative**: "Here's the blueprint, you build it!"

