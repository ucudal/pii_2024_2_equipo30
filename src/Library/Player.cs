using System.Threading.Channels;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Collections.Generic;
using DSharpPlus.SlashCommands;
using DSharpPlus.Interactivity.Extensions;


namespace Library
{
    /// <summary>
    /// Clase que representa a un jugador en el juego. Implementa la interfaz <see cref="IPlayer"/>.
    /// </summary>
    public class Player : IPlayer
    {
        /// <summary>
        /// Nombre del jugador.
        /// </summary>
        public string NamePlayer { get; set; }
        
        private static readonly HttpClient client = new HttpClient();

        /// <summary>
        /// Equipo de Pokémon del jugador.
        /// </summary>
        public List<Pokemon>? Team { get; set; }

        /// <summary>
        /// Pokémon actualmente en combate del jugador.
        /// </summary>
        public Pokemon actualPokemon { get; set; }

        /// <summary>
        /// Inventario de ítems del jugador.
        /// </summary>
        public List<IItem> Inventario { get; set; }

        /// <summary>
        /// Objeto Superpoción que el jugador puede usar para curar a su Pokémon.
        /// </summary>
        public SuperPotion Superpotion { get; set; }

        /// <summary>
        /// Objeto Revivir que el jugador puede usar para revivir a un Pokémon debilitado.
        /// </summary>
        public Revive Revive { get; set; }

        /// <summary>
        /// Objeto Cura Total que el jugador puede usar para curar cualquier estado alterado de su Pokémon.
        /// </summary>
        public TotalCure Totalcure { get; set; }

        /// <summary>
        /// Diccionario que registra los turnos en los que se usaron ataques especiales.
        /// </summary>
        private Dictionary<string, int> ataquesEspecialesUsados = new Dictionary<string, int>();

        /// <summary>
        /// Contador de los turnos personales del jugador.
        /// </summary>
        private int turnoPersonal = 1;

        public bool InBattle { get; set; }

        /// <summary>
        /// Constructor para inicializar el jugador con su nombre y equipo de Pokémon.
        /// </summary>
        /// <param name="namePlayer">Nombre del jugador.</param>
        /// <param name="team">Lista de Pokémon que conforman el equipo del jugador.</param>
        public Player(string namePlayer, List<Pokemon>? team = null, bool inBattle=false)
        {
            NamePlayer = namePlayer;
            Team = team;
            InBattle = inBattle;
            Inventario = new List<IItem>(); // Inicializamos el inventario vacío
            Superpotion = new SuperPotion(4, 70);
            Revive = new Revive(1);
            Totalcure = new TotalCure(2);
        }

        /// <summary>
        /// Método que permite al jugador elegir un equipo de Pokémon.
        /// </summary>
        /// <param name="pokemon">Nombre del Pokémon para formar parte del equipo.</param>
        /// <returns>Una lista de Pokémon que forman el equipo.</returns>
        public List<Pokemon> ElegirEquipo(string pokemon)
        {
            return new List<Pokemon>();
        }
       public async Task PokemonElection(InteractionContext ctx)
{
    PokemonApi pokemonApi = new PokemonApi(client);
    PokemonCreator pokemonCreator = new PokemonCreator(pokemonApi);

    List<Pokemon> PokemonList = new List<Pokemon>();

    await ctx.Channel.SendMessageAsync("\n==================== SELECCIÓN DE POKÉMON ====================\n");
    await ctx.Channel.SendMessageAsync($"Selección de Pokémon para el jugador {NamePlayer}:\n");

    for (int i = 0; i < 6; i++)
    {
        bool pokemonAgregado = false;
        while (!pokemonAgregado)
        {
            try
            {
                var interactividad = ctx.Client.GetInteractivity();
                var respuesta = await interactividad.WaitForMessageAsync(
                    m => m.Author.Id == ctx.User.Id && m.ChannelId == ctx.Channel.Id,
                    TimeSpan.FromMinutes(5)
                );

                if (respuesta.TimedOut || respuesta.Result == null)
                {
                    await ctx.Channel.SendMessageAsync("Tiempo agotado o respuesta inválida. Por favor, intenta nuevamente.");
                    continue;
                }

                string pokemonId = respuesta.Result.Content.ToLower();
                var response = await client.GetAsync($"https://pokeapi.co/api/v2/pokemon/{pokemonId}");
                if (response.IsSuccessStatusCode)
                {
                    var pokemon = await pokemonCreator.CreatePokemon(pokemonId);
                    PokemonList.Add(pokemon);
                    await ctx.Channel.SendMessageAsync($"Has seleccionado a: {pokemon.Name}\n");
                    pokemonAgregado = true;
                }
                else
                {
                    await ctx.Channel.SendMessageAsync($"No se pudo obtener datos para: {pokemonId}. Por favor, intenta nuevamente.\n");
                }
            }
            catch (Exception ex)
            {
                await ctx.Channel.SendMessageAsync($"Ocurrió un error al intentar obtener el Pokémon: {ex.Message}. Por favor, intente nuevamente.\n");
            }
        }
    }

    Team = PokemonList;
    actualPokemon = PokemonList.FirstOrDefault(); // Asignar el primer Pokémon si existe
}




