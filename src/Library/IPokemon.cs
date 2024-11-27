using DSharpPlus.SlashCommands;

namespace Library
{
    /// <summary>
    /// Interfaz que define las propiedades y métodos básicos de un Pokémon en el contexto de un juego.
    /// </summary>
    public interface IPokemon
    {
        /// <summary>
        /// Obtiene o establece el nombre del Pokémon.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Obtiene o establece la salud actual del Pokémon.
        /// </summary>
        double Health { get; set; }

        /// <summary>
        /// Obtiene o establece la salud máxima del Pokémon.
        /// </summary>
        double MaxHealt { get; set; }

        /// <summary>
        /// Obtiene o establece el identificador único del Pokémon.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Obtiene o establece el valor de ataque del Pokémon.
        /// </summary>
        int Attack { get; set; }

        /// <summary>
        /// Obtiene o establece el valor de defensa del Pokémon.
        /// </summary>
        int Defense { get; set; }

        /// <summary>
        /// Obtiene o establece el valor de ataque especial del Pokémon.
        /// </summary>
        int SpecialAttack { get; set; }

        /// <summary>
        /// Obtiene o establece el valor de defensa especial del Pokémon.
        /// </summary>
        int SpecialDefense { get; set; }

        /// <summary>
        /// Obtiene o establece el tipo del Pokémon (por ejemplo, fuego, agua, planta).
        /// </summary>
        Type Type { get; set; }

        /// <summary>
        /// Obtiene o establece la lista de movimientos que el Pokémon conoce.
        /// </summary>
        List<Move> Moves { get; set; }

        /// <summary>
        /// Obtiene o establece el estado especial del Pokémon (por ejemplo, envenenado, paralizado).
        /// </summary>
        SpecialStatus Status { get; set; }

        /// <summary>
        /// Obtiene o establece el número de turnos restantes que el Pokémon permanecerá dormido.
        /// </summary>
        int SleepTurnsLeft { get; set; }

        /// <summary>
        /// Calcula el daño infligido por un movimiento específico al oponente.
        /// </summary>
        /// <param name="movimiento">El movimiento que el Pokémon usará para atacar.</param>
        /// <param name="efectividad">El factor de efectividad del movimiento (por ejemplo, si es súper efectivo).</param>
        /// <param name="oponente">El Pokémon enemigo que recibirá el daño.</param>
        /// <returns>Devuelve el valor de daño infligido.</returns>
        double CalculateDamage(Move movimiento, double efectividad, Pokemon oponente);

        /// <summary>
        /// Determina si el Pokémon puede atacar según su estado actual.
        /// </summary>
        /// <param name="ctx">El contexto de la interacción en Discord.</param>
        /// <returns>Devuelve un valor booleano indicando si el Pokémon puede atacar.</returns>
        Task<bool> CanAtack(InteractionContext ctx);

        /// <summary>
        /// Realiza un ataque hacia un Pokémon enemigo.
        /// </summary>
        /// <param name="player">El jugador propietario del Pokémon que ataca.</param>
        /// <param name="enemy">El Pokémon enemigo que recibirá el ataque.</param>
        /// <param name="movement">El movimiento que se usará para atacar.</param>
        /// <param name="currentShift">El turno actual en la batalla.</param>
        /// <param name="ctx">El contexto de la interacción en Discord.</param>
        void AttackP(Player player, Pokemon enemy, Move movement, int currentShift, InteractionContext ctx);

        /// <summary>
        /// Procesa el estado actual del Pokémon y aplica los efectos correspondientes (por ejemplo, daño por veneno).
        /// </summary>
        /// <returns>Devuelve una descripción del estado actual del Pokémon.</returns>
        string ProcessStatus();

        /// <summary>
        /// Verifica si el Pokémon está fuera de combate.
        /// </summary>
        /// <returns>Devuelve true si el Pokémon ya no puede combatir, de lo contrario false.</returns>
        bool OutOfAction();
    }
}
