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

// build.ts
async function build() {
  const result = await Bun.build({
    // Your configuration here
  });

  // Report results
}

build();