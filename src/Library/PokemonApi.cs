using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Library;

// Clase que encapsula la lógica para obtener datos desde la API
public class PokemonApi : IPokemonApi
{
    private HttpClient httpclient;

    public PokemonApi(HttpClient client)
    {
        httpclient = client;
    }

    public async Task<Pokemon> GetPokemonDetails(string pokemonId)
    {
        string apiUrl = $"https://pokeapi.co/api/v2/pokemon/{pokemonId}";
        return await httpclient.GetFromJsonAsync<Pokemon>(apiUrl);
    }

    public async Task<MoveDetail> GetMoveDetails(string url)
    {
        return await httpclient.GetFromJsonAsync<MoveDetail>(url);
    }
}
