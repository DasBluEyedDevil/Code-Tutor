// Git repository simulation
let repo = {
  commits: [],
  branches: ['main'],
  currentBranch: 'main'
};

function makeCommit(message) {
  let commit = {
    id: Math.random().toString(36).substr(2, 7),
    message: message,
    timestamp: new Date().toISOString()
  };
  repo.commits.push(commit);
  console.log(`Committed: ${message} (${commit.id})`);
  return commit;
}

function createBranch(name) {
  repo.branches.push(name);
  console.log(`Created branch: ${name}`);
}

// Test
makeCommit('Initial commit');
makeCommit('Add homepage');
makeCommit('Fix CSS');
createBranch('feature/login');

console.log('\nRepository state:');
console.log('Commits:', repo.commits.length);
console.log('Branches:', repo.branches);