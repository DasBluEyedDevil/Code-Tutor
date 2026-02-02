---
type: "ANALOGY"
title: "The Air Traffic Control"
---

Imagine you're managing a busy airport.

1.  **Local Checks:** Every pilot has their own checklist and "local" procedure for handling small issues in the cockpit. (These are your `try/catch` blocks).
2.  **The Safety Net:** But what if something happens that the pilot can't handle, or what if the radio fails? 
3.  **The Tower:** You have **Air Traffic Control (ATC)**. They monitor every flight from a "Global" perspective. If a plane disappears from the radar or sends an emergency signal, the tower sees it, even if no one inside the plane reported it.

Global error handlers are your **Air Traffic Control**. They are the final safety net that monitors your entire application. They catch the errors that "escaped" the local checklists. When an unhandled crash happens, the Tower's job isn't to fix the plane mid-air—it's to record the event, alert the engineers, and make sure no other planes crash because of it.
