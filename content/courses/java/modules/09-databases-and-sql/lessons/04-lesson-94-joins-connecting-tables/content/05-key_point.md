---
type: "KEY_POINT"
title: "JOINs are Like Matching Puzzle Pieces"
---

INNER JOIN = Only matched pieces:
Student ⟷ Enrollment
  🧩─🧩  (connected)
  🧩─🧩  (connected)
  🧩 (no match, excluded)

LEFT JOIN = Keep all left pieces:
Student → Enrollment
  🧩─🧩  (connected)
  🧩─🧩  (connected)
  🧩─❓  (student kept, enrollment NULL)

Think of LEFT table as the "main" one you want to keep.