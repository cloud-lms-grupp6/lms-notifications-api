using Lms.Notifications.Application.DTOs;
using Lms.Notifications.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lms.Notifications.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationsController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<NotificationResponse>>> GetAll()
    {
        var notifications = await _notificationService.GetAllAsync();
        return Ok(notifications);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<NotificationResponse>> GetById(Guid id)
    {
        var notification = await _notificationService.GetByIdAsync(id);

        if (notification is null)
            return NotFound();

        return Ok(notification);
    }

    [HttpPost]
    public async Task<ActionResult<NotificationResponse>> Create(CreateNotificationRequest request)
    {
        var notification = await _notificationService.CreateAsync(request);

        return CreatedAtAction(nameof(GetById), new { id = notification.Id }, notification);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateNotificationRequest request)
    {
        var updated = await _notificationService.UpdateAsync(id, request);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _notificationService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}