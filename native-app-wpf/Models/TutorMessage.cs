namespace CodeTutor.Wpf.Models;

public enum MessageRole
{
    User,
    Assistant,
    System
}

public class TutorMessage
{
    public MessageRole Role { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.Now;

    public TutorMessage() { }

    public TutorMessage(MessageRole role, string content)
    {
        Role = role;
        Content = content;
    }
}
