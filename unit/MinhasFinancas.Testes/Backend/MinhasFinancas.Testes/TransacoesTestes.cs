using MinhasFinancas.Domain.Entities;

namespace MinhasFinancas.Testes;

public class TransacaoTestes
{
    [Fact]
    public void Transacao_CategoriaDespesa_EmReceitaDeveLancarExcecao()
    {
        var transacao = new Transacao
        {
            Descricao = "Teste",
            Valor = 100,
            Tipo = Transacao.ETipo.Receita,
            Data = DateTime.Today
        };        

        var categoria = new Categoria
        {
            Descricao = "Teste",
            Finalidade = Categoria.EFinalidade.Despesa
        };
        Assert.Throws<InvalidOperationException>(() => transacao.Categoria = categoria);
    }

    [Fact]
    public void Transacao_CategoriaReceita_EmDespesaDeveLancarExcecao()
    {
        var transacao = new Transacao
        {
            Descricao = "Teste",
            Valor = 100,
            Tipo = Transacao.ETipo.Despesa,
            Data = DateTime.Today
        }; 
        var categoria = new Categoria
        {
            Descricao = "Teste",
            Finalidade = Categoria.EFinalidade.Receita
        };
        Assert.Throws<InvalidOperationException>(() => transacao.Categoria = categoria);
    }

    [Fact]
    public void Transacao_Receita_ParaMenorDeIdadeDeveLancarExcecao()
    {
        var transacao = new Transacao
        {
            Descricao = "Teste",
            Valor = 100,
            Tipo = Transacao.ETipo.Despesa,
            Data = DateTime.Today
        }; 
        var pessoa = new Pessoa
        {
            Nome = "test nome",
            DataNascimento = DateTime.Today.AddYears(-17)
        };
        Assert.Throws<InvalidOperationException>(() => transacao.Pessoa = pessoa);
    }
}