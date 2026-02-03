---
type: "ANALOGY"
title: "The Contract Between Departments"
---

Shared types are like a formal contract between two departments in a company. The backend team (API) and frontend team (React) agree on the exact shape of every message they exchange. If the contract says a Task has `id`, `title`, `status`, and `dueDate`, both sides must honor that. When the contract lives in a shared package, changing a field name in one place immediately flags mismatches everywhere else -- no more "but the API sends `due_date` and the frontend expects `dueDate`" bugs.
