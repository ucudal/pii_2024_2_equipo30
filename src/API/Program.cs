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
        string pokemonName = "25";  
        // URL de la API de Pokémon
        string apiUrl = $"https://pokeapi.co/api/v2/pokemon/{pokemonName}";

        try
        {
            // Realiza una solicitud GET a la PokeAPI y deserializa la respuesta en un objeto Pokemon
            Pokemon pokemon = await client.GetFromJsonAsync<Pokemon>(apiUrl);
            
            if (pokemon != null && pokemon.Stats!=null && pokemon.Types !=null &&pokemon.Abilities!=null&&pokemon.Moves!=null)   
            {
                //Parámetros
                string Nombre = pokemon.Name;
                int Numero = pokemon.Id;
                int Vida = pokemon.Stats[0].base_stat;
                int Ataque = pokemon.Stats[1].base_stat;
                int Defensa = pokemon.Stats[2].base_stat;
                int AtaqueEspecial = pokemon.Stats[3].base_stat;
                int DefensaEspecial = pokemon.Stats[4].base_stat;
                List<Ability> ListaHabilidad = new List<Ability>();
                Type Tipo = new Type();
                Tipo.SetType(pokemon.Types[0].TypeDetail.Name);
                foreach (var ability in pokemon.Abilities)
                {
                    ListaHabilidad.Add(new Ability
                    {
                        AbilityDetail = ability.AbilityDetail
                    });
                }

                List<Move> ListMoves = new List<Move>();
                int counter = 0;
                foreach (var move in pokemon.Moves)
                {
                    if (counter >= 10) break;
                    {
                        MoveDetail moveDetails = await client.GetFromJsonAsync<MoveDetail>(move.MoveDetails.URL);
                        if (moveDetails.Accuracy != null && moveDetails.Power != null)
                        {
                            ListMoves.Add(new Move
                            {
                                MoveDetails = new MoveDetail
                                {
                                    Name = moveDetails.Name,
                                    Accuracy = moveDetails.Accuracy,
                                    Power = moveDetails.Power
                                }
                            });
                            counter++;
                        }
                    }
                }
                
                
                Pokemon pokemon1 = new Pokemon(Nombre, Numero, Vida,Ataque,Defensa, AtaqueEspecial, DefensaEspecial,Tipo,ListaHabilidad, ListMoves);

                Console.WriteLine($"Nombre: {pokemon1.Name}");
                Console.WriteLine($"ID: {pokemon1.Id}");
                Console.WriteLine($"Vida: {pokemon1.Health}");
                Console.WriteLine($"Ataque: {pokemon1.Attack}");
                Console.WriteLine($"Ataque: {pokemon1.Defense}");
                Console.WriteLine($"Ataque: {pokemon1.SpecialAttack}");
                Console.WriteLine($"Ataque: {pokemon1.SpecialDefense}");
                Console.WriteLine($"Tipo: {pokemon1.Tipo.TypeDetail.Name}");
                Console.WriteLine("Habilidades:");
                foreach (var habilidad in pokemon.Abilities)
                {
                    Console.WriteLine($"- {habilidad.AbilityDetail.Name}");
                }
                Console.WriteLine($"Efectividades: ");
                foreach (var diccionario in pokemon1.Tipo.Effectiveness)
                {
                    Console.WriteLine($"Tipo: {diccionario.Key}   -    Potenciador del ataque: {diccionario.Value}");
                }

                foreach (var move in ListMoves)
                {
                    Console.WriteLine($"Movimiento: {move.MoveDetails.Name}  - Precisión: {move.MoveDetails.Accuracy}  -  Daño:  {move.MoveDetails.Power}");
                }
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
