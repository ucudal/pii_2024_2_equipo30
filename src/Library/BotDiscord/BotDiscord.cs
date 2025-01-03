﻿using DSharpPlus;
using DSharpPlus.SlashCommands;
namespace Library.BotDiscord
{
    /// <summary>
    /// Clase para la inicialización y configuración del bot de Discord.
    /// Utiliza DSharpPlus para conectarse a Discord y permitir utilizar comandos.
    /// </summary>
    public class DiscordBot
    {
        /// <summary>
        /// Cliente de Discord utilizado para conectar y gestionar el bot.
        /// </summary>
        private DiscordClient Client;
        
        /// <summary>
        /// Extensión de comandos de barra para manejar interacciones de comandos con el bot.
        /// </summary>
        private SlashCommandsExtension SlashCommands;
        
        /// <summary>
        /// Método principal que configura y conecta el bot de Discord.
        /// </summary>
        /// <remarks>
        /// Obtiene el token del bot desde la variable de entorno <c>DISCORD_TOKEN</c> y configura
        /// el cliente de Discord utilizando <c>DiscordConfiguration</c> proporcionada por <c>DSharpPlus</c>
        /// y aplica los comandos del bot de discord pasandole a <c>SlashCommands</c> la clase <c>DiscordCommands</c>.
        /// </remarks>
        /// <exception cref="Exception">Lanzada cuando no se encuentra el token del bot en la variable de entorno <c>DISCORD_TOKEN</c>.</exception>
        public async Task Iniciate()
        {
            var token = Environment.GetEnvironmentVariable("DISCORD_TOKEN") 
                        ?? throw new Exception("No se ha encontrado el token del bot de discord.");
            var discordConfig = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All, 
                Token = token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                HttpTimeout = TimeSpan.FromSeconds(60)
            };
            Client = new DiscordClient(discordConfig);
            Client.Ready += async (sender, e) =>//basicamente para saber si el bot logro conectarse con el gateway de ds
            {
                Console.WriteLine("¡El bot está conectado y listo para recibir comandos!");
            };
            
            SlashCommands = Client.UseSlashCommands();
            
            
            SlashCommands.RegisterCommands<BotDiscord.BattleCommands>();
            SlashCommands.RegisterCommands<BotDiscord.BotQueuePlayers>();
            SlashCommands.RegisterCommands<BotDiscord.BotCommands>();
            await Client.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}