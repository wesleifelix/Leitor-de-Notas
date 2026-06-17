using LeitorDeNotas.ClearArch.Commons;
using LeitorDeNotas.ClearArch.Domain.Entities;

namespace LeitorDeNotas.ClearArch.Application.Interfaces;

public interface INotaService
{
    Task<OperationResult<IEnumerable<Nota>>> ObterTodasAsync(CancellationToken cancellationToken = default);
}
