using System.ComponentModel.DataAnnotations.Schema;
using DSharpPlus;
using DSharpPlus.SlashCommands;

namespace Library.BotDiscord
{
    /// <summary>
    /// Clase que contiene los comandos para manejar las batallas y las colas de espera en Discord.
    /// Hereda de ApplicationCommandModule de DSharpPlus.
    /// </summary>
    public class BattleCommands : ApplicationCommandModule
    {
        /// <summary>
        /// Servicio de Pokémon para la interacción con la API y la selección de Pokémon.
        /// </summary>
        private readonly PokemonService _pokemonService = new PokemonService();

        /// <summary>
        /// Diccionario que mapea los usuarios de Discord a instancias de Player.
        /// </summary>
        static readonly Dictionary<string, Player> PlayersRegistry = new Dictionary<string, Player>();

        /// <summary>
        /// Comando para unirse a la cola de espera.
        /// </summary>
        /// <param name="ctx">El contexto de la interacción en Discord.</param>
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

        /// <summary>
        /// Comando para salir de la cola de espera.
        /// </summary>
        /// <param name="ctx">El contexto de la interacción en Discord.</param>
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

        /// <summary>
        /// Comando para iniciar una batalla.
        /// </summary>
        /// <param name="ctx">El contexto de la interacción en Discord.</param>
        [SlashCommand("StartBattle", "Inicia una batalla.")]
        public async Task StartBattle(InteractionContext ctx)
        {
            await BotQueuePlayers.GetInstance().Battle.StartBattle(ctx);
        }

        /// <summary>
        /// Comando para iniciar el juego con los primeros jugadores en la cola.
        /// </summary>
        /// <param name="ctx">El contexto de la interacción en Discord.</param>
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

        /// <summary>
        /// Comando para mostrar la lista de jugadores en espera.
        /// </summary>
        /// <param name="ctx">El contexto de la interacción en Discord.</param>
        [SlashCommand("ShowQueue", "Muestra la lista de jugadores en espera.")]
        public async Task ShowQueue(InteractionContext ctx)
        {
            var mens = BotQueuePlayers.GetInstance().MostrarJugadores();
            await ctx.CreateResponseAsync(mens);
        }

        /// <summary>
        /// Comando para elegir un Pokémon.
        /// </summary>
        /// <param name="ctx">El contexto de la interacción en Discord.</param>
        /// <param name="pokemonName">Nombre o ID del Pokémon que se desea seleccionar.</param>
        [SlashCommand("Choose", "Permite elegir un Pokémon.")]
        public async Task Choose(InteractionContext ctx, [Option("Pokemon", "Ingrese el nombre o ID del Pokémon.")] string pokemonName)
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
                await ctx.CreateResponseAsync($"El Pokémon {pokemonName} no se ha encontrado.");
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

        /// <summary>
        /// Comando para cambiar el Pokémon actual.
        /// </summary>
        /// <param name="ctx">El contexto de la interacción en Discord.</param>
        /// <param name="pokemonIndex">Índice del Pokémon al que se desea cambiar.</param>
        [SlashCommand("Switch", "Cambia el Pokémon actual.")]
        public async Task Switch(InteractionContext ctx, [Option("PokemonIndex", "Ingrese el índice del Pokémon.")] string pokemonIndex)
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

        /// <summary>
        /// Comando para seleccionar un ítem del inventario.
        /// </summary>
        /// <param name="ctx">El contexto de la interacción en Discord.</param>
        /// <param name="itemNumber">Número del ítem a usar.</param>
        /// <param name="pokemonName">Nombre del Pokémon sobre el cual usar el ítem.</param>
        [SlashCommand("SelectItem", "Selecciona un ítem del inventario.")]
        public async Task SelectItem(InteractionContext ctx, [Option("ItemNumber", "Ingrese el número del ítem a usar.")] string itemNumber, [Option("PokemonName", "Ingrese el nombre del Pokémon.")] string pokemonName)
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

        /// <summary>
        /// Comando para atacar al Pokémon enemigo.
        /// </summary>
        /// <param name="ctx">El contexto de la interacción en Discord.</param>
        /// <param name="moveNumber">Número del movimiento a usar.</param>
        [SlashCommand("Attack", "Ataca a un Pokémon enemigo.")]
        public async Task Attack(InteractionContext ctx, [Option("MoveNumber", "Ingrese el número del movimiento a usar.")] string moveNumber)
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
}
