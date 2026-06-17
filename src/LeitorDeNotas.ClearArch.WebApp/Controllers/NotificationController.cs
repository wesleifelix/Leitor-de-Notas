using LeitorDeNotas.ClearArch.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LeitorDeNotas.ClearArch.WebApp.Controllers;

public class NotificationController : Controller
{
    private readonly INotificationService _notificationService;

    public NotificationController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public IActionResult Index()
    {
        var notification = _notificationService.GetNotificationMessage();
        return View(notification);
    }
}
