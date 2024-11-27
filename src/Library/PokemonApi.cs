using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Library
{
    /// <summary>
    /// Clase que proporciona métodos para interactuar con la API de Pokémon.
    /// Implementa la interfaz IPokemonApi.
    /// </summary>
    public class PokemonApi : IPokemonApi
    {
        /// <summary>
        /// Cliente HTTP para realizar solicitudes a la API.
        /// </summary>
        private HttpClient httpclient;

        /// <summary>
        /// Constructor de la clase PokemonApi.
        /// </summary>
        /// <param name="client">Una instancia de HttpClient para realizar las solicitudes.</param>
        public PokemonApi(HttpClient client)
        {
            httpclient = client;
        }

        /// <summary>
        /// Obtiene los detalles de un Pokémon específico desde la API de Pokémon.
        /// </summary>
        /// <param name="pokemonId">El identificador (nombre o número) del Pokémon.</param>
        /// <returns>Una tarea que representa la operación asincrónica. El resultado contiene los detalles del Pokémon solicitado.</returns>
        public async Task<Pokemon> GetPokemonDetails(string pokemonId)
        {
            string apiUrl = $"https://pokeapi.co/api/v2/pokemon/{pokemonId}";
            return await httpclient.GetFromJsonAsync<Pokemon>(apiUrl);
        }

        /// <summary>
        /// Obtiene los detalles de un movimiento específico desde la API de Pokémon.
        /// </summary>
        /// <param name="url">La URL del recurso que contiene los detalles del movimiento.</param>
        /// <returns>Una tarea que representa la operación asincrónica. El resultado contiene los detalles del movimiento solicitado.</returns>
        public async Task<MoveDetail> GetMoveDetails(string url)
        {
            return await httpclient.GetFromJsonAsync<MoveDetail>(url);
        }
    }
}