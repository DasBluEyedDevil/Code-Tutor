// Complete Docker Simulation with Multi-Stage Builds

class DockerImage {
  constructor(name, baseImage) {
    this.name = name;
    this.baseImage = baseImage;
    this.layers = [];
    this.builtAt = null;
  }

  addLayer(instruction, sizeKB) {
    this.layers.push({
      instruction,
      sizeKB,
      cached: false
    });
    console.log(`[Layer ${this.layers.length}] ${instruction} (+${sizeKB}KB)`);
    return this;
  }

  copyFrom(sourceImage, sourcePath, destPath) {
    // Simulate COPY --from=builder
    const layer = {
      instruction: `COPY --from=${sourceImage.name} ${sourcePath} ${destPath}`,
      sizeKB: sourceImage.layers
        .filter(l => l.instruction.includes(sourcePath.replace('./', '')))
        .reduce((sum, l) => sum + l.sizeKB, 0) || 100,
      cached: false
    };
    this.layers.push(layer);
    console.log(`[Layer ${this.layers.length}] ${layer.instruction} (+${layer.sizeKB}KB)`);
    return this;
  }

  build() {
    const totalSize = this.layers.reduce((sum, l) => sum + l.sizeKB, 0);
    this.builtAt = new Date();
    console.log(`\n=== Image Built: ${this.name} ===`);
    console.log(`Base: ${this.baseImage}`);
    console.log(`Layers: ${this.layers.length}`);
    console.log(`Total size: ${(totalSize / 1024).toFixed(1)}MB`);
    return this;
  }

  getSize() {
    return this.layers.reduce((sum, l) => sum + l.sizeKB, 0);
  }
}

class DockerContainer {
  constructor(image, name) {
    this.image = image;
    this.name = name;
    this.running = false;
    this.env = {};
    this.ports = [];
    this.healthCheck = null;
    this.startedAt = null;
  }

  setEnv(key, value) {
    this.env[key] = value;
    return this;
  }

  mapPort(hostPort, containerPort) {
    this.ports.push({ host: hostPort, container: containerPort });
    return this;
  }

  setHealthCheck(endpoint) {
    this.healthCheck = endpoint;
    return this;
  }

  start() {
    if (!this.image.builtAt) {
      throw new Error('Image must be built before running container');
    }
    this.running = true;
    this.startedAt = new Date();
    console.log(`\nContainer ${this.name} started`);
    console.log(`  Image: ${this.image.name}`);
    console.log(`  Ports: ${this.ports.map(p => `${p.host}:${p.container}`).join(', ')}`);
    console.log(`  Health: ${this.healthCheck || 'none'}`);
    return this;
  }

  stop() {
    this.running = false;
    this.startedAt = null;
    console.log(`Container ${this.name} stopped`);
    return this;
  }

  status() {
    if (!this.running) return 'stopped';
    const uptime = Math.floor((Date.now() - this.startedAt) / 1000);
    return `running (up ${uptime}s)`;
  }

  logs() {
    if (this.running) {
      console.log(`[${this.name}] Server listening on port ${this.ports[0]?.container || 3000}`);
      console.log(`[${this.name}] Connected to database`);
      console.log(`[${this.name}] Health check: OK`);
    }
  }
}

// ============================================
// Multi-Stage Build Demonstration
// ============================================

console.log('=== Multi-Stage Build Demo ===\n');
console.log('Stage 1: Builder Image (with dev dependencies)\n');

const builderImage = new DockerImage('my-api:builder', 'oven/bun:1.1');
builderImage
  .addLayer('WORKDIR /app', 0)
  .addLayer('COPY package.json bun.lockb ./', 5)
  .addLayer('RUN bun install (all deps)', 80000) // Dev deps included
  .addLayer('COPY src/ ./src/', 100)
  .addLayer('RUN bun run build', 50)
  .build();

console.log('\n\nStage 2: Production Image (minimal)\n');

const productionImage = new DockerImage('my-api:latest', 'oven/bun:1.1-slim');
productionImage
  .addLayer('WORKDIR /app', 0)
  .addLayer('COPY package.json bun.lockb ./', 5)
  .addLayer('RUN bun install --production', 40000) // Only prod deps
  .copyFrom(builderImage, './dist', './dist')
  .addLayer('ENV NODE_ENV=production', 0)
  .addLayer('EXPOSE 3000', 0)
  .addLayer('HEALTHCHECK /health', 0)
  .build();

// Size comparison
console.log('\n=== Size Comparison ===');
console.log(`Builder image: ${(builderImage.getSize() / 1024).toFixed(1)}MB`);
console.log(`Production image: ${(productionImage.getSize() / 1024).toFixed(1)}MB`);
console.log(`Savings: ${((1 - productionImage.getSize() / builderImage.getSize()) * 100).toFixed(0)}%`);

// Run production container
console.log('\n=== Running Production Container ===');

const apiContainer = new DockerContainer(productionImage, 'api-prod-1');
apiContainer
  .setEnv('DATABASE_URL', 'postgres://prod-db/app')
  .setEnv('NODE_ENV', 'production')
  .setEnv('JWT_SECRET', '***hidden***')
  .mapPort(3000, 3000)
  .setHealthCheck('/health')
  .start();

console.log('\n=== Container Status ===');
console.log(`Status: ${apiContainer.status()}`);
console.log('Environment variables:', Object.keys(apiContainer.env).length);
apiContainer.logs();

// Docker Compose simulation
console.log('\n\n=== Docker Compose Simulation ===\n');

const services = {
  api: {
    image: productionImage,
    container: apiContainer,
    depends_on: ['db']
  },
  db: {
    image: new DockerImage('postgres:16', 'postgres:16'),
    container: null,
    volumes: ['postgres_data']
  }
};

console.log('Services defined:');
Object.entries(services).forEach(([name, service]) => {
  console.log(`  ${name}: ${service.image.name}`);
  if (service.depends_on) {
    console.log(`    depends_on: ${service.depends_on.join(', ')}`);
  }
  if (service.volumes) {
    console.log(`    volumes: ${service.volumes.join(', ')}`);
  }
});

console.log('\n--- Key Docker Commands ---');
const commands = [
  'docker build -t my-app .',
  'docker run -p 3000:3000 my-app',
  'docker compose up -d',
  'docker compose logs -f api',
  'docker compose down'
];
commands.forEach(cmd => console.log(`$ ${cmd}`));