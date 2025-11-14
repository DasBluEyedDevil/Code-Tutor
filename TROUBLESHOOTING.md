# Quick Troubleshooting Guide

## Current Issue: Docker Error

You're seeing: `unable to get image 'code-tutor-python-executor'`

**Good News:** Docker is **optional**! The platform works fine without it.

---

## ✅ What's Working

Based on your output:
- ✅ Node.js is installed
- ✅ Dependencies are installed
- ✅ Web app is running on http://localhost:3000

---

## ⚠️ What's Not Working

- ❌ Docker executors (but they're optional for now!)

---

## Quick Fixes

### Option 1: Run Without Code Execution (Easiest)

The platform will work perfectly for viewing all the course content you just imported!

**Just start the API server:**

1. Open a **new terminal**
2. Run:
   ```bash
   cd apps/api
   npm run dev
   ```

3. Keep the web terminal running
4. Open http://localhost:3000 in your browser

**That's it!** You can now browse all 7 courses you imported.

---

### Option 2: Fix Docker (For Code Execution)

If you want to run code examples:

1. **Install Docker Desktop:**
   - Download from: https://www.docker.com/products/docker-desktop
   - Install and restart your computer

2. **Start Docker Desktop:**
   - Open Docker Desktop from Start menu
   - Wait until it says "Docker Desktop is running"
   - You'll see a whale icon in your system tray

3. **Build the executor images:**
   ```bash
   cd apps/executors/python
   docker build -t code-tutor-python-executor .
   cd ../../..
   ```

4. **Start the executors:**
   ```bash
   docker-compose up -d
   ```

---

### Option 3: Use the Startup Script

I created a script that handles everything:

```bash
.\start.ps1
```

This will:
- Check if everything is installed
- Start the API server
- Start the web app
- Optionally start Docker executors if available

---

## Current Status Checklist

Based on your terminal output:

- [x] Node.js installed
- [x] Dependencies installed  
- [x] Web app running on port 3000
- [ ] API server running on port 3001 ← **Start this!**
- [ ] Docker running (optional)
- [ ] Executors running (optional)

---

## What You Can Do Right Now

### 1. Start the API (Required)

**New terminal:**
```bash
cd C:\Users\dasbl\WebstormProjects\Code-Tutor\apps\api
npm run dev
```

You should see:
```
🚀 API server running on http://localhost:3001
📚 Courses endpoint: http://localhost:3001/api/courses
```

### 2. Open the Browser

Go to: **http://localhost:3000**

You should see:
- Landing page with 7 programming languages
- All your imported courses ready to view!

### 3. Browse Courses

You can now:
- ✅ View all course content
- ✅ Read lessons
- ✅ See code examples
- ❌ Run code (needs Docker)

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

