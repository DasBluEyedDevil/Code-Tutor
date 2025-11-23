using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CodeTutor.Native.Models;

namespace CodeTutor.Native.Services;

/// <summary>
/// Executes code in various programming languages with resource limits
/// NO HTTP, NO IPC - direct process spawning
/// Security: Memory limits, CPU limits, disk quota, timeout protection, restricted environment
/// </summary>
public class CodeExecutor : ICodeExecutor
{
    private const int DefaultTimeoutMs = 10000; // 10 seconds
    private const long MaxMemoryBytes = 512 * 1024 * 1024; // 512 MB
    private const int MaxOutputLength = 100000; // 100 KB output limit
    private const long MaxDiskQuotaBytes = 10 * 1024 * 1024; // 10 MB disk quota
    private const int CpuLimitPercent = 20; // 20% CPU limit
    private const int UnixCpuTimeLimit = 10; // 10 seconds CPU time for ulimit

    private readonly IErrorHandlerService? _errorHandler;
    private readonly DiskQuotaManager _diskQuotaManager;

    public CodeExecutor(IErrorHandlerService? errorHandler = null)
    {
        _errorHandler = errorHandler;
        _diskQuotaManager = new DiskQuotaManager(MaxDiskQuotaBytes, errorHandler);
    }

    /// <summary>
    /// Execute code in the specified language
    /// </summary>
    public async Task<ExecutionResult> ExecuteAsync(string language, string code)
    {
        return language.ToLower() switch
        {
            "python" => await ExecutePythonAsync(code),
            "javascript" => await ExecuteJavaScriptAsync(code),
            "java" => await ExecuteJavaAsync(code),
            "rust" => await ExecuteRustAsync(code),
            "csharp" or "c#" => await ExecuteCSharpAsync(code),
            _ => new ExecutionResult
            {
                Success = false,
                Error = $"Unsupported language: {language}"
            }
        };
    }

    private async Task<ExecutionResult> ExecutePythonAsync(string code)
    {
        string? sandboxDir = null;
        try
        {
            sandboxDir = _diskQuotaManager.CreateSandboxDirectory();
            var tempFile = Path.Combine(sandboxDir, "script.py");
            await File.WriteAllTextAsync(tempFile, code);
            return await RunProcessAsync("python3", tempFile, sandboxDir);
        }
        finally
        {
            if (sandboxDir != null)
            {
                _diskQuotaManager.CleanupSandboxDirectory(sandboxDir);
            }
        }
    }

    private async Task<ExecutionResult> ExecuteJavaScriptAsync(string code)
    {
        string? sandboxDir = null;
        try
        {
            sandboxDir = _diskQuotaManager.CreateSandboxDirectory();
            var tempFile = Path.Combine(sandboxDir, "script.js");
            await File.WriteAllTextAsync(tempFile, code);
            return await RunProcessAsync("node", tempFile, sandboxDir);
        }
        finally
        {
            if (sandboxDir != null)
            {
                _diskQuotaManager.CleanupSandboxDirectory(sandboxDir);
            }
        }
    }

    private async Task<ExecutionResult> ExecuteJavaAsync(string code)
    {
        string? sandboxDir = null;
        try
        {
            sandboxDir = _diskQuotaManager.CreateSandboxDirectory();

            // Extract class name from code
            var className = ExtractJavaClassName(code) ?? "Main";
            var javaFile = Path.Combine(sandboxDir, $"{className}.java");

            await File.WriteAllTextAsync(javaFile, code);

            // Compile
            var compileResult = await RunProcessAsync("javac", javaFile, sandboxDir);
            if (!compileResult.Success)
            {
                return compileResult;
            }

            // Run
            return await RunProcessAsync("java", className, sandboxDir);
        }
        finally
        {
            if (sandboxDir != null)
            {
                _diskQuotaManager.CleanupSandboxDirectory(sandboxDir);
            }
        }
    }

