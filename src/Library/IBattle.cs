using System.Collections.Generic;

namespace Library
{
    /// <summary>
    /// Interfaz que define los métodos para la batalla entre dos jugadores en un juego de Pokémon.
    /// </summary>
    public interface IBattle
    {
        /// <summary>
        /// Inicia la batalla entre dos jugadores.
        /// Este método se encarga de gestionar el flujo principal de la batalla.
        /// </summary

        /// <summary>
        /// Método que maneja un turno de batalla, donde el jugador actual y el enemigo realizan sus acciones.
        /// </summary>
        /// <param name="actualPlayer">El jugador que está realizando su turno.</param>
        /// <param name="enemyPlayer">El jugador enemigo que está esperando su turno.</param>
        void PlayShift(Player actualPlayer, Player enemyPlayer);

        /// <summary>
        /// Permite a un jugador usar un ítem durante su turno en la batalla.
        /// </summary>
        /// <param name="player">El jugador que usará el ítem.</param>
        void UseItem(Player player);

        /// <summary>
        /// Método para que el jugador realice un ataque contra el Pokémon enemigo.
        /// </summary>
        /// <param name="actualPlayer">El jugador que está atacando.</param>
        /// <param name="enemyPlayer">El jugador cuyo Pokémon será atacado.</param>
        void Attack(Player actualPlayer, Player enemyPlayer);

        /// <summary>
        /// Permite al jugador cambiar de Pokémon durante su turno.
        /// </summary>
        /// <param name="player">El jugador que realizará el cambio de Pokémon.</param>
        void SwitchPokemon(Player player);

        /// <summary>
        /// Inicializa el Pokémon actual de un jugador, asignándole el primer Pokémon disponible que no esté fuera de combate.
        /// </summary>
        /// <param name="player">El jugador cuyo Pokémon actual debe ser inicializado.</param>
        void InitializeCurrentPokemon(Player player);
    }
}