using System.ComponentModel.DataAnnotations.Schema;
using DSharpPlus;
using DSharpPlus.SlashCommands;

namespace Library.BotDiscord;

public class BattleCommands : ApplicationCommandModule
{
    static readonly BotQueuePlayers BotQueuePlayers = new BotQueuePlayers();

    [SlashCommand("JoinQueue", "Unirse a la cola de espera")]
    public async Task JoinQueue(InteractionContext ctx)
    {
        var mens = BotQueuePlayers.JoinQueue(ctx.User.Username);//capza cambiar guild
        await ctx.CreateResponseAsync(mens);
    }
    
    [SlashCommand("ExitQueue", "Salir de la cola de espera")]
    public async Task ExitQueue(InteractionContext ctx)
    {
        var mens = BotQueuePlayers.ExitQueue(ctx.User.Username);
        await ctx.CreateResponseAsync(mens);
    }
    
    [SlashCommand("Start", "Inicia una batalla con los primeros jugadores de la cola.")]
    public async Task Start(InteractionContext ctx)
    {
        var jugadores = BotQueuePlayers.ObtenerProximosJugadores();
        if (jugadores == null)
        {
            await ctx.CreateResponseAsync("No hay suficientes jugadores en la cola para iniciar una batalla.");
            return;
        }

        //agregar lógica de batalla
        var ganador = jugadores[0]; // Por ahora, asumimos que el primero gana
        await ctx.CreateResponseAsync($"¡La batalla entre {jugadores[0]} y {jugadores[1]} ha comenzado!\nEl ganador es {ganador}.");
    }

    [SlashCommand("ShowQueue", "Muestra la lista de jugadores en espera.")]
    public async Task ShowQueue(InteractionContext ctx)
    {
        var mens = BotQueuePlayers.MostrarJugadores();
        await ctx.CreateResponseAsync(mens);
    }
}