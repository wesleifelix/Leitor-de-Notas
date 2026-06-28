using LeitorDeNotas.ClearArch.Application.Interfaces;
using LeitorDeNotas.ClearArch.Domain.Entities;
using LeitorDeNotas.ClearArch.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LeitorDeNotas.ClearArch.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotaFiscalController : ControllerBase
{
    private readonly INotaFiscalXmlParser _xmlParser;
    private readonly INotaFiscalRepository _notaFiscalRepository;

    public NotaFiscalController(INotaFiscalXmlParser xmlParser, INotaFiscalRepository notaFiscalRepository)
    {
        _xmlParser = xmlParser;
        _notaFiscalRepository = notaFiscalRepository;
    }

    [HttpPost("importar")]
    public async Task<IActionResult> Importar([FromBody] string xml)
    {
        if (string.IsNullOrWhiteSpace(xml))
            return BadRequest("XML de nota fiscal não informado.");

        var notaFiscal = _xmlParser.Parse(xml);
        await _notaFiscalRepository.AdicionarAsync(notaFiscal);

        return CreatedAtAction(nameof(ObterPorPeriodo), new { dataInicial = notaFiscal.DataEmissao.Date, dataFinal = notaFiscal.DataEmissao.Date }, notaFiscal);
    }

    [HttpGet("periodo")]
    public async Task<IActionResult> ObterPorPeriodo(DateTime dataInicial, DateTime dataFinal)
    {
        var notas = await _notaFiscalRepository.ObterPorPeriodoAsync(dataInicial, dataFinal);
        return Ok(notas);
    }
}
