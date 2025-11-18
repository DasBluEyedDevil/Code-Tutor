using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeTutor.Native.Models;

/// <summary>
/// Represents a user of the application
/// Entity model for Entity Framework Core
/// </summary>
[Table("Users")]
public class User
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    public string Name { get; set; } = string.Empty;

    public string? Email { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    public DateTime LastLoginAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public ICollection<UserProgress> Progress { get; set; } = new List<UserProgress>();
    public ICollection<Achievement> Achievements { get; set; } = new List<Achievement>();
    public ICollection<Streak> Streaks { get; set; } = new List<Streak>();
    public ICollection<CodeHistory> CodeHistory { get; set; } = new List<CodeHistory>();
    public ICollection<KeyboardShortcut> KeyboardShortcuts { get; set; } = new List<KeyboardShortcut>();
    public ICollection<Statistic> Statistics { get; set; } = new List<Statistic>();
}
