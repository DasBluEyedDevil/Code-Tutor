---
type: "ANALOGY"
title: "Understanding the Concept"
---

Imagine opening a restaurant:

WITHOUT COMPILED MODELS:
Every morning, chef rebuilds the entire menu from scratch:
- Reads all recipes
- Calculates ingredients
- Sets up stations
- 30 minutes before serving!

WITH COMPILED MODELS:
Chef pre-builds menu once, stores it:
- Opens restaurant
- Menu already ready
- Serves immediately!

EF Core normally builds your model at startup:
- Scans all entity classes
- Discovers relationships
- Configures conventions
- For 500 entities = SLOW startup!

Compiled Models do this at BUILD time:
- Model pre-generated as C# code
- Startup just loads the code
- 60-80% faster startup for large models!

NEW in EF Core 9: AUTO-COMPILED MODELS!
- Install Microsoft.EntityFrameworkCore.Tasks package
- Model regenerates AUTOMATICALLY when you build
- No more forgetting to run 'dotnet ef dbcontext optimize'!

Think: Compiled Models = 'Pre-cooked model, just reheat and serve!'