# Packaging Code-Tutor as a Standalone Application

This guide explains how to package Code-Tutor into a standalone executable for easy distribution and use.

## Option 1: Electron Desktop App (Recommended)

Package the entire platform as a desktop application with a built-in Node.js runtime.

### Why Electron?
- ✅ Single executable file for Windows/Mac/Linux
- ✅ No need to install Node.js separately
- ✅ Professional desktop app experience
- ✅ Auto-updates support
- ✅ System tray integration possible

### Setup

1. **Install Electron Builder:**
   ```bash
   npm install --save-dev electron electron-builder
   ```

2. **Create Electron main process** (`electron/main.js`):
   ```javascript
   const { app, BrowserWindow } = require('electron');
   const path = require('path');
   const { spawn } = require('child_process');

   let apiProcess;
   let mainWindow;

   function startAPI() {
     // Start the Express API server
     apiProcess = spawn('node', [path.join(__dirname, '../apps/api/dist/index.js')], {
       stdio: 'pipe'
     });
     
     apiProcess.stdout.on('data', (data) => {
       console.log(`API: ${data}`);
     });
   }

   function createWindow() {
     mainWindow = new BrowserWindow({
       width: 1200,
       height: 800,
       webPreferences: {
         nodeIntegration: false,
         contextIsolation: true
       },
       icon: path.join(__dirname, '../assets/icon.png')
     });

     // Load the built frontend
     mainWindow.loadFile(path.join(__dirname, '../apps/web/dist/index.html'));

     mainWindow.on('closed', () => {
       mainWindow = null;
     });
   }

   app.on('ready', () => {
     startAPI();
     setTimeout(createWindow, 2000); // Wait for API to start
   });

   app.on('window-all-closed', () => {
     if (apiProcess) apiProcess.kill();
     app.quit();
   });
   ```

3. **Update package.json:**
   ```json
   {
     "main": "electron/main.js",
     "scripts": {
       "electron:dev": "electron .",
       "electron:build": "electron-builder",
       "package:win": "electron-builder --win --x64",
       "package:mac": "electron-builder --mac",
       "package:linux": "electron-builder --linux"
     },
     "build": {
       "appId": "com.codetutor.app",
       "productName": "Code Tutor",
       "directories": {
         "output": "release"
       },
       "files": [
         "electron/**/*",
         "apps/api/dist/**/*",
         "apps/web/dist/**/*",
         "apps/api/content/**/*"
       ],
       "win": {
         "target": "nsis",
         "icon": "assets/icon.ico"
       },
       "mac": {
         "target": "dmg",
         "icon": "assets/icon.icns"
       },
       "linux": {
         "target": "AppImage",
         "icon": "assets/icon.png"
       }
     }
   }
   ```

4. **Build the executable:**
   ```bash
   # Build frontend and backend
   cd apps/web && npm run build
   cd ../api && npm run build
   cd ../..

   # Package for Windows
   npm run package:win
   ```

**Output:** `release/Code-Tutor-Setup-1.0.0.exe` (~100-150 MB)

---

## Option 2: PKG - Single Executable Binary

Create a single executable file using `pkg`.

### Setup

1. **Install pkg:**
   ```bash
   npm install -g pkg
   ```

2. **Create a launcher script** (`launcher.js`):
   ```javascript
   const { spawn } = require('child_process');
   const path = require('path');
   const open = require('open');

   console.log('Starting Code Tutor...');

   // Start API server
   const api = spawn('node', [path.join(__dirname, 'apps/api/dist/index.js')]);

   api.stdout.on('data', (data) => {
     console.log(`API: ${data}`);
     if (data.toString().includes('running on')) {
       // Open browser when API is ready
       console.log('Opening browser...');
       open('http://localhost:3000');
     }
   });

   // Start web server (using serve package)
   const web = spawn('npx', ['serve', '-s', 'apps/web/dist', '-l', '3000']);

   web.stdout.on('data', (data) => {
     console.log(`Web: ${data}`);
   });

   process.on('SIGINT', () => {
     console.log('Shutting down...');
     api.kill();
     web.kill();
     process.exit();
   });
   ```

3. **Update package.json:**
   ```json
   {
     "bin": "launcher.js",
     "pkg": {
       "targets": ["node18-win-x64"],
       "outputPath": "dist",
       "assets": [
         "apps/api/content/**/*",
         "apps/web/dist/**/*"
       ]
     }
   }
   ```

4. **Build:**
   ```bash
   npm run build
   pkg . --output code-tutor.exe
   ```

**Output:** `code-tutor.exe` (~50 MB)

---

## Option 3: Portable ZIP Package

Create a portable package that requires Node.js but no installation.

### Setup

