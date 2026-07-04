namespace LeitorDeNotas.ClearArch.Domain.Entities;

public sealed class NotaFiscal
{
    public Guid Id { get; private set; }
    public string ChaveAcesso { get; private set; }
    public string Serie { get; private set; }
    public DateTime DataEmissao { get; private set; }
    public TipoNota Tipo { get; private set; } = TipoNota.PRODUTO;

    private decimal _ValorTotal { get; set; } = 0;
    public decimal ValorTotal { get => _ValorTotal; private set {
            if (value == 0)
            {
                _ValorTotal = Itens?.Sum(x => x.ValorTotal) ?? 0;
            }
            else
                _ValorTotal = value;
        } }
    private decimal _EstimativaImposto { get; set; } = 0;
    public decimal EstimativaImposto { get => _EstimativaImposto; private set {

            if(value == 0)
            {
                decimal prevalor = 0;
                //decimal.TryParse( (_ValorTotal * 0,063), out prevalor);
            }
            _EstimativaImposto = value;
        } }
    public decimal _EstimativaLucroPrejuizo { get; set; } = 0;
    public decimal EstimativaLucroPrejuizo { get =>_EstimativaLucroPrejuizo ; private set; }
    public IReadOnlyCollection<NotaFiscalItem> Itens => _itens.AsReadOnly();

    private readonly List<NotaFiscalItem> _itens = new();

    private NotaFiscal(Guid id, string chaveAcesso, string serie, DateTime dataEmissao, TipoNota tipo, decimal valorTotal, decimal estimativaImposto, decimal estimativaLucroPrejuizo)
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

    public static NotaFiscal Criar(string chaveAcesso, string serie, DateTime dataEmissao, TipoNota tipo, IEnumerable<NotaFiscalItem> itens)
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

        var notaFiscal = new NotaFiscal(Guid.NewGuid(), chaveAcesso.Trim(), serie.Trim(), dataEmissao, tipo, valorTotal, estimativaImposto, estimativaLucroPrejuizo);
        notaFiscal._itens.AddRange(itensLista);

        return notaFiscal;
    }
    public static NotaFiscal Criar(string chaveAcesso, string serie, DateTime dataEmissao, TipoNota tipo, decimal valortotal, decimal estimativaImposto, decimal lucro)
    {
        ArgumentNullException.ThrowIfNull(chaveAcesso);
        ArgumentNullException.ThrowIfNull(serie);
        ArgumentNullException.ThrowIfNull(tipo);

        var notaFiscal = new NotaFiscal(Guid.NewGuid(), chaveAcesso.Trim(), serie.Trim(), dataEmissao, tipo,valortotal, estimativaImposto,lucro);
        
        return notaFiscal;
    }

    public enum TipoNota
    {
        PRODUTO,
        SERVICO
    }
    private static decimal CalcularEstimativaImposto(decimal valorTotal)
        => Math.Round(valorTotal * 0.1m, 2);

    private static decimal CalcularEstimativaLucroPrejuizo(decimal valorTotal)
        => Math.Round(valorTotal * 0.15m, 2);
}