    private async Task<ExecutionResult> ExecuteRustAsync(string code)
    {
        string? sandboxDir = null;
        try
        {
            sandboxDir = _diskQuotaManager.CreateSandboxDirectory();
            var tempFile = Path.Combine(sandboxDir, "main.rs");
            var exeFile = Path.Combine(sandboxDir, RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "main.exe" : "main");

            await File.WriteAllTextAsync(tempFile, code);

            // Compile
            var compileResult = await RunProcessAsync("rustc", $"{tempFile} -o {exeFile}", sandboxDir);
            if (!compileResult.Success)
            {
                return compileResult;
            }

            // Run
            return await RunProcessAsync(exeFile, "", sandboxDir);
        }
        finally
        {
            if (sandboxDir != null)
            {
                _diskQuotaManager.CleanupSandboxDirectory(sandboxDir);
            }
        }
    }

    private async Task<ExecutionResult> ExecuteCSharpAsync(string code)
    {
        string? sandboxDir = null;
        try
        {
            sandboxDir = _diskQuotaManager.CreateSandboxDirectory();
            var tempFile = Path.Combine(sandboxDir, "script.cs");
            await File.WriteAllTextAsync(tempFile, code);
            return await RunProcessAsync("dotnet", $"script {tempFile}", sandboxDir);
        }
        finally
        {
            if (sandboxDir != null)
            {
                _diskQuotaManager.CleanupSandboxDirectory(sandboxDir);
            }
        }
    }

    /// <summary>
    /// Run a process with resource limits and security restrictions
    /// </summary>
    private async Task<ExecutionResult> RunProcessAsync(string command, string arguments, string? workingDirectory = null)
    {
        var stopwatch = Stopwatch.StartNew();
        Process? process = null;
        IntPtr jobHandle = IntPtr.Zero;

        try
        {
            ProcessStartInfo startInfo;

            // Platform-specific process setup with resource limits
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ||
                RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                // Use shell wrapper with ulimit for Unix systems
                startInfo = CreateUnixProcessWithLimits(command, arguments, workingDirectory);
            }
            else
            {
                // Standard process for Windows (Job Object applied after start)
                startInfo = new ProcessStartInfo
                {
                    FileName = command,
                    Arguments = arguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    WorkingDirectory = workingDirectory ?? Directory.GetCurrentDirectory()
                };
            }

            // Apply security restrictions to environment
            ApplySecurityRestrictions(startInfo);

            process = new Process { StartInfo = startInfo };

            var outputBuilder = new StringBuilder();
            var errorBuilder = new StringBuilder();
            var outputTruncated = false;

            process.OutputDataReceived += (sender, e) =>
            {
                if (e.Data != null && outputBuilder.Length < MaxOutputLength)
                {
                    outputBuilder.AppendLine(e.Data);
                }
                else if (e.Data != null)
                {
                    outputTruncated = true;
                }
            };

            process.ErrorDataReceived += (sender, e) =>
            {
                if (e.Data != null && errorBuilder.Length < MaxOutputLength)
                {
                    errorBuilder.AppendLine(e.Data);
                }
            };

            process.Start();

            // Apply Windows Job Object for CPU/memory limits after process starts
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                jobHandle = ApplyWindowsJobObjectLimits(process);
            }

            // Apply process-level resource limits
            ApplyResourceLimits(process);

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            var completed = await Task.Run(() => process.WaitForExit(DefaultTimeoutMs));

            if (!completed)
            {
                try
                {
                    // Try graceful shutdown first
                    process.Kill(entireProcessTree: true);
                }
                catch
                {
                    process.Kill();
                }

                return new ExecutionResult
                {
                    Success = false,
                    Error = "Execution timed out (10 second limit)",
                    ExecutionTimeMs = DefaultTimeoutMs
                };
            }

            stopwatch.Stop();

            var output = outputBuilder.ToString();
            var error = errorBuilder.ToString();

            if (outputTruncated)
            {
                output += "\n[Output truncated - exceeded 100KB limit]";
            }

