using System.Net;
using System.Net.Http.Json;
using FluentAssertions;

namespace MinhasFinancas.IntegrationTestes;

public class TotaisIntegrationTestes : BaseIntegrationTest
{
    
    [Fact]
    public async Task Verificar_TotaisPorPessoa_GET_ComTransacoes_DeveRetornar200()
    {
        var response = await _client.GetAsync("/api/v1/Totais/Pessoas");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    } 

    [Fact]
    public async Task Verificar_TotaisPorCategoria_GET_ComTransacoes_DeveRetornar200()
    {
        var response = await _client.GetAsync("/api/v1/Totais/Categorias");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    } 

    [Fact]
    public async Task Verificar_TotaisPorPessoa_GET_SemTransacoes_DeveRetornar200()
    {
        var PessoaId = await CriarPessoaTest();
        var response = await _client.GetAsync("/api/v1/Totais/Pessoas");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    } 

    [Fact]
    public async Task Verificar_TotaisPorPessoa_GET_SaldoNegativo_DeveRetornar200()
    {
        
        var pessoaId = await CriarPessoaTest();
        var dadosCategoria = new { Descricao = "Teste", Finalidade = 0 };
        var jsonCategoria = JsonContent.Create(dadosCategoria);
        var resCategoria = await _client.PostAsync("/api/v1/Categorias", jsonCategoria);
        var contentCategoria = await resCategoria.Content.ReadFromJsonAsync<dynamic>();
        var categoriaId = contentCategoria!.GetProperty("id").GetString();   

        var dadosTest = new
        {
          Descricao = "Dado Test",
          Valor = 100000.0,
          Tipo = 0,
          Data= "2026-04-21T00:00:00.000Z",
          PessoaId = pessoaId,
          CategoriaId = categoriaId
        };
        var transacaoJson = JsonContent.Create(dadosTest);
        await _client.PostAsync("/api/v1/Transacoes", transacaoJson);        
        var response = await _client.GetAsync("/api/v1/Totais/Pessoas"); 
        response.StatusCode.Should().Be(HttpStatusCode.OK);       
    } 
}