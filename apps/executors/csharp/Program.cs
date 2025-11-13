using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System.Diagnostics;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseCors(policy => policy
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.MapGet("/health", () => Results.Json(new
{
    status = "ok",
    service = "csharp-executor"
}));

app.MapPost("/execute", async (HttpRequest request) =>
{
    using var reader = new StreamReader(request.Body);
    var body = await reader.ReadToEndAsync();
    var json = System.Text.Json.JsonDocument.Parse(body);

    if (!json.RootElement.TryGetProperty("code", out var codeElement))
    {
        return Results.BadRequest(new
        {
            success = false,
            error = "No code provided"
        });
    }

    string code = codeElement.GetString() ?? "";
    if (string.IsNullOrEmpty(code))
    {
        return Results.BadRequest(new
        {
            success = false,
            error = "No code provided"
        });
    }

    var result = await ExecuteCSharpCode(code);
    return Results.Json(result);
});

app.Run("http://0.0.0.0:4004");

static async Task<object> ExecuteCSharpCode(string code)
{
    var startTime = DateTime.Now;

    try
    {
        // Wrap code in a class if needed
        if (!code.Contains("class ") && !code.Contains("namespace "))
        {
            code = @"
using System;
using System.Linq;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
" + code + @"
    }
}";
        }

        // Parse the code
        var syntaxTree = CSharpSyntaxTree.ParseText(code);

        // Add references
        var references = new[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(Console).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(System.Linq.Enumerable).Assembly.Location),
            MetadataReference.CreateFromFile(Assembly.Load("System.Runtime").Location),
            MetadataReference.CreateFromFile(Assembly.Load("System.Collections").Location),
            MetadataReference.CreateFromFile(Assembly.Load("System.Console").Location),
        };

        // Compile
        var compilation = CSharpCompilation.Create(
            "DynamicAssembly",
            syntaxTrees: new[] { syntaxTree },
            references: references,
            options: new CSharpCompilationOptions(OutputKind.ConsoleApplication)
        );

        using var ms = new MemoryStream();
        EmitResult emitResult = compilation.Emit(ms);

        if (!emitResult.Success)
        {
            var errors = string.Join("\n", emitResult.Diagnostics
                .Where(d => d.Severity == DiagnosticSeverity.Error)
                .Select(d => d.GetMessage()));

            return new
            {
                success = false,
                output = "",
                error = $"Compilation error:\n{errors}",
                executionTime = (DateTime.Now - startTime).TotalMilliseconds
            };
        }

        // Execute
        ms.Seek(0, SeekOrigin.Begin);
        var assembly = Assembly.Load(ms.ToArray());

        var outputWriter = new StringWriter();
        var errorWriter = new StringWriter();

        Console.SetOut(outputWriter);
        Console.SetError(errorWriter);

        var entryPoint = assembly.EntryPoint;
        if (entryPoint == null)
        {
            return new
            {
                success = false,
                output = "",
                error = "No entry point (Main method) found",
                executionTime = (DateTime.Now - startTime).TotalMilliseconds
            };
        }

        // Execute with timeout
        var cts = new CancellationTokenSource();
        cts.CancelAfter(TimeSpan.FromSeconds(5));

        try
        {
            await Task.Run(() =>
            {
                entryPoint.Invoke(null, entryPoint.GetParameters().Length == 0 ? null : new object[] { Array.Empty<string>() });
            }, cts.Token);
        }
        catch (OperationCanceledException)
        {
            return new
            {
                success = false,
                output = outputWriter.ToString(),
                error = "Execution timed out after 5 seconds",
                executionTime = (DateTime.Now - startTime).TotalMilliseconds
            };
        }

        var output = outputWriter.ToString();
        var error = errorWriter.ToString();

        return new
        {
            success = true,
            output = string.IsNullOrEmpty(output) ? "(No output)" : output,
            error = string.IsNullOrEmpty(error) ? null : error,
            executionTime = (DateTime.Now - startTime).TotalMilliseconds
        };
    }
    catch (Exception ex)
    {
        return new
        {
            success = false,
            output = "",
            error = $"Execution error: {ex.Message}",
            executionTime = (DateTime.Now - startTime).TotalMilliseconds
        };
    }
}

Console.WriteLine("🟩 C# executor service running on port 4004...");
