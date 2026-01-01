---
type: "KEY_POINT"
title: "Branches: Parallel Universes for Your Code"
---

A BRANCH is an independent line of development.

Think of it like a tree:
- The MAIN branch (trunk) is your stable, production-ready code
- FEATURE branches (branches) grow from the trunk to develop new features
- When a feature is complete, you MERGE it back to the trunk

Visually:
                    feature/add-history
                   /
main: A---B---C---D---E---F---G
                       \
                        feature/multiplication

Each branch has its own separate timeline:
- main: The stable, working version
- feature/add-history: Experimental history feature
- feature/multiplication: New math operations

KEY BENEFITS:
1. ISOLATION: Bugs in one branch don't affect others
2. EXPERIMENTATION: Try risky changes safely
3. COLLABORATION: Different people work on different branches
4. ORGANIZATION: Keep work-in-progress separate from stable code

BRANCHES ARE CHEAP:
- Creating a branch takes milliseconds
- Switching branches is instant
- Having many branches costs almost nothing