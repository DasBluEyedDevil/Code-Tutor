using System.Threading.Tasks;
using CodeTutor.Native.Models;

namespace CodeTutor.Native.Services;

/// <summary>
/// Service for executing code in various programming languages
/// </summary>
public interface ICodeExecutor
{
    /// <summary>
    /// Execute code in the specified language and return the result
    /// </summary>
    /// <param name="language">Programming language (python, javascript, etc.)</param>
    /// <param name="code">Code to execute</param>
    /// <returns>Execution result with output or error</returns>
    Task<ExecutionResult> ExecuteAsync(string language, string code);
}
