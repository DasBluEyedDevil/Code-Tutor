/**
 * Expand Deployment Module (Module 15)
 * - Add Docker for JavaScript Apps lesson
 * - Enhance Environment Configuration lesson
 * - Enhance CI/CD Pipeline lesson
 * - Enhance Platform Deployment lesson
 */

const fs = require('fs');
const path = require('path');

const coursePath = path.join(__dirname, '..', 'content', 'courses', 'javascript', 'course.json');
const course = JSON.parse(fs.readFileSync(coursePath, 'utf8'));

const mod15 = course.modules.find(m => m.id === 'module-15');

// ============================================================
// NEW LESSON: Docker for JavaScript Apps
// ============================================================

const dockerLesson = {
  "id": "15.6",
  "title": "Docker for JavaScript Apps (The Shipping Container Analogy)",
  "moduleId": "module-15",
  "order": 6,
  "estimatedMinutes": 35,
  "difficulty": "advanced",
  "contentSections": [
    {
      "type": "ANALOGY",
      "title": "Understanding the Concept",
      "content": `Imagine shipping products internationally:

Without containers (traditional deployment):
- Pack items loosely in a truck
- Reach the port, unpack everything
- Repack into a ship format
- Arrive overseas, unpack again
- Repack for local delivery
- "It worked in my warehouse!" but breaks during shipping

With shipping containers (Docker):
- Pack once into a standard container
- Container fits on trucks, ships, trains
- Never opened during transport
- Arrives exactly as packed
- "Works in container" = works everywhere!

Docker for your code:
- Package your app + Bun/Node + dependencies
- Same container runs on your laptop, CI, and production
- "But it works on my machine" becomes impossible
- Reproducible builds every time

Key Docker concepts:
- Image: The blueprint (like a shipping container design)
- Container: Running instance (the actual container with your stuff)
- Dockerfile: Instructions to build the image (packing list)
- docker-compose: Orchestrate multiple containers (fleet management)

With Bun, Docker images are TINY and FAST to build!`
    },
    {
      "type": "EXAMPLE",
      "title": "Dockerfile for Bun Apps",
      "content": "Complete Dockerfile using multi-stage builds for production-ready images.",
      "language": "dockerfile",
      "code": `# Dockerfile for Bun/Hono API
# Multi-stage build for smallest possible image

# ============================================
# Stage 1: Install dependencies
# ============================================
FROM oven/bun:1 AS base
WORKDIR /app

# Copy package files first (better caching)
COPY package.json bun.lockb ./

# Install dependencies
RUN bun install --frozen-lockfile --production

# ============================================
# Stage 2: Build (if needed)
# ============================================
FROM base AS build
WORKDIR /app

# Copy source code
COPY . .

# If you have a build step (TypeScript, etc.)
# RUN bun run build

# ============================================
# Stage 3: Production image
# ============================================
FROM oven/bun:1-slim AS production
WORKDIR /app

# Copy only what we need from build stage
COPY --from=base /app/node_modules ./node_modules
COPY --from=build /app/src ./src
COPY --from=build /app/package.json ./

# Set production environment
ENV NODE_ENV=production
ENV PORT=3000

# Expose port
EXPOSE 3000

# Health check
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \\
  CMD curl -f http://localhost:3000/health || exit 1

# Run the app
CMD ["bun", "run", "src/index.ts"]

# ============================================
# Image size comparison:
# - Node.js full: ~1GB
# - Node.js slim: ~200MB
# - Bun slim: ~150MB
# - Bun distroless: ~100MB
# ============================================`
    },
    {
      "type": "THEORY",
      "title": "Breaking Down the Syntax",
      "content": `Docker fundamentals for JavaScript developers:

1. **Basic Dockerfile Commands**:
   \`\`\`dockerfile
   FROM oven/bun:1        # Base image
   WORKDIR /app           # Set working directory
   COPY . .               # Copy files
   RUN bun install        # Run command during build
   EXPOSE 3000            # Document which port
   CMD ["bun", "start"]   # Command to run container
   \`\`\`

2. **Multi-Stage Builds** (smaller images):
   \`\`\`dockerfile
   # Stage 1: Build
   FROM oven/bun:1 AS builder
   COPY . .
   RUN bun install && bun run build

   # Stage 2: Production (only copy what's needed)
   FROM oven/bun:1-slim
   COPY --from=builder /app/dist ./dist
   CMD ["bun", "dist/index.js"]
   \`\`\`

3. **.dockerignore** (like .gitignore for Docker):
   \`\`\`
   node_modules
   .git
   .env
   .env.local
   dist
   *.log
   .DS_Store
   Dockerfile
   docker-compose.yml
   README.md
   \`\`\`

4. **Building Images**:
   \`\`\`bash
   # Build image
   docker build -t my-app .

   # Build with tag (for versioning)
   docker build -t my-app:1.0.0 .
   docker build -t my-app:latest .

   # Build for specific platform
   docker build --platform linux/amd64 -t my-app .
   \`\`\`

5. **Running Containers**:
   \`\`\`bash
   # Run container
   docker run -p 3000:3000 my-app

   # Run with environment variables
   docker run -p 3000:3000 -e DATABASE_URL=... my-app

   # Run with env file
   docker run -p 3000:3000 --env-file .env my-app

   # Run in background (detached)
   docker run -d -p 3000:3000 my-app

   # View running containers
   docker ps

   # Stop container
   docker stop <container-id>
   \`\`\`

6. **docker-compose for Local Development**:
   \`\`\`yaml
   # docker-compose.yml
   version: '3.8'

   services:
     api:
       build: .
       ports:
         - "3000:3000"
       environment:
         - DATABASE_URL=postgres://postgres:password@db:5432/myapp
       depends_on:
         - db

     db:
       image: postgres:16
       environment:
         POSTGRES_USER: postgres
         POSTGRES_PASSWORD: password
         POSTGRES_DB: myapp
       ports:
         - "5432:5432"
       volumes:
         - postgres_data:/var/lib/postgresql/data

   volumes:
     postgres_data:
   \`\`\`

7. **Common Commands**:
   \`\`\`bash
   # Start services
   docker compose up

   # Start in background
   docker compose up -d

   # Rebuild and start
   docker compose up --build

   # Stop services
   docker compose down

   # Stop and remove volumes (reset database!)
   docker compose down -v

   # View logs
   docker compose logs -f api
   \`\`\`

8. **Pushing to Registry**:
   \`\`\`bash
   # Login to Docker Hub
   docker login

   # Tag for registry
   docker tag my-app username/my-app:latest

   # Push to registry
   docker push username/my-app:latest

   # Or use GitHub Container Registry
   docker tag my-app ghcr.io/username/my-app:latest
   docker push ghcr.io/username/my-app:latest
   \`\`\``
    },
    {
      "type": "WARNING",
      "title": "Common Pitfalls",
      "content": `Common Docker mistakes:

1. **Copying node_modules** (huge and wrong!):
   \`\`\`dockerfile
   # WRONG! Copies local node_modules
   COPY . .
   # This includes node_modules which:
   # - May have wrong binaries for Linux
   # - Is HUGE
   # - Defeats purpose of Docker

   # CORRECT! Use .dockerignore
   # .dockerignore:
   node_modules

   # Then install in Docker
   COPY package.json bun.lockb ./
   RUN bun install
   COPY . .
   \`\`\`

2. **Missing .dockerignore**:
   \`\`\`
   # Always create .dockerignore!
   node_modules
   .git
   .env
   .env.*
   dist
   *.log
   \`\`\`

3. **Running as root** (security risk):
   \`\`\`dockerfile
   # Add non-root user
   FROM oven/bun:1-slim

   # Create app user
   RUN addgroup --system app && adduser --system --group app
   USER app

   WORKDIR /app
   COPY --chown=app:app . .
   \`\`\`

4. **Not using multi-stage builds**:
   \`\`\`dockerfile
   # WRONG! Image is 1GB+
   FROM node:20
   COPY . .
   RUN npm install
   CMD ["node", "index.js"]

   # CORRECT! Image is ~150MB
   FROM oven/bun:1-slim
   COPY --from=build /app/dist ./dist
   CMD ["bun", "dist/index.js"]
   \`\`\`

5. **Hardcoded secrets**:
   \`\`\`dockerfile
   # NEVER DO THIS!
   ENV DATABASE_URL=postgres://user:password@db/app

   # CORRECT: Pass at runtime
   # docker run -e DATABASE_URL=... my-app
   \`\`\`

6. **Wrong platform** (M1/M2 Mac issue):
   \`\`\`bash
   # Building on M1 Mac for Linux servers
   # WRONG! Uses arm64
   docker build -t my-app .

   # CORRECT! Specify platform
   docker build --platform linux/amd64 -t my-app .
   \`\`\`

7. **No health check**:
   \`\`\`dockerfile
   # Always add health check!
   HEALTHCHECK --interval=30s --timeout=3s \\
     CMD curl -f http://localhost:3000/health || exit 1
   \`\`\`

8. **Forgetting to expose port**:
   \`\`\`dockerfile
   # Document which port (doesn't publish, just documents)
   EXPOSE 3000

   # Then when running:
   docker run -p 3000:3000 my-app
   \`\`\``
    },
    {
      "type": "LEGACY_COMPARISON",
      "title": "Node.js Dockerfile Equivalent",
      "legacy": "node",
      "content": "Here's how the same Dockerfile would look with Node.js instead of Bun. The Bun version is faster and produces smaller images.",
      "code": `# Dockerfile for Node.js (traditional approach)
# Compare with Bun version above

# ============================================
# Stage 1: Install dependencies
# ============================================
FROM node:20-slim AS base
WORKDIR /app

# Copy package files
COPY package.json package-lock.json ./

# Install dependencies (slower than bun install)
RUN npm ci --only=production

# ============================================
# Stage 2: Build
# ============================================
FROM base AS build
WORKDIR /app

COPY . .
RUN npm run build

# ============================================
# Stage 3: Production
# ============================================
FROM node:20-slim AS production
WORKDIR /app

# Copy from build stages
COPY --from=base /app/node_modules ./node_modules
COPY --from=build /app/dist ./dist
COPY package.json ./

ENV NODE_ENV=production
ENV PORT=3000

EXPOSE 3000

HEALTHCHECK --interval=30s --timeout=3s \\
  CMD curl -f http://localhost:3000/health || exit 1

CMD ["node", "dist/index.js"]

# ============================================
# Comparison:
# Node.js image: ~200MB
# Bun image: ~100MB
#
# npm ci: ~30 seconds
# bun install: ~5 seconds
# ============================================`,
      "language": "dockerfile"
    }
  ],
  "challenges": [
    {
      "type": "FREE_CODING",
      "id": "15.6-challenge",
      "title": "Practice Challenge",
      "description": "Create a simulation of Docker concepts:\n\n1. Create a DockerImage class that:\n   - Has layers array (each instruction adds a layer)\n   - Has addLayer(instruction, size) method\n   - Has build() method that shows total size\n\n2. Create a DockerContainer class that:\n   - Takes an image to run\n   - Has start(), stop(), and status() methods\n   - Tracks environment variables\n\n3. Demonstrate multi-stage build concept by creating:\n   - A 'build' image with dev dependencies\n   - A 'production' image with only runtime files",
      "instructions": "Create a simulation of Docker concepts:\n\n1. Create a DockerImage class that:\n   - Has layers array (each instruction adds a layer)\n   - Has addLayer(instruction, size) method\n   - Has build() method that shows total size\n\n2. Create a DockerContainer class that:\n   - Takes an image to run\n   - Has start(), stop(), and status() methods\n   - Tracks environment variables\n\n3. Demonstrate multi-stage build concept by creating:\n   - A 'build' image with dev dependencies\n   - A 'production' image with only runtime files",
      "starterCode": `// Docker Simulation

class DockerImage {
  constructor(name, baseImage) {
    this.name = name;
    this.baseImage = baseImage;
    this.layers = [];
  }

  addLayer(instruction, sizeKB) {
    this.layers.push({ instruction, sizeKB });
    console.log(\`[Layer \${this.layers.length}] \${instruction} (+\${sizeKB}KB)\`);
    return this;
  }

  build() {
    const totalSize = this.layers.reduce((sum, l) => sum + l.sizeKB, 0);
    console.log(\`\\nBuilt image: \${this.name}\`);
    console.log(\`Base: \${this.baseImage}\`);
    console.log(\`Layers: \${this.layers.length}\`);
    console.log(\`Total size: \${totalSize}KB\`);
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
    console.log(\`Container \${this.name} started from \${this.image.name}\`);
    return this;
  }

  stop() {
    this.running = false;
    console.log(\`Container \${this.name} stopped\`);
    return this;
  }

  status() {
    return this.running ? 'running' : 'stopped';
  }
}

// Build a Bun app image
console.log('=== Building Docker Image ===\\n');

const appImage = new DockerImage('my-api', 'oven/bun:1-slim');
appImage
  .addLayer('WORKDIR /app', 0)
  .addLayer('COPY package.json bun.lockb ./', 5)
  .addLayer('RUN bun install', 50000)
  .addLayer('COPY src/ ./src/', 100)
  .addLayer('EXPOSE 3000', 0)
  .build();

// Run container
console.log('\\n=== Running Container ===\\n');

const container = new DockerContainer(appImage, 'api-1');
container
  .setEnv('DATABASE_URL', 'postgres://...')
  .setEnv('NODE_ENV', 'production')
  .start();

console.log('Status:', container.status());
console.log('Environment:', container.env);`,
      "solution": `// Complete Docker Simulation with Multi-Stage Builds

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
    console.log(\`[Layer \${this.layers.length}] \${instruction} (+\${sizeKB}KB)\`);
    return this;
  }

  copyFrom(sourceImage, sourcePath, destPath) {
    // Simulate COPY --from=builder
    const layer = {
      instruction: \`COPY --from=\${sourceImage.name} \${sourcePath} \${destPath}\`,
      sizeKB: sourceImage.layers
        .filter(l => l.instruction.includes(sourcePath.replace('./', '')))
        .reduce((sum, l) => sum + l.sizeKB, 0) || 100,
      cached: false
    };
    this.layers.push(layer);
    console.log(\`[Layer \${this.layers.length}] \${layer.instruction} (+\${layer.sizeKB}KB)\`);
    return this;
  }

  build() {
    const totalSize = this.layers.reduce((sum, l) => sum + l.sizeKB, 0);
    this.builtAt = new Date();
    console.log(\`\\n=== Image Built: \${this.name} ===\`);
    console.log(\`Base: \${this.baseImage}\`);
    console.log(\`Layers: \${this.layers.length}\`);
    console.log(\`Total size: \${(totalSize / 1024).toFixed(1)}MB\`);
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
    console.log(\`\\nContainer \${this.name} started\`);
    console.log(\`  Image: \${this.image.name}\`);
    console.log(\`  Ports: \${this.ports.map(p => \`\${p.host}:\${p.container}\`).join(', ')}\`);
    console.log(\`  Health: \${this.healthCheck || 'none'}\`);
    return this;
  }

  stop() {
    this.running = false;
    this.startedAt = null;
    console.log(\`Container \${this.name} stopped\`);
    return this;
  }

  status() {
    if (!this.running) return 'stopped';
    const uptime = Math.floor((Date.now() - this.startedAt) / 1000);
    return \`running (up \${uptime}s)\`;
  }

  logs() {
    if (this.running) {
      console.log(\`[\${this.name}] Server listening on port \${this.ports[0]?.container || 3000}\`);
      console.log(\`[\${this.name}] Connected to database\`);
      console.log(\`[\${this.name}] Health check: OK\`);
    }
  }
}

// ============================================
// Multi-Stage Build Demonstration
// ============================================

console.log('=== Multi-Stage Build Demo ===\\n');
console.log('Stage 1: Builder Image (with dev dependencies)\\n');

const builderImage = new DockerImage('my-api:builder', 'oven/bun:1');
builderImage
  .addLayer('WORKDIR /app', 0)
  .addLayer('COPY package.json bun.lockb ./', 5)
  .addLayer('RUN bun install (all deps)', 80000) // Dev deps included
  .addLayer('COPY src/ ./src/', 100)
  .addLayer('RUN bun run build', 50)
  .build();

console.log('\\n\\nStage 2: Production Image (minimal)\\n');

const productionImage = new DockerImage('my-api:latest', 'oven/bun:1-slim');
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
console.log('\\n=== Size Comparison ===');
console.log(\`Builder image: \${(builderImage.getSize() / 1024).toFixed(1)}MB\`);
console.log(\`Production image: \${(productionImage.getSize() / 1024).toFixed(1)}MB\`);
console.log(\`Savings: \${((1 - productionImage.getSize() / builderImage.getSize()) * 100).toFixed(0)}%\`);

// Run production container
console.log('\\n=== Running Production Container ===');

const apiContainer = new DockerContainer(productionImage, 'api-prod-1');
apiContainer
  .setEnv('DATABASE_URL', 'postgres://prod-db/app')
  .setEnv('NODE_ENV', 'production')
  .setEnv('JWT_SECRET', '***hidden***')
  .mapPort(3000, 3000)
  .setHealthCheck('/health')
  .start();

console.log('\\n=== Container Status ===');
console.log(\`Status: \${apiContainer.status()}\`);
console.log('Environment variables:', Object.keys(apiContainer.env).length);
apiContainer.logs();

// Docker Compose simulation
console.log('\\n\\n=== Docker Compose Simulation ===\\n');

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
  console.log(\`  \${name}: \${service.image.name}\`);
  if (service.depends_on) {
    console.log(\`    depends_on: \${service.depends_on.join(', ')}\`);
  }
  if (service.volumes) {
    console.log(\`    volumes: \${service.volumes.join(', ')}\`);
  }
});

console.log('\\n--- Key Docker Commands ---');
const commands = [
  'docker build -t my-app .',
  'docker run -p 3000:3000 my-app',
  'docker compose up -d',
  'docker compose logs -f api',
  'docker compose down'
];
commands.forEach(cmd => console.log(\`$ \${cmd}\`));`,
      "language": "javascript",
      "testCases": [
        {
          "id": "test-1",
          "description": "Should build Docker image",
          "expectedOutput": "Built",
          "isVisible": true
        }
      ],
      "hints": [
        {
          "level": 1,
          "text": "Use array to track layers, reduce() to calculate total size."
        },
        {
          "level": 2,
          "text": "Multi-stage builds copy specific files from builder stage to production stage."
        }
      ],
      "commonMistakes": [
        {
          "mistake": "Not using multi-stage builds",
          "consequence": "Images are unnecessarily large with development dependencies",
          "correction": "Use separate stages for building and production runtime"
        }
      ],
      "difficulty": "intermediate"
    }
  ]
};

