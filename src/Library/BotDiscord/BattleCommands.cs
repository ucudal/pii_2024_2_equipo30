using System.ComponentModel.DataAnnotations.Schema;
using DSharpPlus;
using DSharpPlus.SlashCommands;

namespace Library.BotDiscord;

public class BattleCommands : ApplicationCommandModule
{
    static readonly BotQueuePlayers BotQueuePlayers = new BotQueuePlayers();

    // Diccionario para mapear usuarios de Discord a instancias de Player
    static readonly Dictionary<string, Player> PlayersRegistry = new Dictionary<string, Player>();

    [SlashCommand("JoinQueue", "Unirse a la cola de espera")]
    public async Task JoinQueue(InteractionContext ctx)
    {
        string username = ctx.User.Username;

        // Verificar si el jugador ya existe en el registro
        if (!PlayersRegistry.TryGetValue(username, out Player player))
        {
            // Crear un nuevo jugador y agregarlo al registro
            player = new Player(username);
            PlayersRegistry[username] = player;
        }

        // Intentar unir al jugador a la cola
        var mens = BotQueuePlayers.JoinQueue(player);
        await ctx.CreateResponseAsync(mens);
    }

    [SlashCommand("ExitQueue", "Salir de la cola de espera")]
    public async Task ExitQueue(InteractionContext ctx)
    {
        string username = ctx.User.Username;

        // Verificar si el jugador existe en el registro
        if (!PlayersRegistry.TryGetValue(username, out Player player))
        {
            await ctx.CreateResponseAsync($"{username}, no estás registrado como jugador.");
            return;
        }

        // Intentar sacar al jugador de la cola
        var mens = BotQueuePlayers.ExitQueue(player);
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

        Battle battle = new Battle(jugadores[0], jugadores[1]);
        await battle.StartBattle(ctx);
        //agregar lógica de batalla
        // var ganador = jugadores[0]; // Por ahora, asumimos que el primero gana
        // await ctx.CreateResponseAsync($"¡La batalla entre {jugadores[0]} y {jugadores[1]} ha comenzado!\nEl ganador es {ganador}.");
    }

    [SlashCommand("ShowQueue", "Muestra la lista de jugadores en espera.")]
    public async Task ShowQueue(InteractionContext ctx)
    {
        var mens = BotQueuePlayers.MostrarJugadores();
        await ctx.CreateResponseAsync(mens);
    }
}