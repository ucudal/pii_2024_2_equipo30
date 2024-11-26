using System.Threading.Channels;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Collections.Generic;
using DSharpPlus.SlashCommands;
using Library.BotDiscord;

namespace Library;

/// <summary>
/// Clase que representa una batalla entre dos jugadores y maneja toda la lógica de turnos, ataques y uso de ítems.
/// </summary>
public class Battle : IBattle
{
    private Player Player1;
    private Player Player2;
    public Shift shift { get; }
    private int maxpokemons = 6;

    /// <summary>
    /// Constructor que inicializa la batalla con dos jugadores.
    /// </summary>
    /// <param name="player1">Jugador 1 de la batalla.</param>
    /// <param name="player2">Jugador 2 de la batalla.</param>
    public Battle(Player player1, Player player2)
    {
        this.Player1 = player1;
        this.Player2 = player2;
        this.shift = new Shift(player1, player2);
    }
    
    /// <summary>
    /// Método para iniciar una batalla.
    /// </summary>
    public async Task StartBattle(InteractionContext ctx)
    {
        if (!Player1.InGame || !Player2.InGame)
        {
            await ctx.Channel.SendMessageAsync("El juego no ha iniciado.");
            return;
        }
        if (Player1.Team.Count < 6 || Player2.Team.Count < 6)
        {
            await ctx.Channel.SendMessageAsync("Ambos jugadores deben tener al menos 6 Pokémons.");
            return;
        }
        await ctx.Channel.SendMessageAsync("La batalla ha iniciado.\n================ TURNOS DE BATALLA ================\n");
        shift.ShowShift(ctx);
        PlayShift(shift.actualPlayer, shift.enemyPlayer, ctx);
    }


    /// <summary>
    /// Método para iniciar el juego.
    /// </summary>
    public async Task StartGame(InteractionContext ctx)
    {
        Player1.InGame = true;
        Player2.InGame = true;
        await ctx.CreateResponseAsync($"Utiliza el comando /choose para elegir un pokemon. Deben elegir 6 pokemons.");
    }

    /// <summary>
    /// Método que ejecuta el turno de un jugador.
    /// </summary>
    /// <param name="actualPlayer">Jugador que realiza la acción en este turno.</param>
    /// <param name="enemyPlayer">Jugador enemigo que recibe la acción.</param>
    public async void PlayShift(Player actualPlayer, Player enemyPlayer, InteractionContext ctx)
    {
        string message = "";
        if (!actualPlayer.actualPokemon.OutOfAction())
        {
            message += "\n\n";
            message += actualPlayer.actualPokemon.ProcessStatus();
            message += "\n\n" +
                       $"{actualPlayer.NamePlayer}, tu pokemon actual es {actualPlayer.actualPokemon.Name} y tiene {actualPlayer.actualPokemon.Health:F1} puntos de vida" +
                       $"{actualPlayer.NamePlayer}, puedes utilizar los siguientes comandos:" +
                       $"/SelectItem <ItemNumber> <PokemonName>";
            message += ShowItemsByPlayer(actualPlayer, ctx);
            message += "\n\n" +
                       $"/Attack <MoveNumber>";
            message += ShowAttacksByPlayer(actualPlayer, ctx);
            message += "\n\n" +
                       $"/Switch <PokemonIndex>";
            message += ShowPokemonsToSwitch(actualPlayer, ctx);
            
        }
        else
        {
            message += $"{actualPlayer.NamePlayer} tu pokemon actual está fuera de combate. Debes elegir otro\n" +
                       $"Utiliza el siguiente comando: /Switch <PokemonIndex>";
            message += ShowPokemonsToSwitch(actualPlayer, ctx);
        }

        await ctx.Channel.SendMessageAsync(message);
    }

