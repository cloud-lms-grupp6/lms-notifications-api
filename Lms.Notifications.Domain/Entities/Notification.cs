namespace Lms.Notifications.Domain.Entities;

// Notification representerar en notifikation i LMS-systemet.
// Klassen används av Entity Framework Core för att skapa och hantera
// tabellen Notifications i databasen.
//
// AI användes som stöd för att förstå entity-strukturen och hur data
// lagras i databasen. Modellen anpassades därefter manuellt efter
// projektets krav.

public class Notification
{
    // Primärnyckel för notifikationen.
    public Guid Id { get; set; } = Guid.NewGuid();

    // Identifierar vilken användare som ska få notifikationen.
    public string UserId { get; set; } = string.Empty;

    // Rubriken som visas i notisen.
    public string Title { get; set; } = string.Empty;

    // Själva meddelandet som visas för användaren.
    public string Message { get; set; } = string.Empty;

    // Anger om användaren har läst notifikationen eller inte.
    public bool IsRead { get; set; }

    // Tidpunkt då notifikationen skapades.
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}