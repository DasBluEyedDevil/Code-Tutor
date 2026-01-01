---
type: "ANALOGY"
title: "Understanding the Concept"
---

Think of building a web API like opening a food stand:

TRADITIONAL API (Full Restaurant):
- Full kitchen with every appliance
- Wait staff, hosts, managers
- Menu changes handled at runtime
- Flexible but heavy setup

MINIMAL API (Food Truck):
- Just what you need to serve food
- Streamlined operations
- Fixed menu, optimized workflow
- Light, fast, mobile

MINIMAL API + AOT (Pre-Packaged Food Truck):
- Everything prepared before opening
- No cooking at runtime, just serving
- Fastest possible service
- Perfect for specific, focused menus

WHY MINIMAL APIS FOR AOT:
- Less framework overhead
- Explicit type declarations (no reflection)
- Source generators for JSON
- Optimized for startup time

KEY DIFFERENCES:

Traditional Controllers:
- [ApiController], [Route], [HttpGet] attributes
- Model binding via reflection
- Complex routing rules

Minimal APIs:
- app.MapGet("/path", handler)
- Explicit parameter types
- Simple, direct routing

AOT REQUIREMENTS:
- Use source-generated JSON contexts
- Explicit type parameters
- Avoid dynamic features
- Configure AOT in project file

Think: 'Minimal APIs + AOT = Food truck with everything pre-packaged. Setup once, serve instantly!'