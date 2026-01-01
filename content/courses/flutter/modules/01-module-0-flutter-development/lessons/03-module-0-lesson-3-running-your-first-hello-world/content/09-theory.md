---
type: "THEORY"
title: "Understanding Hot Reload vs Hot Restart"
---


These are two important concepts:

- **Hot Reload** (`Ctrl/Cmd + S` or the lightning bolt icon):
  - Injects your code changes into the running app
  - Keeps the app's current state (counter stays at 10)
  - Takes 1-2 seconds
  - Use this 95% of the time

- **Hot Restart** (`Ctrl/Cmd + Shift + F5` or circular arrow icon):
  - Restarts the app from scratch
  - Resets all state (counter goes back to 0)
  - Takes a few seconds
  - Use this when something seems broken

