namespace Library;

public interface IPokemonCreator
{
    Task<Pokemon> CreatePokemon(string pokemonId);
}
