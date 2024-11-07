namespace Library;

public class Jugador
{
    public string Nombre { get; set; }
    public List<Pokemon> Equipo { get; set; }
    public Pokemon PokemonActual { get; set; }
    public List<IItem> Inventario { get; set; }

    public Jugador(string nombre, List<Pokemon> equipo)
    {
        Nombre = nombre;
        Equipo = equipo;
        Inventario = new List<IItem>(); // Inicializamos el inventario vacío
    }

    public List<Pokemon> ElegirEquipo(string pokemon)
    {
        return new List<Pokemon>();
    }

    public void CambiarPokemon(int indice)
    {
        PokemonActual = Equipo[indice];
        Console.WriteLine($"{Nombre} cambió a {PokemonActual.Name}!");
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

        return true; // Retorna True si todos están fuera de combate
    }

}
