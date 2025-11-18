import { app, BrowserWindow, ipcMain, dialog } from 'electron';
import * as path from 'path';
import * as fs from 'fs/promises';
import { executeCode } from './executors';
import { checkAllRuntimes, RuntimeInfo } from './runtime-installer';
import { validateChallenge, validateVisibleTestCases } from './challenge-validator';
import type { Challenge, ChallengeSubmission } from './types';

// Handle creating/removing shortcuts on Windows when installing/uninstalling
if (require('electron-squirrel-startup')) {
  app.quit();
}

let mainWindow: BrowserWindow | null = null;

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

// Get the content directory based on whether app is packaged
function getContentPath() {
  if (app.isPackaged) {
    return path.join(process.resourcesPath, 'content');
  }
  return path.join(__dirname, '../../../content');
}

// Progress file management
const getProgressFilePath = () => path.join(app.getPath('userData'), 'progress.json');

async function readProgress(): Promise<any> {
  try {
    const data = await fs.readFile(getProgressFilePath(), 'utf-8');
    return JSON.parse(data);
  } catch {
    return {};
  }
}

async function writeProgress(data: any): Promise<void> {
  await fs.writeFile(getProgressFilePath(), JSON.stringify(data, null, 2));
}

// Setup all IPC handlers for the desktop app
function setupIpcHandlers() {
  const contentPath = getContentPath();
  console.log('📁 Content path:', contentPath);
  console.log('💾 Progress file:', getProgressFilePath());

  // Get all courses
  ipcMain.handle('get-courses', async () => {
    try {
      const coursesDir = path.join(contentPath, 'courses');
      const languages = await fs.readdir(coursesDir);

      const courses = [];
      for (const language of languages) {
        const courseJsonPath = path.join(coursesDir, language, 'course.json');
        try {
          const stats = await fs.stat(courseJsonPath);
          if (stats.isFile()) {
            const courseData = await fs.readFile(courseJsonPath, 'utf-8');
            const course = JSON.parse(courseData);
            courses.push({
              id: course.id,
              language: course.language,
              title: course.title,
              description: course.description,
              difficulty: course.difficulty,
              estimatedHours: course.estimatedHours,
              moduleCount: course.modules?.length || 0
            });
          }
        } catch (err) {
          console.warn(`No course.json found for ${language}`);
        }
      }

      return { success: true, courses };
    } catch (error: any) {
      console.error('Error fetching courses:', error);
      return { success: false, error: error.message };
    }
  });

  // Get specific course
  ipcMain.handle('get-course', async (event, { language }) => {
    try {
      const courseJsonPath = path.join(contentPath, 'courses', language, 'course.json');
      const courseData = await fs.readFile(courseJsonPath, 'utf-8');
      const course = JSON.parse(courseData);
      return { success: true, course };
    } catch (error: any) {
      console.error(`Error fetching course ${language}:`, error);
      return { success: false, error: error.message };
    }
  });

  // Execute code
  ipcMain.handle('execute-code', async (event, { language, code }) => {
    try {
      if (!language || !code) {
        return {
          success: false,
          error: 'Missing required fields: language and code'
        };
      }
      const result = await executeCode(language, code);
      return { success: true, result };
    } catch (error: any) {
      console.error('Execution error:', error);
      return {
        success: false,
        error: error.message || 'Execution failed'
      };
    }
  });

  // Validate challenge
  ipcMain.handle('validate-challenge', async (event, { challenge, userSubmission }) => {
    try {
      if (!challenge || !userSubmission) {
        return {
          success: false,
          error: 'Missing required fields: challenge and userSubmission'
        };
      }

      if (!challenge.type) {
        return {
          success: false,
          error: 'Invalid challenge: missing type field'
        };
      }

      if (!userSubmission.challengeId || userSubmission.userAnswer === undefined) {
        return {
          success: false,
          error: 'Invalid submission: missing challengeId or userAnswer'
        };
      }

      const validationResult = await validateChallenge(challenge, userSubmission);
      return { success: true, result: validationResult };
    } catch (error: any) {
      console.error('Validation error:', error);
      return {
        success: false,
        error: error.message || 'Validation failed'
      };
    }
  });

  // Validate visible test cases only
  ipcMain.handle('validate-visible-tests', async (event, { code, language, testCases }) => {
    try {
      if (!code || !language || !testCases) {
        return {
          success: false,
          error: 'Missing required fields: code, language, and testCases'
        };
      }

      const validationResult = await validateVisibleTestCases(code, language, testCases);
      return { success: true, result: validationResult };
    } catch (error: any) {
      console.error('Visible test validation error:', error);
      return {
        success: false,
        error: error.message || 'Validation failed'
      };
    }
  });

  // Get progress
  ipcMain.handle('get-progress', async (event, { userId = 'default' }) => {
    try {
      const allProgress = await readProgress();
      const userProgress = allProgress[userId] || {};
      return { success: true, progress: userProgress };
    } catch (error: any) {
      console.error('Error reading progress:', error);
      return { success: false, error: error.message };
    }
  });

  // Save progress
  ipcMain.handle('save-progress', async (event, { courseId, moduleId, lessonId, userId = 'default', progressData }) => {
    try {
      if (!courseId || !moduleId || !lessonId) {
        return {
          success: false,
          error: 'courseId, moduleId, and lessonId are required'
        };
      }

      const allProgress = await readProgress();

      if (!allProgress[userId]) {
        allProgress[userId] = {};
      }
      if (!allProgress[userId][courseId]) {
        allProgress[userId][courseId] = {};
      }
      if (!allProgress[userId][courseId][moduleId]) {
        allProgress[userId][courseId][moduleId] = {};
      }

      allProgress[userId][courseId][moduleId][lessonId] = {
        ...allProgress[userId][courseId][moduleId][lessonId],
        ...progressData,
        lastUpdated: new Date().toISOString(),
      };

      await writeProgress(allProgress);
      return { success: true };
    } catch (error: any) {
      console.error('Error saving progress:', error);
      return { success: false, error: error.message };
    }
  });

  // Auth handlers (simplified for desktop - single user)
  ipcMain.handle('auth-register', async () => {
    return {
      success: true,
      user: { id: '1', email: 'user@localhost', name: 'Desktop User' },
      token: 'desktop-user-token'
    };
  });

  ipcMain.handle('auth-login', async () => {
    return {
      success: true,
      user: { id: '1', email: 'user@localhost', name: 'Desktop User' },
      token: 'desktop-user-token'
    };
  });

  ipcMain.handle('auth-verify', async () => {
    return {
      success: true,
      valid: true,
      user: { id: '1', email: 'user@localhost', name: 'Desktop User' }
    };
  });

  // Check runtimes
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

  console.log('✅ All IPC handlers registered');
}

// App lifecycle
app.on('ready', async () => {
  try {
    console.log('🚀 Starting Code Tutor Desktop Application...');

    // Setup IPC handlers first
    setupIpcHandlers();

    // Check programming language runtimes
    const runtimes = await checkRuntimes();

    // Create the window
    createWindow();

    // Show runtime dialog after window is created (if needed)
    if (mainWindow) {
      showRuntimeDialog(runtimes);
    }

    console.log('✅ Application started successfully');
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
