import { app } from 'electron';
import { spawn, execSync } from 'child_process';
import * as path from 'path';
import * as fs from 'fs/promises';
import * as os from 'os';
import * as https from 'https';
import * as http from 'http';
import { createWriteStream } from 'fs';

export interface RuntimeInfo {
  name: string;
  displayName: string;
  installed: boolean;
  version?: string;
  executablePath?: string;
  downloadUrl?: string;
}

export interface InstallProgress {
  runtime: string;
  status: 'checking' | 'downloading' | 'extracting' | 'configuring' | 'complete' | 'error' | 'skipped';
  progress: number;
  message: string;
  error?: string;
}

type ProgressCallback = (progress: InstallProgress) => void;

// Get the runtimes directory in user data
function getRuntimesPath(): string {
  return path.join(app.getPath('userData'), 'runtimes');
}

// Check if a command exists in PATH
function commandExists(command: string): boolean {
  try {
    const result = execSync(
      os.platform() === 'win32'
        ? `where ${command}`
        : `which ${command}`,
      { stdio: 'pipe' }
    );
    return result.toString().trim().length > 0;
  } catch {
    return false;
  }
}

// Get version of an installed runtime
function getVersion(command: string, versionArg: string = '--version'): string | null {
  try {
    const result = execSync(`${command} ${versionArg}`, {
      stdio: 'pipe',
      timeout: 5000
    });
    return result.toString().trim();
  } catch {
    return null;
  }
}

// Check if Python is installed
export async function checkPython(): Promise<RuntimeInfo> {
  const pythonCommands = ['python3', 'python'];

  for (const cmd of pythonCommands) {
    if (commandExists(cmd)) {
      const version = getVersion(cmd);
      return {
        name: 'python',
        displayName: 'Python',
        installed: true,
        version: version || 'Unknown',
        executablePath: cmd
      };
    }
  }

  return {
    name: 'python',
    displayName: 'Python',
    installed: false,
    downloadUrl: 'https://www.python.org/downloads/'
  };
}

// Check if Java is installed
export async function checkJava(): Promise<RuntimeInfo> {
  if (commandExists('java')) {
    const version = getVersion('java', '-version');
    return {
      name: 'java',
      displayName: 'Java JDK',
      installed: true,
      version: version || 'Unknown',
      executablePath: 'java'
    };
  }

  return {
    name: 'java',
    displayName: 'Java JDK',
    installed: false,
    downloadUrl: 'https://adoptium.net/'
  };
}

// Check if Rust is installed
export async function checkRust(): Promise<RuntimeInfo> {
  if (commandExists('rustc')) {
    const version = getVersion('rustc');
    return {
      name: 'rust',
      displayName: 'Rust',
      installed: true,
      version: version || 'Unknown',
      executablePath: 'rustc'
    };
  }

  return {
    name: 'rust',
    displayName: 'Rust',
    installed: false,
    downloadUrl: 'https://rustup.rs/'
  };
}

// Check if .NET is installed
export async function checkDotNet(): Promise<RuntimeInfo> {
  if (commandExists('dotnet')) {
    const version = getVersion('dotnet', '--version');
    return {
      name: 'dotnet',
      displayName: '.NET SDK (C#)',
      installed: true,
      version: version || 'Unknown',
      executablePath: 'dotnet'
    };
  }

  return {
    name: 'dotnet',
    displayName: '.NET SDK (C#)',
    installed: false,
    downloadUrl: 'https://dotnet.microsoft.com/download'
  };
}

// Check if Kotlin is installed
export async function checkKotlin(): Promise<RuntimeInfo> {
  if (commandExists('kotlinc')) {
    const version = getVersion('kotlinc', '-version');
    return {
      name: 'kotlin',
      displayName: 'Kotlin',
      installed: true,
      version: version || 'Unknown',
      executablePath: 'kotlinc'
    };
  }

  return {
    name: 'kotlin',
    displayName: 'Kotlin',
    installed: false,
    downloadUrl: 'https://kotlinlang.org/docs/command-line.html'
  };
}

// Check if Dart is installed
export async function checkDart(): Promise<RuntimeInfo> {
  if (commandExists('dart')) {
    const version = getVersion('dart', '--version');
    return {
      name: 'dart',
      displayName: 'Dart',
      installed: true,
      version: version || 'Unknown',
      executablePath: 'dart'
    };
  }

  return {
    name: 'dart',
    displayName: 'Dart',
    installed: false,
    downloadUrl: 'https://dart.dev/get-dart'
  };
}

// Check Node.js (always installed with Electron)
export async function checkNode(): Promise<RuntimeInfo> {
  return {
    name: 'node',
    displayName: 'Node.js / JavaScript',
    installed: true,
    version: process.version,
    executablePath: process.execPath
  };
}

