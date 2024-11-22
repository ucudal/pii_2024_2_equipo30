using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Library;

/// <summary>
/// Interfaz que define métodos para interactuar con una API de Pokémon y obtener detalles sobre Pokémon y movimientos específicos.
/// </summary>
public interface IPokemonApi
{
    /// <summary>
    /// Obtiene los detalles de un Pokémon específico a partir de su identificador o nombre.
    /// </summary>
    /// <param name="pokemonId">Identificador o nombre del Pokémon que se desea consultar.</param>
    /// <returns>Un objeto <see cref="Pokemon"/> con los detalles del Pokémon.</returns>
    Task<Pokemon> GetPokemonDetails(string pokemonId);

    /// <summary>
    /// Obtiene los detalles de un movimiento específico a partir de una URL.
    /// </summary>
    /// <param name="url">URL del movimiento para obtener sus detalles.</param>
    /// <returns>Un objeto <see cref="MoveDetail"/> con los detalles del movimiento.</returns>
    Task<MoveDetail> GetMoveDetails(string url);
}