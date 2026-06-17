using LeitorDeNotas.ClearArch.Commons;

namespace LeitorDeNotas.ClearArch.Application.Interfaces;

public interface INotificationService
{
    OperationResult<string> GetNotificationMessage();
}