// Check all runtimes
export async function checkAllRuntimes(): Promise<RuntimeInfo[]> {
  const results = await Promise.all([
    checkNode(),
    checkPython(),
    checkJava(),
    checkRust(),
    checkDotNet(),
    checkKotlin(),
    checkDart()
  ]);

  return results;
}

// Download a file with progress
async function downloadFile(
  url: string,
  destPath: string,
  onProgress?: (downloaded: number, total: number) => void
): Promise<void> {
  return new Promise((resolve, reject) => {
    const protocol = url.startsWith('https') ? https : http;

    protocol.get(url, (response) => {
      // Handle redirects
      if (response.statusCode === 301 || response.statusCode === 302) {
        if (response.headers.location) {
          downloadFile(response.headers.location, destPath, onProgress)
            .then(resolve)
            .catch(reject);
          return;
        }
      }

      if (response.statusCode !== 200) {
        reject(new Error(`Download failed with status ${response.statusCode}`));
        return;
      }

      const total = parseInt(response.headers['content-length'] || '0', 10);
      let downloaded = 0;

      const fileStream = createWriteStream(destPath);

      response.on('data', (chunk) => {
        downloaded += chunk.length;
        if (onProgress) {
          onProgress(downloaded, total);
        }
      });

      response.pipe(fileStream);

      fileStream.on('finish', () => {
        fileStream.close();
        resolve();
      });

      fileStream.on('error', (err) => {
        fs.unlink(destPath).catch(() => {});
        reject(err);
      });
    }).on('error', reject);
  });
}

// Install runtime using system package manager or installer
async function installRuntime(
  runtime: string,
  onProgress?: ProgressCallback
): Promise<boolean> {
  const platform = os.platform();

  if (onProgress) {
    onProgress({
      runtime,
      status: 'checking',
      progress: 0,
      message: `Checking ${runtime} installation...`
    });
  }

  try {
    switch (runtime) {
      case 'python':
        return await installPython(onProgress);
      case 'java':
        return await installJava(onProgress);
      case 'rust':
        return await installRust(onProgress);
      case 'dotnet':
        return await installDotNet(onProgress);
      case 'kotlin':
        return await installKotlin(onProgress);
      case 'dart':
        return await installDart(onProgress);
      default:
        if (onProgress) {
          onProgress({
            runtime,
            status: 'error',
            progress: 0,
            message: `Unknown runtime: ${runtime}`,
            error: `Unknown runtime: ${runtime}`
          });
        }
        return false;
    }
  } catch (error: any) {
    if (onProgress) {
      onProgress({
        runtime,
        status: 'error',
        progress: 0,
        message: `Failed to install ${runtime}`,
        error: error.message
      });
    }
    return false;
  }
}

// Platform-specific installers
async function installPython(onProgress?: ProgressCallback): Promise<boolean> {
  const platform = os.platform();

  if (onProgress) {
    onProgress({
      runtime: 'python',
      status: 'downloading',
      progress: 0,
      message: 'Python installation requires manual setup. Opening download page...'
    });
  }

  // Open download page for user to install manually
  const url = 'https://www.python.org/downloads/';
  if (platform === 'win32') {
    execSync(`start ${url}`);
  } else if (platform === 'darwin') {
    execSync(`open ${url}`);
  } else {
    execSync(`xdg-open ${url}`);
  }

  if (onProgress) {
    onProgress({
      runtime: 'python',
      status: 'skipped',
      progress: 0,
      message: 'Please install Python from the opened webpage and restart the app.'
    });
  }

  return false;
}

async function installJava(onProgress?: ProgressCallback): Promise<boolean> {
  if (onProgress) {
    onProgress({
      runtime: 'java',
      status: 'downloading',
      progress: 0,
      message: 'Java installation requires manual setup. Opening download page...'
    });
  }

  const url = 'https://adoptium.net/';
  const platform = os.platform();

  if (platform === 'win32') {
    execSync(`start ${url}`);
  } else if (platform === 'darwin') {
    execSync(`open ${url}`);
  } else {
    execSync(`xdg-open ${url}`);
  }

  if (onProgress) {
    onProgress({
      runtime: 'java',
      status: 'skipped',
      progress: 0,
      message: 'Please install Java from the opened webpage and restart the app.'
    });
  }

  return false;
}

