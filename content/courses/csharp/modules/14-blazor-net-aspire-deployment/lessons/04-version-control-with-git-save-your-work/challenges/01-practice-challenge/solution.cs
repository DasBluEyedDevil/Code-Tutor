Console.WriteLine("═══════════════════════════════════════════");
Console.WriteLine("  GIT VERSION CONTROL WORKFLOW");
Console.WriteLine("═══════════════════════════════════════════\n");

Console.WriteLine("STEP 1: Initialize Repository");
Console.WriteLine("  Command: git init");
Console.WriteLine("  Creates .git folder");
Console.WriteLine("  Repository ready to track changes\n");

Console.WriteLine("STEP 2: Stage Files");
Console.WriteLine("  Command: git add .");
Console.WriteLine("  Stages all files for commit");
Console.WriteLine("  Files in 'staging area' ready to commit\n");

Console.WriteLine("STEP 3: First Commit");
Console.WriteLine("  Command: git commit -m 'Initial commit'");
Console.WriteLine("  Saves snapshot of code");
Console.WriteLine("  Commit ID: abc123... (unique hash)\n");

Console.WriteLine("STEP 4: Check Status");
Console.WriteLine("  Command: git status");
Console.WriteLine("  Shows: current branch, staged/unstaged files");
Console.WriteLine("  Output: 'On branch main, nothing to commit'\n");

Console.WriteLine("STEP 5: Create Feature Branch");
Console.WriteLine("  Command: git branch feature/add-books");
Console.WriteLine("  Creates new branch for book feature");
Console.WriteLine("  Doesn't switch yet - still on main\n");

Console.WriteLine("STEP 6: Switch to Feature Branch");
Console.WriteLine("  Command: git checkout feature/add-books");
Console.WriteLine("  Now working on feature branch");
Console.WriteLine("  Changes won't affect main branch\n");

Console.WriteLine("STEP 7: Make Changes");
Console.WriteLine("  Edit files: BookController.cs, BookService.cs");
Console.WriteLine("  Add new files: Book.cs, IBookRepository.cs\n");

Console.WriteLine("STEP 8: Stage Changes");
Console.WriteLine("  Command: git add .");
Console.WriteLine("  Stages all modified and new files\n");

Console.WriteLine("STEP 9: Commit Feature");
Console.WriteLine("  Command: git commit -m 'Add book CRUD operations'");
Console.WriteLine("  Saves book feature snapshot");
Console.WriteLine("  Feature complete on branch!\n");

Console.WriteLine("STEP 10: Return to Main");
Console.WriteLine("  Command: git checkout main");
Console.WriteLine("  Switches back to main branch");
Console.WriteLine("  Book files disappear (they're on feature branch)\n");

Console.WriteLine("STEP 11: Merge Feature");
Console.WriteLine("  Command: git merge feature/add-books");
Console.WriteLine("  Brings book feature into main");
Console.WriteLine("  Book files now in main branch!\n");

Console.WriteLine("STEP 12: Push to GitHub");
Console.WriteLine("  Command: git push origin main");
Console.WriteLine("  Uploads main branch to GitHub");
Console.WriteLine("  Team can see changes, code is backed up!\n");

Console.WriteLine("═══════════════════════════════════════════");
Console.WriteLine("✓ Professional Git workflow complete!");
Console.WriteLine("✓ Feature developed in isolation");
Console.WriteLine("✓ Merged to main when ready");
Console.WriteLine("✓ Pushed to remote for backup/collaboration");