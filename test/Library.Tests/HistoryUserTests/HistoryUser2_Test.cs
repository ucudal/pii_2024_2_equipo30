namespace Library.Tests.HistoryUserTests;
using NUnit.Framework;
using System.Collections.Generic;
using Library.BotDiscord;
using DSharpPlus.Entities;

/*public class HistoryUser2_Test
{
    [Test]
    public void VerAtaquesDisponibles_Y_RestringirAtaquesEspeciales()
    {
        // Arrange
        // Crear los ataques con detalles
        var ataqueNormal = new Move { MoveDetails = new Attack { Name = "Tackle", IsSpecial = false } };
        var ataqueEspecial = new Move { MoveDetails = new Attack { Name = "Flamethrower", IsSpecial = true } };

        var pokemonJugador1 = new Pokemon
        {
            Name = "Charmander",
            Moves = new List<Move> { ataqueNormal, ataqueEspecial }
        };

        var pokemonJugador2 = new Pokemon
        {
            Name = "Squirtle",
            Moves = new List<Move> { ataqueNormal, ataqueEspecial }
        };

        var equipoJugador1 = new List<Pokemon> { pokemonJugador1 };
        var equipoJugador2 = new List<Pokemon> { pokemonJugador2 };

        // Crear jugadores
        var jugador1 = new Player(null, "Jugador 1", equipoJugador1);
        var jugador2 = new Player(null, "Jugador 2", equipoJugador2);

        // Asignar el primer Pokémon de cada jugador
        jugador1.actualPokemon = jugador1.Team.FirstOrDefault();
        jugador2.actualPokemon = jugador2.Team.FirstOrDefault();

        // Crear el objeto Shift para manejar los turnos
        var turno = new Shift(jugador1, jugador2);

        // Act - Verificar ataques disponibles en el primer turno (Turno 1)
        var ataquesDisponiblesJugador1 = jugador1.actualPokemon.Moves;

        // Verificar que los ataques normales y especiales están disponibles
        Assert.Contains(ataqueNormal, ataquesDisponiblesJugador1, "El ataque normal debe estar disponible.");
        Assert.Contains(ataqueEspecial, ataquesDisponiblesJugador1, "El ataque especial debe estar disponible.");

        // El jugador 1 intenta usar el ataque especial en el turno 1
        var resultadoAtaqueEspecialTurno1 = turno.ExecuteSpecialAttack(jugador1, jugador1.actualPokemon, ataqueEspecial,
            turno.shiftNumber, null);

        // El ataque especial no debe poder usarse en el primer turno
        Assert.IsFalse(resultadoAtaqueEspecialTurno1, "El ataque especial no debería poder usarse en el primer turno.");

        // SwitchShift: El turno pasa al Jugador 2
        turno.SwitchShift();

        // Act - Verificar que el jugador 2 también tiene acceso a los ataques disponibles
        var ataquesDisponiblesJugador2 = jugador2.actualPokemon.Moves;
        Assert.Contains(ataqueNormal, ataquesDisponiblesJugador2, "El ataque normal debe estar disponible.");
        Assert.Contains(ataqueEspecial, ataquesDisponiblesJugador2, "El ataque especial debe estar disponible.");

        // El jugador 2 intenta usar el ataque especial en el turno 2
        var resultadoAtaqueEspecialTurno2 = turno.ExecuteSpecialAttack(jugador2, jugador2.actualPokemon, ataqueEspecial,
            turno.shiftNumber, null);

        // El ataque especial debería poder usarse en el segundo turno
        Assert.IsTrue(resultadoAtaqueEspecialTurno2, "El ataque especial debería poder usarse en el segundo turno.");

        // SwitchShift: El turno vuelve al Jugador 1
        turno.SwitchShift();

        // Act - Verificar que el jugador 1 ahora puede usar el ataque especial en el turno 3
        var resultadoAtaqueEspecialTurno3 = turno.ExecuteSpecialAttack(jugador1, jugador1.actualPokemon, ataqueEspecial,
            turno.shiftNumber, null);

        // El ataque especial debería poder usarse en el tercer turno
        Assert.IsTrue(resultadoAtaqueEspecialTurno3, "El ataque especial debería poder usarse en el tercer turno.");
    }
}*/
