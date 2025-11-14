import { spawn } from 'child_process';
import * as fs from 'fs/promises';
import * as path from 'path';
import * as os from 'os';
import { app } from 'electron';

interface ExecutionResult {
  success: boolean;
  output: string;
  error?: string;
  executionTime?: number;
}

// Create a temporary file for code execution
async function createTempFile(extension: string, code: string): Promise<string> {
  const tempDir = app.getPath('temp');
  const fileName = `code-tutor-${Date.now()}-${Math.random().toString(36).substr(2, 9)}${extension}`;
  const filePath = path.join(tempDir, fileName);
  await fs.writeFile(filePath, code, 'utf-8');
  return filePath;
}

// Clean up temporary file
async function cleanupTempFile(filePath: string): Promise<void> {
  try {
    await fs.unlink(filePath);
  } catch (err) {
    console.warn('Failed to cleanup temp file:', filePath);
  }
}

// Execute code with a command
async function executeWithCommand(
  command: string,
  args: string[],
  input?: string,
  timeoutMs: number = 10000
): Promise<ExecutionResult> {
  const startTime = Date.now();

  return new Promise((resolve) => {
    let output = '';
    let errorOutput = '';
    let killed = false;

    const proc = spawn(command, args, {
      shell: true,
      timeout: timeoutMs
    });

    // Set timeout
    const timer = setTimeout(() => {
      killed = true;
      proc.kill('SIGKILL');
      resolve({
        success: false,
        output: output,
        error: 'Execution timeout (10 seconds)',
        executionTime: Date.now() - startTime
      });
    }, timeoutMs);

    proc.stdout.on('data', (data) => {
      output += data.toString();
    });

    proc.stderr.on('data', (data) => {
      errorOutput += data.toString();
    });

    proc.on('close', (code) => {
      clearTimeout(timer);
      if (killed) return;

      const executionTime = Date.now() - startTime;

      if (code === 0) {
        resolve({
          success: true,
          output: output || errorOutput,
          executionTime
        });
      } else {
        resolve({
          success: false,
          output: output,
          error: errorOutput || `Process exited with code ${code}`,
          executionTime
        });
      }
    });

    proc.on('error', (err) => {
      clearTimeout(timer);
      if (killed) return;

      resolve({
        success: false,
        output: '',
        error: err.message,
        executionTime: Date.now() - startTime
      });
    });

    // Send input if provided
    if (input) {
      proc.stdin.write(input);
      proc.stdin.end();
    }
  });
}

// Execute Python code
async function executePython(code: string): Promise<ExecutionResult> {
  const filePath = await createTempFile('.py', code);
  try {
    // Try python3 first, then python
    const pythonCommands = ['python3', 'python'];
    let result: ExecutionResult | null = null;

    for (const cmd of pythonCommands) {
      try {
        result = await executeWithCommand(cmd, [filePath]);
        if (!result.error?.includes('not found') && !result.error?.includes('not recognized')) {
          break;
        }
      } catch (err) {
        continue;
      }
    }

    if (!result || result.error?.includes('not found') || result.error?.includes('not recognized')) {
      return {
        success: false,
        output: '',
        error: 'Python is not installed. Please install Python 3 from https://www.python.org/'
      };
    }

    return result;
  } finally {
    await cleanupTempFile(filePath);
  }
}

// Execute JavaScript code using Node.js
async function executeJavaScript(code: string): Promise<ExecutionResult> {
  const filePath = await createTempFile('.js', code);
  try {
    return await executeWithCommand('node', [filePath]);
  } finally {
    await cleanupTempFile(filePath);
  }
}

// Execute TypeScript code
async function executeTypeScript(code: string): Promise<ExecutionResult> {
  const filePath = await createTempFile('.ts', code);
  try {
    const result = await executeWithCommand('npx', ['tsx', filePath]);
    if (result.error?.includes('not found') || result.error?.includes('not recognized')) {
      return {
        success: false,
        output: '',
        error: 'TypeScript runtime not available. JavaScript execution is supported.'
      };
    }
    return result;
  } finally {
    await cleanupTempFile(filePath);
  }
}

