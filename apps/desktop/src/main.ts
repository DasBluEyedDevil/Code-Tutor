import { app, BrowserWindow, ipcMain, dialog } from 'electron';
import * as path from 'path';
import express from 'express';
import cors from 'cors';
import { startApiServer } from './api-server';
import { executeCode } from './executors';
import { checkAllRuntimes, RuntimeInfo } from './runtime-installer';

// Handle creating/removing shortcuts on Windows when installing/uninstalling
if (require('electron-squirrel-startup')) {
  app.quit();
}

let mainWindow: BrowserWindow | null = null;
let apiPort = 3001;

// Create the browser window
function createWindow() {
  mainWindow = new BrowserWindow({
    width: 1400,
    height: 900,
    minWidth: 800,
    minHeight: 600,
    webPreferences: {
      nodeIntegration: false,
      contextIsolation: true,
      preload: path.join(__dirname, 'preload.js')
    },
    icon: path.join(__dirname, '../../assets/icon.png'),
    title: 'Code Tutor',
    backgroundColor: '#1a1a1a'
  });

  // Load the frontend
    if (app.isPackaged) {
        // Production: load from built files in resources/app/web/dist
        mainWindow.loadFile(path.join(__dirname, '../web/dist/index.html'));
    } else {
        // Development: load from apps/web/dist
        const webPath = path.join(__dirname, '../../web/dist/index.html');
        mainWindow.loadFile(webPath);
    }

  // Open DevTools in development
  if (!app.isPackaged) {
    mainWindow.webContents.openDevTools();
  }

  mainWindow.on('closed', () => {
    mainWindow = null;
  });
}

// Check programming language runtimes
async function checkRuntimes(): Promise<RuntimeInfo[]> {
  console.log('🔍 Checking installed programming language runtimes...');
  const runtimes = await checkAllRuntimes();

  const installed = runtimes.filter(r => r.installed);
  const missing = runtimes.filter(r => !r.installed);

  console.log(`✅ Installed runtimes: ${installed.map(r => r.displayName).join(', ')}`);

  if (missing.length > 0) {
    console.log(`⚠️  Missing runtimes: ${missing.map(r => r.displayName).join(', ')}`);
  }

  return runtimes;
}

// Show runtime status dialog
function showRuntimeDialog(runtimes: RuntimeInfo[]) {
  const installed = runtimes.filter(r => r.installed);
  const missing = runtimes.filter(r => !r.installed);

  if (missing.length === 0) {
    return; // All runtimes installed, no need to show dialog
  }

  let message = 'Code Tutor has detected that some programming language runtimes are not installed.\n\n';

  message += '✅ Installed:\n';
  installed.forEach(r => {
    message += `  • ${r.displayName} ${r.version || ''}\n`;
  });

  message += '\n⚠️  Not Installed:\n';
  missing.forEach(r => {
    message += `  • ${r.displayName}\n`;
  });

  message += '\nYou can still use the app, but code execution will only work for installed languages.\n\n';
  message += 'To install missing runtimes, visit:\n';
  missing.forEach(r => {
    message += `  • ${r.displayName}: ${r.downloadUrl}\n`;
  });

  dialog.showMessageBox({
    type: 'warning',
    title: 'Runtime Check',
    message: 'Some Programming Languages Not Installed',
    detail: message,
    buttons: ['Continue Anyway', 'Exit and Install'],
    defaultId: 0,
    cancelId: 0
  }).then(result => {
    if (result.response === 1) {
      // User chose to exit
      app.quit();
    }
  });
}

// Start the embedded API server
async function startServer() {
  const server = express();
  server.use(cors());
  server.use(express.json());

  // Mount API routes
  await startApiServer(server);

  // Health check
  server.get('/health', (req, res) => {
    res.json({ status: 'ok', mode: 'desktop' });
  });

  // Start listening
  return new Promise<void>((resolve, reject) => {
    const serverInstance = server.listen(apiPort, () => {
      console.log(`API server running on http://localhost:${apiPort}`);
      resolve();
    }).on('error', (err: NodeJS.ErrnoException) => {
      if (err.code === 'EADDRINUSE') {
        console.log(`Port ${apiPort} in use, trying ${apiPort + 1}`);
        apiPort++;
        serverInstance.close();
        // Try next port
        server.listen(apiPort, () => {
          console.log(`API server running on http://localhost:${apiPort}`);
          resolve();
        });
      } else {
        reject(err);
      }
    });
  });
}

// App lifecycle
app.on('ready', async () => {
  try {
    // Check programming language runtimes
    const runtimes = await checkRuntimes();

    // Start the API server
    await startServer();

    // Create the window
    createWindow();

    // Show runtime dialog after window is created (if needed)
    if (mainWindow) {
      showRuntimeDialog(runtimes);
    }
  } catch (error) {
    console.error('Failed to start application:', error);
    app.quit();
  }
});

app.on('window-all-closed', () => {
  if (process.platform !== 'darwin') {
    app.quit();
  }
});

app.on('activate', () => {
  if (BrowserWindow.getAllWindows().length === 0) {
    createWindow();
  }
});

// Handle code execution via IPC (alternative to HTTP)
ipcMain.handle('execute-code', async (event, { language, code }) => {
  try {
    const result = await executeCode(language, code);
    return { success: true, result };
  } catch (error: any) {
    return {
      success: false,
      error: error.message || 'Execution failed'
    };
  }
});

// Handle runtime check via IPC
ipcMain.handle('check-runtimes', async () => {
  try {
    const runtimes = await checkAllRuntimes();
    return { success: true, runtimes };
  } catch (error: any) {
    return {
      success: false,
      error: error.message || 'Failed to check runtimes'
    };
  }
});
