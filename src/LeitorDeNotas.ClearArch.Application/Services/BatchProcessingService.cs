using LeitorDeNotas.ClearArch.Application.Interfaces;
using LeitorDeNotas.ClearArch.Commons;
using LeitorDeNotas.ClearArch.Domain.Entities;
using LeitorDeNotas.ClearArch.Domain.Interfaces;

namespace LeitorDeNotas.ClearArch.Application.Services;

public sealed class BatchProcessingService : IBatchProcessingService
{
    private readonly INotaRepository _notaRepository;

    public BatchProcessingService(INotaRepository notaRepository)
    {
        _notaRepository = notaRepository;
    }

    public async Task<OperationResult<IEnumerable<Nota>>> ProcessarNotasEmLoteAsync(IEnumerable<Nota> notas, CancellationToken cancellationToken = default)
    {
        var adicionado = new List<Nota>();
        foreach (var nota in notas)
        {
            await _notaRepository.AdicionarAsync(nota, cancellationToken);
            adicionado.Add(nota);
        }

        return OperationResult<IEnumerable<Nota>>.Ok(adicionado, "Processamento em lote concluído com sucesso.");
    }
}
