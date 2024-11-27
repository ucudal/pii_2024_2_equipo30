namespace Library.Tests.HistoryUserTests
{
    using Library;
    using NUnit.Framework;
    using System.Collections.Generic;

    /// <summary>
    /// Clase que contiene las pruebas unitarias relacionadas con la funcionalidad de batalla de los usuarios.
    /// </summary>
    public class HistoryUser6_Test
    {
        /// <summary>
        /// Clase de prueba para verificar la funcionalidad de la clase Battle.
        /// </summary>
        public class BatallaTests
        {
            /// <summary>
            /// Jugador 1 en la prueba.
            /// </summary>
            private Player jugador1;

            /// <summary>
            /// Jugador 2 en la prueba.
            /// </summary>
            private Player jugador2;

            /// <summary>
            /// Instancia de la batalla utilizada en la prueba.
            /// </summary>
            private Battle battle;

            /// <summary>
            /// Método de configuración que se ejecuta antes de cada prueba.
            /// Inicializa los jugadores y sus equipos de Pokémon, además de la instancia de la batalla.
            /// </summary>
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

                jugador1 = new Player(null, "Jugador 1", equipoJugador1);
                jugador2 = new Player(null, "Jugador 2", equipoJugador2);
                battle = new Battle(jugador1, jugador2);
            }

            /// <summary>
            /// Prueba que verifica si la batalla termina cuando todos los Pokémon de un jugador están fuera de combate.
            /// </summary>
            [Test]
            public void BatallaTermina()
            {
                // Act: Comprobar si el jugador 2 tiene todos sus Pokémon fuera de combate
                bool oponenteFueraDeCombate = jugador2.AllOutOfCombat();

                // Assert: Verificar que el jugador 2 está efectivamente fuera de combate
                Assert.IsTrue(oponenteFueraDeCombate, "El jugador 2 debería estar fuera de combate.");
            }
        }
    }
}
