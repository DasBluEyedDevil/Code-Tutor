---
type: "THEORY"
title: "The Problem: Parallel Development"
---

Imagine you're building a calculator app. Your calculator works perfectly for addition and subtraction. Now you want to add new features:

1. Multiplication and division
2. A history feature to show past calculations
3. A graphing mode for visualizing equations

Problem: If you work on all three simultaneously in the same code:
- Feature 1 might break while working on Feature 2
- If Feature 3 has a bug, it's mixed in with Features 1 and 2
- Your teammate working on Feature 1 will conflict with your Feature 2 changes
- You can't release Feature 1 (which is done) without also releasing buggy Feature 3

You need a way to:
✓ Work on features independently
✓ Keep the main code stable
✓ Switch between features easily
✓ Combine features when they're ready

This is exactly what BRANCHES do!