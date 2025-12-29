using CodeTutor.Wpf.Models;

namespace CodeTutor.Wpf.Services;

public class TutorContext
{
    public string? CurrentLanguage { get; set; }
    public string? LessonTitle { get; set; }
    public string? LessonContent { get; set; }
    public string? UserCode { get; set; }
    public string? ExecutionError { get; set; }
    public string? ExpectedOutput { get; set; }
}

public interface ITutorService
{
    /// <summary>
    /// Whether the model is loaded and ready for inference
    /// </summary>
    bool IsModelLoaded { get; }

    /// <summary>
    /// Loading progress (0-100)
    /// </summary>
    int LoadingProgress { get; }

    /// <summary>
    /// Event fired when loading progress changes
    /// </summary>
    event EventHandler<int>? LoadingProgressChanged;

    /// <summary>
    /// Initialize and load the Phi-4 model
    /// </summary>
    Task LoadModelAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Send a message to the tutor and get a streaming response
    /// </summary>
    IAsyncEnumerable<string> SendMessageAsync(
        string userMessage,
        TutorContext context,
        IReadOnlyList<TutorMessage> history,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Unload the model to free memory
    /// </summary>
    void UnloadModel();
}
