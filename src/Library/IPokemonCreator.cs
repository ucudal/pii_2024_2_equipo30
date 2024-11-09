namespace Library
{
    /// <summary>
    /// Interfaz que define el contrato para la creación de un objeto Pokémon a partir de su identificador.
    /// </summary>
    public interface IPokemonCreator
    {
        /// <summary>
        /// Método asíncrono que crea un Pokémon basado en su identificador.
        /// </summary>
        /// <param name="pokemonId">El identificador del Pokémon (nombre o ID) utilizado para crear el objeto Pokémon.</param>
        /// <returns>Devuelve una tarea que se resuelve en un objeto Pokémon creado.</returns>
        Task<Pokemon> CreatePokemon(string pokemonId);
    }
}