    /// <summary>
    /// Método para realizar un ataque del Pokémon del jugador actual al del jugador enemigo.
    /// </summary>
    /// <param name="actualPlayer">Jugador que realiza el ataque.</param>
    /// <param name="enemyPlayer">Jugador enemigo que recibe el ataque.</param>
    public async void Attack(Player actualPlayer, Player enemyPlayer, int moveNumber, InteractionContext ctx)
    {
        Pokemon actualPokemon = actualPlayer.actualPokemon;

        if (actualPokemon == null || actualPokemon.Moves == null || actualPokemon.Moves.Count == 0)
        {
            await ctx.Channel.SendMessageAsync($"{actualPokemon?.Name ?? "Ningún Pokémon"} no tiene movimientos disponibles.");
            shift.ShowShift(ctx);
            PlayShift(shift.actualPlayer, shift.enemyPlayer, ctx);
            return;  // No tiene movimientos disponibles, salir de la función
        }

        if (!(await actualPokemon.CanAtack(ctx)))
        {
            shift.ShowShift(ctx);
            PlayShift(shift.actualPlayer, shift.enemyPlayer, ctx);
            return;  // El Pokémon no puede atacar debido a su estado (dormido, paralizado, etc.)
        }

        int selectedMovement = moveNumber - 1;
        
        if (selectedMovement < 0 || selectedMovement >= actualPokemon.Moves.Count)
        {
            await ctx.Channel.SendMessageAsync("Selección de movimiento inválida. Intenta nuevamente.");
            shift.ShowShift(ctx);
            PlayShift(shift.actualPlayer, shift.enemyPlayer, ctx);
            return;
        }

        var MovementSelected = actualPokemon.Moves[selectedMovement];

        if (MovementSelected.SpecialAttack)
        {
            // Verificar si el ataque especial puede ser usado
            bool canUseEspecialAtack = actualPlayer.CanUseEspecialAtack(MovementSelected.MoveDetails.Name, actualPlayer.ObtainPersonalShift());

            await ctx.Channel.SendMessageAsync($"Verificando uso de ataque especial: {MovementSelected.MoveDetails.Name}. Shift personal actual: {actualPlayer.ObtainPersonalShift()}, Shift último uso: {actualPlayer.ObtainLastShiftofAttack(MovementSelected.MoveDetails.Name)}");

            if (canUseEspecialAtack)
            {
                // Ejecutar ataque especial y registrar el _shift
                bool succefulAttack = shift.ExecuteSpecialAttack(actualPlayer, actualPokemon, MovementSelected, actualPlayer.ObtainPersonalShift(), ctx);
                if (succefulAttack)
                {
                    actualPlayer.RegisterSpecialAttack(MovementSelected.MoveDetails.Name, actualPlayer.ObtainPersonalShift());
                }
            }
            else
            {
                await ctx.Channel.SendMessageAsync($"No puedes usar el ataque especial {MovementSelected.MoveDetails.Name} en este momento. Debes esperar más turnos. Selecciona otro movimiento.");
                shift.ShowShift(ctx);
                PlayShift(shift.actualPlayer, shift.enemyPlayer, ctx);
                return;
            }
        }
        else
        {
            // Realizar un ataque regular si no es un ataque especial
            actualPokemon.AttackP(actualPlayer, enemyPlayer.actualPokemon, MovementSelected, actualPlayer.ObtainPersonalShift(), ctx);
            await ctx.Channel.SendMessageAsync($"{actualPlayer.NamePlayer}'s {actualPokemon.Name} ha atacado a {enemyPlayer.NamePlayer}'s {enemyPlayer.actualPokemon.Name} causando daño.");
        }
        
        // Incrementar _shift personal del player actual después de que termine su _shift
        actualPlayer.IncrementPersonalShift();
        
        // Verifica el ganador
        if (Player1.AllOutOfCombat())
        {
            await ctx.Channel.SendMessageAsync($"{Player1.NamePlayer} no tiene Pokémon disponibles. Todos están fuera de combate.");
            await ctx.Channel.SendMessageAsync($"¡{Player2.NamePlayer} ha ganado!");
            Player1.InGame = false;
            Player2.InGame = false;
            return;
        }
        if (Player2.AllOutOfCombat())
        {
            await ctx.Channel.SendMessageAsync($"{Player2.NamePlayer} no tiene Pokémon disponibles. Todos están fuera de combate.");
            await ctx.Channel.SendMessageAsync($"¡{Player1.NamePlayer} ha ganado!");
            Player1.InGame = false;
            Player2.InGame = false;
            return;
        }
        
        shift.SwitchShift();
        shift.ShowShift(ctx);
        PlayShift(shift.actualPlayer, shift.enemyPlayer, ctx);
    }

