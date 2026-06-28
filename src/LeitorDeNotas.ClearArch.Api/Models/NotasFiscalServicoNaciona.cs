using System;
using System.Collections.Generic;

namespace LeitorDeNotas.ClearArch.Api.Models
{
    public class NotasFiscalServicoNacional
    {
        public string Versao { get; set; }
        public string IdNFSe { get; set; }
        
        // Informações Gerais
        public string LocalEmissao { get; set; }
        public string LocalPrestacao { get; set; }
        public long NumeroNFSe { get; set; }
        public int CodigoLocalIncidencia { get; set; }
        public string NomeLocalIncidencia { get; set; }
        public string DescricaoTributacaoNacional { get; set; }
        public string VersaoAplicacao { get; set; }
        public int AmbienteGeracao { get; set; }
        public int TipoEmissao { get; set; }
        public int CodigoStatus { get; set; }
        public DateTime DataProcessamento { get; set; }
        public long NumeroDFSe { get; set; }

        // Emitente
        public string CNPJEmitente { get; set; }
        public string InscricaoMunicipalEmitente { get; set; }
        public string NomeEmitente { get; set; }
        public string LogradouroEmitente { get; set; }
        public int NumeroEmitente { get; set; }
        public string BairroEmitente { get; set; }
        public int CodigoMunicipioEmitente { get; set; }
        public string UFEmitente { get; set; }
        public string CEPEmitente { get; set; }
        public string TelefoneEmitente { get; set; }
        public string EmailEmitente { get; set; }

        // Regime Tributário Emitente
        public int OperacaoSimplesNacional { get; set; }
        public int RegimeAplicacaoTributacaoSN { get; set; }
        public int RegimeEspecialTributacao { get; set; }

        // Tomador (Cliente)
        public string CNPJTomador { get; set; }
        public string NomeTomador { get; set; }
        public int CodigoMunicipioTomador { get; set; }
        public string CEPTomador { get; set; }
        public string LogradouroTomador { get; set; }
        public int NumeroTomador { get; set; }
        public string ComplementoTomador { get; set; }
        public string BairroTomador { get; set; }

        // Serviço
        public int CodigoTributacaoNacional { get; set; }
        public string DescricaoServico { get; set; }
        public string CodigoNBS { get; set; }
        public int CodigoLocalPrestacao { get; set; }

        // Valores
        public decimal ValorServicoPrestado { get; set; }
        public decimal ValorBaseCalculo { get; set; }
        public decimal AliquotaAplicavel { get; set; }
        public decimal ValorISSQN { get; set; }
        public decimal ValorLiquido { get; set; }

        // Tributação Municipal (ISS)
        public int TributacaoISSQN { get; set; }
        public int TipoRetencaoISSQN { get; set; }

        // Tributação Federal
        public string CodigoSituacaoTributariaPIS { get; set; }
        public decimal ValorBaseCalculoPisCofins { get; set; }
        public decimal AliquotaPIS { get; set; }
        public decimal AliquotaCOFINS { get; set; }
        public decimal ValorPIS { get; set; }
        public decimal ValorCOFINS { get; set; }
        public int TipoRetencaoPisCofins { get; set; }

        // Totais de Tributos
        public decimal TotalTributosFedera { get; set; }
        public decimal TotalTributosEstadual { get; set; }
        public decimal TotalTributosMunicipal { get; set; }

        // Informações do DPS
        public int EnvironmentoDPS { get; set; }
        public DateTime DataEmissaoDPS { get; set; }
        public string SerieDPS { get; set; }
        public long NumeroDPS { get; set; }
        public DateTime DataCompetenciaDPS { get; set; }
        public int TipoEmissaoDPS { get; set; }
        public int CodigoLocalEmissaoDPS { get; set; }
    }
}
