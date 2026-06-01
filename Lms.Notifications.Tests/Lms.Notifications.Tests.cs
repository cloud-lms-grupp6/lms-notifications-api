using Lms.Notifications.Application.DTOs;
using Lms.Notifications.Infrastructure.Data;
using Lms.Notifications.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Lms.Notifications.Tests;

public class NotificationServiceTests
{
    private NotificationDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<NotificationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new NotificationDbContext(options);
    }

    [Fact]
    public async Task CreateAsync_Should_Create_Notification()
    {
        var context = CreateDbContext();
        var service = new NotificationService(context);

        var request = new CreateNotificationRequest
        {
            UserId = "user-1",
            Title = "Test title",
            Message = "Test message"
        };

        var result = await service.CreateAsync(request);

        Assert.NotNull(result);
        Assert.Equal("Test title", result.Title);
        Assert.Equal("Test message", result.Message);
        Assert.False(result.IsRead);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Notification_When_Exists()
    {
        var context = CreateDbContext();
        var service = new NotificationService(context);

        var created = await service.CreateAsync(new CreateNotificationRequest
        {
            UserId = "user-2",
            Title = "Hello",
            Message = "World"
        });

        var result = await service.GetByIdAsync(created.Id);

        Assert.NotNull(result);
        Assert.Equal(created.Id, result.Id);
        Assert.Equal("Hello", result.Title);
    }

    [Fact]
    public async Task UpdateAsync_Should_Update_IsRead_When_Notification_Exists()
    {
        var context = CreateDbContext();
        var service = new NotificationService(context);

        var created = await service.CreateAsync(new CreateNotificationRequest
        {
            UserId = "user-3",
            Title = "Unread",
            Message = "This should be marked as read"
        });

        var updateRequest = new UpdateNotificationRequest
        {
            IsRead = true
        };

        var updated = await service.UpdateAsync(created.Id, updateRequest);
        var result = await service.GetByIdAsync(created.Id);

        Assert.True(updated);
        Assert.NotNull(result);
        Assert.True(result.IsRead);
    }

    [Fact]
    public async Task DeleteAsync_Should_Delete_Notification_When_Exists()
    {
        var context = CreateDbContext();
        var service = new NotificationService(context);

        var created = await service.CreateAsync(new CreateNotificationRequest
        {
            UserId = "user-4",
            Title = "Delete",
            Message = "Delete this notification"
        });

        var deleted = await service.DeleteAsync(created.Id);
        var result = await service.GetByIdAsync(created.Id);

        Assert.True(deleted);
        Assert.Null(result);
    }
}