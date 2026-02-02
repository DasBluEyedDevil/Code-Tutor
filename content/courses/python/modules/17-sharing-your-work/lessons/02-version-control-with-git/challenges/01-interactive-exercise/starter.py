import subprocess
from typing import Optional

class GitCommitHelper:
    def is_git_repo(self) -> bool:
        # TODO: Check if current directory is Git repo
        # Hint: Run 'git rev-parse --is-inside-work-tree'
        pass
    
    def get_status(self) -> str:
        # TODO: Get git status
        pass
    
    def get_branch(self) -> str:
        # TODO: Get current branch name
        pass
    
    def has_changes(self) -> bool:
        # TODO: Check if there are uncommitted changes
        pass
    
    def commit_changes(self, message: str) -> bool:
        # TODO: Stage all changes and commit
        # Validate message is not empty
        pass

# Test the helper
helper = GitCommitHelper()
if helper.is_git_repo():
    print(f"Branch: {helper.get_branch()}")
    if helper.has_changes():
        message = input("Commit message: ")
        helper.commit_changes(message)