using System.ComponentModel.DataAnnotations.Schema;
using DSharpPlus;
using DSharpPlus.SlashCommands;

namespace Library.BotDiscord;

public class BattleCommands : ApplicationCommandModule
{
    private readonly PokemonService _pokemonService = new PokemonService();

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
        var mens = BotQueuePlayers.GetInstance().JoinQueue(player);
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
        var mens = BotQueuePlayers.GetInstance().ExitQueue(player);
        await ctx.CreateResponseAsync(mens);
    }
    
    [SlashCommand("StartBattle", "Inicia una batalla.")]
    public async Task StartBattle(InteractionContext ctx)
    {
        await BotQueuePlayers.GetInstance().Battle.StartBattle(ctx);
    }
    
    [SlashCommand("StartGame", "Inicia el juego con los primeros jugadores de la cola.")]
    public async Task StartGame(InteractionContext ctx)
    {
        if (BotQueuePlayers.GetInstance().Battle != null)
        {
            await ctx.CreateResponseAsync("Ya hay jugadores en batalla. Espera a que termine.");
            return;
        }
        BotQueuePlayers.GetInstance().PlayersInGame = BotQueuePlayers.GetInstance().ObtenerProximosJugadores();
        if (BotQueuePlayers.GetInstance().PlayersInGame == null)
        {
            await ctx.CreateResponseAsync("No hay suficientes jugadores en la cola para iniciar el juego.");
            return;
        }

        BotQueuePlayers.GetInstance().Battle = new Battle(BotQueuePlayers.GetInstance().PlayersInGame[0], BotQueuePlayers.GetInstance().PlayersInGame[1]);

        await BotQueuePlayers.GetInstance().Battle.StartGame(ctx);
    }

    [SlashCommand("ShowQueue", "Muestra la lista de jugadores en espera.")]
    public async Task ShowQueue(InteractionContext ctx)
    {
        var mens = BotQueuePlayers.GetInstance().MostrarJugadores();
        await ctx.CreateResponseAsync(mens);
    }

    [SlashCommand("Choose", "Permite elegir un Pokémon.")]
    public async Task Choose(InteractionContext ctx,
        [Option("Pokemon", "Ingrese el nombre o ID del Pokémon.")] string pokemonName)
    {
        if (BotQueuePlayers.GetInstance().PlayersInGame == null)
        {
            await ctx.CreateResponseAsync("La partida aun no ha comenzado.");
            return;
        }
        
        var player = BotQueuePlayers.GetInstance().GetPlayerByName(ctx.Member.Username, BotQueuePlayers.GetInstance().PlayersInGame);
        if (player == null)
        {
            await ctx.CreateResponseAsync($"Debes esperar tu turno. Es el turno de {BotQueuePlayers.GetInstance().Battle.shift.actualPlayer}");
            return;
        }
        if (player.Team.Count == 6)
        {
            await ctx.CreateResponseAsync($"{ctx.User.Username} ha completado la selección de sus 6 Pokémon.");
            return;
        }

        var pokemon = await _pokemonService.PokemonElection(pokemonName, ctx);
        if (pokemon == null)
        {
            await ctx.CreateResponseAsync($"El pokemon {pokemonName} no se ha encontrado.");
            return;
        }
        
        player.Team.Add(pokemon);
        if (player.Team.Count == 6)
        {
            player.actualPokemon = player.Team[0];
        }
        await ctx.Channel.SendMessageAsync($"¡El Pokémon {pokemon.Name} ha sido elegido con éxito!  {player.NamePlayer} ha seleccionado a {pokemon.Name} con {pokemon.Health} puntos de salud.");
        await ctx.CreateResponseAsync($"Tienes {player.Team.Count} Pokémon(s) en tu equipo.");
    }
    
    [SlashCommand("Switch", "Cambia el pokemon actual.")]
    public async Task Switch(InteractionContext ctx,
        [Option("PokemonIndex", "Ingrese el indice del Pokémon.")] string pokemonIndex)
    {
        if (BotQueuePlayers.GetInstance().PlayersInGame == null)
        {
            await ctx.CreateResponseAsync("La partida aun no ha comenzado.");
            return;
        }
        var player = BotQueuePlayers.GetInstance().GetPlayerByName(ctx.Member.Username, BotQueuePlayers.GetInstance().PlayersInGame);
        if (player == null)
        {
            await ctx.CreateResponseAsync($"Debes esperar tu turno. Es el turno de {BotQueuePlayers.GetInstance().Battle.shift.actualPlayer}");
            return;
        }

        BotQueuePlayers.GetInstance().Battle.SwitchPokemon(BotQueuePlayers.GetInstance().Battle.shift.actualPlayer, int.Parse(pokemonIndex), ctx);
    }
    
    [SlashCommand("SelectItem", "Selecciona un item del inventario")]
    public async Task SelectItem(InteractionContext ctx,
        [Option("ItemNumber", "Ingrese el número del item a usar.")] string itemNumber,
        [Option("PokemonName", "Ingrese el nombre del Pokémon.")] string pokemonName)
    {
        if (BotQueuePlayers.GetInstance().PlayersInGame == null)
        {
            await ctx.CreateResponseAsync("La partida aun no ha comenzado.");
            return;
        }
        
        var player = BotQueuePlayers.GetInstance().GetPlayerByName(ctx.Member.Username, BotQueuePlayers.GetInstance().PlayersInGame);
        if (player == null)
        {
            await ctx.CreateResponseAsync($"Debes esperar tu turno. Es el turno de {BotQueuePlayers.GetInstance().Battle.shift.actualPlayer}");
            return;
        }
        
        BotQueuePlayers.GetInstance().Battle.UseItem(BotQueuePlayers.GetInstance().Battle.shift.actualPlayer, int.Parse(itemNumber), pokemonName, ctx);
    }
    
    [SlashCommand("Attack", "Ataca a un pokemon")]
    public async Task Attack(InteractionContext ctx,
        [Option("MoveNumber", "Ingrese el número del movimiento a usar.")] string moveNumber)
    {
        if (BotQueuePlayers.GetInstance().PlayersInGame == null)
        {
            await ctx.CreateResponseAsync("La partida aun no ha comenzado.");
            return;
        }
        
        var player = BotQueuePlayers.GetInstance().GetPlayerByName(ctx.Member.Username, BotQueuePlayers.GetInstance().PlayersInGame);
        if (player == null)
        {
            await ctx.CreateResponseAsync($"Debes esperar tu turno. Es el turno de {BotQueuePlayers.GetInstance().Battle.shift.actualPlayer}");
            return;
        }
        
        BotQueuePlayers.GetInstance().Battle.Attack(BotQueuePlayers.GetInstance().Battle.shift.actualPlayer, BotQueuePlayers.GetInstance().Battle.shift.enemyPlayer, int.Parse(moveNumber), ctx);
    }
}