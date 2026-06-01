using System.ComponentModel.DataAnnotations;

namespace Lms.Notifications.Application.DTOs;

public class CreateNotificationRequest
{
    [Required]
    public string UserId { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(1000)]
    public string Message { get; set; } = string.Empty;
}