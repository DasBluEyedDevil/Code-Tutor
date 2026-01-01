---
type: "KEY_POINT"
title: "Pull Requests: The Heart of Collaboration"
---

A PULL REQUEST (PR) is a request to merge your branch into another branch.

It's called a 'pull' request because you're asking the maintainer to 'pull' your changes into their branch.

WHY USE PULL REQUESTS?
1. CODE REVIEW: Teammates can review your code before it's merged
2. DISCUSSION: Discuss changes, ask questions, suggest improvements
3. QUALITY GATE: Run automated tests before merging
4. DOCUMENTATION: PRs create a record of why changes were made

PR WORKFLOW:

1. Create a feature branch and push it:
   $ git switch -c feature/add-validation
   $ # make changes, commit
   $ git push -u origin feature/add-validation

2. On GitHub:
   - Click 'Compare & pull request' (or go to Pull Requests > New)
   - Write a description of your changes
   - Request reviewers
   - Submit the PR

3. Reviewers examine your code:
   - Leave comments on specific lines
   - Request changes if needed
   - Approve when satisfied

4. Merge the PR:
   - Click 'Merge pull request' on GitHub
   - Delete the feature branch

5. Update your local main:
   $ git switch main
   $ git pull origin main