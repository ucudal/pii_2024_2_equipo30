using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace API;
public class Program
{
    private static readonly HttpClient client = new HttpClient();

    public static async Task Main(string[] args)
    {
        // Nombre del Pokémon que quieres obtener
        string pokemonName = "2";  // Cambia esto por el nombre o ID de tu Pokémon

        // URL de la API de Pokémon
        string apiUrl = $"https://pokeapi.co/api/v2/pokemon/{pokemonName}";

        try
        {
            // Realiza una solicitud GET a la PokeAPI y deserializa la respuesta en un objeto Pokemon
            Pokemon pokemon = await client.GetFromJsonAsync<Pokemon>(apiUrl);

            if (pokemon != null)
            {
                string Nombre = pokemon.Name;
                int Altura = pokemon.Height;
                int Peso = pokemon.Weight;
                int Numero = pokemon.Id;
                int Orden = pokemon.Order;
                List<Ability> ListaHabilidad = new List<Ability>();
                
                Console.WriteLine($"Nombre: {pokemon.Name}");
                Console.WriteLine($"Altura: {pokemon.Height}");
                Console.WriteLine($"Peso: {pokemon.Weight}");
                Console.WriteLine($"Número: {pokemon.Id}");
                Console.WriteLine($"Orden: {pokemon.Order}");
                Console.WriteLine("Tipo/s:");
                if (pokemon.Types != null)
                {
                    foreach (var type in pokemon.Types)
                    {
                        Console.WriteLine($"- {type.TypeDetail.Name}");
                    }
                }
                foreach (var habilidad in ListaHabilidad)
                {
                    Console.WriteLine($"aaaaaaaaaaaaaaaaa{habilidad.Name}");
                }

                // Imprimir las habilidades del Pokémon
                Console.WriteLine("Habilidades:");
                if (pokemon.Abilities != null)
                {
                    foreach (var ability in pokemon.Abilities)
                    {
                        ListaHabilidad.Add(new Ability(ability.AbilityDetail.Name));
                        Console.WriteLine($"- {ability.AbilityDetail.Name}");
                    }
                }
                else
                {
                    Console.WriteLine("No se encontraron habilidades.");
                }
            }
            else
            {
                Console.WriteLine("No se pudo obtener el Pokémon.");
            }
            Console.WriteLine(("Stats:"));
            if (pokemon.Stats != null)
            {
                Console.WriteLine($"- {pokemon.Stats[0].StatsDetail.Name}:  {pokemon.Stats[0].base_stat}");
                Console.WriteLine($"- {pokemon.Stats[1].StatsDetail.Name}:  {pokemon.Stats[1].base_stat}");
                Console.WriteLine($"- {pokemon.Stats[2].StatsDetail.Name}:  {pokemon.Stats[2].base_stat}");
                Console.WriteLine($"- {pokemon.Stats[3].StatsDetail.Name}:  {pokemon.Stats[3].base_stat}");
            }
            else
            {
                Console.WriteLine($"No se han encontrado stats");
            }
            
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("Hubo un problema al realizar la solicitud:");
            Console.WriteLine(e.Message);
        }
        
    }
}
