// Complete Git simulation
let repo = {
  commits: [],
  branches: ['main'],
  currentBranch: 'main',
  files: {}
};

function makeCommit(message, files = []) {
  let commit = {
    id: Math.random().toString(36).substr(2, 7),
    message: message,
    branch: repo.currentBranch,
    files: files,
    timestamp: new Date().toISOString(),
    author: 'Developer'
  };
  
  repo.commits.push(commit);
  console.log(`✓ [${commit.id}] ${message}`);
  return commit;
}

function createBranch(name) {
  if (repo.branches.includes(name)) {
    console.log(`✗ Branch ${name} already exists`);
    return false;
  }
  repo.branches.push(name);
  console.log(`✓ Created branch: ${name}`);
  return true;
}

function switchBranch(name) {
  if (!repo.branches.includes(name)) {
    console.log(`✗ Branch ${name} does not exist`);
    return false;
  }
  repo.currentBranch = name;
  console.log(`✓ Switched to branch: ${name}`);
  return true;
}

function showLog() {
  console.log('\n=== Commit History ===');
  repo.commits.forEach((c, i) => {
    console.log(`${i + 1}. [${c.id}] (${c.branch}) ${c.message}`);
    if (c.files.length > 0) {
      console.log(`   Files: ${c.files.join(', ')}`);
    }
  });
}

function showStatus() {
  console.log('\n=== Repository Status ===');
  console.log(`Current branch: ${repo.currentBranch}`);
  console.log(`Total branches: ${repo.branches.join(', ')}`);
  console.log(`Total commits: ${repo.commits.length}`);
}

// Simulate development workflow
console.log('=== Git Workflow Simulation ===\n');

makeCommit('Initial commit', ['README.md', 'package.json']);
makeCommit('Add Express server', ['server.js']);
makeCommit('Add database connection', ['db.js', 'prisma/schema.prisma']);

createBranch('feature/authentication');
switchBranch('feature/authentication');

makeCommit('Add login route', ['routes/auth.js']);
makeCommit('Add JWT middleware', ['middleware/auth.js']);

switchBranch('main');
makeCommit('Update README', ['README.md']);

showLog();
showStatus();

console.log('\n--- Git Best Practices ---');
let practices = [
  '✓ Commit often (small, focused commits)',
  '✓ Write clear commit messages',
  '✓ Use branches for features',
  '✓ Never commit secrets (.env files)',
  '✓ Pull before you push',
  '✓ Review changes before committing (git diff)',
  '✓ Use .gitignore for node_modules, etc.'
];
practices.forEach(p => console.log(p));