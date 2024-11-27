using Library.BotDiscord;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;

namespace Library
{
    /// <summary>
    /// Servicio que proporciona funcionalidades relacionadas con la selección y gestión de Pokémon.
    /// </summary>
    public class PokemonService
    {
        /// <summary>
        /// Cliente HTTP estático utilizado para realizar solicitudes a la API de Pokémon.
        /// </summary>
        private static readonly HttpClient client = new HttpClient();

        /// <summary>
        /// Método para seleccionar un Pokémon utilizando su identificador.
        /// </summary>
        /// <param name="pokemonId">El identificador del Pokémon que se desea seleccionar (nombre o número).</param>
        /// <param name="ctx">El contexto de la interacción en Discord.</param>
        /// <returns>Una tarea que representa la operación asincrónica, que contiene el Pokémon seleccionado si la operación es exitosa, de lo contrario devuelve null.</returns>
        public async Task<Pokemon> PokemonElection(string pokemonId, InteractionContext ctx)
        {
            // Inicializa los objetos para interactuar con la API y crear un Pokémon.
            PokemonApi pokemonApi = new PokemonApi(client);
            PokemonCreator pokemonCreator = new PokemonCreator(pokemonApi);

            // Mensaje de bienvenida para la selección de Pokémon.
            string message = "\n\n" +
                             "==================== SELECCIÓN DE POKÉMON ====================\n" +
                             $"Selección de Pokémon para el jugador {ctx.User.Username}:\n";

            try
            {
                // Realiza una solicitud GET a la API de Pokémon con el identificador proporcionado.
                var response = await client.GetAsync($"https://pokeapi.co/api/v2/pokemon/{pokemonId}");
                
                // Verifica si la respuesta fue exitosa.
                if (response.IsSuccessStatusCode)
                {
                    // Crea el Pokémon con el identificador proporcionado.
                    var pokemon = await pokemonCreator.CreatePokemon(pokemonId);
                    message += $"Has seleccionado a: {pokemon.Name}\n";
                    await ctx.Channel.SendMessageAsync(message);
                    return pokemon;
                }
                else
                {
                    message += $"No se pudo obtener datos para: {pokemonId}. Por favor, intenta nuevamente.\n";
                }
            }
            catch (Exception ex)
            {
                // Captura y maneja las excepciones ocurridas durante la solicitud.
                message += $"Ocurrió un error al intentar obtener el Pokémon: {ex.Message}. Por favor, intente nuevamente.\n";
            }

            // Envío del mensaje final al canal de Discord.
            await ctx.Channel.SendMessageAsync(message);
            return null;
        }
    }
}