// ============================================================
// ENHANCE LESSON 14.4: Environment Variables
// Add: staging vs production, secrets management, configuration validation
// ============================================================

const envLesson = mod15.lessons.find(l => l.id === '14.4');

// Add new sections for staging/production differentiation and secrets management
const envEnhancementSections = [
  {
    "type": "THEORY",
    "title": "Development vs Staging vs Production",
    "content": `Managing environments in real-world applications:

1. **Environment Overview**:
   \`\`\`
   DEVELOPMENT (localhost)
   - Your laptop
   - Local database (Docker Postgres)
   - Relaxed security
   - Detailed error messages
   - Hot reloading enabled

   STAGING (preview environment)
   - Cloud-hosted (Render, Railway)
   - Copy of production architecture
   - Test before going live
   - May use production database copy
   - Accessible to team only

   PRODUCTION (live)
   - Real users
   - Real data
   - Maximum security
   - Minimal error details
   - Performance optimized
   \`\`\`

2. **Environment-Specific Variables**:
   \`\`\`bash
   # .env.development (checked into git, safe defaults)
   NODE_ENV=development
   DATABASE_URL=postgres://localhost:5432/myapp_dev
   API_URL=http://localhost:3000
   LOG_LEVEL=debug
   DEBUG=true

   # .env.staging (NOT in git - set in hosting platform)
   NODE_ENV=staging
   DATABASE_URL=postgres://staging-db.internal/myapp_staging
   API_URL=https://staging-api.myapp.com
   LOG_LEVEL=info
   DEBUG=false

   # Production (NEVER in git - always in hosting platform)
   NODE_ENV=production
   DATABASE_URL=postgres://prod-db.aws.com/myapp
   API_URL=https://api.myapp.com
   LOG_LEVEL=error
   DEBUG=false
   \`\`\`

3. **Loading Environment Files** (Bun does this automatically!):
   \`\`\`typescript
   // Bun loads .env automatically based on NODE_ENV
   // .env                 (always loaded)
   // .env.local           (always loaded, git-ignored)
   // .env.development     (when NODE_ENV=development)
   // .env.production      (when NODE_ENV=production)

   const env = process.env.NODE_ENV; // 'development' | 'staging' | 'production'
   const dbUrl = process.env.DATABASE_URL;
   \`\`\`

4. **Conditional Logic by Environment**:
   \`\`\`typescript
   const isDev = process.env.NODE_ENV === 'development';
   const isProd = process.env.NODE_ENV === 'production';

   // Show detailed errors only in development
   app.onError((err, c) => {
     console.error(err);
     if (isDev) {
       return c.json({ error: err.message, stack: err.stack }, 500);
     }
     return c.json({ error: 'Internal server error' }, 500);
   });

   // Enable debug logging in development
   if (isDev) {
     app.use('*', logger());
   }
   \`\`\``
  },
  {
    "type": "THEORY",
    "title": "Secrets Management Overview",
    "content": `Handling secrets securely in production:

1. **Types of Secrets**:
   \`\`\`
   - Database passwords
   - API keys (Stripe, SendGrid, etc.)
   - JWT signing secrets
   - OAuth client secrets
   - Encryption keys
   - Service account credentials
   \`\`\`

2. **Secrets Management Options**:
   \`\`\`
   Level 1: Platform Environment Variables (Good Start)
   - Render, Railway, Vercel dashboards
   - Encrypted at rest
   - Limited access control

   Level 2: Secret Management Services (Better)
   - Doppler (developer-friendly)
   - HashiCorp Vault (enterprise)
   - AWS Secrets Manager
   - Automatic rotation
   - Audit logs

   Level 3: Cloud Provider Secrets (Enterprise)
   - AWS Parameter Store / Secrets Manager
   - Google Secret Manager
   - Azure Key Vault
   \`\`\`

3. **Doppler Example** (recommended for teams):
   \`\`\`bash
   # Install Doppler CLI
   brew install dopplerhq/cli/doppler

   # Login and setup
   doppler login
   doppler setup

   # Run with injected secrets
   doppler run -- bun start

   # CI/CD integration
   doppler run -- bun test
   \`\`\`

4. **Never Do This**:
   \`\`\`typescript
   // NEVER hardcode secrets
   const JWT_SECRET = 'my-secret-key-123'; // WRONG!

   // NEVER commit .env files with real secrets
   // .gitignore MUST include:
   .env
   .env.local
   .env.production

   // NEVER log secrets
   console.log('Connecting with:', process.env.DATABASE_URL); // WRONG!
   console.log('Connecting to database...'); // CORRECT
   \`\`\`

5. **Secrets Rotation**:
   \`\`\`
   Good practices:
   - Rotate secrets quarterly (at minimum)
   - Rotate immediately if compromised
   - Use short-lived tokens where possible
   - Have a rotation procedure documented
   \`\`\``
  },
  {
    "type": "THEORY",
    "title": "Configuration Validation at Startup",
    "content": `Validate environment variables before your app starts:

1. **Why Validate at Startup?**:
   \`\`\`
   Without validation:
   - App starts
   - User makes request
   - Request tries to use DATABASE_URL
   - DATABASE_URL is undefined
   - Error! App crashes
   - Bad user experience

   With validation:
   - App checks all required vars
   - Missing DATABASE_URL detected
   - App refuses to start
   - Error caught in deployment
   - Fix before users affected
   \`\`\`

2. **Manual Validation**:
   \`\`\`typescript
   // config.ts - Validate on import
   const requiredEnvVars = [
     'DATABASE_URL',
     'JWT_SECRET',
     'NODE_ENV'
   ] as const;

   function validateEnv() {
     const missing: string[] = [];

     for (const envVar of requiredEnvVars) {
       if (!process.env[envVar]) {
         missing.push(envVar);
       }
     }

     if (missing.length > 0) {
       console.error('Missing required environment variables:');
       missing.forEach(v => console.error(\`  - \${v}\`));
       process.exit(1);
     }

     console.log('Environment validated successfully');
   }

   validateEnv();

   // Export validated config
   export const config = {
     databaseUrl: process.env.DATABASE_URL!,
     jwtSecret: process.env.JWT_SECRET!,
     nodeEnv: process.env.NODE_ENV as 'development' | 'staging' | 'production',
     port: parseInt(process.env.PORT || '3000', 10)
   };
   \`\`\`

3. **Using Zod for Validation** (type-safe):
   \`\`\`typescript
   import { z } from 'zod';

   const envSchema = z.object({
     NODE_ENV: z.enum(['development', 'staging', 'production']),
     DATABASE_URL: z.string().url(),
     JWT_SECRET: z.string().min(32, 'JWT secret must be at least 32 characters'),
     PORT: z.string().transform(Number).default('3000'),
     LOG_LEVEL: z.enum(['debug', 'info', 'warn', 'error']).default('info'),

     // Optional with defaults
     CORS_ORIGIN: z.string().default('*'),
     RATE_LIMIT_MAX: z.string().transform(Number).default('100'),
   });

   // Validate and export
   export const env = envSchema.parse(process.env);

   // Now TypeScript knows the exact types!
   // env.PORT is number
   // env.NODE_ENV is 'development' | 'staging' | 'production'
   \`\`\`

4. **Usage Pattern**:
   \`\`\`typescript
   // index.ts
   import { env } from './config';

   // Type-safe access
   const app = new Hono();

   app.get('/health', (c) => c.json({
     status: 'ok',
     environment: env.NODE_ENV,
     port: env.PORT
   }));

   export default {
     port: env.PORT,
     fetch: app.fetch
   };
   \`\`\`

5. **Example Startup Output**:
   \`\`\`
   $ bun start

   Validating environment...
   Environment: production
   Database: connected
   Server listening on port 3000

   # Or if validation fails:
   $ bun start

   Environment validation failed:
   - DATABASE_URL: Required
   - JWT_SECRET: String must be at least 32 characters

   Process exited with code 1
   \`\`\``
  }
];

