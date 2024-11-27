using System.Collections.Generic;
using DSharpPlus.SlashCommands;

namespace Library
{
    /// <summary>
    /// Interfaz que define las operaciones básicas que deben implementarse para una batalla.
    /// </summary>
    public interface IBattle
    {
        /// <summary>
        /// Juega un turno entre dos jugadores en una batalla.
        /// </summary>
        /// <param name="actualPlayer">El jugador que toma el turno actual.</param>
        /// <param name="enemyPlayer">El jugador enemigo contra quien se juega el turno.</param>
        /// <param name="ctx">El contexto de la interacción en Discord.</param>
        void PlayShift(Player actualPlayer, Player enemyPlayer, InteractionContext ctx);

        /// <summary>
        /// Utiliza un ítem del inventario de un jugador sobre un Pokémon.
        /// </summary>
        /// <param name="player">El jugador que usa el ítem.</param>
        /// <param name="itemNumber">El número del ítem a usar del inventario del jugador.</param>
        /// <param name="pokemonName">El nombre del Pokémon sobre el cual se aplicará el ítem.</param>
        /// <param name="ctx">El contexto de la interacción en Discord.</param>
        void UseItem(Player player, int itemNumber, string pokemonName, InteractionContext ctx);

        /// <summary>
        /// Ejecuta un ataque desde el jugador actual hacia el jugador enemigo.
        /// </summary>
        /// <param name="actualPlayer">El jugador que realiza el ataque.</param>
        /// <param name="enemyPlayer">El jugador enemigo que recibe el ataque.</param>
        /// <param name="moveNumber">El número del movimiento/ataque que se va a ejecutar.</param>
        /// <param name="ctx">El contexto de la interacción en Discord.</param>
        void Attack(Player actualPlayer, Player enemyPlayer, int moveNumber, InteractionContext ctx);

        /// <summary>
        /// Cambia el Pokémon activo de un jugador.
        /// </summary>
        /// <param name="player">El jugador que realiza el cambio de Pokémon.</param>
        /// <param name="pokemonIndex">El índice del nuevo Pokémon que se desea usar.</param>
        /// <param name="ctx">El contexto de la interacción en Discord.</param>
        void SwitchPokemon(Player player, int pokemonIndex, InteractionContext ctx);
    }
}