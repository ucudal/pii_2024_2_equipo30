using DSharpPlus.SlashCommands;

namespace Library.BotDiscord
{
    /// <summary>
    /// Clase que maneja la cola de jugadores para las batallas del bot en Discord.
    /// Implementa un patrón Singleton.
    /// </summary>
    public class BotQueuePlayers : ApplicationCommandModule
    {
        /// <summary>
        /// Cola de jugadores en espera de entrar en una batalla.
        /// </summary>
        private readonly Queue<Player> _players = new Queue<Player>();

        /// <summary>
        /// Lista de jugadores actualmente en la partida.
        /// </summary>
        public List<Player> PlayersInGame { get; set; }

        /// <summary>
        /// Instancia de la batalla en curso.
        /// </summary>
        public Battle Battle { get; set; }

        /// <summary>
        /// Instancia única de la clase BotQueuePlayers (implementación del patrón Singleton).
        /// </summary>
        private static BotQueuePlayers instance;

        /// <summary>
        /// Constructor privado para evitar instanciación directa (patrón Singleton).
        /// </summary>
        private BotQueuePlayers() {}

        /// <summary>
        /// Obtiene la instancia única de la clase BotQueuePlayers.
        /// </summary>
        /// <returns>La instancia única de BotQueuePlayers.</returns>
        public static BotQueuePlayers GetInstance()
        {
            if (instance == null)
            {
                instance = new BotQueuePlayers();
            }
            return instance;
        }

        /// <summary>
        /// Añade un jugador a la cola de batalla.
        /// </summary>
        /// <param name="player">El jugador que se desea añadir a la cola.</param>
        /// <returns>Un mensaje indicando si el jugador se ha añadido exitosamente o si ya estaba en la cola.</returns>
        public string JoinQueue(Player player)
        {
            if (_players.Contains(player))
                return $"{player.NamePlayer}, ya estás en la cola de batalla.";

            _players.Enqueue(player);
            return $"{player.NamePlayer} se ha unido a la cola de batalla.";
        }

        /// <summary>
        /// Elimina un jugador de la cola de batalla.
        /// </summary>
        /// <param name="player">El jugador que se desea eliminar de la cola.</param>
        /// <returns>Un mensaje indicando si el jugador se ha eliminado exitosamente o si no estaba en la cola.</returns>
        public string ExitQueue(Player player)
        {
            if (!_players.Contains(player))
                return $"{player.NamePlayer}, no estás en la cola.";

            var newQueue = new Queue<Player>(_players);
            _players.Clear();

            foreach (var j in newQueue)
            {
                if (j != player)
                    _players.Enqueue(j);
            }
            return $"{player.NamePlayer} ha salido de la cola.";
        }

        /// <summary>
        /// Obtiene los próximos dos jugadores de la cola para iniciar una batalla.
        /// </summary>
        /// <returns>Una lista con los dos próximos jugadores si hay suficientes, de lo contrario null.</returns>
        public List<Player> ObtenerProximosJugadores()
        {
            if (_players.Count < 2)
                return null;

            return new List<Player> { _players.Dequeue(), _players.Dequeue() };
        }

        /// <summary>
        /// Muestra la lista de jugadores actualmente en la cola.
        /// </summary>
        /// <returns>Una cadena con los nombres de los jugadores en la cola o un mensaje indicando que la cola está vacía.</returns>
        public string MostrarJugadores()
        {
            if (_players.Count == 0)
                return "No hay jugadores en la cola.";

            List<string> namelist = new List<string>();
            foreach (var player in _players)
            {
                namelist.Add(player.NamePlayer);
            }
            return $"Jugadores en cola: {string.Join(", ", namelist)}";
        }

        /// <summary>
        /// Obtiene un jugador por su nombre a partir de una lista de jugadores.
        /// </summary>
        /// <param name="name">El nombre del jugador que se desea buscar.</param>
        /// <param name="players">La lista de jugadores donde buscar el nombre.</param>
        /// <returns>El jugador correspondiente al nombre proporcionado o null si no se encuentra.</returns>
        /// <exception cref="ArgumentException">Lanza una excepción si el nombre es nulo o vacío.</exception>
        public Player GetPlayerByName(string name, List<Player> players)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre no puede estar vacío o nulo.", nameof(name));

            // Busca el jugador en la lista
            foreach (var player in players)
            {
                if (player.NamePlayer.Equals(name, StringComparison.OrdinalIgnoreCase))
                    return player;
            }

            // Si no se encuentra, devuelve null
            return null;
        }
    }
}
