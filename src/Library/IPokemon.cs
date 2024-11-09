namespace Library
{
    /// <summary>
    /// Interfaz que define los métodos y propiedades esenciales para un Pokémon en el juego.
    /// Permite gestionar las estadísticas, movimientos y estados del Pokémon durante la batalla.
    /// </summary>
    public interface IPokemon
    {
        /// <summary>
        /// Propiedad que contiene el nombre del Pokémon.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Propiedad que contiene los puntos de vida actuales del Pokémon.
        /// </summary>
        double Health { get; set; }

        /// <summary>
        /// Propiedad que contiene los puntos de vida máximos del Pokémon.
        /// </summary>
        double MaxHealt { get; set; }

        /// <summary>
        /// Propiedad que representa el identificador único del Pokémon.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Propiedad que contiene el valor del ataque físico del Pokémon.
        /// </summary>
        int Attack { get; set; }

        /// <summary>
        /// Propiedad que contiene el valor de defensa física del Pokémon.
        /// </summary>
        int Defense { get; set; }

        /// <summary>
        /// Propiedad que contiene el valor de ataque especial del Pokémon.
        /// </summary>
        int SpecialAttack { get; set; }

        /// <summary>
        /// Propiedad que contiene el valor de defensa especial del Pokémon.
        /// </summary>
        int SpecialDefense { get; set; }

        /// <summary>
        /// Propiedad que define el tipo de Pokémon (Fuego, Agua, Planta, etc.).
        /// </summary>
        Type Type { get; set; }

        /// <summary>
        /// Propiedad que contiene una lista de los movimientos que el Pokémon puede realizar.
        /// </summary>
        List<Move> Moves { get; set; }

        /// <summary>
        /// Propiedad que representa el estado especial del Pokémon (Dormido, Paralizado, etc.).
        /// </summary>
        SpecialStatus Status { get; set; }

        /// <summary>
        /// Propiedad que indica cuántos turnos le quedan al Pokémon para estar dormido.
        /// </summary>
        int SleepTurnsLeft { get; set; }

        /// <summary>
        /// Método para calcular el daño que un Pokémon inflige a otro Pokémon con un movimiento y su efectividad.
        /// </summary>
        /// <param name="movimiento">El movimiento utilizado por el Pokémon atacante.</param>
        /// <param name="efectividad">La efectividad del movimiento (puede depender del tipo de Pokémon).</param>
        /// <param name="oponente">El Pokémon enemigo al que se le aplica el daño.</param>
        /// <returns>Devuelve el daño calculado.</returns>
        double CalculateDamage(Move movimiento, double efectividad, Pokemon oponente);

        /// <summary>
        /// Método que verifica si el Pokémon puede atacar en el turno actual.
        /// </summary>
        /// <returns>Devuelve true si el Pokémon puede atacar, de lo contrario false.</returns>
        bool CanAtack();

        /// <summary>
        /// Método que permite a un Pokémon realizar un ataque físico o especial contra otro Pokémon.
        /// </summary>
        /// <param name="player">El jugador que controla al Pokémon atacante.</param>
        /// <param name="enemy">El Pokémon enemigo que recibirá el ataque.</param>
        /// <param name="movement">El movimiento seleccionado por el jugador para el ataque.</param>
        /// <param name="currentShift">El turno actual en la batalla.</param>
        void AttackP(Player player, Pokemon enemy, Move movement, int currentShift);

        /// <summary>
        /// Método que procesa el estado actual del Pokémon (como dormir, paralizado, etc.) y aplica los efectos correspondientes.
        /// </summary>
        /// <param name="enemy">El Pokémon enemigo (opcional) en caso de que se necesite aplicar efectos relacionados con el enemigo.</param>
        void ProcessStatus(Pokemon enemy = null);

        /// <summary>
        /// Método que verifica si el Pokémon está fuera de combate (es decir, si su salud ha llegado a cero).
        /// </summary>
        /// <returns>Devuelve true si el Pokémon está fuera de combate, de lo contrario false.</returns>
        bool OutOfAction();
    }
}
