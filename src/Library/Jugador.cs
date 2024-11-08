namespace Library;

public class Jugador:IJugador //a
{
    public string Nombre { get; set; }
    public List<Pokemon> Equipo { get; set; }
    public Pokemon PokemonActual { get; set; }
    public List<IItem> Inventario { get; set; }
    public SuperPotion Superpotion { get; set; }
    public Revive Revive { get; set; }
    public TotalCure Totalcure { get; set; }
    private Dictionary<string, int> ataquesEspecialesUsados = new Dictionary<string, int>();
    private int turnoPersonal = 1;

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
// Método para registrar el turno en el que se usó un ataque especial
    public void RegistrarAtaqueEspecial(string nombreAtaque, int turnoActual)
    {
        ataquesEspecialesUsados[nombreAtaque] = turnoActual;
    }

    // Método para verificar si el ataque especial está disponible
    public bool PuedeUsarAtaqueEspecial(string nombreAtaque, int turnoActual)
    {
        int turnoUltimoUso = ObtenerUltimoTurnoDeAtaque(nombreAtaque);

        // Verificar si se puede usar el ataque (se debe esperar 2 turnos del jugador)
        if (turnoUltimoUso == -1 || turnoActual - turnoUltimoUso >= 2)
        {
            return true;
        }
        return false;
    }

    // Método para obtener el turno en el que se usó el ataque especial por última vez
    public int ObtenerUltimoTurnoDeAtaque(string nombreAtaque)
    {
        return ataquesEspecialesUsados.ContainsKey(nombreAtaque) ? ataquesEspecialesUsados[nombreAtaque] : -1;
    }
    
    public void IncrementarTurnoPersonal()
    {
        turnoPersonal++;
    }
    
    public int ObtenerTurnoPersonal()
    {
        return turnoPersonal;
    }
}
