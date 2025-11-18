// Preload script for Electron
// This runs in a sandboxed context with access to Node.js APIs
// It exposes a safe API to the renderer process via IPC

import { contextBridge, ipcRenderer } from 'electron';

// Expose comprehensive desktop API to renderer
contextBridge.exposeInMainWorld('electronAPI', {
  // Course management
  getCourses: () =>
    ipcRenderer.invoke('get-courses'),

  getCourse: (language: string) =>
    ipcRenderer.invoke('get-course', { language }),

  // Code execution
  executeCode: (language: string, code: string) =>
    ipcRenderer.invoke('execute-code', { language, code }),

  // Challenge validation
  validateChallenge: (challenge: any, userSubmission: any) =>
    ipcRenderer.invoke('validate-challenge', { challenge, userSubmission }),

  validateVisibleTests: (code: string, language: string, testCases: any[]) =>
    ipcRenderer.invoke('validate-visible-tests', { code, language, testCases }),

  // Progress tracking
  getProgress: (userId?: string) =>
    ipcRenderer.invoke('get-progress', { userId }),

  saveProgress: (courseId: string, moduleId: string, lessonId: string, progressData: any, userId?: string) =>
    ipcRenderer.invoke('save-progress', { courseId, moduleId, lessonId, progressData, userId }),

  // Authentication (desktop single-user)
  register: (email: string, password: string, name: string) =>
    ipcRenderer.invoke('auth-register'),

  login: (email: string, password: string) =>
    ipcRenderer.invoke('auth-login'),

  verifyAuth: () =>
    ipcRenderer.invoke('auth-verify'),

  // Runtime detection
  checkRuntimes: () =>
    ipcRenderer.invoke('check-runtimes'),

  // Platform info
  getVersion: () => process.versions.electron,
  platform: process.platform,
  isPackaged: process.env.NODE_ENV === 'production'
});

// Mark that we're running in Electron
contextBridge.exposeInMainWorld('isElectron', true);
