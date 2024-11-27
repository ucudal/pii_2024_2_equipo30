namespace Library.Tests.HistoryUserTests
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;

    /// <summary>
    /// Clase que contiene las pruebas unitarias relacionadas con el cambio de Pokémon durante una batalla.
    /// </summary>
    public class HistoryUser7_Test
    {
        /// <summary>
        /// Clase de prueba para verificar la funcionalidad de cambio de Pokémon y su impacto en el turno de batalla.
        /// </summary>
        [TestFixture]
        public class CambiarPokemonTests
        {
            /// <summary>
            /// Prueba que verifica que un jugador pierde su turno al cambiar de Pokémon.
            /// </summary>
            [Test]
            public void PerdidaDeTurno()
            {
                // Arrange: Configuración del estado inicial de la prueba.

                // Definir el equipo de Pokémon para el Jugador 1
                var equipoJugador1 = new List<Pokemon>
                {
                    new Pokemon { Name = "Charmander" },
                    new Pokemon { Name = "Charizard" }
                };

                // Definir el equipo de Pokémon para el Jugador 2
                var equipoJugador2 = new List<Pokemon>
                {
                    new Pokemon { Name = "Charmander" },
                    new Pokemon { Name = "Charizard" }
                };

                // Crear las instancias de los jugadores con sus equipos
                var jugador1 = new Player(null, "Jugador 1", equipoJugador1);
                var jugador2 = new Player(null, "Jugador 2", equipoJugador2);

                // Crear la instancia del turno de batalla
                var turno = new Shift(jugador1, jugador2);

                // Act: Realizar las acciones de la prueba.

                // Jugador 1 cambia a su segundo Pokémon
                jugador1.SwitchPokemon(1, null); // Cambia a Charizard
                turno.SwitchShift(); // Se pierde el turno, pasa a Jugador 2

                // Assert: Verificar que el resultado sea el esperado.

                // Verificar que el turno cambió al jugador oponente después de cambiar de Pokémon.
                Assert.That(turno.actualPlayer, Is.EqualTo(jugador2), "El turno debería haber cambiado al oponente después de cambiar de Pokémon.");
            }
        }
    }
}
