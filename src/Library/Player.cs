using System.Threading.Channels;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Collections.Generic;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.Interactivity.Extensions;
using Library.BotDiscord;


namespace Library
{
    /// <summary>
    /// Clase que representa a un jugador en el juego. Implementa la interfaz <see cref="IPlayer"/>.
    /// </summary>
    public class Player : IPlayer
    {
        public string NamePlayer { get; set; }
        public List<Pokemon> PokemonList { get; set; } = new List<Pokemon>();
        public DiscordMember Member { get; set; } // Asegúrate de tener esta propiedad
        public List<Pokemon> Team { get; set; } = new List<Pokemon>();
        public List<Pokemon> TempTeam { get; set; } = new List<Pokemon>(); // Lista temporal
        public Pokemon actualPokemon { get; set; }
        public List<IItem> Inventario { get; set; }
        public SuperPotion Superpotion { get; set; }
        public Revive Revive { get; set; }
        public TotalCure Totalcure { get; set; }
        private Dictionary<string, int> ataquesEspecialesUsados = new Dictionary<string, int>();
        private int turnoPersonal = 1;
        public bool InBattle { get; set; }

        public Player(DiscordMember member, string namePlayer, List<Pokemon>? team = null, bool inBattle = false)
        {
            NamePlayer = namePlayer;
            Team = team ?? new List<Pokemon>();
            Member = member;
            InBattle = inBattle;
            Inventario = new List<IItem>();
            Superpotion = new SuperPotion(4, 70);
            Revive = new Revive(1);
            Totalcure = new TotalCure(2);
            TempTeam = new List<Pokemon>();
        }


    public List<Pokemon> ElegirEquipo(string pokemon)
    {
        // Lógica para elegir un equipo de Pokémon (pendiente de implementación)
        return new List<Pokemon>();
    }

    public void AddPokemon(Pokemon pokemon)
    {
        PokemonList.Add(pokemon);
    }

    public void SwitchPokemon(int indice)
    {
        actualPokemon = Team[indice];
        Console.WriteLine($"\n{NamePlayer} cambió a {actualPokemon.Name}!\n");
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
        Console.WriteLine($"\n{NamePlayer} no tiene Pokémon disponibles. Todos están fuera de combate.\n");
        return true;
    }

    public void RegisterSpecialAttack(string nombreAtaque, int turnoActual)
    {
        ataquesEspecialesUsados[nombreAtaque] = turnoActual;
    }

    public bool CanUseEspecialAtack(string nombreAtaque, int turnoActual)
    {
        int turnoUltimoUso = ObtainLastShiftofAttack(nombreAtaque);
        return turnoUltimoUso == -1 || turnoActual - turnoUltimoUso >= 2;
    }

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

    public void GetTeam(List<Pokemon> listapokemon)
    {
        Team = listapokemon;
    }
}

}
