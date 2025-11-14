// Preload script for Electron
// This runs in a sandboxed context with access to Node.js APIs
// It exposes a limited API to the renderer process

import { contextBridge, ipcRenderer } from 'electron';

// Expose safe APIs to renderer
contextBridge.exposeInMainWorld('electronAPI', {
  // Execute code via IPC (alternative to HTTP)
  executeCode: (language: string, code: string) =>
    ipcRenderer.invoke('execute-code', { language, code }),

  // Get app version
  getVersion: () => process.versions.electron,

  // Platform info
  platform: process.platform
});

// Mark that we're running in Electron
contextBridge.exposeInMainWorld('isElectron', true);
