---
type: "ANALOGY"
title: "The Test Kitchen, Dress Rehearsal, and Opening Night"
---

Build flavors and environments are like the stages of producing a restaurant's menu. The **development environment** is your test kitchen -- you experiment freely, taste-test with fake ingredients (mock data), and nobody sees your failures. The **staging environment** is a dress rehearsal -- real recipes, real equipment, invited testers giving feedback, but the restaurant is not open to the public. The **production environment** is opening night -- real customers, real food, and everything has to work perfectly.

Each environment has different configuration: the test kitchen connects to a local stove, staging uses the same commercial oven as production but with a smaller gas line, and production runs at full capacity. In your app, this translates to different API URLs, different API keys, different logging levels, and different feature flags.

**Build flavors let you package the same code with different configurations** so that your development app points to a test server, your staging app points to a pre-production server, and your release app points to the real thing -- all from the same codebase, built with a single command.
