using DSharpPlus.SlashCommands;

namespace Library;

/// <summary>
/// Interfaz que define la estructura y el comportamiento de un Pokémon en el juego.
/// </summary>
public interface IPokemon
{
    /// <summary>
    /// Nombre del Pokémon.
    /// </summary>
    string Name { get; set; }

    /// <summary>
    /// Puntos de salud actuales del Pokémon.
    /// </summary>
    double Health { get; set; }

    /// <summary>
    /// Máximo de puntos de salud que el Pokémon puede tener.
    /// </summary>
    double MaxHealt { get; set; }

    /// <summary>
    /// Identificador único del Pokémon.
    /// </summary>
    int Id { get; set; }

    /// <summary>
    /// Valor de ataque del Pokémon.
    /// </summary>
    int Attack { get; set; }

    /// <summary>
    /// Valor de defensa del Pokémon.
    /// </summary>
    int Defense { get; set; }

    /// <summary>
    /// Valor de ataque especial del Pokémon.
    /// </summary>
    int SpecialAttack { get; set; }

    /// <summary>
    /// Valor de defensa especial del Pokémon.
    /// </summary>
    int SpecialDefense { get; set; }

    /// <summary>
    /// Tipo del Pokémon (por ejemplo, Agua, Fuego, Planta, etc.).
    /// </summary>
    Type Type { get; set; }

    /// <summary>
    /// Lista de movimientos que el Pokémon puede realizar.
    /// </summary>
    List<Move> Moves { get; set; }

    /// <summary>
    /// Estado especial actual del Pokémon (por ejemplo, envenenado, quemado, etc.).
    /// </summary>
    SpecialStatus Status { get; set; }

    /// <summary>
    /// Número de turnos que le quedan al Pokémon durmiendo, si está dormido.
    /// </summary>
    int SleepTurnsLeft { get; set; }

    /// <summary>
    /// Calcula el daño infligido al oponente por un movimiento.
    /// </summary>
    /// <param name="movimiento">Movimiento que realiza el ataque.</param>
    /// <param name="efectividad">Factor de efectividad del ataque.</param>
    /// <param name="oponente">El Pokémon oponente que recibe el ataque.</param>
    /// <returns>Devuelve el daño calculado.</returns>
    double CalculateDamage(Move movimiento, double efectividad, Pokemon oponente);

    /// <summary>
    /// Verifica si el Pokémon puede atacar en su turno, teniendo en cuenta su estado actual.
    /// </summary>
    /// <returns>Devuelve true si el Pokémon puede atacar, de lo contrario false.</returns>
    Task<bool> CanAtack(InteractionContext ctx);

    /// <summary>
    /// Realiza un ataque al Pokémon enemigo.
    /// </summary>
    /// <param name="player">El jugador que posee el Pokémon que ataca.</param>
    /// <param name="enemy">El Pokémon enemigo que recibe el ataque.</param>
    /// <param name="movement">El movimiento utilizado para el ataque.</param>
    /// <param name="currentShift">El turno actual en el que se realiza el ataque.</param>
    void AttackP(Player player, Pokemon enemy, Move movement, int currentShift, InteractionContext ctx);

    /// <summary>
    /// Procesa el estado actual del Pokémon y aplica los efectos correspondientes.
    /// </summary>
    /// <param name="enemy">El Pokémon enemigo (opcional) en caso de que el estado afecte a ambos.</param>
    string ProcessStatus();

    /// <summary>
    /// Verifica si el Pokémon está fuera de combate (sin puntos de salud).
    /// </summary>
    /// <returns>Devuelve true si el Pokémon está fuera de combate, de lo contrario false.</returns>
    bool OutOfAction();
}
