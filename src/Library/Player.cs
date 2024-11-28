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
    /// Clase que representa a un jugador en el juego, implementando la interfaz IPlayer.
    /// </summary>
    public class Player : IPlayer
    {
        /// <summary>
        /// Nombre del jugador.
        /// </summary>
        public string NamePlayer { get; set; }

        /// <summary>
        /// Miembro de Discord asociado al jugador.
        /// </summary>
        public DiscordMember Member { get; set; } // Asegúrate de tener esta propiedad

        /// <summary>
        /// Equipo de Pokémon del jugador.
        /// </summary>
        public List<Pokemon> Team { get; set; } = new List<Pokemon>();

        /// <summary>
        /// Pokémon actualmente activo del jugador.
        /// </summary>
        public Pokemon actualPokemon { get; set; }

        /// <summary>
        /// Inventario de ítems del jugador.
        /// </summary>
        public List<IItem> Inventario { get; set; }

        /// <summary>
        /// Objeto SuperPotion del jugador, usado para curar a sus Pokémon.
        /// </summary>
        public SuperPotion Superpotion { get; set; }

        /// <summary>
        /// Objeto Revive del jugador, usado para revivir a sus Pokémon caídos.
        /// </summary>
        public Revive Revive { get; set; }

        /// <summary>
        /// Objeto TotalCure del jugador, usado para curar completamente a sus Pokémon.
        /// </summary>
        public TotalCure Totalcure { get; set; }

        /// <summary>
        /// Diccionario para llevar el registro de los ataques especiales usados y en qué turno.
        /// </summary>
        private Dictionary<string, int> ataquesEspecialesUsados = new Dictionary<string, int>();

        /// <summary>
        /// Número del turno personal del jugador.
        /// </summary>
        private int turnoPersonal = 1;

        /// <summary>
        /// Indica si el jugador está en una partida activa.
        /// </summary>
        public bool InGame { get; set; }

        /// <summary>
        /// Constructor de la clase Player.
        /// </summary>
        /// <param name="member">Miembro de Discord asociado al jugador.</param>
        /// <param name="namePlayer">Nombre del jugador.</param>
        /// <param name="team">Equipo de Pokémon del jugador (opcional).</param>
        /// <param name="inGame">Indica si el jugador está en una partida activa (opcional).</param>
        public Player(DiscordMember member, string namePlayer, List<Pokemon>? team = null, bool inGame = false)
        {
            NamePlayer = namePlayer;
            Team = team ?? new List<Pokemon>();
            Member = member;
            InGame = inGame;
            Inventario = new List<IItem>();
            Superpotion = new SuperPotion(4, 70);
            Revive = new Revive(1);
            Totalcure = new TotalCure(2);
        }

        /// <summary>
        /// Método para elegir un equipo de Pokémon.
        /// </summary>
        /// <param name="pokemon">Nombre del Pokémon para elegir en el equipo.</param>
        /// <returns>Lista de Pokémon seleccionados como equipo.</returns>
        public List<Pokemon> ElegirEquipo(string pokemon)
        {
            // Lógica para elegir un equipo de Pokémon (pendiente de implementación)
            return new List<Pokemon>();
        }

        /// <summary>
        /// Cambia el Pokémon activo del jugador.
        /// </summary>
        /// <param name="indice">Índice del Pokémon al que se desea cambiar.</param>
        /// <param name="ctx">El contexto de la interacción en Discord.</param>
        public void SwitchPokemon(int indice, InteractionContext ctx)
        {
            actualPokemon = Team[indice];
            ctx.Channel.SendMessageAsync($"\n{NamePlayer} cambió a {actualPokemon.Name}!\n");
        }

        /// <summary>
        /// Verifica si todos los Pokémon del equipo están fuera de combate.
        /// </summary>
        /// <returns>Devuelve true si todos los Pokémon están fuera de combate, de lo contrario false.</returns>
        public bool AllOutOfCombat()
        {
            foreach (var pokemon in Team)
            {
                if (!pokemon.OutOfAction())
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Registra el uso de un ataque especial en el turno actual.
        /// </summary>
        /// <param name="nombreAtaque">El nombre del ataque especial utilizado.</param>
        /// <param name="turnoActual">El turno actual en el que se usa el ataque.</param>
        public void RegisterSpecialAttack(string nombreAtaque, int turnoActual)
        {
            ataquesEspecialesUsados[nombreAtaque] = turnoActual;
        }

        /// <summary>
        /// Verifica si un ataque especial puede ser utilizado en el turno actual.
        /// </summary>
        /// <param name="nombreAtaque">El nombre del ataque especial.</param>
        /// <param name="turnoActual">El turno actual.</param>
        /// <returns>Devuelve true si el ataque especial puede ser usado, de lo contrario false.</returns>
        public bool CanUseEspecialAtack(string nombreAtaque, int turnoActual)
        {
            int turnoUltimoUso = ObtainLastShiftofAttack(nombreAtaque);
            return turnoUltimoUso == -1 || turnoActual - turnoUltimoUso >= 2;
        }

        /// <summary>
        /// Obtiene el turno en el que se utilizó por última vez un ataque especial.
        /// </summary>
        /// <param name="nombreAtaque">El nombre del ataque especial.</param>
        /// <returns>Devuelve el turno del último uso del ataque, o -1 si nunca ha sido utilizado.</returns>
        public int ObtainLastShiftofAttack(string nombreAtaque)
        {
            return ataquesEspecialesUsados.ContainsKey(nombreAtaque) ? ataquesEspecialesUsados[nombreAtaque] : -1;
        }

        /// <summary>
        /// Incrementa el turno personal del jugador.
        /// </summary>
        public void IncrementPersonalShift()
        {
            turnoPersonal++;
        }

        /// <summary>
        /// Obtiene el número del turno personal actual del jugador.
        /// </summary>
        /// <returns>Devuelve el turno personal actual del jugador.</returns>
        public int ObtainPersonalShift()
        {
            return turnoPersonal;
        }
        
        
    }
}
