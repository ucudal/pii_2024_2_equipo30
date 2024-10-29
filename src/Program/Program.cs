namespace Program;
using Library;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.Intrinsics.Arm;
using System.Text.Json.Serialization;



class Program
{
    private static readonly HttpClient client = new HttpClient(); 
    public static async Task Main(string[] args)
        {
            string pokemonName = "25";  
            // URL de la API de Pokémon
            string apiUrl = $"https://pokeapi.co/api/v2/pokemon/{pokemonName}";
            try
            {
                Pokemon pokemon = await client.GetFromJsonAsync<Pokemon>(apiUrl);
                if (pokemon.Types != null && pokemon.Stats != null && pokemon.Moves != null)
                {
                    //Parámetros
                    string Nombre = pokemon.Name;
                    int Numero = pokemon.Id;
                    int Vida = pokemon.Stats[0].base_stat;
                    int Ataque = pokemon.Stats[1].base_stat;
                    int Defensa = pokemon.Stats[2].base_stat;
                    int AtaqueEspecial = pokemon.Stats[3].base_stat;
                    int DefensaEspecial = pokemon.Stats[4].base_stat;
                    Library.Type Tipo = new Library.Type();
                    Tipo.SetType(pokemon.Types[0].TypeDetail.Name);
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

                }
            }
            catch(HttpRequestException e)
            {
                Console.WriteLine("Hubo un problema al realizar la solicitud:");
                Console.WriteLine(e.Message);
            }
            // Crear dos jugadores
            Jugador jugador1 = new Jugador("Jugador 1", pokemonsDisponibles.ObtenerEquipoAleatorio());
            Jugador jugador2 = new Jugador("Jugador 2", pokemonsDisponibles.ObtenerEquipoAleatorio());

            // Crear la batalla
            Batalla batalla = new Batalla(jugador1, jugador2);
                
            // Iniciar la batalla
            batalla.IniciarBatalla();
        }
     
}



