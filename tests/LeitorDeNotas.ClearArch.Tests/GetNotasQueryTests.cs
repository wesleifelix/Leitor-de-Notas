using LeitorDeNotas.ClearArch.Application.UseCases.Notas;
using LeitorDeNotas.ClearArch.Domain.Entities;
using LeitorDeNotas.ClearArch.Domain.Interfaces;
using Moq;
using Xunit;

namespace LeitorDeNotas.ClearArch.Tests;

public sealed class GetNotasQueryTests
{
    [Fact]
    public async Task ExecuteAsync_ReturnsAllNotas()
    {
        var notas = new[]
        {
            Nota.Criar("Primeira nota", "Conteúdo da primeira nota."),
            Nota.Criar("Segunda nota", "Conteúdo da segunda nota.")
        };

        var repositoryMock = new Mock<INotaRepository>();
        repositoryMock
            .Setup(x => x.ObterTodasAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(notas);

        var query = new GetNotasQuery(repositoryMock.Object);
        var result = await query.ExecuteAsync();

        Assert.Equal(2, result.Count());
        Assert.Contains(result, x => x.Titulo == "Primeira nota");
        Assert.Contains(result, x => x.Titulo == "Segunda nota");
    }
}
