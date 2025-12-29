using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using CodeTutor.Wpf.Models;
using Microsoft.ML.OnnxRuntimeGenAI;

namespace CodeTutor.Wpf.Services;

public class Phi4TutorService : ITutorService, IDisposable
{
    private Model? _model;
    private Tokenizer? _tokenizer;
    private readonly string _modelPath;
    private bool _disposed;

    public bool IsModelLoaded => _model != null && _tokenizer != null;
    public int LoadingProgress { get; private set; }
    public event EventHandler<int>? LoadingProgressChanged;

    public Phi4TutorService()
    {
        // Model path relative to application directory
        _modelPath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "models", "phi4", "gpu", "gpu-int4-rtn-block-32");
    }

    public async Task LoadModelAsync(CancellationToken cancellationToken = default)
    {
        if (IsModelLoaded) return;

        await Task.Run(() =>
        {
            UpdateProgress(10);

            if (!Directory.Exists(_modelPath))
            {
                throw new DirectoryNotFoundException(
                    $"Phi-4 model not found at: {_modelPath}. " +
                    "Please download the model using: huggingface-cli download microsoft/Phi-4-mini-instruct-onnx " +
                    "--include gpu/gpu-int4-rtn-block-32/* --local-dir models/phi4");
            }

            UpdateProgress(30);
            _model = new Model(_modelPath);

            UpdateProgress(70);
            _tokenizer = new Tokenizer(_model);

            UpdateProgress(100);
        }, cancellationToken);
    }

    public async IAsyncEnumerable<string> SendMessageAsync(
        string userMessage,
        TutorContext context,
        IReadOnlyList<TutorMessage> history,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        if (!IsModelLoaded)
            throw new InvalidOperationException("Model not loaded. Call LoadModelAsync first.");

        var prompt = BuildPrompt(userMessage, context, history);

        using var tokens = _tokenizer!.Encode(prompt);

        using var generatorParams = new GeneratorParams(_model!);
        generatorParams.SetSearchOption("max_length", 2048);
        generatorParams.SetSearchOption("temperature", 0.7);
        generatorParams.SetSearchOption("top_p", 0.9);
        generatorParams.SetInputSequences(tokens);

        using var generator = new Generator(_model!, generatorParams);

        while (!generator.IsDone())
        {
            cancellationToken.ThrowIfCancellationRequested();

            generator.ComputeLogits();
            generator.GenerateNextToken();

            var tokenText = GetLastGeneratedToken(generator);

            // Skip special tokens
            if (!tokenText.Contains("<|") && !tokenText.Contains("|>"))
            {
                yield return tokenText;
            }

            await Task.Yield(); // Allow UI updates
        }
    }

    private string GetLastGeneratedToken(Generator generator)
    {
        var outputTokens = generator.GetSequence(0);
        var newToken = outputTokens[^1];
        return _tokenizer!.Decode(new ReadOnlySpan<int>(new[] { newToken }));
    }

    private string BuildPrompt(string userMessage, TutorContext context, IReadOnlyList<TutorMessage> history)
    {
        var sb = new StringBuilder();

        // System prompt
        sb.AppendLine("<|system|>");
        sb.AppendLine("You are a friendly and knowledgeable programming tutor helping students learn to code.");
        sb.AppendLine("Guidelines:");
        sb.AppendLine("- Give clear, concise explanations suitable for beginners");
        sb.AppendLine("- Use examples when helpful, but keep them short");
        sb.AppendLine("- If the student has an error, explain what went wrong and guide them to fix it");
        sb.AppendLine("- Encourage good coding practices");
        sb.AppendLine("- Be supportive and patient");
        sb.AppendLine("- Keep responses focused and under 200 words unless more detail is needed");

        // Add context if available
        if (!string.IsNullOrEmpty(context.CurrentLanguage))
        {
            sb.AppendLine($"\nCurrent programming language: {context.CurrentLanguage}");
        }
        if (!string.IsNullOrEmpty(context.LessonTitle))
        {
            sb.AppendLine($"Current lesson: {context.LessonTitle}");
        }
        if (!string.IsNullOrEmpty(context.UserCode))
        {
            sb.AppendLine($"\nStudent's current code:\n```{context.CurrentLanguage?.ToLower() ?? ""}\n{context.UserCode}\n```");
        }
        if (!string.IsNullOrEmpty(context.ExecutionError))
        {
            sb.AppendLine($"\nExecution error:\n{context.ExecutionError}");
        }
        sb.AppendLine("<|end|>");

        // Add conversation history (last 6 messages to stay within context)
        var recentHistory = history.TakeLast(6);
        foreach (var msg in recentHistory)
        {
            var role = msg.Role == MessageRole.User ? "user" : "assistant";
            sb.AppendLine($"<|{role}|>");
            sb.AppendLine(msg.Content);
            sb.AppendLine("<|end|>");
        }

        // Add current user message
        sb.AppendLine("<|user|>");
        sb.AppendLine(userMessage);
        sb.AppendLine("<|end|>");
        sb.AppendLine("<|assistant|>");

        return sb.ToString();
    }

    private void UpdateProgress(int progress)
    {
        LoadingProgress = progress;
        LoadingProgressChanged?.Invoke(this, progress);
    }

    public void UnloadModel()
    {
        _tokenizer?.Dispose();
        _tokenizer = null;
        _model?.Dispose();
        _model = null;
        LoadingProgress = 0;
    }

    public void Dispose()
    {
        if (_disposed) return;
        UnloadModel();
        _disposed = true;
    }
}
