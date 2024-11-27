using DSharpPlus.SlashCommands;

namespace Library.BotDiscord
{
    /// <summary>
    /// Clase que contiene los comandos básicos del bot para interactuar con los usuarios de Discord.
    /// Hereda de ApplicationCommandModule de DSharpPlus.
    /// </summary>
    public class BotCommands : ApplicationCommandModule
    {
        /// <summary>
        /// Comando que responde con "Pong!" e indica la latencia actual del bot.
        /// </summary>
        /// <param name="ctx">El contexto de la interacción en Discord.</param>
        [SlashCommand("Ping", "Estado del bot")]
        public async Task PingCommand(InteractionContext ctx)
        {
            await ctx.CreateResponseAsync($"Pong! Latencia: {ctx.Client.Ping}");
        }

        /// <summary>
        /// Comando que responde con un saludo al usuario que lo invoca.
        /// </summary>
        /// <param name="ctx">El contexto de la interacción en Discord.</param>
        [SlashCommand("Hello", "Saludo")]
        public async Task HelloCommand(InteractionContext ctx)
        {
            await ctx.CreateResponseAsync($"Hola {ctx.User.Username}!");
        }
    }
}