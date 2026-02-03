// NOTE: This challenge uses Bun.build() (built-in bundler).
// The simulation below lets you practice the config API in any runtime.
// When running with Bun, remove the simulation and use Bun.build() directly.

// --- Simulation for non-Bun runtimes ---
const Bun = {
  async build(config) {
    console.log('[Simulation] Bun.build() called with config:');
    console.log(JSON.stringify(config, null, 2));
    return {
      success: true,
      outputs: [
        { path: `${config.outdir || './dist'}/index.js`, size: 24576 },
      ],
      logs: [],
    };
  }
};
// --- End simulation ---

async function build() {
  const result = await Bun.build({
    entrypoints: ['./src/index.ts'],
    outdir: './dist',
    minify: true,
    sourcemap: 'external',
    target: 'browser',
  });

  if (!result.success) {
    console.error('Build failed!');
    for (const log of result.logs) {
      console.error(log);
    }
    process.exit(1);
  }

  console.log('Build successful!');
  for (const output of result.outputs) {
    const size = (output.size / 1024).toFixed(2);
    console.log(`  ${output.path}: ${size} KB`);
  }
}

build();