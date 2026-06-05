using Lms.Notifications.Application.DTOs;
using Lms.Notifications.Application.Interfaces;
using Lms.Notifications.Domain.Entities;
using Lms.Notifications.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

// NotificationService innehåller affärslogiken för notifikationer.
// Här hanteras skapande, hämtning, uppdatering och borttagning av notiser.
//
// AI användes som stöd för att strukturera service-lagret och EF Core-frågor.
// Implementationen anpassades därefter manuellt efter projektets DTO:er,
// databasmodell och LMS-projektets krav.

namespace Lms.Notifications.Infrastructure.Services;

public class NotificationService : INotificationService
{
    private readonly NotificationDbContext _context;

    public NotificationService(NotificationDbContext context)
    {
        _context = context;
    }

    // Hämtar samtliga notifikationer från databasen.
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

    // Hämtar en specifik notifikation baserat på dess ID.
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

    // Skapar en ny notifikation och sparar den i databasen.
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

        // Returnerar den skapade notifikationen som DTO.
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

    // Uppdaterar status på en notifikation, exempelvis om den har lästs.
    public async Task<bool> UpdateAsync(Guid id, UpdateNotificationRequest request)
    {
        var notification = await _context.Notifications.FindAsync(id);

        if (notification is null)
            return false;

        notification.IsRead = request.IsRead;

        await _context.SaveChangesAsync();

        return true;
    }

    // Tar bort en notifikation från databasen.
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