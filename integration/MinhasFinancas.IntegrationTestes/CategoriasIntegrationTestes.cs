using System.Net;
using System.Net.Http.Json;
using FluentAssertions;

namespace MinhasFinancas.IntegrationTestes;

public class CategoriasIntegrationTestes : BaseIntegrationTest
{       
    [Fact]
    public async Task Verificar_CriarCategoria_POST_TipoReceita_DeveRetornar201()
    {
        var dadosTest = new
        {
          Descricao = "Dado Test",
          Finalidade = 1 
        };
        var json = JsonContent.Create(dadosTest);
        var response = await _client.PostAsync("/api/v1/Categorias", json);
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Verificar_CriarCategoria_POST_TipoDespesa_DeveRetornar201()
    {
        var dadosTest = new
        {
          Descricao = "Dado Test",
          Finalidade = 0 
        };
        var json = JsonContent.Create(dadosTest);
        var response = await _client.PostAsync("/api/v1/Categorias", json);
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Verificar_CriarCategoria_POST_TipoAmbas_DeveRetornar201()
    {
        var dadosTest = new
        {
          Descricao = "Dado Test",
          Finalidade = 2 
        };
        var json = JsonContent.Create(dadosTest);
        var response = await _client.PostAsync("/api/v1/Categorias", json);
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Verificar_BuscarCategorias_GET_DeveRetornar200()
    {   
        var response = await _client.GetAsync("/api/v1/Categorias");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Verificar_BuscarCategoria_GET_IdExistente_DeveRetornar200()
    {   
        var id = await CriarCategoriaTest();
        var response = await _client.GetAsync($"/api/v1/Categorias/{id}");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Verificar_BuscarCategoria_GET_IdInexistente_DeveRetornar404()
    {   
        var id = Guid.NewGuid().ToString();    
        var response = await _client.GetAsync($"/api/v1/Categorias/{id}");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Verificar_CriarCategoria_POST_SemDescricao_DeveRetornar400()
    {   
        var dadosTest = new
        {
          Descricao = "",
          Finalidade = 2 
        };        
        var json = JsonContent.Create(dadosTest);
        var response = await _client.PostAsync("/api/v1/Categorias", json);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Verificar_CriarCategoria_POST_SemFinalidade_DeveRetornar400()
    {   
        var dadosTest = new
        {
          Descricao = "Descricao Test",          
        };        
        var json = JsonContent.Create(dadosTest);
        var response = await _client.PostAsync("/api/v1/Categorias", json);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}