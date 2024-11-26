using System.ComponentModel.DataAnnotations.Schema;
using DSharpPlus;
using DSharpPlus.SlashCommands;

namespace Library.BotDiscord;

public class BattleCommands : ApplicationCommandModule
{
    private readonly PokemonService _pokemonService = new PokemonService();
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
            player = new Player(ctx.Member, username);
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

    /*[SlashCommand("Start", "Inicia una batalla con los primeros jugadores de la cola.")]
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
    }*/

    [SlashCommand("ShowQueue", "Muestra la lista de jugadores en espera.")]
    public async Task ShowQueue(InteractionContext ctx)
    {
        var mens = BotQueuePlayers.MostrarJugadores();
        await ctx.CreateResponseAsync(mens);
    }

    [SlashCommand("Choose", "Permite elegir un Pokémon.")]
    public async Task ChoosePokemon(InteractionContext ctx,
        [Option("Pokemon", "Ingrese el nombre o ID del Pokémon.")] string pokemonName)
    {
        var player =_pokemonService.PokemonElection(ctx.Member.Username,InteractionContext ctx); // Utiliza el ID del usuario para obtener el jugador
        if (player == null)
        {
            await ctx.CreateResponseAsync("Jugador no encontrado.");
            return;
        }

        var pokemon = await _pokemonService.PokemonElection(pokemonName, ctx);
        if (pokemon != null)
        {
            player.TempTeam.Add(pokemon); // Agregar el Pokémon a la lista temporal
            await ctx.CreateResponseAsync($"¡El Pokémon {pokemon.Name} ha sido elegido con éxito! Tienes {player.TempTeam.Count} Pokémon(s) en tu equipo temporal.");
        }
        else
        {
            await ctx.CreateResponseAsync("No se pudo elegir el Pokémon, intenta nuevamente.");
        }

        if (player.TempTeam.Count == 6)
        {
            await ctx.Channel.SendMessageAsync($"{ctx.User.Username} ha completado la selección de sus 6 Pokémon.");
        }
    }
    [SlashCommand("SelectItem", "Selecciona un item del inventario")]
    public async Task SelecItem(InteractionContext ctx)
    {

    }

    [SlashCommand("TotalCure", "Usar la poscion TotalCure para eleminar efectos de estado")]
    public async Task Totalcure_Bot(InteractionContext ctx)
    {

    }
    
    [SlashCommand("Revive", "Usa el item revive para revivir un pokemon muerto")]
    public async Task Revive_Bot(InteractionContext ctx)
    {
        
    }
    
    [SlashCommand("SuperPotion", "Usa la pocion SuperPotion para curar a un pokemon")]
    public async Task SuperPotion_bot(InteractionContext ctx)
    {
        
    }


}