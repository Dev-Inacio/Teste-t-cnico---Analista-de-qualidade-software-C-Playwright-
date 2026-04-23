using MinhasFinancas.Domain.Entities;

namespace MinhasFinancas.Testes;

public class PessoaTestes
{
    [Fact]
    public void Pessoa_MenorDeIdade_DeveRetornarFalse()
    {
        var pessoa = new Pessoa
        {
            DataNascimento = DateTime.Today.AddYears(-17)
        };

        var resultado = pessoa.EhMaiorDeIdade();
        Assert.False(resultado);
    }

    [Fact]
    public void Pessoa_MaiorDeIdade_DeveRetornarTrue()
    {
        var pessoa = new Pessoa
        {
            DataNascimento = DateTime.Today.AddYears(-18)
        };

        var resultado = pessoa.EhMaiorDeIdade();
        Assert.True(resultado);
    }
}
