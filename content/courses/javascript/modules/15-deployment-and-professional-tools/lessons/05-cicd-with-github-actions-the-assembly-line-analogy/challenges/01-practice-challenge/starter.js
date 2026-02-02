// CI/CD Pipeline Simulation

class CIPipeline {
  constructor(branch) {
    this.branch = branch;
    this.steps = [];
    this.failed = false;
  }
  
  runStep(name, action) {
    if (this.failed) {
      console.log(`  [SKIPPED] ${name}`);
      return false;
    }
    
    const startTime = Date.now();
    console.log(`  [RUNNING] ${name}...`);
    
    const success = action();
    const duration = Date.now() - startTime;
    
    if (success) {
      console.log(`  [PASSED] ${name} (${duration}ms)`);
    } else {
      console.log(`  [FAILED] ${name}`);
      this.failed = true;
    }
    
    return success;
  }
  
  run(testsShouldPass = true) {
    console.log(`\n=== CI Pipeline: ${this.branch} ===\n`);
    
    this.runStep('Checkout', () => true);
    this.runStep('bun install', () => true);
    this.runStep('bun test', () => testsShouldPass);
    this.runStep('bun run build', () => true);
    
    if (this.branch === 'main' && !this.failed) {
      this.runStep('Deploy to Production', () => true);
    } else if (this.branch !== 'main') {
      console.log('  [INFO] Not main branch - skipping deploy');
    }
    
    console.log(`\n${this.failed ? 'Pipeline FAILED' : 'Pipeline PASSED'}\n`);
  }
}

// Test
const pipeline = new CIPipeline('main');
pipeline.run(true);