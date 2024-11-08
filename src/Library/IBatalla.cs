using System.Collections.Generic;

namespace Library
{
    public interface IBatalla
    {
        // Inicia la batalla entre dos jugadores
        void IniciarBatalla();

        // Método para jugar un turno, recibe los jugadores involucrados
        void JugarTurno(Jugador jugadorActual, Jugador jugadorOponente);

        // Método para usar un ítem en la batalla
        void UsarItem(Jugador jugador);

        // Método para realizar un ataque
        void Atacar(Jugador jugadorActual, Jugador jugadorOponente);

        // Método para cambiar el Pokémon en la batalla
        void CambiarPokemon(Jugador jugador);

        // Inicializa el Pokémon actual de un jugador
        void InicializarPokemonActual(Jugador jugador);
    }
}
