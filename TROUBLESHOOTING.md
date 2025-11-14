# Quick Troubleshooting Guide

## Current Issue: Docker Required

Code-Tutor **requires** Docker to function properly. The code execution feature is a core part of the learning platform.

---

## ✅ What's Working

Based on typical setup:
- ✅ Node.js is installed
- ✅ Dependencies are installed  
- ✅ Web app can start on http://localhost:3000
- ✅ All 7 courses are imported

---

## ⚠️ What Needs Docker

- ⚠️ Code execution (required for interactive learning)
- ⚠️ Running exercises
- ⚠️ Testing student code
- ⚠️ All 7 language executors

---

## Quick Fix: Install Docker

### Step 1: Install Docker Desktop

1. **Download:**
   - Windows: https://www.docker.com/products/docker-desktop
   - Download and run the installer

2. **Install:**
   - Follow the installation wizard
   - Restart your computer when prompted

3. **Start Docker Desktop:**
   - Open from Start menu
   - Wait until you see "Docker Desktop is running"
   - Check the whale icon in your system tray

### Step 2: Build the Executors

```bash
cd C:\Users\dasbl\WebstormProjects\Code-Tutor
docker-compose build
```

This will take 5-10 minutes the first time.

### Step 3: Start Everything

```bash
# Start Docker containers
docker-compose up -d

# Start the application
npm run dev
```

Or just run:
```bash
.\start.ps1
```

---

## Detailed Docker Setup

See **[DOCKER_SETUP.md](./DOCKER_SETUP.md)** for complete Docker installation and configuration guide.

---

## Current Status Checklist

Make sure you have:

- [ ] Node.js installed
- [ ] Dependencies installed (`npm install`)
- [ ] **Docker Desktop installed and running** ← **Required!**
- [ ] Executors built (`docker-compose build`)
- [ ] Executors started (`docker-compose up -d`)
- [ ] API server running on port 3001
- [ ] Web app running on port 3000

---

## Quick Start Commands

### Full Setup from Scratch

```bash
# 1. Install dependencies
npm install

# 2. Build Docker images (first time only, takes 5-10 min)
docker-compose build

# 3. Start everything
.\start.ps1
```

### Already Set Up?

```bash
.\start.ps1
```

Or manually:
```bash
# Terminal 1
docker-compose up -d

# Terminal 2  
cd apps/api
npm run dev

# Terminal 3
cd apps/web
npm run dev
```


---

## Testing Your Setup

### Test 1: Check if API is running

Open: http://localhost:3001/health

Should return:
```json
{
  "status": "ok",
  "timestamp": "..."
}
```

### Test 2: Check if courses are loaded

Open: http://localhost:3001/api/courses

Should return list of all 7 courses!

### Test 3: Check the web app

Open: http://localhost:3000

Should show the Code Tutor landing page.

---

## npm Warnings (Safe to Ignore)

These warnings are safe to ignore for now:

```
npm warn Unknown project config "auto-install-peers"
npm warn Unknown project config "hoist"
```

These are just workspace configuration warnings that don't affect functionality.

The security warnings about esbuild are also non-critical for development.

---

## Next Steps

### Minimal Setup (No Docker):

1. ✅ Keep web server running (you already have this)
2. ⏳ Start API server (run the command above)
3. ✅ Browse to http://localhost:3000
4. 🎉 Enjoy all 7 courses!

### Full Setup (With Docker):

1. Install Docker Desktop
2. Start Docker Desktop
3. Build executor images
4. Run `docker-compose up -d`
5. Now code execution works!

---

## Still Having Issues?

**Check these:**

1. **Is port 3000 already in use?**
   ```bash
   netstat -ano | findstr :3000
   ```

2. **Is port 3001 already in use?**
   ```bash
   netstat -ano | findstr :3001
   ```

3. **Do you have the .env files?**
   ```bash
   ls apps/web/.env
   ls apps/api/.env
   ```

4. **Are dependencies actually installed?**
   ```bash
   ls apps/api/node_modules
   ls apps/web/node_modules
   ```

---

## TL;DR - Start Right Now

**Two terminals:**

Terminal 1 (Already running):
```bash
cd apps/web
npm run dev
```

Terminal 2 (Run this now):
```bash
cd apps/api  
npm run dev
```

Then open: **http://localhost:3000**

**Done!** 🎉

