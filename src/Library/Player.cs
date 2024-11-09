namespace Library;

public class Player:IJugador
{
    public string NamePlayer { get; set; }
    public List<Pokemon> Team { get; set; }
    public Pokemon actualPokemon { get; set; }
    public List<IItem> Inventory { get; set; }
    public SuperPotion Superpotion { get; set; }
    public Revive Revive { get; set; }
    public TotalCure Totalcure { get; set; }
    private Dictionary<string, int> ataquesEspecialesUsados = new Dictionary<string, int>();
    private int turnoPersonal = 1;

    public Player(string namePlayer, List<Pokemon> team)
    {
        NamePlayer = namePlayer;
        Team = team;
        Inventory = new List<IItem>(); // Inicializamos el inventario vacío
        Superpotion = new SuperPotion(4, 70);
        Revive = new Revive(1);
        actualPokemon = Team[0];
        
    }

    public List<Pokemon> ChooseTeam(string pokemon)
    {
        return new List<Pokemon>();
    }

    public void SwitchPokemon(int indice)
    {
        actualPokemon = Team[indice];
        Console.WriteLine($"\n {NamePlayer} cambió a {actualPokemon.Name}!\n");
    }

    public bool AllOutOfCombat()
    {
        foreach (var pokemon in Team)
        {
            if (!pokemon.OutOfAction())
            {
                return false;
            }
        }

        Console.WriteLine($"\n {NamePlayer} no tiene Pokémon disponibles. Todos están fuera de combate.\n");
        return true; // Retorna True si todos están fuera de combate
    }
// Método para registrar el turno en el que se usó un ataque especial
// Método para registrar el turno en el que se usó un ataque especial
    public void RegisterSpecialAttack(string nombreAtaque, int turnoActual)
    {
        ataquesEspecialesUsados[nombreAtaque] = turnoActual;
    }

    // Método para verificar si el ataque especial está disponible
    public bool CanUseEspecialAtack(string nombreAtaque, int turnoActual)
    {
        int turnoUltimoUso = ObtainLastShiftofAttack(nombreAtaque);

        // Verificar si se puede usar el ataque (se debe esperar 2 turnos del player)
        if (turnoUltimoUso == -1 || turnoActual - turnoUltimoUso >= 2)
        {
            return true;
        }
        return false;
    }

    // Método para obtener el turno en el que se usó el ataque especial por última vez
    public int ObtainLastShiftofAttack(string nombreAtaque)
    {
        return ataquesEspecialesUsados.ContainsKey(nombreAtaque) ? ataquesEspecialesUsados[nombreAtaque] : -1;
    }
    
    public void IncrementPersonalShift()
    {
        turnoPersonal++;
    }
    
    public int ObtainPersonalShift()
    {
        return turnoPersonal;
    }
}
