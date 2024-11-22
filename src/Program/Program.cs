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

/*class Program
{
    private static async Task Main(string[] args)
    {
        var mainApp = new BotTester(); // Instancia de tu programa principal
        var bot = new Bot(mainApp); // Pasamos la instancia al bot
        await bot.StartAsync();

        await Task.Delay(-1);
    }

    class Bot
    {
        private readonly DiscordSocketClient _client;
        private readonly BotTester _botTester; // Referencia a la clase principal

        public Bot(BotTester botTester)
        {
            _botTester = botTester; // Guardamos la instancia para usarla luego
            _client = new DiscordSocketClient();
            _client.Log += Log;
            _client.MessageReceived += MessageReceived;
        }

        public async Task StartAsync()
        {
            string token = "";

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

            else if (arg.Content.StartsWith("!hello"))
            {
                string userName = arg.Author.Username;
                string greeting = _botTester.GetGreeting(userName); // Lógica del programa principal
                await arg.Channel.SendMessageAsync(greeting);
            }

            else if (arg.Content.StartsWith("!add"))
            {
                // Ejemplo de comando "!add 5 10"
                var parts = arg.Content.Split(' ');
                if (parts.Length == 3 && int.TryParse(parts[1], out int a) && int.TryParse(parts[2], out int b))
                {
                    string result = _botTester.PerformCalculation(a, b); // Lógica del programa principal
                    await arg.Channel.SendMessageAsync(result);
                }
                else
                {
                    await arg.Channel.SendMessageAsync("Uso: !add <número1> <número2>");
                }
            }
        }
    }
}*/

