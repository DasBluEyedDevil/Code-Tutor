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