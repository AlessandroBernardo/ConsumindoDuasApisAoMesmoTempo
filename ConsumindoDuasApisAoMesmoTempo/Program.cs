using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

class Program
{
    static async Task Main(string[] args)
    {

        Console.WriteLine("Informe o Id de um pokemon - Por exemplo: 1 para o Bulbassaur");
        var pokeNameOrId = Console.ReadLine();
        Console.WriteLine("Informe um CEP sem pontos ou traços");
        var cep = Console.ReadLine();   

        var httpClient = new HttpClient();
        // Faz a chamada à API do Pokemon
        var pokemonResponse = await httpClient.GetAsync($"https://pokeapi.co/api/v2/pokemon/{pokeNameOrId}");
        var pokemonContent = await pokemonResponse.Content.ReadAsStringAsync();
        var pokemon = JsonConvert.DeserializeObject<Pokemon>(pokemonContent);

        // Faz a chamada à API dos Correios
        var correiosResponse = await httpClient.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
        var correiosContent = await correiosResponse.Content.ReadAsStringAsync();
        var correios = JsonConvert.DeserializeObject<Correios>(correiosContent);

        // Cria o objeto com os dados das duas APIs
        var objeto = new MeuObjeto
        {
            Nome = pokemon.Name,
            Estado = correios.Uf
        };

        Console.WriteLine($"Nome do Pokemon: {objeto.Nome}");
        Console.WriteLine($"Estado: {objeto.Estado}");
        Console.ReadKey();
    }
}

class Pokemon
{
    public string Name { get; set; }
}

class Correios
{
    public string Uf { get; set; }
}

class MeuObjeto
{
    public string Nome { get; set; }
    public string Estado { get; set; }
}

