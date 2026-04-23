using MinhasFinancas.Domain.Entities;

namespace MinhasFinancas.Testes;

public class CategoriaTestes
{
    [Fact]
    public void Categoria_Despesa_DevePermitirTipoDespesa()
    {        
        var categoria = new Categoria
        {
            Finalidade = Categoria.EFinalidade.Despesa
        };

        var resultado = categoria.PermiteTipo(Transacao.ETipo.Despesa);

        Assert.True(resultado);
    }

    [Fact]
    public void Categoria_Receita_DevePermitirTipoReceita()
    {
        var categoria = new Categoria
        {
            Finalidade = Categoria.EFinalidade.Receita
        };

        var resultado = categoria.PermiteTipo(Transacao.ETipo.Receita);

        Assert.True(resultado);
    }

    [Fact]
    public void Categoria_Ambas_DevePermitirTipoDespesa()
    {
        var categoria = new Categoria
        {
            Finalidade = Categoria.EFinalidade.Ambas
        };

        var resultado = categoria.PermiteTipo(Transacao.ETipo.Despesa);

        Assert.True(resultado);
    }

    [Fact]
    public void Categoria_Ambas_DevePermitirTipoReceita()
    {
        var categoria = new Categoria
        {
            Finalidade = Categoria.EFinalidade.Ambas
        };

        var resultado = categoria.PermiteTipo(Transacao.ETipo.Receita);
        Assert.True(resultado);
    }

    [Fact]
    public void Categoria_Despesa_DeveRejeitarTipoReceita()
    {
        var categoria = new Categoria
        {
            Finalidade = Categoria.EFinalidade.Despesa
        };
        var resultado = categoria.PermiteTipo(Transacao.ETipo.Receita);
        Assert.False(resultado);
    }

    [Fact]
    public void Categoria_Receita_DeveRejeitarTipoDespesa()
    {
        var categoria = new Categoria
        {
            Finalidade = Categoria.EFinalidade.Receita
        };

        var resultado = categoria.PermiteTipo(Transacao.ETipo.Despesa);
        Assert.False(resultado);
    }
}