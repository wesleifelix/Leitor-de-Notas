using LeitorDeNotas.ClearArch.Application.Interfaces;
using LeitorDeNotas.ClearArch.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using LeitorDeNotas.ClearArch.WebApp.Hubs;

namespace LeitorDeNotas.ClearArch.WebApp.Controllers;

public class BatchController : Controller
{
    private readonly IBatchProcessingService _batchProcessingService;
    private readonly IHubContext<BatchHub> _hubContext;

    public BatchController(IBatchProcessingService batchProcessingService, IHubContext<BatchHub> hubContext)
    {
        _batchProcessingService = batchProcessingService;
        _hubContext = hubContext;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Processar([FromForm] string titulos)
    {
        var notas = titulos
            .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(t => Nota.Criar(t.Trim(), "Processamento em lote"))
            .ToArray();

        await _hubContext.Clients.All.SendAsync("ReceiveProgress", "Iniciando processamento em lote...");

        for (var i = 0; i < notas.Length; i++)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveProgress", $"Processando nota {i + 1} de {notas.Length}...");
            await _batchProcessingService.ProcessarNotasEmLoteAsync(new[] { notas[i] });
        }

        await _hubContext.Clients.All.SendAsync("ReceiveProgress", "Processamento finalizado.");

        return View("Index", notas);
    }
}
