using System.Collections.Generic;

namespace Library
{
    public interface IBattle
    {
        // Inicia la batalla entre dos jugadores
        void StartBattle();

        // Método para jugar un turno, recibe los jugadores involucrados
        void PlayShift(Player actualPlayer, Player enemyPlayer);

        // Método para usar un ítem en la batalla
        void UseItem(Player player);

        // Método para realizar un ataque
        void Attack(Player actualPlayer, Player enemyPlayer);

        // Método para cambiar el Pokémon en la batalla
        void SwitchPokemon(Player player);

        // Inicializa el Pokémon actual de un player
        void InitializeCurrentPokemon(Player player);
    }
}
