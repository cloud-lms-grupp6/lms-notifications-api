using Lms.Notifications.Application.DTOs;
using Lms.Notifications.Application.Interfaces;
using Lms.Notifications.Domain.Entities;
using Lms.Notifications.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Lms.Notifications.Infrastructure.Services;

public class NotificationService : INotificationService
{
    private readonly NotificationDbContext _context;

    public NotificationService(NotificationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<NotificationResponse>> GetAllAsync()
    {
        return await _context.Notifications
            .Select(n => new NotificationResponse
            {
                Id = n.Id,
                UserId = n.UserId,
                Title = n.Title,
                Message = n.Message,
                IsRead = n.IsRead,
                CreatedAt = n.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<NotificationResponse?> GetByIdAsync(Guid id)
    {
        return await _context.Notifications
            .Where(n => n.Id == id)
            .Select(n => new NotificationResponse
            {
                Id = n.Id,
                UserId = n.UserId,
                Title = n.Title,
                Message = n.Message,
                IsRead = n.IsRead,
                CreatedAt = n.CreatedAt
            })
            .FirstOrDefaultAsync();
    }

    public async Task<NotificationResponse> CreateAsync(CreateNotificationRequest request)
    {
        var notification = new Notification
        {
            UserId = request.UserId,
            Title = request.Title,
            Message = request.Message
        };

        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync();

        return new NotificationResponse
        {
            Id = notification.Id,
            UserId = notification.UserId,
            Title = notification.Title,
            Message = notification.Message,
            IsRead = notification.IsRead,
            CreatedAt = notification.CreatedAt
        };
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateNotificationRequest request)
    {
        var notification = await _context.Notifications.FindAsync(id);

        if (notification is null)
            return false;

        notification.IsRead = request.IsRead;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var notification = await _context.Notifications.FindAsync(id);

        if (notification is null)
            return false;

        _context.Notifications.Remove(notification);
        await _context.SaveChangesAsync();

        return true;
    }
}