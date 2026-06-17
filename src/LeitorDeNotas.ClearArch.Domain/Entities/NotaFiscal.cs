namespace LeitorDeNotas.ClearArch.Domain.Entities;

public sealed class NotaFiscal
{
    public Guid Id { get; private set; }
    public string ChaveAcesso { get; private set; }
    public string Serie { get; private set; }
    public DateTime DataEmissao { get; private set; }
    public string Tipo { get; private set; }
    public decimal ValorTotal { get; private set; }
    public decimal EstimativaImposto { get; private set; }
    public decimal EstimativaLucroPrejuizo { get; private set; }
    public IReadOnlyCollection<NotaFiscalItem> Itens => _itens.AsReadOnly();

    private readonly List<NotaFiscalItem> _itens = new();

    private NotaFiscal(Guid id, string chaveAcesso, string serie, DateTime dataEmissao, string tipo, decimal valorTotal, decimal estimativaImposto, decimal estimativaLucroPrejuizo)
    {
        Id = id;
        ChaveAcesso = chaveAcesso;
        Serie = serie;
        DataEmissao = dataEmissao;
        Tipo = tipo;
        ValorTotal = valorTotal;
        EstimativaImposto = estimativaImposto;
        EstimativaLucroPrejuizo = estimativaLucroPrejuizo;
    }

    public static NotaFiscal Criar(string chaveAcesso, string serie, DateTime dataEmissao, string tipo, IEnumerable<NotaFiscalItem> itens)
    {
        ArgumentNullException.ThrowIfNull(chaveAcesso);
        ArgumentNullException.ThrowIfNull(serie);
        ArgumentNullException.ThrowIfNull(tipo);

        var itensLista = itens?.ToList() ?? throw new ArgumentNullException(nameof(itens));
        if (!itensLista.Any())
            throw new ArgumentException("A nota fiscal deve conter ao menos um item.", nameof(itens));

        var valorTotal = itensLista.Sum(item => item.ValorTotal);
        var estimativaImposto = CalcularEstimativaImposto(valorTotal);
        var estimativaLucroPrejuizo = CalcularEstimativaLucroPrejuizo(valorTotal);

        var notaFiscal = new NotaFiscal(Guid.NewGuid(), chaveAcesso.Trim(), serie.Trim(), dataEmissao, tipo.Trim(), valorTotal, estimativaImposto, estimativaLucroPrejuizo);
        notaFiscal._itens.AddRange(itensLista);

        return notaFiscal;
    }

    private static decimal CalcularEstimativaImposto(decimal valorTotal)
        => Math.Round(valorTotal * 0.1m, 2);

    private static decimal CalcularEstimativaLucroPrejuizo(decimal valorTotal)
        => Math.Round(valorTotal * 0.15m, 2);
}
