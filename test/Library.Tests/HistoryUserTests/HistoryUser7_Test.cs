namespace Library.Tests.HistoryUserTests;

using System;
using System.Collections.Generic;
using NUnit.Framework;

public class HistoryUser7_Test
{
    [TestFixture]
    public class CambiarPokemonTests
    {
        [Test]
        public void PerdidaDeTurno()
        {
            // Arrange
            var equipoJugador1 = new List<Pokemon>
            {
                new Pokemon { Name = "Charmander" }, 
                new Pokemon { Name = "Charizard" } 
            };
            
            var equipoJugador2 = new List<Pokemon>
            {
                new Pokemon { Name = "Charmander" },
                new Pokemon { Name = "Charizard" } 
            };

            var jugador1 = new Player("Jugador 1", equipoJugador1);
            var jugador2 = new Player("Jugador 2", equipoJugador2);
            var turno = new Shift(jugador1, jugador2);

            // Act
            // Jugador 1 cambia a su segundo Pokémon
            jugador1.SwitchPokemon(1); // Cambia a Charmander
            turno.SwitchShift(); // Se pierde el turno, pasa a Jugador 2

            // Assert
            //Assert.AreEqual(pokemon2, jugador1.PokemonActual, "El Pokémon actual debería ser Charmander después del cambio.");
            Assert.That(turno.actualPlayer, Is.EqualTo(jugador2), "El turno debería haber cambiado al oponente después de cambiar de Pokémon.");
        }
    }
}
