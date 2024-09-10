using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using YourNamespace.Models;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public PokemonController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet("{startId}/{endId}")]
        public async Task<IActionResult> GetPokemons(int startId, int endId)
        {
            var tasks = new List<Task<Pokemon>>();
            string baseUrl = "https://pokeapi.co/api/v2/pokemon/";

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            for (int i = startId; i <= endId; i++)
            {
                string url = baseUrl + i;
                tasks.Add(GetPokemonAsync(url, options));
            }

            var pokemons = await Task.WhenAll(tasks);

            return Ok(pokemons.Where(p => p != null));
        }

        private async Task<Pokemon> GetPokemonAsync(string url, JsonSerializerOptions options)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var pokemonData = JsonSerializer.Deserialize<PokemonData>(jsonResponse, options);

                    // Mapeo de los datos obtenidos a nuestro modelo simplificado
                    var pokemon = new Pokemon
                    {
                        Name = pokemonData.Name,
                        FrontImage = pokemonData.Sprites.Other.Home.FrontDefault,
                        Ability = pokemonData.Abilities.FirstOrDefault()?.Ability.Name // Toma la primera habilidad
                    };

                    return pokemon;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
