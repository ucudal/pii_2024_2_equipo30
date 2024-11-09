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
    /// Implementa la interfaz <see cref="IPokemonApi"/>.
    /// </summary>
    public class PokemonApi : IPokemonApi
    {
        /// <summary>
        /// Cliente HTTP utilizado para realizar solicitudes a la API.
        /// </summary>
        private HttpClient httpclient;

        /// <summary>
        /// Constructor para inicializar la clase `PokemonApi` con un cliente HTTP.
        /// </summary>
        /// <param name="client">Instancia de <see cref="HttpClient"/> que se utilizará para realizar solicitudes a la API.</param>
        public PokemonApi(HttpClient client)
        {
            httpclient = client;
        }

        /// <summary>
        /// Obtiene los detalles de un Pokémon específico a partir de su identificador o nombre.
        /// </summary>
        /// <param name="pokemonId">Identificador o nombre del Pokémon que se desea consultar.</param>
        /// <returns>Una tarea que representa la operación asíncrona y devuelve un objeto <see cref="Pokemon"/> con los detalles del Pokémon.</returns>
        public async Task<Pokemon> GetPokemonDetails(string pokemonId)
        {
            string apiUrl = $"https://pokeapi.co/api/v2/pokemon/{pokemonId}";
            return await httpclient.GetFromJsonAsync<Pokemon>(apiUrl);
        }

        /// <summary>
        /// Obtiene los detalles de un movimiento específico a partir de una URL proporcionada.
        /// </summary>
        /// <param name="url">URL del movimiento en la API de Pokémon para obtener sus detalles.</param>
        /// <returns>Una tarea que representa la operación asíncrona y devuelve un objeto <see cref="MoveDetail"/> con los detalles del movimiento.</returns>
        public async Task<MoveDetail> GetMoveDetails(string url)
        {
            return await httpclient.GetFromJsonAsync<MoveDetail>(url);
        }
    }
}
