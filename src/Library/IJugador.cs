namespace Library;

public interface IJugador
    {
        string Nombre { get; set; }
        List<Pokemon> Equipo { get; set; }
        Pokemon PokemonActual { get; set; }
        List<IItem> Inventario { get; set; }
        SuperPotion Superpotion { get; set; }
        Revive Revive { get; set; }
        TotalCure Totalcure { get; set; }

        List<Pokemon> ElegirEquipo(string pokemon);
        void CambiarPokemon(int indice);
        bool TodosFueraDeCombate();
    }
