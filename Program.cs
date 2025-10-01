using static System.Console;
using System.Net.Http;
using ConsumerViaCep.Models;

class Program
{
    static async Task Main(string[] args)
    {
        WriteLine("Digite seu CEP:");
        var cep = ReadLine();
        var enderecoUrl = $@"https://viacep.com.br/ws/{cep}/json/";

        using var client = new HttpClient();

        try
        {
            HttpResponseMessage response = await client.GetAsync(enderecoUrl);

            response.EnsureSuccessStatusCode();
            string respostaApiJson = await response.Content.ReadAsStringAsync();
            Endereco? endereco = System.Text.Json.JsonSerializer.Deserialize<Endereco>(respostaApiJson);



            WriteLine("CEP:\n" + endereco.cep);
            WriteLine("LOGRADOURO:\n" + endereco.logradouro);
            WriteLine("BAIRRO:\n" + endereco.bairro);
            WriteLine("CIDADE:\n" + endereco.localidade);
        }
        catch (System.Exception e)
        {
            WriteLine("Erro ao acessar a API: " + e.Message);
        }
    }
}
