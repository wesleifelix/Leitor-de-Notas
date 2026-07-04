using LeitorDeNotas.ClearArch.Domain.Entities;
using System;
using System.Collections.Generic;

namespace LeitorDeNotas.ClearArch.Application.UseCases.Notas;

public class NotaFiscalNacional
{
    public string VersaoNFe { get; set; }
    
    // Identificação
    public int CodigoUF { get; set; }
    public long NumeroNota { get; set; }
    public string NaturezaOperacao { get; set; }
    public int Modelo { get; set; }
    public int Serie { get; set; }
    public DateTime DataEmissao { get; set; }
    public DateTime DataSaidaEntrada { get; set; }
    public int TipoNota { get; set; }
    public int DestinoOperacao { get; set; }
    public int CodigoMunicipioFisco { get; set; }
    public int TipoImpressao { get; set; }
    public int TipoEmissao { get; set; }
    public int CodigoVerificacao { get; set; }
    public string TipoAmbiente { get; set; }
    public int FinalidadeNFe { get; set; }
    public int IndicadorConsumidorFinal { get; set; }
    public int IndicadorPresenca { get; set; }
    public int IndicadorIntermediador { get; set; }
    public int ProcessoEmissao { get; set; }
    public string VersaoProcesso { get; set; }

    // Emitente
    public string CNPJEmitente { get; set; }
    public string NomeEmpresaEmitente { get; set; }
    public string NomeFanatasiaEmitente { get; set; }
    public string LogradouroEmitente { get; set; }
    public string NumeroEmitente { get; set; }
    public string ComplementoEmitente { get; set; }
    public string BairroEmitente { get; set; }
    public int MunicipioEmitente { get; set; }
    public string MunicipioNomeEmitente { get; set; }
    public string UFEmitente { get; set; }
    public string CEPEmitente { get; set; }
    public string PaisEmitente { get; set; }
    public string TelefoneEmitente { get; set; }
    public string InscricaoEstadualEmitente { get; set; }
    public int RegimeEspecialTributario { get; set; }

    // Destinatário
    public string CPFDestino { get; set; }
    public string NomeDestino { get; set; }
    public string LogradouroDestino { get; set; }
    public string NumeroDestino { get; set; }
    public string ComplementoDestino { get; set; }
    public string BairroDestino { get; set; }
    public int MunicipioDestino { get; set; }
    public string MunicipioNomeDestino { get; set; }
    public string UFDestino { get; set; }
    public string CEPDestino { get; set; }
    public string PaisDestino { get; set; }
    public string TelefoneDestino { get; set; }
    public int IndicadorInscricaoEstadualDestino { get; set; }

    // Produtos/Itens
    public List<NotaFiscalNacionalItem> Itens { get; set; } = new List<NotaFiscalNacionalItem>();

    // Totais
    public decimal ValorBaseICMS { get; set; }
    public decimal ValorICMS { get; set; }
    public decimal ValorICMSDesoneracao { get; set; }
    public decimal ValorProdutos { get; set; }
    public decimal ValorFrete { get; set; }
    public decimal ValorSeguro { get; set; }
    public decimal ValorDesconto { get; set; }
    public decimal ValorOutrosDespesa { get; set; }
    public decimal ValorTotalNota { get; set; }
    public decimal ValorTributosEstimados { get; set; }

    // Transporte
    public int ModalidadeFrete { get; set; }
    public string CNPJTransportadora { get; set; }
    public string NomeTransportadora { get; set; }
    public string InscricaoEstadualTransportadora { get; set; }
    public string EnderecoTransportadora { get; set; }
    public string MunicipioTransportadora { get; set; }
    public string UFTransportadora { get; set; }
    public decimal PesoLiquido { get; set; }
    public decimal PesoBruto { get; set; }

    // Pagamento
    public int IndicadorPagamento { get; set; }
    public string FormaPagamento { get; set; }
    public decimal ValorPagamento { get; set; }
    public string Bandeira { get; set; }
    public string CodigoAutorizacao { get; set; }

    // Intermediador
    public string CNPJIntermediador { get; set; }
    public string IDCadastroIntermediador { get; set; }

    // Informações adicionais
    public string InformacaoComplementar { get; set; }
    public string InformacaoComplementarFisco { get; set; }
    public string IDExterno { get; set; }
    public string ChaveNFe { get; set; }

    //public void ConvertNotaProduto()
    //{
    //    var ItensNotas = new List<NotaFiscalItem>();
    //    var notaID = Guid.NewGuid();
    //    foreach (var item in this.Itens)
    //    {
    //        ItensNotas.Add(
    //             NotaFiscalItem.Criar(item.)
    //            );
    //    }
    //    var notaDO =  NotaFiscal.Criar(this.ChaveNFe,this.Serie.ToString(), NotaFiscal.TipoNota.PRODUTO, ItensNotas);
    //}
}

public class NotaFiscalNacionalItem
{
    public int NumeroItem { get; set; }
    public string CodigoProduto { get; set; }
    public string CodigoEANComercial { get; set; }
    public string DescricaoProduto { get; set; }
    public string NCM { get; set; }
    public string CFOP { get; set; }
    public string UnidadeComercial { get; set; }
    public decimal QuantidadeComercial { get; set; }
    public decimal ValorUnitarioComercial { get; set; }
    public decimal ValorTotalProduto { get; set; }
    public string UnidadeTributavel { get; set; }
    public decimal QuantidadeTributavel { get; set; }
    public decimal ValorUnitarioTributavel { get; set; }
    public int IndicadorTotal { get; set; }
    public string PedidoExerno { get; set; }
    
    // Impostos
    public decimal ValorTotalTributos { get; set; }
    public string CodigoOrigemICMS { get; set; }
    public string CodigoSituacaoTributariaICMS { get; set; }
    public string CodigoSituacaoTributariaPIS { get; set; }
    public decimal BaseCalculoPIS { get; set; }
    public decimal AliquotaPIS { get; set; }
    public decimal ValorPIS { get; set; }
    public string CodigoSituacaoTributariaCOFINS { get; set; }
    public decimal BaseCalculoCOFINS { get; set; }
    public decimal AliquotaCOFINS { get; set; }
    public decimal ValorCOFINS { get; set; }
}