    /// <summary>
    /// Método para usar un ítem durante el turno de un jugador.
    /// </summary>
    /// <param name="player">Jugador que va a usar el ítem.</param>
    public async void UseItem(Player player, int itemNumber, string pokemonName , InteractionContext ctx)
    {
        Pokemon pokemonToUseItem = player.Team.Find(pokemon => pokemon.Name == pokemonName);
        switch (itemNumber)
        {
            case 1:
                if (player.Superpotion.Quantity > 0)
                {
                    if (player.actualPokemon == null || player.actualPokemon.OutOfAction())
                    {
                        await ctx.Channel.SendMessageAsync("No hay un Pokémon activo que pueda ser curado. Por favor, selecciona otro Pokémon para curar.");
                        shift.ShowShift(ctx);
                        PlayShift(shift.actualPlayer, shift.enemyPlayer, ctx);
                        return;
                    }

                    player.Superpotion.Use(pokemonToUseItem);
                    shift.SwitchShift();
                    await ctx.Channel.SendMessageAsync($"\n Superpoción usada con éxito en {player.actualPokemon.Name}.\n");
                }
                else
                {
                    await ctx.Channel.SendMessageAsync("No te quedan Superpociones.");
                }
                break;
            case 2:
                if (player.Revive.Quantity > 0)
                {
                    List<string> deadPokemons = new List<string>();
                    foreach (var pokemon in player.Team)
                    {
                        if (pokemon.OutOfAction())
                        {
                            deadPokemons.Add(pokemon.Name);
                        }
                    }

                    if (deadPokemons.Count == 0)
                    {
                        await ctx.Channel.SendMessageAsync("No hay Pokémon muertos para revivir.");
                        shift.ShowShift(ctx);
                        PlayShift(shift.actualPlayer, shift.enemyPlayer, ctx);
                        return;
                    }
                    
                    if (deadPokemons.Contains(pokemonName))
                    {
                        player.Revive.Use(pokemonToUseItem);
                        await ctx.Channel.SendMessageAsync($"\n {pokemonToUseItem.Name} ha sido revivido con éxito.\n");
                        shift.SwitchShift();
                    }
                    else
                    {
                        await ctx.Channel.SendMessageAsync("NamePlayer inválido o el Pokémon no está fuera de combate. Por favor, intenta nuevamente.");
                    }
                }
                else
                {
                    await ctx.Channel.SendMessageAsync("No te quedan pociones para Revivir.");
                }
                break;
            case 3:
                if (player.Totalcure.Quantity > 0)
                {
                    player.Totalcure.Use(pokemonToUseItem);
                    await ctx.Channel.SendMessageAsync($"\n Cura Total usada con éxito en {player.actualPokemon.Name}.\n");
                    shift.SwitchShift();
                }
                else
                {
                    await ctx.Channel.SendMessageAsync("No te quedan Cura Total.");
                }
                break;
            default:
                await ctx.Channel.SendMessageAsync("Elección inválida. Por favor, intenta nuevamente.");
                break;
        }
        
        shift.ShowShift(ctx);
        PlayShift(shift.actualPlayer, shift.enemyPlayer, ctx);
    }

    /// <summary>
    /// Método para cambiar el Pokémon del jugador durante un turno.
    /// </summary>
    /// <param name="player">Jugador que cambiará su Pokémon.</param>
    public async void SwitchPokemon(Player player, int pokemonIndex, InteractionContext ctx)
    {
        if (pokemonIndex > 0 && pokemonIndex <= player.Team.Count)
        {
            var choosedPokemon = player.Team[pokemonIndex - 1];
            if (choosedPokemon != player.actualPokemon && !choosedPokemon.OutOfAction())
            {
                player.SwitchPokemon(pokemonIndex - 1, ctx);
                shift.SwitchShift();
                await ctx.Channel.SendMessageAsync($"\nCambio realizado. Ahora tu Pokémon es {player.actualPokemon.Name}\n");
            }
            else
            {
                await ctx.Channel.SendMessageAsync("El Pokémon seleccionado está fuera de combate o ya es tu Pokémon actual.");
            }
        }
        else
        {
            await ctx.Channel.SendMessageAsync("Elección inválida. Intenta nuevamente.");
        }
        
        shift.ShowShift(ctx);
        PlayShift(shift.actualPlayer, shift.enemyPlayer, ctx);
    }

    public string ShowItemsByPlayer(Player player, InteractionContext ctx)
    {
        string message = "\nItems disponibles:\n" +
                         $"1- Superpoción: {player.Superpotion.Quantity}\n" +
                         $"2- Revivir: {player.Revive.Quantity}\n" +
                         $"3- Cura Total: {player.Totalcure.Quantity}";
        return message;
    }

    public string ShowAttacksByPlayer(Player player, InteractionContext ctx)
    {
        string message = $"\n{player.NamePlayer}, elige un movimiento de: {player.actualPokemon.Name}";
    
        for (int i = 0; i < player.actualPokemon.Moves.Count; i++)
        {
            var movement = player.actualPokemon.Moves[i];
            message += $"{i + 1}: {movement.MoveDetails.Name} (Poder: {movement.MoveDetails.Power}) (Precisión: {movement.MoveDetails.Accuracy}) Especial: {movement.SpecialStatus}";
        }

        return message;
    }
    
    public string ShowPokemonsToSwitch(Player player, InteractionContext ctx)
    {
        string message = $"\nPokemons disponibles para cambiar:\n";
        for (int i = 0; i < player.Team.Count; i++)
        {   
            var pokemon = player.Team[i];
            if (!pokemon.OutOfAction() && (pokemon != player.actualPokemon))
            {
                message += $"{i + 1}: {pokemon.Name} - {pokemon.Health} de vida";
            }
        }

        return message;
    }
}
