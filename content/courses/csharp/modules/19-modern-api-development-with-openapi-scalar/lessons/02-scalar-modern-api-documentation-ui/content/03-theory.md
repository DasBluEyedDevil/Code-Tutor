---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`app.MapScalarApiReference()`**: Adds Scalar UI to your app. By default available at `/scalar/v1`. Reads your OpenAPI spec and generates beautiful documentation.

**`.WithTitle("My API")`**: Sets the title displayed in the Scalar header. Use your API's name or product name.

**`.WithTheme(ScalarTheme.Purple)`**: Choose from built-in themes. Options: Default, Purple, Solarized, BluePlanet, Saturn, Kepler, Mars, DeepSpace.

**`.WithDarkMode(true)`**: Enable dark mode by default. Users can still toggle. Modern developers often prefer dark mode.

**`.WithDefaultHttpClient(target, client)`**: Set the default code example language. ScalarTarget.CSharp shows C# examples first.

**`.WithPreferredScheme("Bearer")`**: Hint for authentication. Tells Scalar your API uses JWT Bearer tokens.

**`.WithSummary("...")`**: Short one-line summary shown in endpoint lists. Keep it brief - 3-5 words ideal.

**`.WithDescription("...")`**: Longer explanation shown when endpoint is expanded. Can include details about behavior, requirements.

**Scalar vs Swagger**: Scalar is a modern alternative with better UX. Both use OpenAPI spec, so you can switch anytime.