namespace Library;

public class Jugador
{
    public string Nombre { get; set; }
    public List<Pokemon> Equipo { get; set; }
    public Pokemon PokemonActual { get; set; }
    public List<IItem> Inventario { get; set; }
    public SuperPotion Superpotion { get; set; }
    public Revive Revive { get; set; }
    public TotalCure Totalcure { get; set; }
    private Dictionary<string, int> ataquesEspecialesUltimoTurno = new Dictionary<string, int>();

    public Jugador(string nombre, List<Pokemon> equipo)
    {
        Nombre = nombre;
        Equipo = equipo;
        Inventario = new List<IItem>(); // Inicializamos el inventario vacío
        Superpotion = new SuperPotion(4, 70);
        Revive = new Revive(1);
        Totalcure = new TotalCure(2);
        PokemonActual = Equipo[0];
        
    }

    public List<Pokemon> ElegirEquipo(string pokemon)
    {
        return new List<Pokemon>();
    }

    public void CambiarPokemon(int indice)
    {
        PokemonActual = Equipo[indice];
        Console.WriteLine($"\n {Nombre} cambió a {PokemonActual.Name}!\n");
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

        Console.WriteLine($"\n {Nombre} no tiene Pokémon disponibles. Todos están fuera de combate.\n");
        return true; // Retorna True si todos están fuera de combate
    }
    
    // Método para registrar el turno en el que se usó un ataque especial
    public void RegistrarAtaqueEspecial(string nombreAtaque, int turnoActual)
    {
        ataquesEspecialesUltimoTurno[nombreAtaque] = turnoActual;
    }

    // Método para verificar si el ataque especial está disponible
    public bool PuedeUsarAtaqueEspecial(string nombreAtaque, int turnoActual)
    {
        if (ataquesEspecialesUltimoTurno.ContainsKey(nombreAtaque))
        {
            int turnoUltimoUso = ataquesEspecialesUltimoTurno[nombreAtaque];
            // Solo permite el uso si han pasado al menos 2 turnos
            return (turnoActual - turnoUltimoUso) >= 2;
        }
        return true; // Si no se ha usado antes, está disponible
    }
}