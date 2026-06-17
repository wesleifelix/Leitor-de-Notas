namespace LeitorDeNotas.ClearArch.Infrastructure.Entities;

public sealed class NotaFiscalItemEntity
{
    public Guid Id { get; set; }
    public Guid NotaFiscalId { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public decimal Quantidade { get; set; }
    public decimal ValorUnitario { get; set; }
    public decimal ValorTotal { get; set; }
    public NotaFiscalEntity? NotaFiscal { get; set; }
}
