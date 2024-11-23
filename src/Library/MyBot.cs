namespace Library
{
    using Discord;
    using Discord.WebSocket;
    using System.Collections.Generic;

    public class MyBot
    {
        private readonly DiscordSocketClient _client;
        private readonly BotController _botController;  // Instancia de BotController

        private Player player1;
        private Player player2;
        private BattleSystem battleSystem;

        public MyBot()
        {
            _client = new DiscordSocketClient();

            // Creación de jugadores, pasando al menos los nombres
            player1 = new Player("Player1", new List<Pokemon>());  // Nombre y lista vacía de Pokémon
            player2 = new Player("Player2", new List<Pokemon>());  // Lo mismo para player2

            // Crea el sistema de batalla
            battleSystem = new BattleSystem(player1, player2);

            // Inicializa el BotController con BattleSystem y los jugadores
            _botController = new BotController(battleSystem, player1, player2);
        }

        public async Task StartBotAsync()
        {
            _client.MessageReceived += OnMessageReceived;
            await _client.LoginAsync(TokenType.Bot, "your-bot-token"); // Cambia por tu token real
            await _client.StartAsync();
            await Task.Delay(-1); // Mantener el bot corriendo
        }

        public async Task OnMessageReceived(SocketMessage arg)
        {
            if (arg is SocketUserMessage message)
            {
                var channel = message.Channel as ISocketMessageChannel;
                if (channel != null)
                {
                    // Aquí pasas el canal real a tu sistema de batalla
                    await _botController.ProcessCommand(message.Content, channel);  // Llamas a ProcessCommand usando _botController
                }
            }
        }
    }
}