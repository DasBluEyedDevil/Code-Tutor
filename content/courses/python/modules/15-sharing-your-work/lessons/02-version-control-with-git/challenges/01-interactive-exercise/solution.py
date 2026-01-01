import subprocess
from typing import Optional, Tuple


class GitCommitHelper:
    """Helper class for common Git operations using Python subprocess.
    
    This class provides a simple interface for checking Git status,
    viewing changes, and creating commits programmatically.
    """
    
    def _run_command(self, command: list) -> Tuple[bool, str]:
        """Run a git command and return success status and output.
        
        Args:
            command: List of command parts (e.g., ['git', 'status'])
        
        Returns:
            Tuple of (success: bool, output: str)
        """
        try:
            result = subprocess.run(
                command,
                capture_output=True,
                text=True,
                check=True
            )
            return True, result.stdout.strip()
        except subprocess.CalledProcessError as e:
            return False, e.stderr.strip()
        except FileNotFoundError:
            return False, 'Git is not installed or not in PATH'
    
    def is_git_repo(self) -> bool:
        """Check if current directory is inside a Git repository.
        
        Returns:
            True if in a git repo, False otherwise
        """
        success, output = self._run_command(
            ['git', 'rev-parse', '--is-inside-work-tree']
        )
        return success and output == 'true'
    
    def get_status(self) -> str:
        """Get the current git status in short format.
        
        Returns:
            Status output showing modified, added, deleted files
        """
        success, output = self._run_command(['git', 'status', '--short'])
        if success:
            return output if output else 'Working tree clean'
        return f'Error: {output}'
    
    def get_branch(self) -> str:
        """Get the name of the current branch.
        
        Returns:
            Current branch name or 'unknown' on error
        """
        success, output = self._run_command(
            ['git', 'branch', '--show-current']
        )
        return output if success else 'unknown'
    
    def has_changes(self) -> bool:
        """Check if there are any uncommitted changes.
        
        This checks both staged and unstaged changes.
        
        Returns:
            True if there are changes to commit, False otherwise
        """
        # Check for any changes (staged or unstaged)
        success, output = self._run_command(['git', 'status', '--porcelain'])
        return success and len(output) > 0
    
    def get_modified_files(self) -> list:
        """Get a list of modified files.
        
        Returns:
            List of filenames that have been modified
        """
        success, output = self._run_command(['git', 'diff', '--name-only'])
        if success and output:
            return output.split('\n')
        return []
    
    def get_staged_files(self) -> list:
        """Get a list of staged files.
        
        Returns:
            List of filenames that are staged for commit
        """
        success, output = self._run_command(
            ['git', 'diff', '--cached', '--name-only']
        )
        if success and output:
            return output.split('\n')
        return []
    
    def stage_all(self) -> bool:
        """Stage all changes for commit.
        
        Returns:
            True if staging succeeded, False otherwise
        """
        success, _ = self._run_command(['git', 'add', '-A'])
        return success
    
    def commit_changes(self, message: str) -> bool:
        """Stage all changes and create a commit.
        
        Args:
            message: Commit message (must not be empty)
        
        Returns:
            True if commit succeeded, False otherwise
        """
        # Validate commit message
        if not message or not message.strip():
            print('Error: Commit message cannot be empty')
            return False
        
        # Stage all changes
        if not self.stage_all():
            print('Error: Failed to stage changes')
            return False
        
        # Create commit
        success, output = self._run_command(
            ['git', 'commit', '-m', message.strip()]
        )
        
        if success:
            print(f'Committed successfully: {message}')
            return True
        else:
            print(f'Error creating commit: {output}')
            return False
    
    def get_last_commit(self) -> str:
        """Get the last commit message.
        
        Returns:
            Short hash and message of the last commit
        """
        success, output = self._run_command(
            ['git', 'log', '-1', '--pretty=format:%h - %s']
        )
        return output if success else 'No commits yet'
    
    def show_summary(self) -> None:
        """Display a summary of the current Git state."""
        print('=== Git Repository Summary ===')
        print(f'\nBranch: {self.get_branch()}')
        print(f'Last commit: {self.get_last_commit()}')
        print(f'\nStatus:\n{self.get_status()}')
        
        modified = self.get_modified_files()
        staged = self.get_staged_files()
        
        if modified:
            print(f'\nModified files ({len(modified)}):') 
            for f in modified:
                print(f'  M {f}')
        
        if staged:
            print(f'\nStaged files ({len(staged)}):') 
            for f in staged:
                print(f'  S {f}')


# Test the helper
print('Git Commit Helper Demo')
print('=' * 40)

helper = GitCommitHelper()

if helper.is_git_repo():
    print('Inside a Git repository')
    helper.show_summary()
    
    if helper.has_changes():
        print('\nYou have uncommitted changes!')
        # In a real script, we'd prompt for input:
        # message = input('Commit message: ')
        # if message:
        #     helper.commit_changes(message)
    else:
        print('\nNo changes to commit.')
else:
    print('Not inside a Git repository.')
    print('Run "git init" to create one.')