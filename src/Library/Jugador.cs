namespace Library;

public class Jugador
{
    public string Nombre { get; set; }
    public List<Pokemon> Equipo { get; set; }
    public Pokemon PokemonActual { get; set; }

    public Jugador(string nombre, List<Pokemon> equipo)
    {
        Nombre = nombre;
        Equipo = equipo;
    }

    public void CambiarPokemon(int indice)
    {
        PokemonActual = Equipo[indice];
        Console.WriteLine($"{Nombre} cambió a {PokemonActual.Nombre}!");
    }

    public bool TodosFueraDeCombate()
    {
        foreach (var pokemon in Equipo)
        {
            if (!pokemon.EstaFueraDeCombate()) 
            {
                return false;
            }
        }
        return true;  // Retorna True si todos están fuera de combate
    }

}
