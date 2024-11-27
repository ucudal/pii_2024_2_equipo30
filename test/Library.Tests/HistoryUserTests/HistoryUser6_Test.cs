namespace Library.Tests.HistoryUserTests;

using Library;
using NUnit.Framework;
using System.Collections.Generic;

public class HistoryUser6_Test
{
    public class BatallaTests
    {
        private Player jugador1;
        private Player jugador2;
        private Battle battle;

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

            jugador1 = new Player(null,"Jugador 1", equipoJugador1);
            jugador2 = new Player(null,"Jugador 2", equipoJugador2);
            battle = new Battle(jugador1, jugador2);
        }

        [Test]
        public void BatallaTermina()
        {
            // Act
            bool oponenteFueraDeCombate = jugador2.AllOutOfCombat();

            // Assert
            Assert.IsTrue(oponenteFueraDeCombate, "El jugador 2 debería estar fuera de combate.");
        }
    }
}
