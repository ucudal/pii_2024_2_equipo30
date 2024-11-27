namespace Library.Tests.HistoryUserTests
{
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
                    new Pokemon { Name = "Squirtle" },
                    new Pokemon { Name = "Blastoise" }
                };

                // Crea los jugadores, pero aquí asignamos manualmente el Pokémon inicial
                var jugador1 = new Player(null, "Jugador 1", equipoJugador1);
                var jugador2 = new Player(null, "Jugador 2", equipoJugador2);

                // Asignar el primer Pokémon de cada equipo como el Pokémon activo
                jugador1.actualPokemon = jugador1.Team.FirstOrDefault(); // Asigna el primer Pokémon de equipoJugador1
                jugador2.actualPokemon = jugador2.Team.FirstOrDefault(); // Asigna el primer Pokémon de equipoJugador2

                // Verifica que el jugador 1 tenga un Pokémon inicial
                Assert.NotNull(jugador1.actualPokemon, "El jugador 1 debería tener un Pokémon inicial.");

                // Act
                // Jugador 1 cambia a su segundo Pokémon (Charmander a Charizard)
                jugador1.SwitchPokemon(1, null); // Cambia a Charizard

                // Después de cambiar de Pokémon, el turno debe pasar a Jugador 2
                var turno = new Shift(jugador1, jugador2);
                turno.SwitchShift(); // El turno cambia a Jugador 2

                // Assert
                // Verifica que el Pokémon actual de Jugador 1 sea Charizard después del cambio
                //Assert.NotNull(jugador1.actualPokemon, "El Pokémon actual de Jugador 1 no debería ser null.");
                //Assert.AreEqual("Charizard", jugador1.actualPokemon.Name, "El Pokémon actual de Jugador 1 debería ser Charizard.");

                // Verifica que el turno haya pasado a Jugador 2
                Assert.That(turno.actualPlayer, Is.EqualTo(jugador2),
                    "El turno debería haber cambiado al oponente después de cambiar de Pokémon.");
            }
        }

    }
}
