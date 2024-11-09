namespace Library
{
    /// <summary>
    /// Clase que representa un jugador en el juego.
    /// Contiene la lógica para manejar el equipo de Pokémon del jugador, los ítems del inventario, y el uso de ataques especiales.
    /// Implementa la interfaz "IJugador".
    /// </summary>
    public class Player : IJugador
    {
        /// <summary>
        /// Nombre del jugador.
        /// </summary>
        public string NamePlayer { get; set; }

        /// <summary>
        /// Equipo de Pokémon del jugador.
        /// </summary>
        public List<Pokemon> Team { get; set; }

        /// <summary>
        /// Pokémon actualmente en combate.
        /// </summary>
        public Pokemon actualPokemon { get; set; }

        /// <summary>
        /// Inventario del jugador que contiene ítems que se pueden utilizar.
        /// </summary>
        public List<IItem> Inventory { get; set; }

        /// <summary>
        /// Objeto "Superpotion" disponible en el inventario del jugador.
        /// </summary>
        public SuperPotion Superpotion { get; set; }

        /// <summary>
        /// Objeto "Revive" disponible en el inventario del jugador.
        /// </summary>
        public Revive Revive { get; set; }

        /// <summary>
        /// Objeto "TotalCure" disponible en el inventario del jugador.
        /// </summary>
        public TotalCure Totalcure { get; set; }

        /// <summary>
        /// Diccionario que almacena el turno en el que se usó un ataque especial por última vez.
        /// </summary>
        private Dictionary<string, int> ataquesEspecialesUsados = new Dictionary<string, int>();

        /// <summary>
        /// Contador de los turnos personales del jugador.
        /// </summary>
        private int turnoPersonal = 1;

        /// <summary>
        /// Constructor de la clase "Player".
        /// Inicializa el nombre del jugador, su equipo de Pokémon y su inventario.
        /// </summary>
        /// <param name="namePlayer">Nombre del jugador.</param>
        /// <param name="team">Lista de Pokémon que forman el equipo del jugador.</param>
        public Player(string namePlayer, List<Pokemon> team)
        {
            NamePlayer = namePlayer;
            Team = team;
            Inventory = new List<IItem>(); // Inicializamos el inventario vacío
            Superpotion = new SuperPotion(4, 70);
            Revive = new Revive(1);
            actualPokemon = Team[0]; // Selecciona el primer Pokémon del equipo como el Pokémon actual
        }

        /// <summary>
        /// Método para seleccionar un equipo de Pokémon basado en un criterio específico (aún sin implementar).
        /// </summary>
        /// <param name="pokemon">Nombre del Pokémon.</param>
        /// <returns>Devuelve una lista vacía de Pokémon por defecto.</returns>
        public List<Pokemon> ChooseTeam(string pokemon)
        {
            return new List<Pokemon>();
        }

        /// <summary>
        /// Cambia el Pokémon actualmente en combate por otro del equipo del jugador.
        /// </summary>
        /// <param name="indice">Índice del Pokémon en el equipo que se seleccionará para combatir.</param>
        public void SwitchPokemon(int indice)
        {
            actualPokemon = Team[indice];
            Console.WriteLine($"\n{NamePlayer} cambió a {actualPokemon.Name}!\n");
        }

        /// <summary>
        /// Verifica si todos los Pokémon del equipo del jugador están fuera de combate.
        /// </summary>
        /// <returns>Devuelve true si todos los Pokémon están fuera de combate, de lo contrario false.</returns>
        public bool AllOutOfCombat()
        {
            foreach (var pokemon in Team)
            {
                if (!pokemon.OutOfAction())
                {
                    return false; // Si hay al menos un Pokémon que pueda combatir, devuelve false.
                }
            }

            Console.WriteLine($"\n{NamePlayer} no tiene Pokémon disponibles. Todos están fuera de combate.\n");
            return true; // Retorna true si todos están fuera de combate.
        }

        /// <summary>
        /// Registra el turno en el que se usó un ataque especial.
        /// </summary>
        /// <param name="nombreAtaque">Nombre del ataque especial utilizado.</param>
        /// <param name="turnoActual">Turno actual en el que se utiliza el ataque especial.</param>
        public void RegisterSpecialAttack(string nombreAtaque, int turnoActual)
        {
            ataquesEspecialesUsados[nombreAtaque] = turnoActual;
        }

        /// <summary>
        /// Verifica si un ataque especial está disponible para ser usado, basado en el tiempo desde el último uso.
        /// </summary>
        /// <param name="nombreAtaque">Nombre del ataque especial.</param>
        /// <param name="turnoActual">Turno actual.</param>
        /// <returns>Devuelve true si el ataque especial se puede usar, de lo contrario false.</returns>
        public bool CanUseEspecialAtack(string nombreAtaque, int turnoActual)
        {
            int turnoUltimoUso = ObtainLastShiftofAttack(nombreAtaque);

            // Verificar si se puede usar el ataque (se debe esperar 2 turnos desde el último uso).
            if (turnoUltimoUso == -1 || turnoActual - turnoUltimoUso >= 2)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Obtiene el turno en el que se usó un ataque especial por última vez.
        /// </summary>
        /// <param name="nombreAtaque">Nombre del ataque especial.</param>
        /// <returns>Devuelve el número de turno en el que se usó el ataque especial por última vez, o -1 si no se ha usado.</returns>
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
        /// Obtiene el número del turno personal actual del jugador.
        /// </summary>
        /// <returns>Devuelve el número del turno personal actual.</returns>
        public int ObtainPersonalShift()
        {
            return turnoPersonal;
        }
    }
}