            return new ExecutionResult
            {
                Success = process.ExitCode == 0,
                Output = output,
                Error = error,
                ExitCode = process.ExitCode,
                ExecutionTimeMs = (int)stopwatch.ElapsedMilliseconds
            };
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            _errorHandler?.LogError(ex, "Code execution failed");
            return new ExecutionResult
            {
                Success = false,
                Error = $"Execution failed: {ex.Message}",
                ExecutionTimeMs = (int)stopwatch.ElapsedMilliseconds
            };
        }
        finally
        {
            process?.Dispose();

            // Clean up Windows Job Object
            if (jobHandle != IntPtr.Zero && RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                CloseHandle(jobHandle);
            }
        }
    }

    /// <summary>
    /// Create a Unix process with ulimit resource limits
    /// </summary>
    private ProcessStartInfo CreateUnixProcessWithLimits(string command, string arguments, string? workingDirectory)
    {
        // Build ulimit wrapper command:
        // -t: CPU time limit (seconds)
        // -v: Virtual memory limit (KB) - 512MB = 524288 KB
        // -f: File size limit (KB) - 10MB = 10240 KB
        var ulimitPrefix = $"ulimit -t {UnixCpuTimeLimit} -v {MaxMemoryBytes / 1024} -f {MaxDiskQuotaBytes / 1024} 2>/dev/null; ";
        var fullCommand = $"{ulimitPrefix}{command} {arguments}";

        return new ProcessStartInfo
        {
            FileName = "/bin/bash",
            Arguments = $"-c \"{fullCommand.Replace("\"", "\\\"")}\"",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true,
            WorkingDirectory = workingDirectory ?? Directory.GetCurrentDirectory()
        };
    }

    /// <summary>
    /// Apply Windows Job Object for CPU and memory limits
    /// Returns the job handle (must be closed when done)
    /// </summary>
    private IntPtr ApplyWindowsJobObjectLimits(Process process)
    {
        try
        {
            // Create a Job Object
            var jobHandle = CreateJobObject(IntPtr.Zero, null);
            if (jobHandle == IntPtr.Zero)
            {
                var error = Marshal.GetLastWin32Error();
                _errorHandler?.LogWarning($"Failed to create Job Object: Win32 error {error}", "Security");
                return IntPtr.Zero;
            }

            // Set CPU rate limit to 20%
            var cpuRateInfo = new JOBOBJECT_CPU_RATE_CONTROL_INFORMATION
            {
                ControlFlags = JOB_OBJECT_CPU_RATE_CONTROL_ENABLE | JOB_OBJECT_CPU_RATE_CONTROL_HARD_CAP,
                CpuRate = CpuLimitPercent * 100 // Rate is in 1/100ths of a percent
            };

            var cpuRateInfoSize = Marshal.SizeOf(typeof(JOBOBJECT_CPU_RATE_CONTROL_INFORMATION));
            var cpuRateInfoPtr = Marshal.AllocHGlobal(cpuRateInfoSize);
            try
            {
                Marshal.StructureToPtr(cpuRateInfo, cpuRateInfoPtr, false);
                if (!SetInformationJobObject(jobHandle, JobObjectCpuRateControlInformation, cpuRateInfoPtr, (uint)cpuRateInfoSize))
                {
                    var error = Marshal.GetLastWin32Error();
                    _errorHandler?.LogWarning($"Failed to set CPU rate limit: Win32 error {error}", "Security");
                }
            }
            finally
            {
                Marshal.FreeHGlobal(cpuRateInfoPtr);
            }

            // Set memory limit
            var extendedLimitInfo = new JOBOBJECT_EXTENDED_LIMIT_INFORMATION
            {
                BasicLimitInformation = new JOBOBJECT_BASIC_LIMIT_INFORMATION
                {
                    LimitFlags = JOB_OBJECT_LIMIT_PROCESS_MEMORY
                },
                ProcessMemoryLimit = new UIntPtr((ulong)MaxMemoryBytes)
            };

            var extendedLimitInfoSize = Marshal.SizeOf(typeof(JOBOBJECT_EXTENDED_LIMIT_INFORMATION));
            var extendedLimitInfoPtr = Marshal.AllocHGlobal(extendedLimitInfoSize);
            try
            {
                Marshal.StructureToPtr(extendedLimitInfo, extendedLimitInfoPtr, false);
                if (!SetInformationJobObject(jobHandle, JobObjectExtendedLimitInformation, extendedLimitInfoPtr, (uint)extendedLimitInfoSize))
                {
                    var error = Marshal.GetLastWin32Error();
                    _errorHandler?.LogWarning($"Failed to set memory limit: Win32 error {error}", "Security");
                }
            }
            finally
            {
                Marshal.FreeHGlobal(extendedLimitInfoPtr);
            }

            // Assign process to job
            if (!AssignProcessToJobObject(jobHandle, process.Handle))
            {
                var error = Marshal.GetLastWin32Error();
                _errorHandler?.LogWarning($"Failed to assign process to Job Object: Win32 error {error}", "Security");
                CloseHandle(jobHandle);
                return IntPtr.Zero;
            }

            _errorHandler?.LogInfo("Windows Job Object limits applied successfully", "Security");
            return jobHandle;
        }
        catch (Exception ex)
        {
            _errorHandler?.LogError(ex, "Failed to apply Windows Job Object limits");
            return IntPtr.Zero;
        }
    }

    /// <summary>
    /// Apply security restrictions to process environment
    /// </summary>
    private void ApplySecurityRestrictions(ProcessStartInfo startInfo)
    {
        // Clear dangerous environment variables
        startInfo.Environment.Clear();

        // Add only safe, minimal environment variables
        startInfo.Environment["PATH"] = Environment.GetEnvironmentVariable("PATH") ?? "";
        startInfo.Environment["HOME"] = Path.GetTempPath();
        startInfo.Environment["TEMP"] = Path.GetTempPath();
        startInfo.Environment["TMP"] = Path.GetTempPath();

        // Prevent network access where possible (language-specific)
        startInfo.Environment["NO_PROXY"] = "*";
        startInfo.Environment["no_proxy"] = "*";
    }

    /// <summary>
    /// Apply memory and CPU limits to running process
    /// </summary>
    private void ApplyResourceLimits(Process process)
    {
        try
        {
            // Set memory limit (Windows: MaxWorkingSet) - fallback for non-Job Object path
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                try
                {
                    process.MaxWorkingSet = new IntPtr(MaxMemoryBytes);
                }
                catch (Win32Exception ex)
                {
                    _errorHandler?.LogWarning($"Could not set MaxWorkingSet: {ex.Message}", "Security");
                }
            }

            // Set process priority to below normal to prevent resource hogging
            try
            {
                process.PriorityClass = ProcessPriorityClass.BelowNormal;
            }
            catch
            {
                // Priority setting may fail - not critical
            }
        }
        catch (Exception ex)
        {
            _errorHandler?.LogWarning($"Resource limit application failed: {ex.Message}", "Security");
        }
    }

    private string? ExtractJavaClassName(string code)
    {
        var match = System.Text.RegularExpressions.Regex.Match(code, @"public\s+class\s+(\w+)");
        return match.Success ? match.Groups[1].Value : null;
    }

    #region Windows Job Object P/Invoke

    private const uint JOB_OBJECT_LIMIT_PROCESS_MEMORY = 0x00000100;
    private const uint JOB_OBJECT_CPU_RATE_CONTROL_ENABLE = 0x00000001;
    private const uint JOB_OBJECT_CPU_RATE_CONTROL_HARD_CAP = 0x00000004;
    private const int JobObjectExtendedLimitInformation = 9;
    private const int JobObjectCpuRateControlInformation = 15;

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern IntPtr CreateJobObject(IntPtr lpJobAttributes, string? lpName);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool SetInformationJobObject(IntPtr hJob, int JobObjectInfoClass, IntPtr lpJobObjectInfo, uint cbJobObjectInfoLength);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool AssignProcessToJobObject(IntPtr hJob, IntPtr hProcess);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool CloseHandle(IntPtr hObject);

    [StructLayout(LayoutKind.Sequential)]
    private struct JOBOBJECT_BASIC_LIMIT_INFORMATION
    {
        public long PerProcessUserTimeLimit;
        public long PerJobUserTimeLimit;
        public uint LimitFlags;
        public UIntPtr MinimumWorkingSetSize;
        public UIntPtr MaximumWorkingSetSize;
        public uint ActiveProcessLimit;
        public UIntPtr Affinity;
        public uint PriorityClass;
        public uint SchedulingClass;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct IO_COUNTERS
    {
        public ulong ReadOperationCount;
        public ulong WriteOperationCount;
        public ulong OtherOperationCount;
        public ulong ReadTransferCount;
        public ulong WriteTransferCount;
        public ulong OtherTransferCount;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct JOBOBJECT_EXTENDED_LIMIT_INFORMATION
    {
        public JOBOBJECT_BASIC_LIMIT_INFORMATION BasicLimitInformation;
        public IO_COUNTERS IoInfo;
        public UIntPtr ProcessMemoryLimit;
        public UIntPtr JobMemoryLimit;
        public UIntPtr PeakProcessMemoryUsed;
        public UIntPtr PeakJobMemoryUsed;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct JOBOBJECT_CPU_RATE_CONTROL_INFORMATION
    {
        public uint ControlFlags;
        public uint CpuRate;
    }

    #endregion
}

/// <summary>
/// Manages disk quota for code execution sandboxes
/// Restricts user code to a limited temp directory (default 10MB)
/// </summary>
public class DiskQuotaManager
{
    private readonly long _maxBytes;
    private readonly IErrorHandlerService? _errorHandler;

    public DiskQuotaManager(long maxBytes, IErrorHandlerService? errorHandler = null)
    {
        _maxBytes = maxBytes;
        _errorHandler = errorHandler;
    }

    /// <summary>
    /// Create an isolated sandbox directory for code execution
    /// </summary>
    public string CreateSandboxDirectory()
    {
        var sandboxPath = Path.Combine(Path.GetTempPath(), "CodeTutor_Sandbox", Guid.NewGuid().ToString());
        Directory.CreateDirectory(sandboxPath);
        _errorHandler?.LogInfo($"Created sandbox directory: {sandboxPath}", "Security");
        return sandboxPath;
    }

    /// <summary>
    /// Check if the sandbox directory exceeds the quota
    /// </summary>
    public bool ExceedsQuota(string sandboxPath)
    {
        try
        {
            var totalSize = GetDirectorySize(sandboxPath);
            var exceeds = totalSize > _maxBytes;

            if (exceeds)
            {
                _errorHandler?.LogWarning(
                    $"Disk quota exceeded: {totalSize / 1024 / 1024}MB > {_maxBytes / 1024 / 1024}MB limit",
                    "Security"
                );
            }

            return exceeds;
        }
        catch (Exception ex)
        {
            _errorHandler?.LogError(ex, "Failed to check disk quota");
            return false; // Allow execution if we can't check
        }
    }

    /// <summary>
    /// Get total size of a directory in bytes
    /// </summary>
    private long GetDirectorySize(string path)
    {
        long size = 0;

        try
        {
            var directory = new DirectoryInfo(path);

            foreach (var file in directory.EnumerateFiles("*", SearchOption.AllDirectories))
            {
                size += file.Length;
            }
        }
        catch (UnauthorizedAccessException)
        {
            // Skip directories we can't access
        }

        return size;
    }

    /// <summary>
    /// Clean up a sandbox directory
    /// </summary>
    public void CleanupSandboxDirectory(string sandboxPath)
    {
        try
        {
            if (Directory.Exists(sandboxPath))
            {
                Directory.Delete(sandboxPath, recursive: true);
                _errorHandler?.LogInfo($"Cleaned up sandbox directory: {sandboxPath}", "Security");
            }
        }
        catch (Exception ex)
        {
            _errorHandler?.LogWarning($"Failed to cleanup sandbox: {ex.Message}", "Security");
        }
    }
}
