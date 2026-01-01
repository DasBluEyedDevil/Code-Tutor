---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
// GIT BASICS

// Initialize repository
git init

// Check status
git status

// Add files to staging
git add .
git add Program.cs

// Commit changes
git commit -m "Add product CRUD endpoints"

// View history
git log
git log --oneline

// BRANCHING
git branch feature/add-auth  // Create branch
git checkout feature/add-auth  // Switch to branch
git checkout -b feature/new-ui  // Create and switch

// Work on feature, then merge
git checkout main
git merge feature/add-auth

// REMOTE (GitHub/Azure DevOps)
git remote add origin https://github.com/user/repo.git
git push -u origin main
git pull origin main

// TYPICAL WORKFLOW
// 1. Create feature branch
git checkout -b feature/user-profile

// 2. Make changes, commit often
git add .
git commit -m "Add user profile page"

// 3. Push to remote
git push origin feature/user-profile

// 4. Create Pull Request on GitHub
// 5. Review, merge to main

// USEFUL COMMANDS
git diff  // See changes
git restore Program.cs  // Undo changes
git reset --soft HEAD~1  // Undo last commit
git stash  // Save work temporarily
git stash pop  // Restore stashed work
```
