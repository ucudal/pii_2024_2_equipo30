using System.Collections.Generic;

namespace Library
{
    /// <summary>
    /// Interfaz que define los métodos necesarios para llevar a cabo una batalla entre dos jugadores.
    /// </summary>
    public interface IBatalla
    {
        /// <summary>
        /// Inicia la batalla entre dos jugadores.
        /// </summary>
        void StartBattle();

        /// <summary>
        /// Juega un turno en la batalla.
        /// </summary>
        /// <param name="actualPlayer">Jugador que toma el turno actual.</param>
        /// <param name="enemyPlayer">Jugador enemigo al que se enfrenta el jugador actual.</param>
        void PlayShift(Player actualPlayer, Player enemyPlayer);

        /// <summary>
        /// Usa un ítem durante la batalla.
        /// </summary>
        /// <param name="player">Jugador que usará el ítem.</param>
        void UseItem(Player player);

        /// <summary>
        /// Realiza un ataque del Pokémon del jugador actual al Pokémon del jugador enemigo.
        /// </summary>
        /// <param name="actualPlayer">Jugador que realiza el ataque.</param>
        /// <param name="enemyPlayer">Jugador enemigo que recibe el ataque.</param>
        void Attack(Player actualPlayer, Player enemyPlayer);

        /// <summary>
        /// Cambia el Pokémon actual del jugador durante la batalla.
        /// </summary>
        /// <param name="player">Jugador que cambiará su Pokémon.</param>
        void SwitchPokemon(Player player);

        /// <summary>
        /// Inicializa el Pokémon actual del jugador si no tiene uno o si el actual está fuera de combate.
        /// </summary>
        /// <param name="player">Jugador cuyo Pokémon será inicializado.</param>
        void InitializeCurrentPokemon(Player player);
    }
}