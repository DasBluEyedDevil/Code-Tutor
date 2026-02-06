using System.ComponentModel;

namespace CodeTutor.Wpf.Models;

public enum MessageRole
{
    User,
    Assistant,
    System
}

public class TutorMessage : INotifyPropertyChanged
{
    private string _content = string.Empty;

    public MessageRole Role { get; set; }

    public string Content
    {
        get => _content;
        set
        {
            if (_content != value)
            {
                _content = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Content)));
            }
        }
    }

    public DateTime Timestamp { get; set; } = DateTime.Now;

    public event PropertyChangedEventHandler? PropertyChanged;

    public TutorMessage() { }

    public TutorMessage(MessageRole role, string content)
    {
        Role = role;
        _content = content;
    }
}
