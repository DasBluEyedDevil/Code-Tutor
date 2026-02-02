---
type: "EXAMPLE"
title: "Code Example"
---

See the code example above demonstrating Code Example.

```javascript
// Git Concepts (Simulated)
// Git is a command-line tool, not JavaScript, but let's understand the concepts!

console.log('=== Git Version Control ===\n');

// CONCEPT 1: REPOSITORY - Project history
let repository = {
  name: 'my-awesome-app',
  commits: [],
  currentBranch: 'main',
  branches: ['main']
};

console.log('Repository:', repository.name);

// CONCEPT 2: COMMIT - Snapshot of your code
function commit(message, files) {
  let snapshot = {
    id: Math.random().toString(36).substr(2, 7),
    message: message,
    files: files,
    timestamp: new Date().toISOString(),
    author: 'You'
  };
  
  repository.commits.push(snapshot);
  console.log(`✓ Committed: "${message}" (${snapshot.id})`);
  return snapshot;
}

// CONCEPT 3: BRANCH - Parallel version of code
function createBranch(name) {
  repository.branches.push(name);
  console.log(`✓ Created branch: ${name}`);
}

function switchBranch(name) {
  if (repository.branches.includes(name)) {
    repository.currentBranch = name;
    console.log(`✓ Switched to branch: ${name}`);
  }
}

// SIMULATE GIT WORKFLOW
console.log('\n--- Simulating Git Workflow ---\n');

// 1. Initial commit
commit('Initial commit', ['index.html', 'app.js', 'styles.css']);

// 2. Add feature
commit('Add user authentication', ['auth.js', 'login.html']);

// 3. Fix bug
commit('Fix login button styling', ['styles.css']);

// 4. Create feature branch
create Branch('feature/dark-mode');
switchBranch('feature/dark-mode');

// 5. Work on feature
commit('Add dark mode toggle', ['darkMode.js', 'styles.css']);

// 6. Switch back to main
switchBranch('main');

console.log('\n--- Repository State ---');
console.log('Total commits:', repository.commits.length);
console.log('Branches:', repository.branches.join(', '));
console.log('Current branch:', repository.currentBranch);

console.log('\n--- Commit History ---');
repository.commits.forEach((c, i) => {
  console.log(`${i + 1}. [${c.id}] ${c.message}`);
});

// GIT COMMANDS REFERENCE
console.log('\n=== Essential Git Commands ===\n');

let gitCommands = {
  'git init': 'Create new Git repository',
  'git clone <url>': 'Download existing repository',
  'git status': 'See which files changed',
  'git add <file>': 'Stage file for commit',
  'git add .': 'Stage all changed files',
  'git commit -m "message"': 'Save snapshot with message',
  'git log': 'View commit history',
  'git branch <name>': 'Create new branch',
  'git checkout <branch>': 'Switch to branch',
  'git merge <branch>': 'Merge branch into current',
  'git pull': 'Download latest changes from remote',
  'git push': 'Upload your commits to remote',
  'git diff': 'See what changed in files'
};

for (let [command, description] of Object.entries(gitCommands)) {
  console.log(`${command.padEnd(30)} - ${description}`);
}

// TYPICAL WORKFLOW
console.log('\n=== Typical Git Workflow ===\n');

let workflow = [
  '1. Make changes to your code',
  '2. git status               (see what changed)',
  '3. git add .                (stage all changes)',
  '4. git commit -m "Add feature X"  (save snapshot)',
  '5. git push                 (upload to GitHub)',
  '',
  'Repeat for every feature/fix!'
];

workflow.forEach(step => console.log(step));

console.log('\n--- Why Use Git? ---');
let benefits = [
  '✓ Never lose code (complete history)',
  '✓ Experiment safely (branches)',
  '✓ Collaborate easily (merge changes)',
  '✓ See who changed what and when',
  '✓ Revert mistakes instantly',
  '✓ Required by all professional teams',
  '✓ Works with GitHub (code hosting)'
];

benefits.forEach(b => console.log(b));
```
