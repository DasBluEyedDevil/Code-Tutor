using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace CodeTutor.Wpf.Services.Executors;

public class RoslynCSharpExecutor
{
    private static readonly ScriptOptions DefaultOptions = ScriptOptions.Default
        .AddReferences(
            typeof(object).Assembly,
            typeof(Console).Assembly,
            typeof(Enumerable).Assembly
        )
        .AddImports(
            "System",
            "System.Collections.Generic",
            "System.Linq",
            "System.Text"
        );

    public async Task<ExecutionResult> ExecuteAsync(string code, CancellationToken cancellationToken = default)
    {
        using var outputCapture = new StringWriter();
        var originalOut = Console.Out;

        try
        {
            Console.SetOut(outputCapture);

            using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            cts.CancelAfter(TimeSpan.FromSeconds(30));

            var result = await CSharpScript.EvaluateAsync(
                code,
                DefaultOptions,
                cancellationToken: cts.Token
            );

            var output = outputCapture.ToString();

            // If script returns a value and no console output, show the value
            if (result != null && string.IsNullOrWhiteSpace(output))
            {
                output = result.ToString() ?? "";
            }

            return new ExecutionResult(true, output.Trim(), "");
        }
        catch (CompilationErrorException ex)
        {
            return new ExecutionResult(false, "", string.Join(Environment.NewLine, ex.Diagnostics));
        }
        catch (OperationCanceledException)
        {
            return new ExecutionResult(false, "", "Execution timed out after 30 seconds");
        }
        catch (Exception ex)
        {
            return new ExecutionResult(false, "", ex.Message);
        }
        finally
        {
            Console.SetOut(originalOut);
        }
    }
}
