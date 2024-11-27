namespace Library.Tests.HistoryUserTests
{
    using Library;
    using System.Collections.Generic;
    using NUnit.Framework;

    /// <summary>
    /// Clase de prueba para verificar la funcionalidad de usar ítems durante una batalla y sus consecuencias.
    /// </summary>
    public class HistoryUser8_Test
    {
        /// <summary>
        /// Clase de prueba para verificar la funcionalidad de usar un ítem y su impacto en el turno de la batalla.
        /// </summary>
        [TestFixture]
        public class UsarItemTests
        {
            /// <summary>
            /// Prueba que verifica si el jugador pierde su turno después de usar un ítem.
            /// </summary>
            [Test]
            public void UsarItem_PierdeTurno()
            {
                // Arrange: Configuración inicial de la prueba.

                // Definir el equipo de Pokémon para el Jugador 1
                var equipoJugador1 = new List<Pokemon>
                {
                    new Pokemon { Health = 50 }, // Pokémon con vida
                    new Pokemon { Health = 60 } // Otro Pokémon con vida
                };

                // Definir el equipo de Pokémon para el Jugador 2
                var equipoJugador2 = new List<Pokemon>
                {
                    new Pokemon { Health = 100 }, // Pokémon con vida
                    new Pokemon { Health = 100 } // Otro Pokémon con vida
                };

                // Crear las instancias de los jugadores con sus equipos
                var jugador1 = new Player(null, "Jugador 1", equipoJugador1);
                var jugador2 = new Player(null, "Jugador 2", equipoJugador2);

                // Crear la instancia del turno de batalla
                var turno = new Shift(jugador1, jugador2);

                // Definir Pokémon específicos de cada jugador
                var pokemon1Jugador1 = equipoJugador1[0]; // Primer Pokémon de Jugador 1
                var pokemon2Jugador1 = equipoJugador1[1]; // Segundo Pokémon de Jugador 1
                var pokemon1Jugador2 = equipoJugador2[0]; // Primer Pokémon de Jugador 2
                var pokemon2Jugador2 = equipoJugador2[1]; // Segundo Pokémon de Jugador 2

                // Añadir un ítem al inventario del jugador 1
                var superPotion = new SuperPotion(1, 50); // Superpoción que cura 50 puntos de vida
                jugador1.Inventario.Add(superPotion);

                // Act: Realizar la acción de la prueba.

                // Jugador 1 usa una superpoción en su Pokémon actual
                jugador2.Superpotion.Use(pokemon1Jugador2); // Uso del ítem por Jugador 2
                turno.SwitchShift(); // Se pierde el turno, pasa a Jugador 2

                // Assert: Verificar que el resultado sea el esperado.

                // Verificar que la vida del Pokémon se haya curado adecuadamente.
                Assert.That(pokemon1Jugador2.Health, Is.EqualTo(0), "La vida del Pokémon debería ser 100 después de usar la superpoción.");
                Assert.That(turno.actualPlayer, Is.EqualTo(jugador2), "El turno debería haber cambiado al oponente después de usar un ítem.");
            }
        }
    }
}
