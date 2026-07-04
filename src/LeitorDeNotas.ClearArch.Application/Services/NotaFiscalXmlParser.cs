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
        var infNSe = document.Descendants().FirstOrDefault(x => x.Name.LocalName.EndsWith("infNFSe", StringComparison.OrdinalIgnoreCase));
        if (infNFe is null && infNSe is null)
            throw new InvalidOperationException("XML de nota fiscal não contém o nó infNFe.");

        List<NotaFiscalItem> itensLista = new List<NotaFiscalItem>();
        string chaveAcesso = string.Empty; 
        string serie = string.Empty;
        NotaFiscal.TipoNota tipo = NotaFiscal.TipoNota.PRODUTO;
        string number = string.Empty;
        decimal vTotal = 0;
        DateTime dataEmissao = DateTime.Now;

        if (infNFe != null)
        {
            //serie = infNFe.Elements().FirstOrDefault(x => x.Name.LocalName == "serie")?.Value ?? string.Empty;
            var dataEmissaoText = infNFe.Descendants().FirstOrDefault(x => x.Name.LocalName == "dhEmi" || x.Name.LocalName == "dEmi")?.Value;
            tipo = NotaFiscal.TipoNota.PRODUTO;
            number = infNFe.Descendants().FirstOrDefault(x => x.Name.LocalName == "nNF")?.Value ?? "0";
            dataEmissao = DateTime.TryParse(dataEmissaoText, out var data) ? data : DateTime.UtcNow;
            chaveAcesso = infNFe.Attribute("Id")?.Value?.Replace("NFe", "") ?? string.Empty;
            serie = infNFe.Descendants().FirstOrDefault(x => x.Name.LocalName == "serie")?.Value ?? "0";
            var _vTotal =  infNFe.Descendants().FirstOrDefault(x => x.Name.LocalName == "vUnTrib")?.Value ??" 0";
            vTotal = decimal.TryParse(_vTotal.Replace(".",","), out decimal total) ? total : 0;

            var itens = document.Descendants().Where(x => x.Name.LocalName == "det")
                .Select(det =>
                {
                    var descricao = det.Descendants().FirstOrDefault(x => x.Name.LocalName == "xProd")?.Value ?? "Item";
                    var tipoItem = det.Descendants().FirstOrDefault(x => x.Name.LocalName == "cProd")?.Value ?? "Produto";
                    var quantidadeText = det.Descendants().FirstOrDefault(x => x.Name.LocalName == "qCom")?.Value ?? "0";
                    var valorUnitarioText = det.Descendants().FirstOrDefault(x => x.Name.LocalName == "vUnCom")?.Value ?? "0";
                    var ncm = det.Descendants().FirstOrDefault(x => x.Name.LocalName == "NCM")?.Value ?? "0";
                    var CodigoProduto = det.Descendants().FirstOrDefault(x => x.Name.LocalName == "cProd")?.Value ?? "0";
                    var quantidade = decimal.TryParse(quantidadeText, out var q) ? q : 0m;
                    var valorUnitario = decimal.TryParse(valorUnitarioText.Replace(".",","), out var v) ? v : 0m;

                    return NotaFiscalItem.Criar(Guid.Empty, descricao, CodigoProduto, quantidade, valorUnitario, ncm);
                });

            itensLista = itens.ToList();

        }
        else if (infNSe != null)
        {
            //serie = infNFe.Elements().FirstOrDefault(x => x.Name.LocalName == "serie")?.Value ?? string.Empty;
            var dataEmissaoText = infNSe.Descendants().FirstOrDefault(x => x.Name.LocalName == "dhEmi" || x.Name.LocalName == "dEmi")?.Value;
            tipo = NotaFiscal.TipoNota.SERVICO;
            number = infNSe.Descendants().FirstOrDefault(x => x.Name.LocalName == "nNFSe")?.Value ?? "0";
            dataEmissao = DateTime.TryParse(dataEmissaoText, out var data) ? data : DateTime.UtcNow;
            chaveAcesso = infNSe.Attribute("Id")?.Value?.Replace("NFSe", "") ?? string.Empty;
            serie = infNSe.Descendants().FirstOrDefault(x => x.Name.LocalName == "serie")?.Value ?? "0";
            var _vTotal = infNSe.Descendants().FirstOrDefault(x => x.Name.LocalName == "vLiq")?.Value ?? " 0";
            vTotal = decimal.TryParse(_vTotal.Replace(".",","), out decimal total) ? total : 0;

            var itens = document.Descendants().Where(x => x.Name.LocalName == "serv")
                .Select(det =>
                {
                    var descricao = det.Descendants().FirstOrDefault(x => x.Name.LocalName == "xDescServ")?.Value ?? "Item";
                    var tipoItem = det.Descendants().FirstOrDefault(x => x.Name.LocalName == "cProd")?.Value ?? "Serviço";
                    var quantidadeText = det.Descendants().FirstOrDefault(x => x.Name.LocalName == "qCom")?.Value ?? "1";
                    var ncm = det.Descendants().FirstOrDefault(x => x.Name.LocalName == "NCM")?.Value ?? "0";
                    var CodigoProduto = det.Descendants().FirstOrDefault(x => x.Name.LocalName == "CodigoProduto")?.Value ?? "0";
                    var quantidade = decimal.TryParse(quantidadeText, out var q) ? q : 1m;
                    var valorUnitario = decimal.TryParse(vTotal.ToString(), out var v) ? v : 0m;

                    return NotaFiscalItem.Criar(Guid.Empty, descricao, CodigoProduto, quantidade, valorUnitario, ncm);
                });

            itensLista = itens.ToList();
        }
        Console.WriteLine($"--- DADOS EXTRAÍDOS DIRETOS ---");
        Console.WriteLine($"Chave: {chaveAcesso}");
        Console.WriteLine($"Série: {serie}");
        Console.WriteLine($"Número NF: {number}");
        Console.WriteLine($"Data Emissão: {dataEmissao.ToString()}");
        Console.WriteLine($"Total: {vTotal.ToString()}");
        Console.WriteLine(new string('-', 40));

        return NotaFiscal.Criar(chaveAcesso, serie, dataEmissao, tipo, itensLista);
    }
}
