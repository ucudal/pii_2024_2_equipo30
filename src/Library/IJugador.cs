namespace Library
{
    /// <summary>
    /// Interfaz que define los métodos y propiedades esenciales para un jugador en el juego.
    /// Permite gestionar el equipo de Pokémon, el inventario de ítems y las acciones del jugador.
    /// </summary>
    public interface IJugador
    {
        /// <summary>
        /// Propiedad que contiene el nombre del jugador.
        /// </summary>
        string NamePlayer { get; set; }

        /// <summary>
        /// Propiedad que contiene el equipo de Pokémon del jugador.
        /// </summary>
        List<Pokemon> Team { get; set; }

        /// <summary>
        /// Propiedad que representa el Pokémon actual que el jugador tiene en combate.
        /// </summary>
        Pokemon actualPokemon { get; set; }

        /// <summary>
        /// Propiedad que contiene el inventario de ítems del jugador, que incluye objetos como pociones, revivir, etc.
        /// </summary>
        List<IItem> Inventory { get; set; }

        /// <summary>
        /// Propiedad que representa la superpoción del jugador.
        /// </summary>
        SuperPotion Superpotion { get; set; }

        /// <summary>
        /// Propiedad que representa el objeto de Revivir del jugador.
        /// </summary>
        Revive Revive { get; set; }

        /// <summary>
        /// Propiedad que representa el objeto de Cura Total del jugador.
        /// </summary>
        TotalCure Totalcure { get; set; }

        /// <summary>
        /// Método que permite al jugador elegir su equipo de Pokémon. 
        /// Retorna una lista de Pokémon basada en el nombre pasado como parámetro.
        /// </summary>
        /// <param name="pokemon">El nombre del Pokémon que se desea elegir.</param>
        /// <returns>Una lista de Pokémon elegidos por el jugador.</returns>
        List<Pokemon> ChooseTeam(string pokemon);

        /// <summary>
        /// Método que permite al jugador cambiar de Pokémon durante la batalla.
        /// </summary>
        /// <param name="indice">El índice del Pokémon en el equipo del jugador que se desea cambiar.</param>
        void SwitchPokemon(int indice);

        /// <summary>
        /// Método que verifica si el jugador ha agotado todos sus Pokémon en combate.
        /// </summary>
        /// <returns>Devuelve true si todos los Pokémon del jugador están fuera de combate; de lo contrario, false.</returns>
        bool AllOutOfCombat();
    }
}
