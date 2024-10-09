namespace Library;

public class CambioPokemon
{
    public (List<Pokemon>) Nombres { get; set;}//lista de nombre de los pokemons,esta de ejemplo "pokemon" pero se usaria la lista de pokemons del entrenador
    public CambioDeTruno CambioDeTruno { get; set; }//cambia de turno

    public string CambiarPokemon(bool CambioDeTruno, string Nombres)
    {
        CambioDeTruno = true;
    }
}