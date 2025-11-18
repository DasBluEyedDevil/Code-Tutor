using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeTutor.Native.Models;

/// <summary>
/// Represents an achievement unlocked by a user
/// Entity model for Entity Framework Core
/// </summary>
[Table("Achievements")]
public class Achievement
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;

    [Required]
    public string AchievementType { get; set; } = string.Empty;

    [Required]
    public DateTime UnlockedAt { get; set; } = DateTime.UtcNow;

    public int Progress { get; set; }

    public int MaxProgress { get; set; } = 1;

    public bool Notified { get; set; }

    // Navigation property
    public User? User { get; set; }
}
