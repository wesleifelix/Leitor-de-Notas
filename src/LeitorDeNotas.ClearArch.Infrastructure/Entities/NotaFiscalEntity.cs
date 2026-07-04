using LeitorDeNotas.ClearArch.Domain.Entities;

namespace LeitorDeNotas.ClearArch.Infrastructure.Entities;

public sealed class NotaFiscalEntity
{
    public Guid Id { get; set; }
    public string ChaveAcesso { get; set; } = string.Empty;
    public string Serie { get; set; } = string.Empty;
    public DateTime DataEmissao { get; set; }
    public int Tipo { get; set; } = 0;
    public decimal ValorTotal { get; set; }
    public decimal EstimativaImposto { get; set; }
    public decimal EstimativaLucroPrejuizo { get; set; }
    public ICollection<NotaFiscalItemEntity> Itens { get; set; } = new List<NotaFiscalItemEntity>();
}
