# LMS Notifications API

## Översikt

Notifications API är en mikrotjänst i Learning Management System (LMS) som ansvarar för att hantera notifikationer för användare.

Tjänsten gör det möjligt att skapa, hämta, uppdatera och ta bort notifikationer samt fungerar som grund för framtida integration med EmailService.

Projektet är utvecklat med ASP.NET Core Web API och följer principerna för Clean Architecture.

---

# Funktionalitet

API:t stödjer:

* Skapa notifikationer
* Hämta alla notifikationer
* Hämta notifikation via ID
* Markera notifikation som läst
* Ta bort notifikationer
* DTO-validering
* Swagger-dokumentation
* Enhetstester

---

# Projektstruktur

## Lms.Notifications.Domain

Innehåller domänmodeller och affärslogik.

## Lms.Notifications.Application

Innehåller DTO:er, interfaces och kontrakt.

## Lms.Notifications.Infrastructure

Innehåller Entity Framework Core, DbContext och implementationer.

## Lms.Notifications.Api

Innehåller controllers, konfiguration och Swagger.

## Lms.Notifications.Tests

Innehåller enhetstester för NotificationService.

---

# Teknik

* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* Swagger/OpenAPI
* xUnit
* Dependency Injection
* Clean Architecture

---

# API Endpoints

## Hämta alla notifikationer

```http
GET /api/notifications
```

## Hämta notifikation via ID

```http
GET /api/notifications/{id}
```

## Skapa notifikation

```http
POST /api/notifications
```

Exempel:

```json
{
  "userId": "user-1",
  "title": "Ny kurs",
  "message": "Du har blivit registrerad på kursen."
}
```

## Uppdatera notifikation

```http
PUT /api/notifications/{id}
```

Exempel:

```json
{
  "isRead": true
}
```

## Ta bort notifikation

```http
DELETE /api/notifications/{id}
```

---

# Databas

Notifications API använder Entity Framework Core tillsammans med SQL Server.

Databasen hanteras med EF Core Migrationer.

Skapa migration:

```bash
dotnet ef migrations add InitialCreate -p Lms.Notifications.Infrastructure -s Lms.Notifications.Api
```

Uppdatera databasen:

```bash
dotnet ef database update -p Lms.Notifications.Infrastructure -s Lms.Notifications.Api
```

---

# Testning

Projektet innehåller enhetstester för NotificationService.

Följande funktioner testas:

* CreateAsync
* GetByIdAsync
* UpdateAsync
* DeleteAsync

Kör tester:

```bash
dotnet test
```

---

# Starta projektet

Bygg projektet:

```bash
dotnet build
```

Starta API:t:

```bash
dotnet run --project Lms.Notifications.Api
```

Swagger:

```text
http://localhost:xxxx/swagger
```

---

# Framtida utveckling

Planerad integration med EmailService för att automatiskt kunna skicka e-post när nya notifikationer skapas.

---

# Utvecklare

Utvecklad som en del av EC Utbildnings Learning Management System (LMS).