// Execute Java code
async function executeJava(code: string): Promise<ExecutionResult> {
  // Extract class name from code
  const classMatch = code.match(/public\s+class\s+(\w+)/);
  const className = classMatch ? classMatch[1] : 'Main';

  const filePath = await createTempFile('.java', code);
  const dir = path.dirname(filePath);
  const javaFileName = `${className}.java`;
  const javaFilePath = path.join(dir, javaFileName);

  try {
    // Rename file to match class name
    await fs.rename(filePath, javaFilePath);

    // Compile
    const compileResult = await executeWithCommand('javac', [javaFilePath]);
    if (!compileResult.success) {
      if (compileResult.error?.includes('not found') || compileResult.error?.includes('not recognized')) {
        return {
          success: false,
          output: '',
          error: 'Java is not installed. Please install JDK from https://adoptium.net/'
        };
      }
      return compileResult;
    }

    // Execute
    return await executeWithCommand('java', ['-cp', dir, className]);
  } finally {
    await cleanupTempFile(javaFilePath);
    // Also cleanup .class file
    const classFilePath = path.join(dir, `${className}.class`);
    await cleanupTempFile(classFilePath);
  }
}

// Execute Rust code
async function executeRust(code: string): Promise<ExecutionResult> {
  const filePath = await createTempFile('.rs', code);
  const exePath = filePath.replace('.rs', os.platform() === 'win32' ? '.exe' : '');

  try {
    // Compile
    const compileResult = await executeWithCommand('rustc', [filePath, '-o', exePath]);
    if (!compileResult.success) {
      if (compileResult.error?.includes('not found') || compileResult.error?.includes('not recognized')) {
        return {
          success: false,
          output: '',
          error: 'Rust is not installed. Please install Rust from https://rustup.rs/'
        };
      }
      return compileResult;
    }

    // Execute
    return await executeWithCommand(exePath, []);
  } finally {
    await cleanupTempFile(filePath);
    await cleanupTempFile(exePath);
  }
}

// Execute C# code
async function executeCSharp(code: string): Promise<ExecutionResult> {
  const filePath = await createTempFile('.cs', code);
  try {
    // Try dotnet script first (simpler), then compilation
    const result = await executeWithCommand('dotnet', ['script', filePath]);
    if (result.error?.includes('not found') || result.error?.includes('not recognized')) {
      return {
        success: false,
        output: '',
        error: 'C# (.NET) is not installed. Please install .NET SDK from https://dotnet.microsoft.com/'
      };
    }
    return result;
  } finally {
    await cleanupTempFile(filePath);
  }
}

// Execute Kotlin code
async function executeKotlin(code: string): Promise<ExecutionResult> {
  const filePath = await createTempFile('.kt', code);
  const jarPath = filePath.replace('.kt', '.jar');

  try {
    // Compile
    const compileResult = await executeWithCommand('kotlinc', [filePath, '-include-runtime', '-d', jarPath]);
    if (!compileResult.success) {
      if (compileResult.error?.includes('not found') || compileResult.error?.includes('not recognized')) {
        return {
          success: false,
          output: '',
          error: 'Kotlin is not installed. Please install Kotlin from https://kotlinlang.org/'
        };
      }
      return compileResult;
    }

    // Execute
    return await executeWithCommand('java', ['-jar', jarPath]);
  } finally {
    await cleanupTempFile(filePath);
    await cleanupTempFile(jarPath);
  }
}

// Execute Dart code
async function executeDart(code: string): Promise<ExecutionResult> {
  const filePath = await createTempFile('.dart', code);
  try {
    const result = await executeWithCommand('dart', ['run', filePath]);
    if (result.error?.includes('not found') || result.error?.includes('not recognized')) {
      return {
        success: false,
        output: '',
        error: 'Dart is not installed. Please install Dart from https://dart.dev/'
      };
    }
    return result;
  } finally {
    await cleanupTempFile(filePath);
  }
}

// Main executor function
export async function executeCode(language: string, code: string): Promise<ExecutionResult> {
  const lang = language.toLowerCase();

  try {
    switch (lang) {
      case 'python':
        return await executePython(code);

      case 'javascript':
      case 'js':
        return await executeJavaScript(code);

      case 'typescript':
      case 'ts':
        return await executeTypeScript(code);

      case 'java':
        return await executeJava(code);

      case 'rust':
        return await executeRust(code);

      case 'csharp':
      case 'c#':
        return await executeCSharp(code);

      case 'kotlin':
        return await executeKotlin(code);

      case 'dart':
      case 'flutter':
        return await executeDart(code);

      default:
        return {
          success: false,
          output: '',
          error: `Language "${language}" is not supported yet`
        };
    }
  } catch (error: any) {
    return {
      success: false,
      output: '',
      error: error.message || 'Unknown execution error'
    };
  }
}
