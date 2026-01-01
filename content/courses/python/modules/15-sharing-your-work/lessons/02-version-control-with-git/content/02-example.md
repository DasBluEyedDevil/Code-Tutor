---
type: "EXAMPLE"
title: "Code Example: Git Basics"
---

**Git workflow stages:**

```
Working Directory  →  Staging Area  →  Repository
   (changes)          (git add)         (git commit)
```

**Branch strategy:**
```
main          ○──○──○──○──○
               \      /
feature         ○──○──○
```

**Commit message format:**
```
Short summary (50 chars max)

Longer description if needed.
Explain WHY, not what.
```

**Good commit messages:**
- "Add user authentication with JWT"
- "Fix null pointer error in login"
- "Refactor database queries for performance"

**Bad commit messages:**
- "Fix bug"
- "Update"
- "WIP"
- "asdfghjkl"

```python
# Git commands demonstration (as comments)
# Run these in your terminal, not Python

print("=== Git Setup (One-time) ===")
"""
# Configure your identity
git config --global user.name "Your Name"
git config --global user.email "your.email@example.com"

# Check configuration
git config --list
"""

print("\n=== Creating a Repository ===")
"""
# Method 1: Start new project
mkdir my-project
cd my-project
git init
# Creates .git folder (hidden)

# Method 2: Clone existing
git clone https://github.com/username/repo.git
cd repo
"""

print("\n=== Basic Workflow ===")
"""
# 1. Check status
git status
# Shows: modified files, staged files, branch

# 2. Stage files (prepare for commit)
git add file.py          # Specific file
git add .                # All files
git add *.py             # All Python files

# 3. Commit changes
git commit -m "Add user authentication"
# -m = message describing what changed

# 4. View history
git log
git log --oneline        # Condensed view
git log --graph          # Visual branch graph

# 5. Push to remote (GitHub)
git push origin main
# origin = remote name (default)
# main = branch name
"""

print("\n=== Working with Branches ===")
"""
# Create new branch
git branch feature/login        # Create
git checkout feature/login      # Switch to it
# Or combined:
git checkout -b feature/login   # Create + switch

# List branches
git branch                      # Local branches
git branch -a                   # All (including remote)

# Switch branches
git checkout main
git checkout feature/login

# Delete branch
git branch -d feature/login     # Safe delete (merged only)
git branch -D feature/login     # Force delete
"""

print("\n=== Merging Branches ===")
"""
# Merge feature into main
git checkout main               # Switch to main
git merge feature/login         # Merge feature in

# If conflicts occur:
# 1. Git marks conflicts in files:
#    <<<<<<< HEAD
#    code from main
#    =======
#    code from feature
#    >>>>>>> feature/login
#
# 2. Manually resolve (edit file)
# 3. Stage resolved files
#    git add conflicted-file.py
# 4. Commit the merge
#    git commit -m "Merge feature/login"
"""

print("\n=== Undoing Changes ===")
"""
# Discard changes in working directory
git checkout -- file.py         # Restore from last commit

# Unstage files (undo git add)
git reset HEAD file.py

# Undo last commit (keep changes)
git reset --soft HEAD~1

# Undo last commit (discard changes)
git reset --hard HEAD~1
# WARNING: Can't recover discarded changes!

# Create new commit that undoes a previous commit
git revert <commit-hash>
# Safer than reset, keeps history
"""

print("\n=== Remote Operations ===")
"""
# View remotes
git remote -v

# Add remote
git remote add origin https://github.com/username/repo.git

# Fetch changes (download, don't merge)
git fetch origin

# Pull changes (fetch + merge)
git pull origin main
# Equivalent to:
#   git fetch origin
#   git merge origin/main

# Push branch to remote
git push -u origin feature/login
# -u sets upstream tracking
# After this, just: git push
"""

print("\n=== .gitignore File ===")
"""
# Create .gitignore in project root
# Lists files/folders Git should ignore

Example .gitignore:
```
# Environment
.env
venv/
__pycache__/
*.pyc

# IDE
.vscode/
.idea/
*.swp

# Database
*.db
*.sqlite

# OS
.DS_Store
Thumbs.db

# Logs
*.log
```
"""

print("\n=== Common Git Scenarios ===")

# Scenario demonstrations
scenarios = [
    {
        "title": "Start new feature",
        "commands": [
            "git checkout main",
            "git pull origin main",
            "git checkout -b feature/new-api",
            "# Make changes...",
            "git add .",
            "git commit -m 'Add new API endpoint'",
            "git push -u origin feature/new-api"
        ]
    },
    {
        "title": "Fix a bug",
        "commands": [
            "git checkout main",
            "git checkout -b bugfix/login-error",
            "# Fix the bug...",
            "git add file.py",
            "git commit -m 'Fix login validation error'",
            "git push -u origin bugfix/login-error"
        ]
    },
    {
        "title": "Update from main",
        "commands": [
            "git checkout feature/my-branch",
            "git fetch origin",
            "git merge origin/main",
            "# Resolve any conflicts...",
            "git push"
        ]
    }
]

for scenario in scenarios:
    print(f"\n{scenario['title']}:")
    for cmd in scenario['commands']:
        if cmd.startswith('#'):
            print(f"  {cmd}")
        else:
            print(f"  $ {cmd}")

print("\n=== Git Best Practices ===")

best_practices = [
    "✓ Commit often, push daily",
    "✓ Write clear commit messages",
    "✓ One feature = one branch",
    "✓ Pull before you push",
    "✓ Never commit secrets (.env, passwords)",
    "✓ Keep commits focused (one logical change)",
    "✓ Review changes before committing (git diff)",
    "✓ Use branches for experiments",
    "✓ Delete merged branches",
    "✓ Don't rewrite public history (main branch)"
]

for practice in best_practices:
    print(practice)
```
