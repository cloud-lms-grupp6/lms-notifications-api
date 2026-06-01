using Lms.Notifications.Application.DTOs;

namespace Lms.Notifications.Application.Interfaces;

public interface INotificationService
{
    Task<IEnumerable<NotificationResponse>> GetAllAsync();

    Task<NotificationResponse?> GetByIdAsync(Guid id);

    Task<NotificationResponse> CreateAsync(CreateNotificationRequest request);

    Task<bool> UpdateAsync(Guid id, UpdateNotificationRequest request);

    Task<bool> DeleteAsync(Guid id);
}