// Insert enhancement sections after the existing EXAMPLE section
const exampleIndex = envLesson.contentSections.findIndex(s => s.type === 'EXAMPLE');
if (exampleIndex !== -1) {
  envLesson.contentSections.splice(exampleIndex + 1, 0, ...envEnhancementSections);
}

// ============================================================
// ENHANCE LESSON 14.5: CI/CD Pipeline
// Add: Docker image building and pushing
// ============================================================

const cicdLesson = mod15.lessons.find(l => l.id === '14.5');

const cicdEnhancementSections = [
  {
    "type": "EXAMPLE",
    "title": "CI/CD with Docker Image Build & Push",
    "content": "Complete workflow that builds Docker images and pushes to GitHub Container Registry.",
    "language": "yaml",
    "code": `# .github/workflows/docker-ci.yml
# Build, test, and push Docker images

name: Docker CI/CD

on:
  push:
    branches: [main]
  pull_request:
    branches: [main]

env:
  REGISTRY: ghcr.io
  IMAGE_NAME: \${{ github.repository }}

jobs:
  test:
    name: Test
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4

      - name: Setup Bun
        uses: oven-sh/setup-bun@v2

      - name: Install dependencies
        run: bun install

      - name: Run tests
        run: bun test

      - name: Type check
        run: bunx tsc --noEmit

  build-and-push:
    name: Build & Push Docker Image
    needs: test
    runs-on: ubuntu-latest
    permissions:
      contents: read
      packages: write

    steps:
      - uses: actions/checkout@v4

      # Login to GitHub Container Registry
      - name: Log in to Container Registry
        uses: docker/login-action@v3
        with:
          registry: \${{ env.REGISTRY }}
          username: \${{ github.actor }}
          password: \${{ secrets.GITHUB_TOKEN }}

      # Extract metadata for Docker
      - name: Extract metadata
        id: meta
        uses: docker/metadata-action@v5
        with:
          images: \${{ env.REGISTRY }}/\${{ env.IMAGE_NAME }}
          tags: |
            type=ref,event=branch
            type=sha,prefix=
            type=raw,value=latest,enable={{is_default_branch}}

      # Set up Docker Buildx (for multi-platform builds)
      - name: Set up Docker Buildx
        uses: docker/setup-buildx-action@v3

      # Build and push Docker image
      - name: Build and push
        uses: docker/build-push-action@v5
        with:
          context: .
          push: \${{ github.event_name != 'pull_request' }}
          tags: \${{ steps.meta.outputs.tags }}
          labels: \${{ steps.meta.outputs.labels }}
          cache-from: type=gha
          cache-to: type=gha,mode=max
          platforms: linux/amd64,linux/arm64

  deploy:
    name: Deploy to Production
    needs: build-and-push
    runs-on: ubuntu-latest
    if: github.ref == 'refs/heads/main'

    steps:
      # Deploy to Fly.io
      - uses: actions/checkout@v4

      - uses: superfly/flyctl-actions/setup-flyctl@master

      - name: Deploy to Fly.io
        run: flyctl deploy --remote-only
        env:
          FLY_API_TOKEN: \${{ secrets.FLY_API_TOKEN }}

      # Or deploy to Railway
      # - name: Deploy to Railway
      #   run: |
      #     curl -X POST \\
      #       -H "Authorization: Bearer \${{ secrets.RAILWAY_TOKEN }}" \\
      #       "https://backboard.railway.app/graphql/v2" \\
      #       -d '{"query":"mutation { deploymentRedeploy(id: \\"$DEPLOYMENT_ID\\") { id } }"}'`
  },
  {
    "type": "THEORY",
    "title": "Docker in CI/CD Explained",
    "content": `Building and deploying Docker images in GitHub Actions:

1. **Container Registries**:
   \`\`\`
   GitHub Container Registry (ghcr.io)
   - Free for public repos
   - Integrated with GitHub
   - ghcr.io/username/repo:latest

   Docker Hub
   - Free tier available
   - Most widely used
   - username/repo:latest

   AWS ECR, Google GCR, Azure ACR
   - Cloud provider registries
   - Private by default
   \`\`\`

2. **Image Tagging Strategy**:
   \`\`\`yaml
   tags: |
     # Branch name (e.g., main, develop)
     type=ref,event=branch

     # Short commit SHA (e.g., abc1234)
     type=sha,prefix=

     # Semantic version from git tag
     type=semver,pattern={{version}}

     # 'latest' only on main branch
     type=raw,value=latest,enable={{is_default_branch}}

   # Result: ghcr.io/user/app:main
   #         ghcr.io/user/app:abc1234
   #         ghcr.io/user/app:latest
   \`\`\`

3. **Caching for Faster Builds**:
   \`\`\`yaml
   - uses: docker/build-push-action@v5
     with:
       # Use GitHub Actions cache
       cache-from: type=gha
       cache-to: type=gha,mode=max

       # Layers are cached between runs
       # Rebuilds only changed layers
   \`\`\`

4. **Multi-Platform Builds**:
   \`\`\`yaml
   - uses: docker/build-push-action@v5
     with:
       platforms: linux/amd64,linux/arm64
       # Builds for both Intel and ARM (M1/M2 Macs)
   \`\`\`

5. **Deploy After Push**:
   \`\`\`yaml
   deploy:
     needs: build-and-push  # Wait for image
     if: github.ref == 'refs/heads/main'  # Only main

     steps:
       # Platform-specific deploy commands
       - name: Deploy to Fly.io
         run: flyctl deploy --image ghcr.io/user/app:latest

       # Or pull new image on server
       - name: Deploy via SSH
         run: |
           ssh server 'docker pull ghcr.io/user/app:latest && docker-compose up -d'
   \`\`\``
  }
];

