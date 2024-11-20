namespace Library.Tests.HistoryUserTests;

using Library;
using System.Collections.Generic;
using NUnit.Framework;

public class HistoryUser8_Test
{
    [TestFixture]
    public class UsarItemTests
    {
        [Test]
        public void UsarItem_PierdeTurno()
        {
            // Arrange
            var equipoJugador1 = new List<Pokemon>
            {
                new Pokemon { Health = 50 }, // Pokémon con vida
                new Pokemon { Health = 60 } // Otro Pokémon con vida
            };

            var equipoJugador2 = new List<Pokemon>
            {
                new Pokemon { Health = 100 }, // Todos los Pokémon del oponente 
                new Pokemon { Health = 100 }
            };

            var jugador1 = new Player("Jugador 1", equipoJugador1);
            var jugador2 = new Player("Jugador 2", equipoJugador2);
            var turno = new Shift(jugador1, jugador2);
            var pokemon1Jugador1 = equipoJugador1[0]; // Charmander
            var pokemon2Jugador1 = equipoJugador1[1]; // Pikachu
            var pokemon1Jugador2 = equipoJugador2[0]; // Bulbasaur
            var pokemon2Jugador2 = equipoJugador2[1]; // Squirtle

            // Añadir un ítem al inventario del jugador1
            var superPotion = new SuperPotion(1, 50); // Cura 50 puntos de vida
            jugador1.Inventario.Add(superPotion);

            // Act
            // Jugador 1 usa una superpoción en su Pokémon actual
            jugador2.Superpotion.Use(pokemon1Jugador2);
            turno.SwitchShift(); // Se pierde el turno, pasa a Jugador 2

            // Assert
            Assert.That(pokemon1Jugador2.Health, Is.EqualTo(0), "La vida del Pokémon debería ser 100 después de usar la superpoción.");
            Assert.That(turno.actualPlayer, Is.EqualTo(jugador2), "El turno debería haber cambiado al oponente después de usar un ítem.");
        }
    }
}
