import { app, BrowserWindow, ipcMain } from 'electron';
import * as path from 'path';
import express from 'express';
import cors from 'cors';
import { startApiServer } from './api-server';
import { executeCode } from './executors';

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
    // Production: load from built files
    mainWindow.loadFile(path.join(__dirname, '../../web/dist/index.html'));
  } else {
    // Development: load from file (we'll build it first)
    const webPath = path.join(__dirname, '../../../web/dist/index.html');
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
    // Start the API server first
    await startServer();

    // Then create the window
    createWindow();
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
