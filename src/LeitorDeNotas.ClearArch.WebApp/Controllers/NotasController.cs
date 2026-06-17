using LeitorDeNotas.ClearArch.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LeitorDeNotas.ClearArch.WebApp.Controllers;

public class NotasController : Controller
{
    private readonly INotaService _notaService;

    public NotasController(INotaService notaService)
    {
        _notaService = notaService;
    }

    public async Task<IActionResult> Index()
    {
        var result = await _notaService.ObterTodasAsync();
        if (!result.Success)
        {
            return View("Error", result);
        }

        return View(result.Data);
    }
}
