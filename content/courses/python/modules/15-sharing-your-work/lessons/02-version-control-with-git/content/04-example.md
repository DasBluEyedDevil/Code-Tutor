---
type: "EXAMPLE"
title: "Code Example: Python Git Automation"
---

**Python + Git integration:**

**Using subprocess:**
```python
import subprocess

result = subprocess.run(
    ['git', 'status'],
    capture_output=True,
    text=True
)
print(result.stdout)
```

**Git hooks:**
- Scripts that run on Git events
- Located in `.git/hooks/`
- Can prevent commits/pushes

**Common hooks:**
- `pre-commit`: Before commit
- `commit-msg`: Validate commit message
- `pre-push`: Before push
- `post-commit`: After commit

**Use cases:**
- Run tests before commit
- Check code style
- Prevent committing secrets
- Generate documentation
- Notify team members

```python
import subprocess
import os
from pathlib import Path
from typing import List, Optional

print("=== Git Automation with Python ===")

class GitHelper:
    """Helper class for Git operations via Python"""
    
    def __init__(self, repo_path: str = "."):
        self.repo_path = Path(repo_path)
    
    def run_command(self, command: List[str]) -> tuple:
        """Run git command and return output"""
        try:
            result = subprocess.run(
                command,
                cwd=self.repo_path,
                capture_output=True,
                text=True,
                check=True
            )
            return True, result.stdout.strip()
        except subprocess.CalledProcessError as e:
            return False, e.stderr.strip()
    
    def get_status(self) -> str:
        """Get git status"""
        success, output = self.run_command(["git", "status", "--short"])
        return output if success else "Error getting status"
    
    def get_current_branch(self) -> str:
        """Get current branch name"""
        success, output = self.run_command(["git", "branch", "--show-current"])
        return output if success else "unknown"
    
    def get_last_commit(self) -> str:
        """Get last commit message"""
        success, output = self.run_command(
            ["git", "log", "-1", "--pretty=format:%h - %s"]
        )
        return output if success else "No commits"
    
    def list_branches(self) -> List[str]:
        """List all local branches"""
        success, output = self.run_command(["git", "branch"])
        if not success:
            return []
        
        branches = [
            line.strip().replace("* ", "")
            for line in output.split("\n")
            if line.strip()
        ]
        return branches
    
    def create_branch(self, branch_name: str) -> bool:
        """Create new branch"""
        success, _ = self.run_command(["git", "checkout", "-b", branch_name])
        return success
    
    def switch_branch(self, branch_name: str) -> bool:
        """Switch to existing branch"""
        success, _ = self.run_command(["git", "checkout", branch_name])
        return success
    
    def add_files(self, files: str = ".") -> bool:
        """Stage files for commit"""
        success, _ = self.run_command(["git", "add", files])
        return success
    
    def commit(self, message: str) -> bool:
        """Commit staged changes"""
        success, _ = self.run_command(["git", "commit", "-m", message])
        return success
    
    def push(self, remote: str = "origin", branch: Optional[str] = None) -> bool:
        """Push to remote"""
        if branch is None:
            branch = self.get_current_branch()
        
        success, _ = self.run_command(["git", "push", remote, branch])
        return success
    
    def pull(self, remote: str = "origin", branch: Optional[str] = None) -> bool:
        """Pull from remote"""
        if branch is None:
            branch = self.get_current_branch()
        
        success, _ = self.run_command(["git", "pull", remote, branch])
        return success
    
    def get_modified_files(self) -> List[str]:
        """Get list of modified files"""
        success, output = self.run_command(["git", "diff", "--name-only"])
        if not success or not output:
            return []
        
        return output.split("\n")
    
    def get_commit_count(self) -> int:
        """Get total commit count"""
        success, output = self.run_command(["git", "rev-list", "--count", "HEAD"])
        if success and output.isdigit():
            return int(output)
        return 0

print("\n=== Using Git Helper ===")

# Note: This would work in a real Git repository
# For demonstration, we'll show the usage pattern

print("\nExample usage:")
print("""
git = GitHelper()

# Check status
status = git.get_status()
print(f"Status: {status}")

# Get current branch
branch = git.get_current_branch()
print(f"Current branch: {branch}")

# Create and switch to feature branch
if git.create_branch('feature/new-feature'):
    print("Branch created successfully")

# Make some changes to files...

# Stage and commit
if git.add_files('.'):
    if git.commit('Add new feature'):
        print("Changes committed")

# Push to remote
if git.push():
    print("Pushed to remote")

# List all branches
branches = git.list_branches()
print(f"Branches: {branches}")

# Get commit count
count = git.get_commit_count()
print(f"Total commits: {count}")
""")

print("\n=== Pre-commit Hook Example ===")

# Example pre-commit hook script
pre_commit_hook = '''
#!/usr/bin/env python3
"""Pre-commit hook: Run checks before allowing commit"""

import subprocess
import sys

def run_tests():
    """Run test suite"""
    result = subprocess.run(['pytest', 'tests/'], capture_output=True)
    return result.returncode == 0

def check_code_style():
    """Check Python code style with flake8"""
    result = subprocess.run(['flake8', '.'], capture_output=True)
    return result.returncode == 0

def check_no_debug_prints():
    """Ensure no print() statements in production code"""
    result = subprocess.run(
        ['git', 'diff', '--cached', '--name-only'],
        capture_output=True,
        text=True
    )
    
    for file in result.stdout.split():
        if file.endswith('.py') and 'test' not in file:
            with open(file) as f:
                if 'print(' in f.read():
                    print(f"❌ Debug print() found in {file}")
                    return False
    return True

if __name__ == '__main__':
    print("Running pre-commit checks...")
    
    checks = [
        ("Tests", run_tests),
        ("Code style", check_code_style),
        ("No debug prints", check_no_debug_prints)
    ]
    
    for name, check_func in checks:
        print(f"Checking {name}...", end=" ")
        if check_func():
            print("✓")
        else:
            print("✗")
            print(f"\n❌ {name} check failed. Commit aborted.")
            sys.exit(1)
    
    print("\n✓ All checks passed. Committing...")
    sys.exit(0)
'''

print("Pre-commit hook script:")
print(pre_commit_hook)

print("\n=== Git Statistics Script ===")

stat_script = '''
import subprocess
from collections import Counter

def get_git_stats():
    """Generate repository statistics"""
    
    # Total commits
    result = subprocess.run(
        ['git', 'rev-list', '--count', 'HEAD'],
        capture_output=True,
        text=True
    )
    total_commits = result.stdout.strip()
    
    # Contributors
    result = subprocess.run(
        ['git', 'shortlog', '-sn', '--all'],
        capture_output=True,
        text=True
    )
    contributors = result.stdout.strip().split('\\n')
    
    # Lines of code
    result = subprocess.run(
        ['git', 'ls-files'],
        capture_output=True,
        text=True
    )
    files = result.stdout.strip().split('\\n')
    
    print(f"Total commits: {total_commits}")
    print(f"Total files: {len(files)}")
    print(f"\\nTop contributors:")
    for contributor in contributors[:5]:
        print(f"  {contributor}")

get_git_stats()
'''

print("Git statistics script:")
print(stat_script)
```
