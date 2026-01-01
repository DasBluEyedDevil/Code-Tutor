---
type: "THEORY"
title: "git log - Viewing History"
---

The 'git log' command shows the commit history of your repository.

BASIC USAGE:
$ git log

OUTPUT:
commit a1b2c3d4e5f6789 (HEAD -> main)
Author: Your Name <you@email.com>
Date:   Mon Jan 15 10:30:00 2025 -0500

    feat: Add Calculator class with add method

commit 9876543210abcdef
Author: Your Name <you@email.com>
Date:   Sun Jan 14 15:45:00 2025 -0500

    Initial project setup


USEFUL VARIATIONS:

$ git log --oneline              # Compact one-line format
a1b2c3d feat: Add Calculator class
9876543 Initial project setup

$ git log -n 3                   # Show only last 3 commits

$ git log --stat                 # Show files changed in each commit

$ git log --graph                # Show branching visually

$ git log --author='Name'        # Filter by author

$ git log --since='2025-01-01'   # Filter by date


EXITING THE LOG:
If the log is longer than your terminal, use:
- Press 'q' to quit
- Press Space to scroll down
- Press 'b' to scroll up