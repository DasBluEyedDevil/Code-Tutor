using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeTutor.Native.Models;

/// <summary>
/// Represents auto-saved code history for a challenge
/// Entity model for Entity Framework Core
/// </summary>
[Table("CodeHistory")]
public class CodeHistory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;

    [Required]
    public string CourseId { get; set; } = string.Empty;

    [Required]
    public string ModuleId { get; set; } = string.Empty;

    [Required]
    public string LessonId { get; set; } = string.Empty;

    [Required]
    public string ChallengeId { get; set; } = string.Empty;

    [Required]
    public string Code { get; set; } = string.Empty;

    [Required]
    public DateTime SavedAt { get; set; } = DateTime.UtcNow;

    // Navigation property
    public User? User { get; set; }
}
