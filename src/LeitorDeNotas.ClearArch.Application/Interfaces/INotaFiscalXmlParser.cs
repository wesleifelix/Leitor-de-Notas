using LeitorDeNotas.ClearArch.Domain.Entities;

namespace LeitorDeNotas.ClearArch.Application.Interfaces;

public interface INotaFiscalXmlParser
{
    NotaFiscal Parse(string xmlContent);
}
