// Docker Simulation

class DockerImage {
  constructor(name, baseImage) {
    this.name = name;
    this.baseImage = baseImage;
    this.layers = [];
  }

  addLayer(instruction, sizeKB) {
    this.layers.push({ instruction, sizeKB });
    console.log(`[Layer ${this.layers.length}] ${instruction} (+${sizeKB}KB)`);
    return this;
  }

  build() {
    const totalSize = this.layers.reduce((sum, l) => sum + l.sizeKB, 0);
    console.log(`\nBuilt image: ${this.name}`);
    console.log(`Base: ${this.baseImage}`);
    console.log(`Layers: ${this.layers.length}`);
    console.log(`Total size: ${totalSize}KB`);
    return this;
  }
}

class DockerContainer {
  constructor(image, name) {
    this.image = image;
    this.name = name;
    this.running = false;
    this.env = {};
  }

  setEnv(key, value) {
    this.env[key] = value;
    return this;
  }

  start() {
    this.running = true;
    console.log(`Container ${this.name} started from ${this.image.name}`);
    return this;
  }

  stop() {
    this.running = false;
    console.log(`Container ${this.name} stopped`);
    return this;
  }

  status() {
    return this.running ? 'running' : 'stopped';
  }
}

// Build a Bun app image
console.log('=== Building Docker Image ===\n');

const appImage = new DockerImage('my-api', 'oven/bun:1-slim');
appImage
  .addLayer('WORKDIR /app', 0)
  .addLayer('COPY package.json bun.lockb ./', 5)
  .addLayer('RUN bun install', 50000)
  .addLayer('COPY src/ ./src/', 100)
  .addLayer('EXPOSE 3000', 0)
  .build();

// Run container
console.log('\n=== Running Container ===\n');

const container = new DockerContainer(appImage, 'api-1');
container
  .setEnv('DATABASE_URL', 'postgres://...')
  .setEnv('NODE_ENV', 'production')
  .start();

console.log('Status:', container.status());
console.log('Environment:', container.env);