using System.Net;
using System.Net.Http.Json;
using FluentAssertions;

namespace MinhasFinancas.IntegrationTestes;

public class BaseIntegrationTest
{
    protected readonly HttpClient _client;

    public BaseIntegrationTest()
    {
        _client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5000")
        };
    }

    protected async Task<string> CriarPessoaTest()
    {
        var dadosTest = new
        {
          Nome = "Dado Test",
          DataNascimento = "2000-01-01"  
        };
        var json = JsonContent.Create(dadosTest);
        var response = await _client.PostAsync("/api/v1/Pessoas", json);
        var content = await response.Content.ReadFromJsonAsync<dynamic>();
        var id = content!.GetProperty("id").GetString();
        return id;        
    }

    protected async Task<string> CriarCategoriaTest()
    {
        var dadosTest = new
        {
          Descricao = "Dado Test",
          Finalidade = 2 
        };
        var json = JsonContent.Create(dadosTest);
        var response = await _client.PostAsync("/api/v1/Categorias", json);
        var content = await response.Content.ReadFromJsonAsync<dynamic>();
        var id = content!.GetProperty("id").GetString();
        return id;        
    }

    protected async Task<string> CriarTransacaoTest()
    {
        var dadosTest = new
        {
          Descricao = "Dado Test",
          Valor = 100.0,
          Tipo = 1,
          Data= "2026-04-21T00:00:00.000Z",
          PessoaId = await CriarPessoaTest(),
          CategoriaId = await CriarCategoriaTest()
        };
        var json = JsonContent.Create(dadosTest);
        var response = await _client.PostAsync("/api/v1/Transacoes", json);
        var content = await response.Content.ReadFromJsonAsync<dynamic>();
        var id = content!.GetProperty("id").GetString();
        return id;        
    }
}