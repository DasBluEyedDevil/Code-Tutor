# Troubleshooting Guide - Code Tutor Desktop App

## Quick Overview

Code Tutor is a **desktop application** that runs code using your **local language runtimes**. No Docker required!

---

## ✅ What You Need

- ✅ Node.js 18+ installed
- ✅ npm dependencies installed (`npm install`)
- ✅ Language runtimes installed (Python, Java, Rust, etc.) for the languages you want to learn

---

## 🚀 Quick Start

### Launch the Desktop App

**Windows:**
```bash
.\launch-desktop.bat
```
Or:
```powershell
.\launch-desktop.ps1
```

**macOS/Linux:**
```bash
./launch-desktop.sh
```

**Or use npm:**
```bash
npm run start:desktop
```

---

## Common Issues

### Issue 1: "Language runtime not found"

**Problem:** You're trying to run code for a language that isn't installed on your system.

**Solution:** Install the language runtime:

- **Python:** https://www.python.org/downloads/
- **Java:** https://adoptium.net/
- **Rust:** https://www.rust-lang.org/tools/install
- **.NET (C#):** https://dotnet.microsoft.com/download
- **Node.js (JavaScript):** https://nodejs.org
- **Dart/Flutter:** https://flutter.dev/docs/get-started/install

After installation, restart the desktop app.

### Issue 2: "Port 3001 already in use"

**Problem:** Another application is using port 3001.

**Solution:**

**Windows:**
```bash
netstat -ano | findstr :3001
taskkill /PID <PID> /F
```

**macOS/Linux:**
```bash
lsof -ti:3001 | xargs kill -9
```

Or change the port in `apps/desktop/src/api-server.ts`.

### Issue 3: Desktop app won't start

**Problem:** Dependencies aren't built or there's an issue with the build.

**Solution:**

1. **Clean install:**
   ```bash
   rm -rf node_modules apps/web/node_modules apps/desktop/node_modules
   npm install
   ```

2. **Build the desktop app:**
   ```bash
   npm run build:desktop
   ```

3. **Try starting again:**
   ```bash
   npm run start:desktop
   ```

### Issue 4: "Cannot find course content"

**Problem:** Course content isn't being loaded.

**Solution:** Ensure the `content/courses/` directory exists and contains course files:

```bash
ls content/courses/
```

You should see directories for: `python`, `java`, `kotlin`, `rust`, `csharp`, `javascript`, `dart`

### Issue 5: Code execution errors

**Problem:** Code runs but produces unexpected errors.

**Solutions:**

1. **Check language version:**
   ```bash
   # Python
   python --version

   # Java
   java -version

   # Rust
   rustc --version

   # .NET
   dotnet --version

   # Node.js
   node --version

   # Dart
   dart --version
   ```

2. **Ensure minimum versions:**
   - Python 3.8+
   - Java 17+
   - Rust 1.70+
   - .NET 6.0+
   - Node.js 18+
   - Dart 3.0+

3. **Check PATH:** Make sure language runtimes are in your system PATH.

---

## Testing Your Setup

### Test 1: Check if the app starts

Run the desktop launcher:
```bash
npm run start:desktop
```

You should see:
```
✓ Built in XXms
Desktop app starting...
Embedded server running on http://localhost:3001
```

And the desktop window should open automatically.

### Test 2: Check available languages

In the desktop app, go to the language selection page. The app will show which languages are available based on installed runtimes.

### Test 3: Run a simple program

Try running a "Hello World" program in any installed language:

**Python:**
```python
print("Hello, World!")
```

**Java:**
```java
public class Main {
    public static void main(String[] args) {
        System.out.println("Hello, World!");
    }
}
```

If it runs successfully, your setup is working!

---

## Building Installers

### Create distributable installers

```bash
npm run dist:desktop
```

Or use the helper scripts:

**Windows:**
```bash
.\build-installers.bat
```

**macOS/Linux:**
```bash
./build-installers.sh
```

Installers will be created in `apps/desktop/dist-electron/`.

---

## npm Warnings (Safe to Ignore)

These warnings are safe to ignore:

```
npm warn Unknown project config "auto-install-peers"
npm warn Unknown project config "hoist"
```

These are workspace configuration warnings that don't affect functionality.

---

## Development Mode

### Running in development

```bash
# Start in dev mode (watches for changes)
npm run start:desktop

# Build without installers
npm run build:desktop

# Build with installers
npm run dist:desktop
```

### Checking logs

The desktop app logs to the console where you started it. Check for any error messages there.

### Debugging

To open DevTools in the desktop app:
- **Windows/Linux:** Press `Ctrl+Shift+I`
- **macOS:** Press `Cmd+Option+I`

Or add this to your development code:
```javascript
mainWindow.webContents.openDevTools();
```

---

## Still Having Issues?

**Check these:**

1. **Node.js version:**
   ```bash
   node --version  # Should be 18.0.0 or higher
   ```

2. **npm version:**
   ```bash
   npm --version  # Should be 9.0.0 or higher
   ```

3. **Dependencies installed:**
   ```bash
   ls node_modules
   ls apps/web/node_modules
   ls apps/desktop/node_modules
   ```

4. **Build output exists:**
   ```bash
   ls apps/web/dist
   ls apps/desktop/dist
   ```

---

## Getting Help

**Found a bug?** Open an issue on GitHub:
https://github.com/DasBluEyedDevil/Code-Tutor/issues

**Need help?** Check the documentation:
- [QUICK_START.md](./QUICK_START.md) - Getting started guide
- [DESKTOP_APP_README.md](./DESKTOP_APP_README.md) - Desktop app details
- [PACKAGING_GUIDE.md](./PACKAGING_GUIDE.md) - Creating installers
- [DISTRIBUTION.md](./DISTRIBUTION.md) - Distribution guide

---

## TL;DR - Quick Start

**Just want to start coding?**

```bash
# Install dependencies (first time only)
npm install

# Launch the app
npm run start:desktop
```

**Done!** 🎉
