using System.Text.Json.Serialization;

namespace YourNamespace.Models
{
    // Modelo simplificado que se devolverá en la API
    public class Pokemon
    {
        public string Name { get; set; }
        public string FrontImage { get; set; }
        public string Ability { get; set; }
    }

    // Modelo que representa la estructura completa de la respuesta de la API de PokeAPI
    public class PokemonData
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("abilities")]
        public AbilityWrapper[] Abilities { get; set; }

        [JsonPropertyName("sprites")]
        public Sprites Sprites { get; set; }
    }

    public class AbilityWrapper
    {
        [JsonPropertyName("ability")]
        public Ability Ability { get; set; }
    }

    public class Ability
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class Sprites
    {
        [JsonPropertyName("other")]
        public Other Other { get; set; }
    }

    public class Other
    {
        [JsonPropertyName("home")]
        public Home Home { get; set; }
    }

    public class Home
    {
        [JsonPropertyName("front_default")]
        public string FrontDefault { get; set; }
    }
}
