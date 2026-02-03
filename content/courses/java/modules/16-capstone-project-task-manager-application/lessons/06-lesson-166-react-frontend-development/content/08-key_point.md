---
type: "KEY_POINT"
title: "Comparing the Two Paths"
---

Both paths produce a working Task Manager. Here is a quick comparison:

Thymeleaf Path:
- Setup time: Minutes (just add a dependency)
- Deployment: Single JAR file
- JavaScript needed: None (or minimal for progressive enhancement)
- Learning curve: Low if you already know Spring MVC
- Best for: Server-rendered applications, forms-heavy CRUD apps, rapid prototyping

React Path:
- Setup time: Longer (separate project, Node.js toolchain)
- Deployment: Two services (backend JAR + frontend static files or container)
- JavaScript needed: Extensive (JSX, hooks, state management)
- Learning curve: Higher (new language, new paradigm)
- Best for: Dynamic SPAs, real-time features, rich interactive UIs

Both paths teach the same architectural principles: separation of concerns, proper error handling, and clean API design. The Thymeleaf path just keeps both concerns in one project.

Whichever path you chose, continue to Lesson 07 where you will connect the frontend to the backend (Thymeleaf: form handling and CSRF; React: CORS and JWT flow) and Lesson 08 where you will add tests for both the backend and your chosen frontend approach.
