// Complete CI/CD Pipeline Simulation

class CIPipeline {
  constructor(branch) {
    this.branch = branch;
    this.steps = [];
    this.failed = false;
    this.totalTime = 0;
  }
  
  runStep(name, action, simulatedTime = 100) {
    if (this.failed) {
      console.log(`  [SKIPPED] ${name}`);
      this.steps.push({ name, status: 'skipped', duration: 0 });
      return false;
    }
    
    const startTime = Date.now();
    console.log(`  [RUNNING] ${name}...`);
    
    const success = action();
    const duration = simulatedTime + Math.floor(Math.random() * 50);
    this.totalTime += duration;
    
    if (success) {
      console.log(`  [PASSED] ${name} (${duration}ms)`);
      this.steps.push({ name, status: 'passed', duration });
    } else {
      console.log(`  [FAILED] ${name}`);
      this.steps.push({ name, status: 'failed', duration });
      this.failed = true;
    }
    
    return success;
  }
  
  run(testsShouldPass = true) {
    console.log('\n' + '='.repeat(50));
    console.log(`CI Pipeline: ${this.branch}`);
    console.log('='.repeat(50) + '\n');
    
    console.log('Trigger: push to ' + this.branch);
    console.log('Runner: ubuntu-latest\n');
    
    // Job 1: Test & Build
    console.log('Job: test');
    console.log('-'.repeat(30));
    
    this.runStep('actions/checkout@v4', () => true, 50);
    this.runStep('oven-sh/setup-bun@v2', () => true, 200);
    this.runStep('bun install', () => true, 800);
    this.runStep('bun run lint', () => true, 300);
    this.runStep('bun test', () => testsShouldPass, 500);
    this.runStep('bun run build', () => !this.failed, 600);
    this.runStep('actions/upload-artifact@v4', () => !this.failed, 150);
    
    console.log('');
    
    // Job 2: Deploy (only on main and if tests pass)
    if (this.branch === 'main') {
      console.log('Job: deploy');
      console.log('-'.repeat(30));
      
      if (!this.failed) {
        this.runStep('Download artifact', () => true, 100);
        this.runStep('Deploy to Render', () => true, 2000);
        console.log('\n  Deployed to: https://my-app.onrender.com');
      } else {
        console.log('  [BLOCKED] Deploy skipped - tests failed');
      }
    } else {
      console.log(`Job: deploy`);
      console.log('-'.repeat(30));
      console.log(`  [SKIPPED] Only deploys on main branch`);
    }
    
    // Summary
    console.log('\n' + '='.repeat(50));
    console.log('PIPELINE SUMMARY');
    console.log('='.repeat(50));
    
    const passed = this.steps.filter(s => s.status === 'passed').length;
    const failed = this.steps.filter(s => s.status === 'failed').length;
    const skipped = this.steps.filter(s => s.status === 'skipped').length;
    
    console.log(`\nSteps: ${passed} passed, ${failed} failed, ${skipped} skipped`);
    console.log(`Total time: ${this.totalTime}ms`);
    console.log(`\nResult: ${this.failed ? 'FAILED' : 'PASSED'}`);
    
    if (this.failed) {
      const failedStep = this.steps.find(s => s.status === 'failed');
      console.log(`\nFailed at: ${failedStep.name}`);
      console.log('Fix the failing tests before merging!');
    } else if (this.branch === 'main') {
      console.log('\nDeployment successful!');
    }
    
    console.log('\n' + '='.repeat(50) + '\n');
  }
}

// Simulate different scenarios
console.log('=== Scenario 1: Feature Branch (tests pass) ===');
const featurePipeline = new CIPipeline('feature/add-login');
featurePipeline.run(true);

console.log('=== Scenario 2: Main Branch (tests pass) ===');
const mainPipeline = new CIPipeline('main');
mainPipeline.run(true);

console.log('=== Scenario 3: Main Branch (tests FAIL) ===');
const failedPipeline = new CIPipeline('main');
failedPipeline.run(false);

// CI/CD Best Practices
console.log('=== CI/CD Best Practices with Bun ===\n');

const bestPractices = [
  '1. Use oven-sh/setup-bun@v2 for fast Bun setup',
  '2. Cache ~/.bun/install/cache for faster installs',
  '3. Add "needs: test" to deploy job',
  '4. Only deploy on main: if: github.ref == refs/heads/main',
  '5. Store secrets in GitHub Secrets, not code',
  '6. Run tests on pull_request AND push',
  '7. Use bun test --coverage for code coverage',
  '8. Fail fast with continue-on-error: false'
];

bestPractices.forEach(p => console.log(p));

console.log('\nBun Advantages in CI:');
console.log('- bun install: 10x faster than npm');
console.log('- bun test: Native test runner, no setup');
console.log('- Less CI minutes = lower costs!');