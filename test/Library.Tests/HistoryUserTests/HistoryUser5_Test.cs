/*namespace Library.Tests.HistoryUserTests;

using Library;
using NUnit.Framework;
using System.Collections.Generic;
[TestFixture]
public class HistoryUser5_Test
{
    private Pokemon Pokemon1;
    private Pokemon Pokemon2;
    private List<Pokemon> listaPokemon;
    [Test]
    public void Test_IndicadorDeTurno_CorrectamenteMuestraElTurnoActual()
    {
        // Creamos instancias de Type y establecemos sus valores
        var fireType = new Type();
        fireType.SetType("fire");

        var grassType = new Type();
        grassType.SetType("grass");
        // Arrange: Crear los Pokémon con vida inicial
        Pokemon1 = new Pokemon("Charizard", 6, 100, 84, 78, 109, 85, fireType, new List<Move>());
        Pokemon2 = new Pokemon("Venusaur", 3, 100, 82, 83, 100, 100, grassType, new List<Move>());
        listaPokemon.Add(Pokemon1);
        listaPokemon.Add(Pokemon2);
        Jugador jugador1 = new Jugador("Jugador 1", listaPokemon);
        Jugador jugador2 = new Jugador("Jugador 2", listaPokemon);
        // Creamos el juego con los dos Pokémon
        var batalla = new Batalla(jugador1, jugador2);
        // Act: El jugador realiza una acción (por ejemplo, un ataque)
        turno.MostrarTurno();
        
        // Assert: Verificar que el indicador de turno se actualice correctamente
        
        // Comprobamos que el indicador de turno se ha actualizado correctamente
        Assert.AreEqual("Es el turno de Charizard", turnoActual); // Después del turno de Pikachu, debería ser el turno de Charizard
        
        // El jugador oponente realiza su acción
        juego.RealizarTurno(pokemonOponente, pokemonJugador); // Charizard ataca a Pikachu
        
        // Verificar nuevamente el indicador de turno
        turnoActual = juego.ObtenerIndicadorTurno();
        Assert.AreEqual("Es el turno de Pikachu", turnoActual); // Después del turno de Charizard, debería ser el turno de Pikachu
    }
}*/
namespace Library.Tests.HistoryUserTests;

using Library;
using NUnit.Framework;
using System.Collections.Generic;

public class HistoryUser5_Test
{
    public class TurnoTests
    {
        private Pokemon Pokemon1;
        private Pokemon Pokemon2;
        private List<Pokemon> listaPokemon;
        [Test]
        public void MostrarTurno_ImprimeTurnoCorrecto()
        {
            // Arrange
            // Creamos instancias de Type y establecemos sus valores
            var fireType = new Type();
            fireType.SetType("fire");

            var grassType = new Type();
            grassType.SetType("grass");
            // Arrange: Crear los Pokémon con vida inicial
            Pokemon1 = new Pokemon("Charizard", 6, 100, 84, 78, 109, 85, fireType, new List<Move>());
            Pokemon2 = new Pokemon("Venusaur", 3, 100, 82, 83, 100, 100, grassType, new List<Move>());
            listaPokemon.Add(Pokemon1);
            listaPokemon.Add(Pokemon2);
            var jugador1 = new Player(null,"Jugador1",listaPokemon);
            var jugador2 = new Player(null,"Jugador2",listaPokemon);
            var turno = new Shift(jugador1, jugador2);

            // Captura la salida de la consola
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                turno.ShowShift(null);

                // Assert
                var expectedOutput = $"-- Turno 1 / {jugador1.NamePlayer} es tu turno! --{Environment.NewLine}";
                Assert.AreEqual(expectedOutput, sw.ToString());
            }
        }
        
        [Test]
        public void CambiarTurno_ActualizaTurnoCorrectamente()
        {
            // Arrange
            var jugador1 = new Player(null,"Jugador1",listaPokemon);
            var jugador2 = new Player(null,"Jugador2",listaPokemon);
            var turno = new Shift(jugador1, jugador2);

            // Act - Cambio de turno
            turno.SwitchShift();

            // Captura la salida de la consola
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                turno.ShowShift(null);

                // Assert
                var expectedOutput = $"-- Turno 2 / {jugador2.NamePlayer} es tu turno! --{Environment.NewLine}";
                Assert.AreEqual(expectedOutput, sw.ToString());
            }
        }
    }
}
