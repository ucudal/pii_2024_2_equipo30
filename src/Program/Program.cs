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
using Library.BotDiscord;

namespace Program
{
    /// <summary>
    /// Clase principal que contiene el punto de entrada del programa.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Punto de entrada principal del programa.
        /// Inicializa e inicia el bot de Discord.
        /// </summary>
        /// <param name="args">Argumentos de la línea de comandos.</param>
        static async Task Main(string[] args)
        {
            // Crear una instancia del bot de Discord e iniciar el proceso de conexión
            var bot = new DiscordBot();
            await bot.Iniciate();
        }
    }
}

