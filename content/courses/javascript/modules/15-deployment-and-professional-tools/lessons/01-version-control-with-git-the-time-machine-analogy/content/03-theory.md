---
type: "THEORY"
title: "Breaking Down the Syntax"
---

Git fundamentals:

1. **Setting Up Git**:
   ```bash
   # Configure Git (one time)
   git config --global user.name "Your Name"
   git config --global user.email "you@example.com"
   
   # Create new repository
   git init
   
   # Or clone existing
   git clone https://github.com/username/repo.git
   ```

2. **Basic Workflow**:
   ```bash
   # 1. Check status
   git status
   
   # 2. Stage changes
   git add filename.js      # Single file
   git add .                # All files
   
   # 3. Commit
   git commit -m "Add user login feature"
   
   # 4. Push to remote (GitHub)
   git push
   ```

3. **Branches**:
   ```bash
   # Create and switch to new branch
   git checkout -b feature/new-feature
   
   # Switch between branches
   git checkout main
   git checkout feature/new-feature
   
   # Merge branch into current
   git checkout main
   git merge feature/new-feature
   
   # Delete branch
   git branch -d feature/new-feature
   ```

4. **Viewing History**:
   ```bash
   # See commits
   git log
   git log --oneline    # Compact view
   
   # See changes
   git diff             # Unstaged changes
   git diff --staged    # Staged changes
   ```

5. **.gitignore** - Files to never commit:
   ```
   node_modules/
   .env
   .DS_Store
   dist/
   build/
   *.log
   ```

6. **GitHub Workflow**:
   ```bash
   # First time setup
   git remote add origin https://github.com/username/repo.git
   git push -u origin main
   
   # Regular updates
   git pull    # Download changes
   git push    # Upload changes
   ```

7. **Commit Messages** (best practices):
   - Start with verb: "Add", "Fix", "Update", "Remove"
   - Be specific: "Fix login button alignment"
   - Not vague: "Fixed stuff" or "Changes"
   - Present tense: "Add feature" not "Added feature"