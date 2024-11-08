using System.Threading.Channels;
using Library;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.Intrinsics.Arm;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Program
{
    class Program
    {
        private static HttpClient client = new HttpClient();

        public static async Task Main(string[] args)
        {
            PokemonApi pokemonApi = new PokemonApi(client);
            PokemonCreator pokemonCreator = new PokemonCreator(pokemonApi);

            // Crear listas de Pokémon para ambos jugadores
            List<Pokemon> listPokemonJugador1 = new List<Pokemon>();
            List<Pokemon> listPokemonJugador2 = new List<Pokemon>();

            Console.WriteLine("\n==================== SELECCIÓN DE POKÉMON ====================\n");

            // Selección de Pokémon para el Jugador 1
            Console.WriteLine("Selección de Pokémon para Jugador 1:\n");
            for (int i = 0; i < 6; i++)
            {
                bool pokemonAgregado = false;
                while (!pokemonAgregado)
                {
                    try
                    {
                        Console.WriteLine("Jugador 1, ingrese un nombre o un ID de un Pokémon: ");
                        string pokemonId = Console.ReadLine();
                        var response = await client.GetAsync($"https://pokeapi.co/api/v2/pokemon/{pokemonId.ToLower()}");
                        if (response.IsSuccessStatusCode)
                        {
                            var responseBody = await response.Content.ReadAsStringAsync();
                            var pokemonData = JsonSerializer.Deserialize<JsonElement>(responseBody);
                            string pokemonName = pokemonData.GetProperty("name").GetString();

                            var pokemon = await pokemonCreator.CreatePokemon(pokemonId);
                            listPokemonJugador1.Add(pokemon);
                            Console.WriteLine($" Has seleccionado a: {pokemonName}\n");
                            pokemonAgregado = true;
                        }
                        else
                        {
                            Console.WriteLine($"No se pudo obtener datos para: {pokemonId}. Por favor, intente nuevamente.\n");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ocurrió un error al intentar obtener el Pokémon: {ex.Message}. Por favor, intente nuevamente.\n");
                    }
                }
            }

            // Selección de Pokémon para el Jugador 2
            Console.WriteLine("\nSelección de Pokémon para Jugador 2:\n");
            for (int i = 0; i < 6; i++)
            {
                bool pokemonAgregado = false;
                while (!pokemonAgregado)
                {
                    try
                    {
                        Console.WriteLine("Jugador 2, ingrese un nombre o un ID de un Pokémon: ");
                        string pokemonId = Console.ReadLine();
                        var response = await client.GetAsync($"https://pokeapi.co/api/v2/pokemon/{pokemonId.ToLower()}");
                        if (response.IsSuccessStatusCode)
                        {
                            var responseBody = await response.Content.ReadAsStringAsync();
                            var pokemonData = JsonSerializer.Deserialize<JsonElement>(responseBody);
                            string pokemonName = pokemonData.GetProperty("name").GetString();

                            var pokemon = await pokemonCreator.CreatePokemon(pokemonId);
                            listPokemonJugador2.Add(pokemon);
                            Console.WriteLine($" Has seleccionado a: {pokemonName}\n");
                            pokemonAgregado = true;
                        }
                        else
                        {
                            Console.WriteLine($"No se pudo obtener datos para: {pokemonId}. Por favor, intente nuevamente.\n");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ocurrió un error al intentar obtener el Pokémon: {ex.Message}. Por favor, intente nuevamente.\n");
                    }
                }
            }

            Console.WriteLine("\n============================================================\n");

            // Validar si ambos jugadores tienen al menos un Pokémon
            if (listPokemonJugador1.Count == 0 || listPokemonJugador2.Count == 0)
            {
                Console.WriteLine("No se pudo obtener suficientes Pokémon para ambos jugadores.");
                return;
            }

            // Crear jugadores
            Jugador jugador1 = new Jugador("Jugador 1", listPokemonJugador1);
            Jugador jugador2 = new Jugador("Jugador 2", listPokemonJugador2);

            // Crear y manejar la batalla
            var batalla = new Batalla(jugador1, jugador2);
            Console.WriteLine("\n==================== INICIANDO BATALLA ====================\n");
            batalla.IniciarBatalla();
        }
    }
}