// Insert after the first EXAMPLE section in CI/CD lesson
const cicdExampleIndex = cicdLesson.contentSections.findIndex(s => s.type === 'EXAMPLE');
if (cicdExampleIndex !== -1) {
  cicdLesson.contentSections.splice(cicdExampleIndex + 1, 0, ...cicdEnhancementSections);
}

// ============================================================
// ENHANCE LESSON 14.3: Platform Deployment
// Add: Railway and Fly.io sections
// ============================================================

const platformLesson = mod15.lessons.find(l => l.id === '14.3');

// Update the title to be more comprehensive
platformLesson.title = "Platform Deployment - Vercel, Railway & Fly.io (The Storefront Analogy)";

const platformEnhancementSections = [
  {
    "type": "THEORY",
    "title": "Platform Comparison",
    "content": `Choosing the right deployment platform:

| Platform | Best For | Pricing | Key Features |
|----------|----------|---------|--------------|
| **Vercel** | React/Next.js frontends | Free tier | Edge functions, instant deploys |
| **Railway** | Full-stack apps | Free $5/month | Database included, simple UI |
| **Render** | APIs & backends | Free tier | Auto-scaling, managed Postgres |
| **Fly.io** | Docker containers | Free tier | Global edge, containers |

1. **Vercel** (Frontend Focus):
   \`\`\`
   Best for:
   - React, Next.js, Vite apps
   - Static sites
   - Serverless functions

   Features:
   - Automatic preview deployments
   - Edge network (fast globally)
   - Built-in analytics
   - GitHub integration
   \`\`\`

2. **Railway** (Full-Stack Friendly):
   \`\`\`
   Best for:
   - Full-stack apps
   - Apps needing databases
   - Simple deployment needs

   Features:
   - One-click Postgres, Redis, MySQL
   - GitHub auto-deploy
   - Simple environment variables UI
   - Team collaboration
   \`\`\`

3. **Fly.io** (Docker & Edge):
   \`\`\`
   Best for:
   - Docker containers
   - Apps needing global presence
   - WebSocket applications
   - Custom runtime needs

   Features:
   - Deploy Docker images
   - Machines (VMs) anywhere
   - Built-in Postgres
   - Horizontal scaling
   \`\`\``
  },
  {
    "type": "EXAMPLE",
    "title": "Deploying to Railway",
    "content": "Railway deployment for Bun/Hono APIs with Postgres.",
    "language": "bash",
    "code": `# Railway Deployment Guide for Bun/Hono

# ============================================
# Step 1: Prepare Your Project
# ============================================

# package.json
{
  "name": "my-api",
  "scripts": {
    "start": "bun run src/index.ts",
    "dev": "bun --watch src/index.ts",
    "build": "bun build src/index.ts --outdir=dist"
  }
}

# Procfile (optional but recommended)
web: bun run src/index.ts

# ============================================
# Step 2: Railway CLI Setup
# ============================================

# Install Railway CLI
npm install -g @railway/cli

# Login
railway login

# Initialize project
railway init

# Link to existing project
railway link

# ============================================
# Step 3: Add Database (One Command!)
# ============================================

# Add Postgres
railway add --plugin postgres

# This creates:
# - Managed Postgres instance
# - DATABASE_URL automatically set
# - Connected to your service

# ============================================
# Step 4: Configure Environment
# ============================================

# Set environment variables
railway variables set NODE_ENV=production
railway variables set JWT_SECRET=your-secret-here
railway variables set CORS_ORIGIN=https://your-frontend.vercel.app

# View all variables
railway variables

# ============================================
# Step 5: Deploy
# ============================================

# Deploy current directory
railway up

# Or connect to GitHub for auto-deploys
# Railway Dashboard -> Settings -> Connect GitHub

# ============================================
# Step 6: View Logs & Status
# ============================================

# View logs
railway logs

# Open dashboard
railway open

# Get deployment URL
railway domain

# ============================================
# railway.json (Optional Configuration)
# ============================================
{
  "$schema": "https://railway.app/railway.schema.json",
  "build": {
    "builder": "NIXPACKS"
  },
  "deploy": {
    "startCommand": "bun run src/index.ts",
    "healthcheckPath": "/health",
    "restartPolicyType": "ON_FAILURE"
  }
}`
  },
  {
    "type": "EXAMPLE",
    "title": "Deploying to Fly.io",
    "content": "Fly.io deployment for Docker containers with global distribution.",
    "language": "bash",
    "code": `# Fly.io Deployment Guide for Docker Apps

# ============================================
# Step 1: Install Fly CLI
# ============================================

# macOS
brew install flyctl

# Windows
powershell -Command "iwr https://fly.io/install.ps1 -useb | iex"

# Linux
curl -L https://fly.io/install.sh | sh

# Login
fly auth login

# ============================================
# Step 2: Launch App
# ============================================

# In your project directory
fly launch

# This creates fly.toml and Dockerfile if needed

# ============================================
# fly.toml Configuration
# ============================================

# fly.toml
app = "my-api"
primary_region = "iad"  # US East

[build]
  dockerfile = "Dockerfile"

[env]
  NODE_ENV = "production"
  PORT = "3000"

[http_service]
  internal_port = 3000
  force_https = true
  auto_stop_machines = true
  auto_start_machines = true
  min_machines_running = 1

  [http_service.concurrency]
    type = "connections"
    hard_limit = 25
    soft_limit = 20

[[http_service.checks]]
  grace_period = "10s"
  interval = "30s"
  method = "GET"
  path = "/health"
  timeout = "5s"

# ============================================
# Step 3: Set Secrets
# ============================================

# Set secrets (encrypted, not in fly.toml!)
fly secrets set DATABASE_URL="postgres://..."
fly secrets set JWT_SECRET="your-secret-here"

# List secrets
fly secrets list

# ============================================
# Step 4: Deploy
# ============================================

# Deploy (builds and pushes Docker image)
fly deploy

# Deploy from GitHub Actions
fly deploy --remote-only

# ============================================
# Step 5: Scale & Monitor
# ============================================

# Scale to multiple regions
fly scale count 2 --region iad,lhr

# View running machines
fly status

# View logs
fly logs

# SSH into machine
fly ssh console

# Open in browser
fly open

# ============================================
# Step 6: Add Postgres (Optional)
# ============================================

# Create Fly Postgres cluster
fly postgres create --name my-api-db

# Attach to app (sets DATABASE_URL automatically)
fly postgres attach my-api-db

# Connect to database
fly postgres connect -a my-api-db`
  },
  {
    "type": "THEORY",
    "title": "Environment Variables by Platform",
    "content": `Setting environment variables on each platform:

1. **Vercel** (Dashboard):
   \`\`\`
   Settings -> Environment Variables

   Name: VITE_API_URL
   Value: https://api.example.com
   Environments: Production, Preview, Development

   Important: Prefix with VITE_ for client-side access!
   \`\`\`

2. **Railway** (CLI or Dashboard):
   \`\`\`bash
   # CLI
   railway variables set KEY=value
   railway variables set DATABASE_URL=postgres://...

   # Dashboard
   Service -> Variables -> Add Variable

   # Reference other variables
   DATABASE_URL=\${{Postgres.DATABASE_URL}}
   \`\`\`

3. **Render** (Dashboard):
   \`\`\`
   Service -> Environment -> Add Environment Variable

   Key: DATABASE_URL
   Value: postgres://...

   # Render auto-injects for managed DBs
   DATABASE_URL is automatic when you add Postgres
   \`\`\`

4. **Fly.io** (Secrets):
   \`\`\`bash
   # Set secrets (encrypted)
   fly secrets set DATABASE_URL=postgres://...
   fly secrets set JWT_SECRET=your-secret

   # fly.toml for non-sensitive env vars
   [env]
     NODE_ENV = "production"
     LOG_LEVEL = "info"

   # Never put secrets in fly.toml!
   \`\`\`

5. **Best Practices**:
   \`\`\`
   DO:
   - Use platform's secret storage
   - Set different values per environment
   - Document required variables
   - Validate at startup

   DON'T:
   - Commit secrets to git
   - Use same secrets in dev/prod
   - Hardcode in Dockerfile
   - Log secret values
   \`\`\``
  }
];

// Find the THEORY section and insert after it
const platformTheoryIndex = platformLesson.contentSections.findIndex(s => s.type === 'THEORY');
if (platformTheoryIndex !== -1) {
  platformLesson.contentSections.splice(platformTheoryIndex + 1, 0, ...platformEnhancementSections);
}

// ============================================================
// Add the Docker lesson to Module 15
// ============================================================
mod15.lessons.push(dockerLesson);

// Update module estimated hours
mod15.estimatedHours = 5;

// Save the updated course
fs.writeFileSync(coursePath, JSON.stringify(course, null, 2));

console.log('Deployment module expanded successfully!');
console.log('Added:');
console.log('  - New Docker lesson (15.6)');
console.log('  - Enhanced Environment Variables lesson (14.4) with staging/production, secrets, validation');
console.log('  - Enhanced CI/CD lesson (14.5) with Docker image building');
console.log('  - Enhanced Platform Deployment lesson (14.3) with Railway and Fly.io');
