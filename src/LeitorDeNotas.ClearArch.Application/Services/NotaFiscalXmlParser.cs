using System.Xml.Linq;
using LeitorDeNotas.ClearArch.Application.Interfaces;
using LeitorDeNotas.ClearArch.Domain.Entities;

namespace LeitorDeNotas.ClearArch.Application.Services;

public sealed class NotaFiscalXmlParser : INotaFiscalXmlParser
{
    public NotaFiscal Parse(string xmlContent)
    {
        if (string.IsNullOrWhiteSpace(xmlContent))
            throw new ArgumentException("Conteúdo XML inválido.", nameof(xmlContent));

        var document = XDocument.Parse(xmlContent);
        var infNFe = document.Descendants().FirstOrDefault(x => x.Name.LocalName.EndsWith("infNFe", StringComparison.OrdinalIgnoreCase));
        if (infNFe is null)
            throw new InvalidOperationException("XML de nota fiscal não contém o nó infNFe.");

        var chaveAcesso = infNFe.Elements().FirstOrDefault(x => x.Name.LocalName == "Id")?.Value ?? string.Empty;
        var serie = infNFe.Elements().FirstOrDefault(x => x.Name.LocalName == "serie")?.Value ?? string.Empty;
        var dataEmissaoText = infNFe.Elements().FirstOrDefault(x => x.Name.LocalName == "dhEmi" || x.Name.LocalName == "dEmi")?.Value;
        var tipo = infNFe.Elements().FirstOrDefault(x => x.Name.LocalName == "tpNF")?.Value ?? "Produto";

        var dataEmissao = DateTime.TryParse(dataEmissaoText, out var data) ? data : DateTime.UtcNow;

        var itens = document.Descendants().Where(x => x.Name.LocalName == "det")
            .Select(det =>
            {
                var descricao = det.Descendants().FirstOrDefault(x => x.Name.LocalName == "xProd")?.Value ?? "Item";
                var tipoItem = det.Descendants().FirstOrDefault(x => x.Name.LocalName == "cProd")?.Value ?? "Produto";
                var quantidadeText = det.Descendants().FirstOrDefault(x => x.Name.LocalName == "qCom")?.Value ?? "0";
                var valorUnitarioText = det.Descendants().FirstOrDefault(x => x.Name.LocalName == "vUnCom")?.Value ?? "0";

                var quantidade = decimal.TryParse(quantidadeText, out var q) ? q : 0m;
                var valorUnitario = decimal.TryParse(valorUnitarioText, out var v) ? v : 0m;

                return NotaFiscalItem.Criar(Guid.Empty, descricao, tipoItem, quantidade, valorUnitario);
            });

        var itensLista = itens.ToList();
        if (!itensLista.Any())
        {
            itensLista.Add(NotaFiscalItem.Criar(Guid.Empty, "Item padrão", "Produto", 1, 0));
        }

        return NotaFiscal.Criar(chaveAcesso, serie, dataEmissao, tipo, itensLista);
    }
}
