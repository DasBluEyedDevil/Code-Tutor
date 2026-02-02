---
type: "WARNING"
title: "Common Pitfalls"
---

Common Git mistakes:

1. **Forgetting to commit**:
   ```bash
   # Made lots of changes, forgot to commit
   # Now have 50 files changed
   # Hard to write one commit message!
   
   # Better: Commit after each feature
   git add .
   git commit -m "Add user login"
   # Then continue coding
   ```

2. **Committing secrets**:
   ```bash
   # NEVER commit .env files!
   # Add to .gitignore:
   .env
   .env.local
   config/secrets.js
   ```

3. **Not using .gitignore**:
   ```bash
   # Don't commit node_modules!
   # Create .gitignore file:
   node_modules/
   .DS_Store
   dist/
   *.log
   ```

4. **Merge conflicts** (scary but normal!):
   ```
   <<<<<<< HEAD
   const API_URL = 'http://localhost:3000';
   =======
   const API_URL = 'https://api.production.com';
   >>>>>>> feature-branch
   ```
   - Choose which version to keep
   - Delete conflict markers (<<<<, ====, >>>>)
   - Commit the resolved file

5. **Wrong branch**:
   ```bash
   # Check current branch BEFORE committing!
   git branch        # Shows all branches
   git status        # Shows current branch
   
   # If on wrong branch:
   git stash         # Save changes temporarily
   git checkout correct-branch
   git stash pop     # Apply changes here
   ```

6. **Push without pull**:
   ```bash
   # Error: Updates were rejected
   # Someone else pushed while you were working
   
   # Fix:
   git pull          # Get their changes
   # Resolve conflicts if any
   git push          # Now it works
   ```

7. **Vague commit messages**:
   ```bash
   # Bad:
   git commit -m "fixes"
   git commit -m "stuff"
   git commit -m "asdf"
   
   # Good:
   git commit -m "Fix login button alignment on mobile"
   git commit -m "Add password reset functionality"
   git commit -m "Update dependencies to latest versions"
   ```