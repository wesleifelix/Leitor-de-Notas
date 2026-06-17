using LeitorDeNotas.ClearArch.Commons;
using LeitorDeNotas.ClearArch.Domain.Entities;

namespace LeitorDeNotas.ClearArch.Application.Interfaces;

public interface IBatchProcessingService
{
    Task<OperationResult<IEnumerable<Nota>>> ProcessarNotasEmLoteAsync(IEnumerable<Nota> notas, CancellationToken cancellationToken = default);
}
