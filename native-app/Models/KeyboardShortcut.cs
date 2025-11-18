using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeTutor.Native.Models;

/// <summary>
/// Represents a custom keyboard shortcut
/// Entity model for Entity Framework Core
/// </summary>
[Table("KeyboardShortcuts")]
public class KeyboardShortcut
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;

    [Required]
    public string ActionName { get; set; } = string.Empty;

    [Required]
    public string KeyCombination { get; set; } = string.Empty;

    public bool IsEnabled { get; set; } = true;

    // Navigation property
    public User? User { get; set; }
}
