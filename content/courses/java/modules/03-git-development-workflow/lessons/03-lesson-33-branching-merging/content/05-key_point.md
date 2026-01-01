---
type: "KEY_POINT"
title: "Merging: Combining Branches"
---

MERGING combines changes from one branch into another.

Typical workflow:
1. Complete your feature on a feature branch
2. Switch to main (the target branch)
3. Merge the feature branch into main

BASIC MERGE SYNTAX:

$ git switch main                     # Go to target branch
$ git merge feature/add-multiply      # Merge feature into main


TYPES OF MERGES:

1. FAST-FORWARD MERGE:
   If main hasn't changed since you branched:
   
   Before:   main: A---B
                        \
             feature:    C---D
   
   After:    main: A---B---C---D
   
   Git just moves the main pointer forward.

2. THREE-WAY MERGE (Merge Commit):
   If main has new commits since you branched:
   
   Before:   main: A---B---E---F
                        \
             feature:    C---D
   
   After:    main: A---B---E---F---M
                        \         /
             feature:    C---D---/
   
   Git creates a new 'merge commit' (M) that combines both histories.


AFTER MERGING:
$ git branch -d feature/add-multiply  # Delete the merged branch

Keep your branch list clean - delete branches after merging!