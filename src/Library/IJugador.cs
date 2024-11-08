namespace Library;

public interface IJugador
    {
        string NamePlayer { get; set; }
        List<Pokemon> Team { get; set; }
        Pokemon actualPokemon { get; set; }
        List<IItem> Inventario { get; set; }
        SuperPotion Superpotion { get; set; }
        Revive Revive { get; set; }
        TotalCure Totalcure { get; set; }

        List<Pokemon> ElegirEquipo(string pokemon);
        void SwitchPokemon(int indice);
        bool AllOutOfCombat();
    }
