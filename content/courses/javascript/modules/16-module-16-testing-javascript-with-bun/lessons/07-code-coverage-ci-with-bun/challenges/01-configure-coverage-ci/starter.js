// bunfig.toml configuration (as JavaScript object for this exercise)
// Complete this configuration
const bunfig = {
  test: {
    preload: ['./tests/setup.ts'],
    // YOUR CODE: Enable coverage
    coverage: false,
    // YOUR CODE: Set coverage reporters
    coverageReporter: [],
    // YOUR CODE: Set coverage thresholds
    coverageThreshold: {}
  }
};

// Verify configuration
const test = bunfig.test;

if (test.coverage === true) {
  console.log('\u2713 Coverage enabled');
} else {
  console.log('\u2717 Enable coverage');
}

if (test.coverageReporter.includes('text')) {
  console.log('\u2713 Text reporter configured');
} else {
  console.log('\u2717 Add text reporter');
}

if (test.coverageThreshold.line === 80) {
  console.log('\u2713 Line coverage threshold set to 80%');
} else {
  console.log('\u2717 Set line threshold to 80');
}

// GitHub Actions workflow (as object for validation)
const workflow = {
  name: 'Tests',
  on: {
    push: { branches: ['main'] },
    pull_request: { branches: ['main'] }
  },
  jobs: {
    test: {
      'runs-on': 'ubuntu-latest',
      steps: [
        { uses: 'actions/checkout@v4' },
        { name: 'Setup Bun', uses: 'oven-sh/setup-bun@v2' },
        { name: 'Install', run: 'bun install' },
        { name: 'Test', run: 'bun test --coverage' }
      ]
    }
  }
};

console.log('\u2713 GitHub Actions workflow configured for Bun');