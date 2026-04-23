using System.Net;
using System.Net.Http.Json;
using FluentAssertions;

namespace MinhasFinancas.IntegrationTestes;

public class PessoasIntegrationTestes : BaseIntegrationTest
{

    // Testes GET

    [Fact]
    public async Task Verifica_BuscarPessoas_GET_DeveRetornar200()
    {
        var response = await _client.GetAsync("/api/v1/Pessoas");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }    

    [Fact]
    public async Task Verifica_BuscarPessoas_ID_GET_DeveRetornar200()
    {
        var id = await CriarPessoaTest();
        var getResponse = await _client.GetAsync($"/api/v1/Pessoas/{id}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);        
    } 

    [Fact]
    public async Task Verifica_BuscarPessoas_GET_IdInexistente_DeveRetornar404()
    {
        var id = Guid.NewGuid().ToString();
        var getResponse = await _client.GetAsync($"/api/v1/Pessoas/{id}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);        
    } 

    // Testes GET

    // Testes POST

    [Fact]
    public async Task Verificar_CriarPessoas_POST_DeveRetornar201()
    {

        var dados = new
        {
          Nome = " josé test",
          DataNascimento = "2000-01-01"  
        };

        var json = JsonContent.Create(dados);
        var response = await _client.PostAsync("/api/v1/Pessoas", json);
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Verifica_CriarPessoas_POST_NomeVazio_DeveRetornar400()
    {

        var dados = new
        {
          Nome = "",
          DataNascimento = "2000-01-01"  
        };

        var json = JsonContent.Create(dados);
        var response = await _client.PostAsync("/api/v1/Pessoas", json);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Verifica_CriarPessoas_POST_DataNascimentoVazio_DeveRetornar400()
    {

        var dados = new
        {
          Nome = "Nome Test",
          DataNascimento = ""  
        };

        var json = JsonContent.Create(dados);
        var response = await _client.PostAsync("/api/v1/Pessoas", json);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    // Testes POST

    // Testes PUT    

    [Fact]
    public async Task Verifica_AtualizarPessoas_ID_PUT_DeveRetornar204()
    {       
        var id = await CriarPessoaTest();

        var dadosAtualizados = new
        {
            Nome = "test PUT atualizado",
            DataNascimento = "1990-05-10"
        };
        var jsonAtualizado = JsonContent.Create(dadosAtualizados);
        var putResponse = await _client.PutAsync($"/api/v1/Pessoas/{id}", jsonAtualizado);
        putResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Verifica_AtualizarPessoas_PUT_NomeVazio_DeveRetornar400()    
    {       
        var id = await CriarPessoaTest();

        var dadosAtualizados = new
        {
            Nome = "",
            DataNascimento = "1990-05-10"
        };
        var jsonAtualizado = JsonContent.Create(dadosAtualizados);
        var putResponse = await _client.PutAsync($"/api/v1/Pessoas/{id}", jsonAtualizado);
        putResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Verifica_AtualizarPessoas_PUT_DataNascimentoVazio_DeveRetornar400()    
    {       
        var id = await CriarPessoaTest();

        var dadosAtualizados = new
        {
            Nome = "Nome Test",
            DataNascimento = ""
        };
        var jsonAtualizado = JsonContent.Create(dadosAtualizados);
        var putResponse = await _client.PutAsync($"/api/v1/Pessoas/{id}", jsonAtualizado);
        putResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Verifica_AtualizarPessoas_PUT_IdInvalido_DeveRetornar404()    
    {    
        var dadosAtualizados = new
        {
            Nome = "Nome Test",
            DataNascimento = "1990-05-10"
        };
        var id = Guid.NewGuid().ToString();
        var jsonAtualizado = JsonContent.Create(dadosAtualizados);
        var putResponse = await _client.PutAsync($"/api/v1/Pessoas/{id}", jsonAtualizado);
        putResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    // Testes PUT  

    // Testes DELETE
    [Fact]
    public async Task Verifica_DeletarPessoas_DELETE_PessoaExistente_DeveRetornar204()
    {
        var id = await CriarPessoaTest();        
        var deleteResponse = await _client.DeleteAsync($"/api/v1/Pessoas/{id}");
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Verifica_DeletarPessoas_DELETE_IdInexistente_DeveRetornar404()
    {
        var id = Guid.NewGuid().ToString();   
        var deleteResponse = await _client.DeleteAsync($"/api/v1/Pessoas/{id}");
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    // error 

    [Fact]
    public async Task Verifica_DeletarPessoas_DELETE_TransacoesEmCascata_DeveRetornar204()
    {
        var idPessoa = await CriarPessoaTest();     
        var idTransacao = await CriarTransacaoTest();
        var deleteResponse = await _client.DeleteAsync($"/api/v1/Pessoas/{idPessoa}");
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var response = await _client.GetAsync($"/api/v1/Transacoes/{idTransacao}");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    // error deletando mesmo com id inexistente

    [Fact]
    public async Task Verifica_DeletarPessoas_DELETE_PessoaInexistente_DeveRetornar404()
    {
        var id = Guid.NewGuid().ToString();    
        var deleteResponse = await _client.DeleteAsync($"/api/v1/Pessoas/{id}");
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}