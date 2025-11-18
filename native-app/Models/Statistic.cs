using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeTutor.Native.Models;

/// <summary>
/// Represents aggregate statistics for a user
/// Entity model for Entity Framework Core
/// </summary>
[Table("Statistics")]
public class Statistic
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;

    [Required]
    public string StatName { get; set; } = string.Empty;

    public int StatValue { get; set; }

    [Required]
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

    // Navigation property
    public User? User { get; set; }
}
