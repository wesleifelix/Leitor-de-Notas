using LeitorDeNotas.ClearArch.Application.Interfaces;
using LeitorDeNotas.ClearArch.Commons;

namespace LeitorDeNotas.ClearArch.Application.Services;

public sealed class NotificationService : INotificationService
{
    public OperationResult<string> GetNotificationMessage()
    {
        return OperationResult<string>.Ok("Mensagem de notificação gerada pelo serviço Transient.");
    }
}
