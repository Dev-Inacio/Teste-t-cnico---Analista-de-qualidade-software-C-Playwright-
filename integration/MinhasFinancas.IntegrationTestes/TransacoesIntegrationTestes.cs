using System.Net;
using System.Net.Http.Json;
using FluentAssertions;

namespace MinhasFinancas.IntegrationTestes;

public class TransacoesIntegrationTestes : BaseIntegrationTest
{   
  
    [Fact]
    public async Task Verificar_CriarTransacao_POST_ReceitaComCategoriaReceita_DeveRetornar201()
    {
        var pessoaId = await CriarPessoaTest();
        var dadosCategoria = new { Descricao = "Teste", Finalidade = 1 };
        var jsonCategoria = JsonContent.Create(dadosCategoria);
        var resCategoria = await _client.PostAsync("/api/v1/Categorias", jsonCategoria);
        var contentCategoria = await resCategoria.Content.ReadFromJsonAsync<dynamic>();
        var categoriaId = contentCategoria!.GetProperty("id").GetString();   

        var dadosTest = new
        {
          Descricao = "Dado Test",
          Valor = 100.0,
          Tipo = 1,
          Data= "2026-04-21T00:00:00.000Z",
          PessoaId = pessoaId,
          CategoriaId = categoriaId
        };
        var json = JsonContent.Create(dadosTest);
        var response = await _client.PostAsync("/api/v1/Transacoes", json);
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Verificar_CriarTransacao_POST_DespesaComCategoriaDespesa_DeveRetornar201()
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
          Valor = 100.0,
          Tipo = 0,
          Data= "2026-04-21T00:00:00.000Z",
          PessoaId = pessoaId,
          CategoriaId = categoriaId
        };
        var json = JsonContent.Create(dadosTest);
        var response = await _client.PostAsync("/api/v1/Transacoes", json);
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Verificar_CriarTransacao_POST_ComCategoriaAmbas_DeveRetornar201()
    {
        var pessoaId = await CriarPessoaTest();
        var dadosCategoria = new { Descricao = "Teste", Finalidade = 2 };
        var jsonCategoria = JsonContent.Create(dadosCategoria);
        var resCategoria = await _client.PostAsync("/api/v1/Categorias", jsonCategoria);
        var contentCategoria = await resCategoria.Content.ReadFromJsonAsync<dynamic>();
        var categoriaId = contentCategoria!.GetProperty("id").GetString();   

        var dadosTest = new
        {
          Descricao = "Dado Test",
          Valor = 100.0,
          Tipo = 0,
          Data= "2026-04-21T00:00:00.000Z",
          PessoaId = pessoaId,
          CategoriaId = categoriaId
        };
        var json = JsonContent.Create(dadosTest);
        var response = await _client.PostAsync("/api/v1/Transacoes", json);
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Verificar_CriarTransacao_POST_DespesaParaMenorDeIdade_DeveRetornar201()
    {

        var dadosPessoa = new { Nome = "Menor Teste", DataNascimento = "2010-01-01" };
        var jsonPessoa = JsonContent.Create(dadosPessoa);
        var resPessoa = await _client.PostAsync("/api/v1/Pessoas", jsonPessoa);
        var contentPessoa = await resPessoa.Content.ReadFromJsonAsync<dynamic>();
        var pessoaId = contentPessoa!.GetProperty("id").GetString();        
        var dadosCategoria = new { Descricao = "Teste", Finalidade = 2 };
        var jsonCategoria = JsonContent.Create(dadosCategoria);
        var resCategoria = await _client.PostAsync("/api/v1/Categorias", jsonCategoria);
        var contentCategoria = await resCategoria.Content.ReadFromJsonAsync<dynamic>();
        var categoriaId = contentCategoria!.GetProperty("id").GetString();   

        var dadosTest = new
        {
          Descricao = "Dado Test",
          Valor = 100.0,
          Tipo = 0,
          Data= "2026-04-21T00:00:00.000Z",
          PessoaId = pessoaId,
          CategoriaId = categoriaId
        };
        var json = JsonContent.Create(dadosTest);
        var response = await _client.PostAsync("/api/v1/Transacoes", json);
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Verificar_BuscarTransacoes_GET_DeveRetornar200()
    {
        var response = await _client.GetAsync("/api/v1/Transacoes");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }    

    [Fact]
    public async Task Verificar_BuscarTransacao_GET_IdExistente_DeveRetornar200()
    {
        var id = await CriarTransacaoTest();
        var response = await _client.GetAsync($"/api/v1/Transacoes/{id}");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }    

    [Fact]
    public async Task Verificar_CriarTransacao_POST_ReceitaComCategoriaDespesa_DeveRetornar400()
    {
        var dadosPessoa = new { Nome = "Menor Teste", DataNascimento = "2000-01-01" };
        var jsonPessoa = JsonContent.Create(dadosPessoa);
        var resPessoa = await _client.PostAsync("/api/v1/Pessoas", jsonPessoa);
        var contentPessoa = await resPessoa.Content.ReadFromJsonAsync<dynamic>();
        var pessoaId = contentPessoa!.GetProperty("id").GetString();        
        var dadosCategoria = new { Descricao = "Teste", Finalidade = 0 };
        var jsonCategoria = JsonContent.Create(dadosCategoria);
        var resCategoria = await _client.PostAsync("/api/v1/Categorias", jsonCategoria);
        var contentCategoria = await resCategoria.Content.ReadFromJsonAsync<dynamic>();
        var categoriaId = contentCategoria!.GetProperty("id").GetString();   

        var dadosTest = new
        {
          Descricao = "Dado Test",
          Valor = 100.0,
          Tipo = 1,
          Data= "2026-04-21T00:00:00.000Z",
          PessoaId = pessoaId,
          CategoriaId = categoriaId
        };
        var json = JsonContent.Create(dadosTest);
        var response = await _client.PostAsync("/api/v1/Transacoes", json);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    } 

    [Fact]
    public async Task Verificar_CriarTransacao_POST_DespesaComCategoriaReceita_DeveRetornar400()
    {
        var dadosPessoa = new { Nome = "Menor Teste", DataNascimento = "2000-01-01" };
        var jsonPessoa = JsonContent.Create(dadosPessoa);
        var resPessoa = await _client.PostAsync("/api/v1/Pessoas", jsonPessoa);
        var contentPessoa = await resPessoa.Content.ReadFromJsonAsync<dynamic>();
        var pessoaId = contentPessoa!.GetProperty("id").GetString();        
        var dadosCategoria = new { Descricao = "Teste", Finalidade = 1 };
        var jsonCategoria = JsonContent.Create(dadosCategoria);
        var resCategoria = await _client.PostAsync("/api/v1/Categorias", jsonCategoria);
        var contentCategoria = await resCategoria.Content.ReadFromJsonAsync<dynamic>();
        var categoriaId = contentCategoria!.GetProperty("id").GetString();   

        var dadosTest = new
        {
          Descricao = "Dado Test",
          Valor = 100.0,
          Tipo = 0,
          Data= "2026-04-21T00:00:00.000Z",
          PessoaId = pessoaId,
          CategoriaId = categoriaId
        };
        var json = JsonContent.Create(dadosTest);
        var response = await _client.PostAsync("/api/v1/Transacoes", json);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    } 

    [Fact]
    public async Task Verificar_CriarTransacao_POST_ReceitaParaMenorDeIdade_DeveRetornar400()
    {
        var dadosPessoa = new { Nome = "Menor Teste", DataNascimento = "2010-01-01" };
        var jsonPessoa = JsonContent.Create(dadosPessoa);
        var resPessoa = await _client.PostAsync("/api/v1/Pessoas", jsonPessoa);
        var contentPessoa = await resPessoa.Content.ReadFromJsonAsync<dynamic>();
        var pessoaId = contentPessoa!.GetProperty("id").GetString();        
        var dadosCategoria = new { Descricao = "Teste", Finalidade = 1 };
        var jsonCategoria = JsonContent.Create(dadosCategoria);
        var resCategoria = await _client.PostAsync("/api/v1/Categorias", jsonCategoria);
        var contentCategoria = await resCategoria.Content.ReadFromJsonAsync<dynamic>();
        var categoriaId = contentCategoria!.GetProperty("id").GetString();   

        var dadosTest = new
        {
          Descricao = "Dado Test",
          Valor = 100.0,
          Tipo = 1,
          Data= "2026-04-21T00:00:00.000Z",
          PessoaId = pessoaId,
          CategoriaId = categoriaId
        };
        var json = JsonContent.Create(dadosTest);
        var response = await _client.PostAsync("/api/v1/Transacoes", json);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    } 

    [Fact]
    public async Task Verificar_CriarTransacao_POST_SemValor_DeveRetornar400()
    {
        var pessoaId = await CriarPessoaTest();
        var categoriaId = await CriarCategoriaTest();

        var dadosTest = new
        {
          Descricao = "Dado Test",
          Valor = 0,
          Tipo = 1,
          Data= "2026-04-21T00:00:00.000Z",
          PessoaId = pessoaId,
          CategoriaId = categoriaId
        };
        var json = JsonContent.Create(dadosTest);
        var response = await _client.PostAsync("/api/v1/Transacoes", json);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Verificar_CriarTransacao_POST_SemPessoaVinculada_DeveRetornar400()
    {        
        var categoriaId = await CriarCategoriaTest();
        var dadosTest = new
        {
          Descricao = "Dado Test",
          Valor = 100.0,
          Tipo = 1,
          Data= "2026-04-21T00:00:00.000Z",          
          CategoriaId = categoriaId
        };
        var json = JsonContent.Create(dadosTest);
        var response = await _client.PostAsync("/api/v1/Transacoes", json);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Verificar_BuscarTransacao_GET_IdInexistente_DeveRetornar404()
    {        
        var id = Guid.NewGuid();        
        var response = await _client.GetAsync($"/api/v1/Transacoes/{id}");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
