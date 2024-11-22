// See https://aka.ms/new-console-template for more information
using DSharpPlus;
using DSharpPlus.EventArgs;
using System;
using System.Threading.Tasks;

namespace BotDiscord
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Reemplaza esto con tu token
            string token = "MTMwNDIwMTYwMjE4MTIzNDczOQ.Gi_jKI.Pa8pvwJsaGVAxR6mzdm41C1EGN0Nrn1uDNpNn4";

            // Configurar el cliente de Discord
            var discord = new DiscordClient(new DiscordConfiguration
            {
                Token = token,
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.AllUnprivileged
            });

            // Registrar el evento de mensajes
            discord.MessageCreated += OnMessageCreated;

            // Conectar el bot
            await discord.ConnectAsync();
            Console.WriteLine("Bot está en línea. Presiona Ctrl+C para salir.");
            await Task.Delay(-1); // Mantener el programa en ejecución
        }

        // Método para manejar mensajes recibidos
        private static Task OnMessageCreated(DiscordClient sender, MessageCreateEventArgs e)
        {
            // Evitar que el bot responda a sus propios mensajes
            if (e.Author.IsBot) return Task.CompletedTask;

            // Verificar si el mensaje es "!ping"
            if (e.Message.Content.ToLower() == "!ping")
            {
                e.Message.RespondAsync("pong"); // Responder con "pong"
            }

            return Task.CompletedTask;
        }
    }
}
