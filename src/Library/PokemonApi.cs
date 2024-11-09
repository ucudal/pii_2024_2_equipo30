using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Library
{
    /// <summary>
    /// Clase que encapsula la lógica para obtener datos desde la API de Pokémon.
    /// Implementa la interfaz "IPokemonApi".
    /// </summary>
    public class PokemonApi : IPokemonApi
    {
        /// <summary>
        /// Cliente HTTP utilizado para realizar solicitudes a la API de Pokémon.
        /// </summary>
        private HttpClient httpclient;

        /// <summary>
        /// Constructor de la clase "PokemonApi".
        /// Inicializa una nueva instancia de "PokemonApi" con un cliente HTTP proporcionado.
        /// </summary>
        /// <param name="client">El cliente HTTP que se utilizará para realizar las solicitudes a la API.</param>
        public PokemonApi(HttpClient client)
        {
            httpclient = client;
        }

        /// <summary>
        /// Método asíncrono que obtiene los detalles de un Pokémon utilizando su identificador (nombre o ID).
        /// Realiza una solicitud GET a la API de Pokémon y deserializa la respuesta en un objeto de tipo "Pokemon".
        /// </summary>
        /// <param name="pokemonId">El identificador del Pokémon (nombre o ID) que se desea obtener.</param>
        /// <returns>Devuelve una tarea que se resuelve en un objeto "Pokemon" con los detalles del Pokémon.</returns>
        public async Task<Pokemon> GetPokemonDetails(string pokemonId)
        {
            string apiUrl = $"https://pokeapi.co/api/v2/pokemon/{pokemonId}";
            return await httpclient.GetFromJsonAsync<Pokemon>(apiUrl);
        }

        /// <summary>
        /// Método asíncrono que obtiene los detalles de un movimiento de un Pokémon a partir de la URL específica del movimiento.
        /// Realiza una solicitud GET a la URL del movimiento y deserializa la respuesta en un objeto de tipo "MoveDetail".
        /// </summary>
        /// <param name="url">La URL del movimiento que se desea obtener.</param>
        /// <returns>Devuelve una tarea que se resuelve en un objeto "MoveDetail" con los detalles del movimiento.</returns>
        public async Task<MoveDetail> GetMoveDetails(string url)
        {
            return await httpclient.GetFromJsonAsync<MoveDetail>(url);
        }
    }
}