async function installRust(onProgress?: ProgressCallback): Promise<boolean> {
  const platform = os.platform();

  if (onProgress) {
    onProgress({
      runtime: 'rust',
      status: 'downloading',
      progress: 10,
      message: 'Installing Rust via rustup...'
    });
  }

  try {
    if (platform === 'win32') {
      // Windows: Download and run rustup-init.exe
      const url = 'https://win.rustup.rs/x86_64';
      const tempPath = path.join(app.getPath('temp'), 'rustup-init.exe');

      await downloadFile(url, tempPath, (downloaded, total) => {
        if (onProgress) {
          const progress = Math.round((downloaded / total) * 50) + 10;
          onProgress({
            runtime: 'rust',
            status: 'downloading',
            progress,
            message: `Downloading rustup installer... ${progress}%`
          });
        }
      });

      if (onProgress) {
        onProgress({
          runtime: 'rust',
          status: 'configuring',
          progress: 60,
          message: 'Running Rust installer...'
        });
      }

      // Run installer
      execSync(`"${tempPath}" -y`, { stdio: 'inherit' });

      if (onProgress) {
        onProgress({
          runtime: 'rust',
          status: 'complete',
          progress: 100,
          message: 'Rust installed successfully! Please restart the app.'
        });
      }

      return true;
    } else {
      // Unix: Run curl script
      execSync('curl --proto \'=https\' --tlsv1.2 -sSf https://sh.rustup.rs | sh -s -- -y', {
        stdio: 'inherit'
      });

      if (onProgress) {
        onProgress({
          runtime: 'rust',
          status: 'complete',
          progress: 100,
          message: 'Rust installed successfully! Please restart the app.'
        });
      }

      return true;
    }
  } catch (error: any) {
    if (onProgress) {
      onProgress({
        runtime: 'rust',
        status: 'error',
        progress: 0,
        message: 'Failed to install Rust automatically',
        error: error.message
      });
    }
    return false;
  }
}

async function installDotNet(onProgress?: ProgressCallback): Promise<boolean> {
  if (onProgress) {
    onProgress({
      runtime: 'dotnet',
      status: 'downloading',
      progress: 0,
      message: '.NET installation requires manual setup. Opening download page...'
    });
  }

  const url = 'https://dotnet.microsoft.com/download';
  const platform = os.platform();

  if (platform === 'win32') {
    execSync(`start ${url}`);
  } else if (platform === 'darwin') {
    execSync(`open ${url}`);
  } else {
    execSync(`xdg-open ${url}`);
  }

  if (onProgress) {
    onProgress({
      runtime: 'dotnet',
      status: 'skipped',
      progress: 0,
      message: 'Please install .NET SDK from the opened webpage and restart the app.'
    });
  }

  return false;
}

async function installKotlin(onProgress?: ProgressCallback): Promise<boolean> {
  if (onProgress) {
    onProgress({
      runtime: 'kotlin',
      status: 'downloading',
      progress: 0,
      message: 'Kotlin requires Java. Opening installation guide...'
    });
  }

  const url = 'https://kotlinlang.org/docs/command-line.html';
  const platform = os.platform();

  if (platform === 'win32') {
    execSync(`start ${url}`);
  } else if (platform === 'darwin') {
    execSync(`open ${url}`);
  } else {
    execSync(`xdg-open ${url}`);
  }

  if (onProgress) {
    onProgress({
      runtime: 'kotlin',
      status: 'skipped',
      progress: 0,
      message: 'Please follow the installation guide and restart the app.'
    });
  }

  return false;
}

async function installDart(onProgress?: ProgressCallback): Promise<boolean> {
  if (onProgress) {
    onProgress({
      runtime: 'dart',
      status: 'downloading',
      progress: 0,
      message: 'Dart installation requires manual setup. Opening download page...'
    });
  }

  const url = 'https://dart.dev/get-dart';
  const platform = os.platform();

  if (platform === 'win32') {
    execSync(`start ${url}`);
  } else if (platform === 'darwin') {
    execSync(`open ${url}`);
  } else {
    execSync(`xdg-open ${url}`);
  }

  if (onProgress) {
    onProgress({
      runtime: 'dart',
      status: 'skipped',
      progress: 0,
      message: 'Please install Dart from the opened webpage and restart the app.'
    });
  }

  return false;
}

// Check and install missing runtimes
export async function checkAndInstallRuntimes(
  onProgress?: ProgressCallback
): Promise<RuntimeInfo[]> {
  console.log('🔍 Checking installed runtimes...');

  const runtimes = await checkAllRuntimes();
  const missing = runtimes.filter(r => !r.installed);

  if (missing.length === 0) {
    console.log('✅ All runtimes are installed!');
    return runtimes;
  }

  console.log(`⚠️  Missing runtimes: ${missing.map(r => r.displayName).join(', ')}`);
  console.log('📥 Attempting to install missing runtimes...');

  for (const runtime of missing) {
    console.log(`\nInstalling ${runtime.displayName}...`);
    await installRuntime(runtime.name, onProgress);
  }

  // Re-check after installation
  const updatedRuntimes = await checkAllRuntimes();
  return updatedRuntimes;
}
