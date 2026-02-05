using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using CodeTutor.Wpf.Models;
using LLama;
using LLama.Common;

namespace CodeTutor.Wpf.Services;

/// <summary>
/// Tutor service implementation using LLamaSharp with GGUF models.
/// Supports Qwen2.5-Coder and other GGUF format models without ONNX conversion.
/// </summary>
public class LlamaTutorService : ITutorService, IDisposable
{
    private LLamaWeights? _model;
    private LLamaContext? _context;
    private ChatSession? _session;
    private readonly string _modelPath;
    private bool _disposed;

    public bool IsModelLoaded => _model != null && _context != null;
    public int LoadingProgress { get; private set; }
    public event EventHandler<int>? LoadingProgressChanged;

    public LlamaTutorService()
    {
        // Model path for GGUF format
        _modelPath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "models", "qwen2.5-coder-7b", "model.gguf");
    }

    public async Task LoadModelAsync(CancellationToken cancellationToken = default)
    {
        if (IsModelLoaded) return;

        await Task.Run(() =>
        {
            UpdateProgress(10);

            if (!File.Exists(_modelPath))
            {
                throw new FileNotFoundException(
                    "The AI Tutor model was not found. Please download it from the AI Tutor panel.");
            }

            UpdateProgress(30);

            // Load model parameters
            var parameters = new ModelParams(_modelPath)
            {
                ContextSize = 4096,  // Context window
                GpuLayerCount = 20,   // Offload 20 layers to GPU (adjust based on VRAM)
            };

            UpdateProgress(50);
            _model = LLamaWeights.LoadFromFile(parameters);

            UpdateProgress(70);
            _context = _model.CreateContext(parameters);

            // Create chat session with system prompt
            var executor = new InteractiveExecutor(_context);
            _session = new ChatSession(executor);
            
            // Add system message using AddSystemMessage
            _session.AddSystemMessage(
                "You are an expert coding tutor specializing in helping beginners learn programming. " +
                "Explain concepts clearly with code examples. Help debug errors step by step. " +
                "Be concise but thorough.");

            UpdateProgress(100);
        }, cancellationToken);
    }

    public async Task WarmUpAsync(CancellationToken cancellationToken = default)
    {
        if (!IsModelLoaded || _session == null) return;

        // Simple warm-up inference
        await Task.Run(() =>
        {
            try
            {
                var warmupResult = _session.ChatAsync(
                    new ChatHistory.Message(AuthorRole.User, "Hello"),
                    inferenceParams: new InferenceParams { MaxTokens = 1 });
                
                // Consume one token
                var enumerator = warmupResult.GetAsyncEnumerator();
                enumerator.MoveNextAsync().AsTask().Wait();
                enumerator.DisposeAsync().AsTask().Wait();
            }
            catch
            {
                // Warm-up failures are non-critical
            }
        }, cancellationToken);
    }

    public async IAsyncEnumerable<string> SendMessageAsync(
        string userMessage,
        TutorContext context,
        IReadOnlyList<TutorMessage> history,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        if (!IsModelLoaded || _session == null)
            throw new InvalidOperationException("Model not loaded. Call LoadModelAsync first.");

        // Build context-aware message
        var contextualMessage = BuildContextualMessage(userMessage, context);

        // Yield immediately to show "Thinking..."
        await Task.Yield();

        var inferenceParams = new InferenceParams
        {
            MaxTokens = 512,
            AntiPrompts = new[] { "<|im_end|>", "<|im_start|>", "User:", "Human:" },
        };

        // Add context from history if needed
        foreach (var msg in history.TakeLast(2))
        {
            if (msg.Role == MessageRole.User)
                _session.AddUserMessage(msg.Content);
            else
                _session.AddAssistantMessage(msg.Content);
        }

        // Generate response
        var result = _session.ChatAsync(
            new ChatHistory.Message(AuthorRole.User, contextualMessage),
            inferenceParams: inferenceParams);

        await using var enumerator = result.GetAsyncEnumerator(cancellationToken);
        while (await enumerator.MoveNextAsync())
        {
            yield return enumerator.Current;
        }
    }

    private string BuildContextualMessage(string userMessage, TutorContext context)
    {
        var sb = new StringBuilder();

        if (!string.IsNullOrEmpty(context.CurrentLanguage))
            sb.AppendLine($"[Language: {context.CurrentLanguage}]");
        if (!string.IsNullOrEmpty(context.LessonTitle))
            sb.AppendLine($"[Lesson: {context.LessonTitle}]");
        if (!string.IsNullOrEmpty(context.UserCode))
        {
            var codeLines = context.UserCode.Split('\n').Take(15);
            sb.AppendLine($"[Code:]\n```{context.CurrentLanguage?.ToLower() ?? ""}\n{string.Join("\n", codeLines)}\n```");
        }
        if (!string.IsNullOrEmpty(context.ExecutionError))
            sb.AppendLine($"[Error: {context.ExecutionError}]");

        sb.AppendLine(userMessage);

        return sb.ToString();
    }

    private void UpdateProgress(int progress)
    {
        LoadingProgress = progress;
        LoadingProgressChanged?.Invoke(this, progress);
    }

    public void UnloadModel()
    {
        // ChatSession doesn't need explicit disposal
        _session = null;
        _context?.Dispose();
        _context = null;
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
