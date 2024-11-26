using DSharpPlus.SlashCommands;

namespace Library;

/// <summary>
/// Interfaz que define la estructura y comportamiento de un jugador en el juego.
/// </summary>
public interface IPlayer
{
    /// <summary>
    /// Nombre del jugador.
    /// </summary>
    string NamePlayer { get; set; }

    /// <summary>
    /// Equipo de Pokémon del jugador.
    /// </summary>
    List<Pokemon> Team { get; set; }
    
    public bool InGame { get; set; }

    /// <summary>
    /// Pokémon actualmente en combate del jugador.
    /// </summary>
    Pokemon actualPokemon { get; set; }

    /// <summary>
    /// Inventario de ítems del jugador.
    /// </summary>
    List<IItem> Inventario { get; set; }

    /// <summary>
    /// Objeto Superpoción que el jugador puede usar.
    /// </summary>
    SuperPotion Superpotion { get; set; }

    /// <summary>
    /// Objeto Revivir que el jugador puede usar para revivir a un Pokémon debilitado.
    /// </summary>
    Revive Revive { get; set; }

    /// <summary>
    /// Objeto Cura Total que el jugador puede usar para curar cualquier estado alterado.
    /// </summary>
    TotalCure Totalcure { get; set; }

    /// <summary>
    /// Método para elegir el equipo de Pokémon del jugador.
    /// </summary>
    /// <param name="pokemon">El nombre del Pokémon a elegir para el equipo.</param>
    /// <returns>Lista de Pokémon que forman el equipo del jugador.</returns>
    List<Pokemon> ElegirEquipo(string pokemon);

    /// <summary>
    /// Método para cambiar el Pokémon en combate por otro del equipo.
    /// </summary>
    /// <param name="indice">Índice del Pokémon del equipo al que se desea cambiar.</param>
    void SwitchPokemon(int indice, InteractionContext ctx);

    /// <summary>
    /// Método para verificar si todos los Pokémon del jugador están fuera de combate.
    /// </summary>
    /// <returns>Devuelve true si todos los Pokémon están fuera de combate; de lo contrario, false.</returns>
    bool AllOutOfCombat();
}