        /// <summary>
        /// Método para cambiar el Pokémon actual en combate.
        /// </summary>
        /// <param name="indice">Índice del Pokémon al que se desea cambiar.</param>
        public void SwitchPokemon(int indice)
        {
            actualPokemon = Team[indice];
            Console.WriteLine($"\n {NamePlayer} cambió a {actualPokemon.Name}!\n");
        }

        /// <summary>
        /// Verifica si todos los Pokémon del jugador están fuera de combate.
        /// </summary>
        /// <returns>Devuelve true si todos los Pokémon están fuera de combate; de lo contrario, false.</returns>
        public bool AllOutOfCombat()
        {
            foreach (var pokemon in Team)
            {
                if (!pokemon.OutOfAction())
                {
                    return false;
                }
            }

            Console.WriteLine($"\n {NamePlayer} no tiene Pokémon disponibles. Todos están fuera de combate.\n");
            return true; // Retorna true si todos los Pokémon están fuera de combate
        }

        /// <summary>
        /// Registra el turno en el que se usó un ataque especial.
        /// </summary>
        /// <param name="nombreAtaque">Nombre del ataque especial utilizado.</param>
        /// <param name="turnoActual">Turno actual en el que se usó el ataque.</param>
        public void RegisterSpecialAttack(string nombreAtaque, int turnoActual)
        {
            ataquesEspecialesUsados[nombreAtaque] = turnoActual;
        }

        /// <summary>
        /// Verifica si un ataque especial está disponible para ser usado nuevamente.
        /// Un ataque especial requiere esperar 2 turnos antes de poder usarse nuevamente.
        /// </summary>
        /// <param name="nombreAtaque">Nombre del ataque especial a verificar.</param>
        /// <param name="turnoActual">Turno actual del jugador.</param>
        /// <returns>Devuelve true si el ataque especial está disponible; de lo contrario, false.</returns>
        public bool CanUseEspecialAtack(string nombreAtaque, int turnoActual)
        {
            int turnoUltimoUso = ObtainLastShiftofAttack(nombreAtaque);

            // Verificar si se puede usar el ataque (se debe esperar 2 turnos del jugador)
            if (turnoUltimoUso == -1 || turnoActual - turnoUltimoUso >= 2)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Obtiene el turno en el que se usó el ataque especial por última vez.
        /// </summary>
        /// <param name="nombreAtaque">Nombre del ataque especial.</param>
        /// <returns>Devuelve el turno del último uso del ataque especial o -1 si no se ha usado.</returns>
        public int ObtainLastShiftofAttack(string nombreAtaque)
        {
            return ataquesEspecialesUsados.ContainsKey(nombreAtaque) ? ataquesEspecialesUsados[nombreAtaque] : -1;
        }

        /// <summary>
        /// Incrementa el contador de turnos personales del jugador.
        /// </summary>
        public void IncrementPersonalShift()
        {
            turnoPersonal++;
        }

        /// <summary>
        /// Obtiene el turno personal actual del jugador.
        /// </summary>
        /// <returns>Devuelve el número del turno personal actual.</returns>
        public int ObtainPersonalShift()
        {
            return turnoPersonal;
        }

        public void GetTeam(List<Pokemon> listapokemon)
        {
            Team = listapokemon;
        }
    }
}
