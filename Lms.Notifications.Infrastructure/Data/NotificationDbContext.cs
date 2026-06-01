using Lms.Notifications.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lms.Notifications.Infrastructure.Data;

public class NotificationDbContext : DbContext
{
    public NotificationDbContext(DbContextOptions<NotificationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Notification> Notifications => Set<Notification>();
}