using System.Threading.Channels;
using Library;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.Intrinsics.Arm;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Program
{
    /// <summary>
    /// Programa principal que permite seleccionar equipos de Pokémon para dos jugadores
    /// y luego inicia una batalla entre ellos.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Cliente HTTP usado para realizar solicitudes a la API de Pokémon.
        /// </summary>

        /// <summary>
        /// Método principal del programa.
        /// Permite la selección de Pokémon para cada jugador y luego maneja la batalla entre ellos.
        /// </summary>
        /// <param name="args">Argumentos del programa (no se utilizan).</param>
        public static async Task Main(string[] args)
        {
            // Crear jugadores
            Player jugador1 = new Player("Player 1", null,true);
            Player jugador2 = new Player("Player 2");
            Player jugador3 = new Player("Player 3");
            Player jugador4 = new Player("Player 4");
            Player jugador5= new Player("Player 5");
            WaitList ListaEspera = new WaitList();
            ListaEspera.AddPlayer(jugador1);
            ListaEspera.AddPlayer(jugador3);
            ListaEspera.AddPlayer(jugador4);
            ListaEspera.AddPlayer(jugador5);
            ListaEspera.ShowPlayers();
            jugador3.InBattle = true;
            ListaEspera.CleanWaitList();
            ListaEspera.ShowPlayers();
            
            // Crear y manejar la batalla
            if (ListaEspera.Contains(jugador4) && ListaEspera.Contains(jugador5))
            {
                var batalla = new Battle(jugador4, jugador5);
                Console.WriteLine("\n==================== INICIANDO BATALLA ====================\n");
                await batalla.StartBattle();
            }
            else
            {
                Console.WriteLine($"La batalla no ha sido iniciada debido a que un jugador no estaba en la lista de espera");
            }
            
        }
    }
}
