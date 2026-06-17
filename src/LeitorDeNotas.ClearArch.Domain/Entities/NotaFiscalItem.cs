namespace LeitorDeNotas.ClearArch.Domain.Entities;

public sealed class NotaFiscalItem
{
    public Guid Id { get; private set; }
    public Guid NotaFiscalId { get; private set; }
    public string Descricao { get; private set; }
    public string Tipo { get; private set; }
    public decimal Quantidade { get; private set; }
    public decimal ValorUnitario { get; private set; }
    public decimal ValorTotal { get; private set; }

    private NotaFiscalItem(Guid id, Guid notaFiscalId, string descricao, string tipo, decimal quantidade, decimal valorUnitario)
    {
        Id = id;
        NotaFiscalId = notaFiscalId;
        Descricao = descricao;
        Tipo = tipo;
        Quantidade = quantidade;
        ValorUnitario = valorUnitario;
        ValorTotal = quantidade * valorUnitario;
    }

    public static NotaFiscalItem Criar(Guid notaFiscalId, string descricao, string tipo, decimal quantidade, decimal valorUnitario)
    {
        ArgumentNullException.ThrowIfNull(descricao);
        ArgumentNullException.ThrowIfNull(tipo);

        return new NotaFiscalItem(Guid.NewGuid(), notaFiscalId, descricao.Trim(), tipo.Trim(), quantidade, valorUnitario);
    }
}
