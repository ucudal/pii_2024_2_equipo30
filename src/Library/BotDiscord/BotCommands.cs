using DSharpPlus.SlashCommands;

namespace Library.BotDiscord;

public class BotCommands : ApplicationCommandModule
{
    [SlashCommand("Ping", "Estado del bot")]
    public async Task PingCommand(InteractionContext ctx)
    {
        await ctx.CreateResponseAsync($"Pong! Latencia: {ctx.Client.Ping}");
        
    }

    [SlashCommand("Hello", "Saludo")]
    public async Task HelloCommand(InteractionContext ctx)
    {
        await ctx.CreateResponseAsync($"Hola {ctx.User.Username}!");
    }
}