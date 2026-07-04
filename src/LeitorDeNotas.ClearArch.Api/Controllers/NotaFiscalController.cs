using LeitorDeNotas.ClearArch.Application.Interfaces;
using LeitorDeNotas.ClearArch.Domain.Entities;
using LeitorDeNotas.ClearArch.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.IO.Compression;

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
    public async Task<IActionResult> Importar([FromBody] System.Xml.Linq.XElement xml)
    {
        if (string.IsNullOrWhiteSpace(xml.ToString()))
            return BadRequest("XML de nota fiscal não informado.");

        var notaFiscal = _xmlParser.Parse(xml.Value.ToString());
        await _notaFiscalRepository.AdicionarAsync(notaFiscal);

        return CreatedAtAction(nameof(ObterPorPeriodo), new { dataInicial = notaFiscal.DataEmissao.Date, dataFinal = notaFiscal.DataEmissao.Date }, notaFiscal);
    }

    [HttpPost("arquivos")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> ImportarArquivo( IFormFile files) 
    {
        if (files == null)
            return BadRequest("XML de nota fiscal não informado.");

        var fileExt = files.ContentType;

        if (files.ContentType == "application/zip" || files.FileName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
        {
            using (var stream = files.OpenReadStream())
            using (var archive = new ZipArchive(stream))
            {
                var lsNotas = new List<NotaFiscal>();
                foreach (var entry in archive.Entries)
                {
                    // Ignora pastas dentro do zip, foca apenas em arquivos XML
                    if (entry.FullName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                    {
                        using (var entryStream = entry.Open())
                        using (var reader = new StreamReader(entryStream))
                        {
                            var xmlConteudo = await reader.ReadToEndAsync();
                            var notaFiscal = _xmlParser.Parse(xmlConteudo);
                            if (notaFiscal != null)
                                lsNotas.Add(notaFiscal);
                        }
                    }
                }

                return Created(string.Empty, lsNotas);
            }
        }
        else if (files.ContentType == "text/xml" ||
                 files.ContentType == "application/xml" ||
                 files.FileName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
        {
            using (var stream = files.OpenReadStream())
            using (var reader = new StreamReader(stream)) // Corrigido: Removido o .Open() que não existia
            {
                var xmlConteudo = await reader.ReadToEndAsync();

                var notaFiscal = _xmlParser.Parse(xmlConteudo); // Corrigido de 'xml' para 'xmlConteudo'
                //await _notaFiscalRepository.AdicionarAsync(notaFiscal);
                //notasProcessadas.Add(notaFiscal);
                return Created(string.Empty,notaFiscal);
            }
        }
        else
        {
            return BadRequest($"O formato do arquivo '{files.FileName}' não é suportado. Envie apenas .xml ou .zip.");
        }

        return BadRequest();
        //return CreatedAtAction(nameof(ObterPorPeriodo), new { dataInicial = notaFiscal.DataEmissao.Date, dataFinal = notaFiscal.DataEmissao.Date }, notaFiscal);
    }

    [HttpGet("periodo")]
    public async Task<IActionResult> ObterPorPeriodo(DateTime dataInicial, DateTime dataFinal)
    {
        var notas = await _notaFiscalRepository.ObterPorPeriodoAsync(dataInicial, dataFinal);
        return Ok(notas);
    }
}
