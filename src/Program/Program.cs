using System.Threading.Channels;
using Library;
using Discord;
using Discord.WebSocket;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.Intrinsics.Arm;
using System.Text.Json.Serialization;
namespace Program;

class Program
{
    /*private static HttpClient client = new HttpClient();

    public static async Task Main(string[] args)
    {
        {
            var bot = new Bot();
            await bot.StartAsync();

            await Task.Delay(-1);
        }
        PokemonApi pokemonApi = new PokemonApi(client);
        PokemonCreator pokemonCreator = new PokemonCreator(pokemonApi);

        List<Pokemon> listPokemon = new List<Pokemon>();
        for (int i = 0; i < 6; i++)
        {
            Console.WriteLine("Ingrese un nombre o un ID de un pokemon: ");
            string pokemonId = Console.ReadLine();
            var pokemon = await pokemonCreator.CreatePokemon(pokemonId);
            if (pokemon != null)
            {
                listPokemon.Add(pokemon);
            }
            else
            {
                Console.WriteLine($"No se pudo obtener datos para: {pokemonId}");
            }
        }

        if (listPokemon.Count == 0)
        {
            Console.WriteLine("No se pudo obtener ningún Pokémon.");
            return;
        }

        // Crear jugadores
        Jugador jugador1 = new Jugador("Jugador 1", listPokemon);
        Jugador jugador2 = new Jugador("Jugador 2", listPokemon);

        // Crear y manejar la batalla
        var batalla = new Batalla(jugador1, jugador2);
        batalla.IniciarBatalla();
    }
    class Bot
    {
        private readonly DiscordSocketClient _client;

        public Bot()
        {
            _client = new DiscordSocketClient();
            _client.Log += Log;
            _client.MessageReceived += MessageReceived;
        }

        public async Task StartAsync()
        {
           //Aca va el to k e n

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
        }

        private Task Log(LogMessage log)
        {
            Console.WriteLine(log);
            return Task.CompletedTask;
        }

        private async Task MessageReceived(SocketMessage arg)
        {
            if (arg.Author.IsBot)
                return;

            if (arg.Content == "!ping")
            {
                await arg.Channel.SendMessageAsync("Pong!");
            }

            else if (arg.Content == "!hello")
            {
                await arg.Channel.SendMessageAsync("¡Hola, soy tu bot!");
            }
        }*/
}




