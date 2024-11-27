using DSharpPlus.SlashCommands;

namespace Library
{
    /// <summary>
    /// Interfaz que define las propiedades y métodos básicos de un jugador en el contexto de un juego.
    /// </summary>
    public interface IPlayer
    {
        /// <summary>
        /// Obtiene o establece el nombre del jugador.
        /// </summary>
        string NamePlayer { get; set; }

        /// <summary>
        /// Obtiene o establece el equipo de Pokémon del jugador.
        /// </summary>
        List<Pokemon> Team { get; set; }

        /// <summary>
        /// Obtiene o establece un valor que indica si el jugador está en una partida activa.
        /// </summary>
        public bool InGame { get; set; }

        /// <summary>
        /// Obtiene o establece el Pokémon que está actualmente en combate.
        /// </summary>
        Pokemon actualPokemon { get; set; }

        /// <summary>
        /// Obtiene o establece el inventario de ítems del jugador.
        /// </summary>
        List<IItem> Inventario { get; set; }

        /// <summary>
        /// Obtiene o establece la super poción que tiene el jugador.
        /// </summary>
        SuperPotion Superpotion { get; set; }

        /// <summary>
        /// Obtiene o establece el revive que tiene el jugador, para revivir a sus Pokémon.
        /// </summary>
        Revive Revive { get; set; }

        /// <summary>
        /// Obtiene o establece la cura total que tiene el jugador.
        /// </summary>
        TotalCure Totalcure { get; set; }

        /// <summary>
        /// Permite al jugador elegir su equipo de Pokémon.
        /// </summary>
        /// <param name="pokemon">El nombre del Pokémon a elegir para el equipo.</param>
        /// <returns>Devuelve una lista de Pokémon que conforman el equipo.</returns>
        List<Pokemon> ElegirEquipo(string pokemon);

        /// <summary>
        /// Cambia el Pokémon activo del jugador.
        /// </summary>
        /// <param name="indice">El índice del Pokémon al que se desea cambiar.</param>
        /// <param name="ctx">El contexto de la interacción en Discord.</param>
        void SwitchPokemon(int indice, InteractionContext ctx);

        /// <summary>
        /// Verifica si todos los Pokémon del equipo están fuera de combate.
        /// </summary>
        /// <returns>Devuelve true si todos los Pokémon están fuera de combate, de lo contrario false.</returns>
        bool AllOutOfCombat();
    }
}
