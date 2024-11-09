namespace Library;

/// <summary>
/// Interfaz que define el método para crear un objeto Pokémon a partir de un identificador o nombre.
/// </summary>
public interface IPokemonCreator
{
    /// <summary>
    /// Crea un objeto Pokémon con todos sus detalles, obteniendo la información necesaria a partir de un identificador o nombre.
    /// </summary>
    /// <param name="pokemonId">Identificador o nombre del Pokémon que se desea crear.</param>
    /// <returns>Una tarea que representa la operación asíncrona de creación y devuelve un objeto <see cref="Pokemon"/>.</returns>
    Task<Pokemon> CreatePokemon(string pokemonId);
}