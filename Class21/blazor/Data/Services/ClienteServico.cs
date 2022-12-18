using System.Net.Http.Headers;
using Blazor.Environments;
using Blazor.Data.Models;

namespace Blazor.Data.Services;

public class ClienteServico
{
    private HttpClient http = new HttpClient();

    public async Task<Cliente[]?> Todos(string? token)
    {
        this.http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return await http.GetFromJsonAsync<Cliente[]>($"{Configuration.Host}/clientes");
    }
    
    public async Task<Cliente?> BuscarPorId(int id, string? token)
    {
        this.http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return await http.GetFromJsonAsync<Cliente?>($"{Configuration.Host}/clientes/{id}");
    }

    public async Task Incluir(Cliente cliente, string? token)
    {
        this.http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        await http.PostAsJsonAsync<Cliente>($"{Configuration.Host}/clientes", cliente);
    }

    public async Task Atualizar(Cliente cliente, string? token)
    {
        this.http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        await http.PutAsJsonAsync<Cliente>($"{Configuration.Host}/clientes/{cliente.Id}", cliente);
    }

    public async Task Excluir(Cliente cliente, string? token)
    {
        this.http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        await http.DeleteAsync($"{Configuration.Host}/clientes/{cliente.Id}");
    }
}