1. **Create package script** (`scripts/create-portable.ps1`):
   ```powershell
   # Build everything
   npm run build

   # Create portable directory
   $portable = "portable-release"
   New-Item -ItemType Directory -Force -Path $portable

   # Copy required files
   Copy-Item -Recurse apps/api/dist $portable/api
   Copy-Item -Recurse apps/api/content $portable/api/content
   Copy-Item -Recurse apps/web/dist $portable/web
   Copy-Item -Recurse node_modules $portable/node_modules

   # Create startup scripts
   @"
   @echo off
   start /min cmd /c "node api\index.js"
   timeout /t 2
   start http://localhost:3000
   npx serve -s web -l 3000
   "@ | Out-File -Encoding ASCII "$portable/start.bat"

   # Create readme
   @"
   Code Tutor - Portable Edition
   
   Requirements:
   - Node.js 18+ (https://nodejs.org)
   
   To run:
   1. Double-click start.bat
   2. Browser will open automatically
   
   To stop:
   - Close the command window
   "@ | Out-File "$portable/README.txt"

   # Zip it
   Compress-Archive -Path $portable -DestinationPath "CodeTutor-Portable.zip"
   ```

2. **Build:**
   ```bash
   .\scripts\create-portable.ps1
   ```

**Output:** `CodeTutor-Portable.zip` (~200 MB)

---

## Option 4: Docker Desktop Application

Package as a Docker container with a simple launcher.

### Setup

1. **Create Dockerfile** (at root):
   ```dockerfile
   FROM node:18-alpine

   WORKDIR /app

   # Copy built files
   COPY apps/api/dist ./api
   COPY apps/api/content ./api/content
   COPY apps/web/dist ./web
   COPY package*.json ./

   # Install production dependencies
   RUN npm install --production

   # Install serve for web
   RUN npm install -g serve

   # Expose ports
   EXPOSE 3000 3001

   # Start script
   COPY docker-start.sh .
   RUN chmod +x docker-start.sh

   CMD ["./docker-start.sh"]
   ```

2. **Create start script** (`docker-start.sh`):
   ```bash
   #!/bin/sh
   node api/index.js &
   serve -s web -l 3000
   ```

3. **Create Windows launcher** (`CodeTutor.bat`):
   ```bat
   @echo off
   docker run -p 3000:3000 -p 3001:3001 code-tutor
   start http://localhost:3000
   ```

4. **Build:**
   ```bash
   docker build -t code-tutor .
   docker save code-tutor -o CodeTutor-Docker.tar
   ```

**Output:** `CodeTutor-Docker.tar` + `CodeTutor.bat` launcher

---

## Comparison

| Method | File Size | Requires Node.js | Ease of Use | Best For |
|--------|-----------|------------------|-------------|----------|
| **Electron** | ~150 MB | ❌ No | ⭐⭐⭐⭐⭐ Easy | End users |
| **PKG** | ~50 MB | ❌ No | ⭐⭐⭐⭐ Easy | Power users |
| **Portable ZIP** | ~200 MB | ✅ Yes | ⭐⭐⭐ Medium | Developers |
| **Docker** | ~300 MB | ❌ No | ⭐⭐ Medium | Tech users |

---

## Recommended Approach: Electron

For the best user experience, I recommend **Option 1 (Electron)**:

### Quick Setup

1. **Install dependencies:**
   ```bash
   npm install --save-dev electron electron-builder
   ```

2. **Create the Electron structure:**
   ```bash
   mkdir electron
   # Create main.js as shown above
   ```

3. **Build everything:**
   ```bash
   cd apps/web && npm run build && cd ../..
   cd apps/api && npm run build && cd ../..
   npm run package:win
   ```

4. **Distribute:**
   - Share `release/Code-Tutor-Setup-1.0.0.exe`
   - Users just run the installer
   - No dependencies needed!

---

## Current Status (Your Issue)

Right now you're seeing:
1. ✅ Platform is installed correctly
2. ✅ Web app is running on http://localhost:3000
3. ❌ Docker error (but Docker is optional!)

### To fix your current setup:

**Option A: Run without Docker (simplest)**
```bash
# Terminal 1: Start API
cd apps/api
npm run dev

# Terminal 2: Web is already running!
# Just open http://localhost:3000
```

**Option B: Fix Docker**
1. Open Docker Desktop
2. Wait for it to fully start
3. Then run: `docker-compose up -d`

**Option C: Use the new startup script**
```bash
.\start.ps1
```

This will handle everything automatically!

---

## Next Steps

Would you like me to:
1. Set up Electron packaging for you?
2. Create a simple installer?
3. Just help you get the current setup running?

Let me know which direction you'd prefer!

