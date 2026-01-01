---
type: "WARNING"
title: "Common GitHub Mistakes"
---

MISTAKES TO AVOID:

1. PUSHING TO MAIN DIRECTLY:
   Problem: Bypasses code review, can introduce bugs
   Solution: Always use feature branches and PRs

2. FORCE PUSHING TO SHARED BRANCHES:
   Problem: Destroys teammates' work, causes sync issues
   Solution: Never use 'git push --force' on shared branches
   Only force push on your own feature branches if needed

3. NOT PULLING BEFORE STARTING WORK:
   Problem: Your changes conflict with recent updates
   Solution: Always 'git pull' on main before creating a branch

4. IGNORING CI/CD FAILURES:
   Problem: Broken builds get merged into main
   Solution: Fix failing tests before merging

5. HUGE PULL REQUESTS:
   Problem: Hard to review, high risk of bugs
   Solution: Keep PRs small (<400 lines is a good target)
   Break large features into multiple smaller PRs

6. NOT READING PROJECT CONTRIBUTION GUIDELINES:
   Problem: Your PR doesn't follow project conventions
   Solution: Always read CONTRIBUTING.md before contributing

7. COMMITTING SENSITIVE DATA:
   Problem: Passwords/keys exposed publicly forever
   Solution: Use .gitignore, environment variables
   If exposed: rotate the credentials IMMEDIATELY