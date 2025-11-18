using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeTutor.Native.Models;

/// <summary>
/// Represents daily learning activity for streak tracking
/// Entity model for Entity Framework Core
/// </summary>
[Table("Streaks")]
public class Streak
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;

    [Required]
    public DateTime Date { get; set; }

    public int LessonsCompleted { get; set; }

    public int ChallengesCompleted { get; set; }

    public int MinutesSpent { get; set; }

    // Navigation property
    public User? User { get; set; }
}
