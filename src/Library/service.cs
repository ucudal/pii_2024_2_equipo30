﻿using Library.BotDiscord;

namespace Library;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;

public class PokemonService
{
    private static readonly HttpClient client = new HttpClient();
    public async Task<Pokemon> PokemonElection(string pokemonId,InteractionContext ctx)
    {   
        PokemonApi pokemonApi = new PokemonApi(client);
        PokemonCreator pokemonCreator = new PokemonCreator(pokemonApi);
        await ctx.Channel.SendMessageAsync("\n==================== SELECCIÓN DE POKÉMON ====================\n");
        await ctx.Channel.SendMessageAsync($"Selección de Pokémon para el jugador {ctx.User.Username}:\n");
        try
        {
            var response = await client.GetAsync($"https://pokeapi.co/api/v2/pokemon/{pokemonId}");
            if (response.IsSuccessStatusCode)
            {
                var pokemon = await pokemonCreator.CreatePokemon(pokemonId);
                await ctx.Channel.SendMessageAsync($"Has seleccionado a: {pokemon.Name}\n");
                return pokemon;
            }
            else
            {
                await ctx.Channel.SendMessageAsync($"No se pudo obtener datos para: {pokemonId}. Por favor, intenta nuevamente.\n");
            }
        }
        catch (Exception ex)
        {
            await ctx.Channel.SendMessageAsync($"Ocurrió un error al intentar obtener el Pokémon: {ex.Message}. Por favor, intente nuevamente.\n");
        }
        return null;
    }
}