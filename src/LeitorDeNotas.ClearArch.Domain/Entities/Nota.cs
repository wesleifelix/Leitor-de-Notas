namespace LeitorDeNotas.ClearArch.Domain.Entities;

public sealed class Nota
{
    public Guid Id { get; private set; }
    public string Titulo { get; private set; }
    public string Conteudo { get; private set; }
    public DateTime CriadoEm { get; private set; }

    private Nota(Guid id, string titulo, string conteudo, DateTime criadoEm)
    {
        Id = id;
        Titulo = titulo;
        Conteudo = conteudo;
        CriadoEm = criadoEm;
    }

    public static Nota Criar(string titulo, string conteudo)
    {
        ArgumentNullException.ThrowIfNull(titulo);
        ArgumentNullException.ThrowIfNull(conteudo);

        if (string.IsNullOrWhiteSpace(titulo))
            throw new ArgumentException("O título não pode ser vazio.", nameof(titulo));

        return new Nota(Guid.NewGuid(), titulo.Trim(), conteudo.Trim(), DateTime.UtcNow);
    }
}
