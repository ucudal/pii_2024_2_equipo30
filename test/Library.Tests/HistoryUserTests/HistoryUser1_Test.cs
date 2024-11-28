using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;
using Library;
using NUnit.Framework;
using Library.BotDiscord;

namespace Library.Tests.HistoryUserTests
{
    /// <summary>
    /// Clase que contiene pruebas unitarias para verificar la funcionalidad relacionada con el historial del usuario.
    /// </summary>
    public class HistoryUser1_Test
    {
        /// <summary>
        /// Configuración que se ejecuta antes de cada prueba.
        /// </summary>
        [SetUp]
        public void setup()
        {
            // Método para configurar las condiciones iniciales necesarias antes de cada prueba.
        }

        /// <summary>
        /// Prueba para verificar que se pueden agregar correctamente Pokémon a una lista.
        /// </summary>
        [Test]
        public void HistoryUser1_Test1()
        {
            // Arrange: Configuración inicial de la prueba.

            // Crear una lista de Pokémon.
            List<Pokemon> listaPokemon = new List<Pokemon>();
            {
                new Pokemon("Pikachu", 35, 100, 55, 40, 90, 50, new Type(), new List<Move> { new Move() });
                new Pokemon("Charizard", 78, 150, 84, 78, 100, 85, new Type(), new List<Move> { new Move() });
                new Pokemon("Bulbasaur", 45, 100, 49, 49, 45, 60, new Type(), new List<Move> { new Move() });
                new Pokemon("Squirtle", 44, 100, 48, 65, 43, 50, new Type(), new List<Move> { new Move() });
                new Pokemon("Jigglypuff", 115, 100, 45, 20, 20, 25, new Type(), new List<Move> { new Move() });
                new Pokemon("Gengar", 60, 100, 65, 60, 110, 75, new Type(), new List<Move> { new Move() });
            }

            // Crear un jugador con una lista de Pokémon.
            Player jugador = new Player(null, "Ernesto_El_entrenador", listaPokemon); //error

            // Act: Acción que se va a realizar en la prueba.

            // Añadir más Pokémon a la lista.
            listaPokemon.Add(new Pokemon("Pikachu", 35, 100, 55, 40, 90, 50, new Type(), new List<Move> { new Move() }));
            listaPokemon.Add(new Pokemon("Charizard", 78, 150, 84, 78, 100, 85, new Type(), new List<Move> { new Move() }));
            listaPokemon.Add(new Pokemon("Bulbasaur", 45, 100, 49, 49, 45, 60, new Type(), new List<Move> { new Move() }));
            listaPokemon.Add(new Pokemon("Squirtle", 44, 100, 48, 65, 43, 50, new Type(), new List<Move> { new Move() }));
            listaPokemon.Add(new Pokemon("Jigglypuff", 115, 100, 45, 20, 20, 25, new Type(), new List<Move> { new Move() }));
            listaPokemon.Add(new Pokemon("Gengar", 60, 100, 65, 60, 110, 75, new Type(), new List<Move> { new Move() }));

            // Assert: Afirmación para verificar el resultado esperado.

            // Verificar que la lista tiene exactamente 6 Pokémon.
            Assert.That(listaPokemon.Count, Is.EqualTo(6));
        }
    }
}
