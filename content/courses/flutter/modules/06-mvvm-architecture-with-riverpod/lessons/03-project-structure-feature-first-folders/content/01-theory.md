---
type: "THEORY"
title: "Why Folder Structure Matters"
---

When you start a Flutter project, you can technically put all your code in one file. For a tiny app, that works. But as your app grows, finding and organizing code becomes a nightmare.

**Small apps (1-3 screens):** Any structure works. You can find files quickly because there are so few.

**Medium apps (4-10 screens):** You start losing track. Where did I put that helper function? Which file has the user model?

**Large apps (10+ screens):** Without good structure, you spend more time searching for code than writing it. New team members take weeks to understand the project.

**Team apps:** Everyone needs to know where things go. If Alice puts models in `/models` and Bob puts them in `/data/entities`, chaos follows.

### The Library Analogy

Imagine organizing a library:

**Bad organization:** Books shelved randomly. Finding a cookbook means searching every shelf in the building.

**Better organization (by type):** All hardcovers together, all paperbacks together. Still hard to find that cookbook though!

**Best organization (by topic/genre):** All cookbooks in one section, all mysteries in another. Need a cookbook? Go to the cookbook section. Simple.

Project folder structure works the same way. We want to organize code so that finding and modifying features is as easy as finding cookbooks in a well-organized library.