namespace Library.Tests.HistoryUserTests;

using Library;
using NUnit.Framework;
using System.Collections.Generic;
public class HistoryUser5_Test

[TestFixture]
public class PokemonTests
{
    [Test]
    public void Test_IndicadorDeTurno_CorrectamenteMuestraElTurnoActual()
    {
        // Arrange: Crear los Pokémon con vida inicial
        Pokemon1 = new Pokemon("Charizard", 6, 100, 84, 78, 109, 85, fireType, new List<Move>());
        Pokemon2 = new Pokemon("Venusaur", 3, 100, 82, 83, 100, 100, grassType, new List<Move>());

        Jugador jugador1 = new Jugador("Jugador 1", listPokemonJugador1);
        Jugador jugador2 = new Jugador("Jugador 2", listPokemonJugador2);
        // Creamos el juego con los dos Pokémon
        
        // Act: El jugador realiza una acción (por ejemplo, un ataque)
        juego.RealizarTurno(pokemonJugador, pokemonOponente); // Pikachu ataca a Charizard
        
        // Assert: Verificar que el indicador de turno se actualice correctamente
        string turnoActual = juego.ObtenerIndicadorTurno();
        
        // Comprobamos que el indicador de turno se ha actualizado correctamente
        Assert.AreEqual("Es el turno de Charizard", turnoActual); // Después del turno de Pikachu, debería ser el turno de Charizard
        
        // El jugador oponente realiza su acción
        juego.RealizarTurno(pokemonOponente, pokemonJugador); // Charizard ataca a Pikachu
        
        // Verificar nuevamente el indicador de turno
        turnoActual = juego.ObtenerIndicadorTurno();
        Assert.AreEqual("Es el turno de Pikachu", turnoActual); // Después del turno de Charizard, debería ser el turno de Pikachu
    }
}
