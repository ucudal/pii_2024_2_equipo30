namespace Library.Tests.HistoryUserTests;

using Library;
using NUnit.Framework;
using System.Collections.Generic;

public class HistoryUser6_Test
{
    public class BatallaTests
    {
        private Jugador jugador1;
        private Jugador jugador2;
        private Batalla batalla;

        [SetUp]
        public void Setup()
        {
            // Configuramos equipos con Pokémon fuera de combate para el oponente
            var equipoJugador1 = new List<Pokemon>
            {
                new Pokemon { Health = 50 }, // Pokémon con vida
                new Pokemon { Health = 60 } // Otro Pokémon con vida
            };

            var equipoJugador2 = new List<Pokemon>
            {
                new Pokemon { Health = 0 }, // Todos los Pokémon del oponente sin vida
                new Pokemon { Health = 0 }
            };

            jugador1 = new Jugador("Jugador 1", equipoJugador1);
            jugador2 = new Jugador("Jugador 2", equipoJugador2);
            batalla = new Batalla(jugador1, jugador2);
        }

        [Test]
        public void BatallaTermina()
        {
            // Act
            bool oponenteFueraDeCombate = jugador2.TodosFueraDeCombate();

            // Assert
            Assert.IsTrue(oponenteFueraDeCombate, "El jugador 2 debería estar fuera de combate.");
        }
    }
}
