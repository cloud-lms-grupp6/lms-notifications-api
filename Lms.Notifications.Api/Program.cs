using Lms.Notifications.Application.Interfaces;
using Lms.Notifications.Infrastructure.Data;
using Lms.Notifications.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

// Program.cs startar och konfigurerar Notifications API.
// Här registreras databas, services, controllers och Swagger.
//
// AI användes som stöd för att strukturera dependency injection,
// databasanslutning och API-dokumentation.
// Koden anpassades därefter manuellt efter LMS-projektets struktur.

var builder = WebApplication.CreateBuilder(args);

// Aktiverar controllers så att API-endpoints kan användas.
builder.Services.AddControllers();

// Kopplar Notifications API till SQL Server via Entity Framework Core.
builder.Services.AddDbContext<NotificationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrerar NotificationService i dependency injection.
// Detta gör att NotificationsController kan använda INotificationService.
builder.Services.AddScoped<INotificationService, NotificationService>();

// Lägger till Swagger/OpenAPI så API:t kan testas i webbläsaren.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Aktiverar Swagger UI.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// Förbereder API:t för authorization om det kopplas på senare.
app.UseAuthorization();

// Mappar alla controllers, till exempel NotificationsController.
app.MapControllers();

app.Run();