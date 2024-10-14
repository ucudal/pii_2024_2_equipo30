using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.Common;
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
                //Parámetros
                string Nombre = pokemon.Name;
                int Altura = pokemon.Height;
                int Peso = pokemon.Weight;
                int Numero = pokemon.Id;
                int Orden = pokemon.Order;
                List<Ability> ListaHabilidad = new List<Ability>();
                List<Type> ListaTipos = new List<Type>();
                List<Dictionary<string, int>> ListaDiccionarios = new List<Dictionary<string, int>>();
                
                
                Console.WriteLine($"Nombre: {pokemon.Name}");
                Console.WriteLine($"Altura: {pokemon.Height}");
                Console.WriteLine($"Peso: {pokemon.Weight}");
                Console.WriteLine($"Número: {pokemon.Id}");
                Console.WriteLine($"Orden: {pokemon.Order}");
                Console.WriteLine("Tipo/s:");
                
                //Mostrar el nombre y el valor de las stats del pokemon (No todas las stats)
                if (pokemon.Stats != null)
                {
                    for (int i = 0; i < pokemon.Stats.Count-2; i++)
                    {
                        Dictionary<string, int> Stats = new Dictionary<string, int>();
                        Stats.Add(pokemon.Stats[i].StatsDetail.Name,pokemon.Stats[i].base_stat );
                        ListaDiccionarios.Add(Stats);
                    }
                }
                else
                {
                    Console.WriteLine($"No se han encontrado stats");
                }
                // Mostrar tipos de pokemon y agregarlos a la lista
                if (pokemon.Types != null)
                {
                    foreach (var type in pokemon.Types)
                    {
                        ListaTipos.Add(new Type(type.TypeDetail.Name));
                        Console.WriteLine($"- {type.TypeDetail.Name}");
                    }
                }

                // Imprimir las habilidades del Pokémon y agregarlas a la lista
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
                
                //Mostrar los nombres de las habilidades en la Lista hecha previamente
                foreach (var diccionario in ListaDiccionarios)
                {
                    foreach (var stat in diccionario)
                    {
                        Console.WriteLine($"Nombre: {stat.Key}, Valor: {stat.Value}");
                    }
                }
                Pokemon pokemon1 = new Pokemon(Nombre, Altura, Peso, Numero, Orden, ListaHabilidad, ListaTipos, ListaDiccionarios);
            }
            else
            {
                Console.WriteLine("No se pudo obtener el Pokémon.");
            }

        }
        
        catch (HttpRequestException e)
        {
            Console.WriteLine("Hubo un problema al realizar la solicitud:");
            Console.WriteLine(e.Message);
        }
        
    }
}
