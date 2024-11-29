using System.Threading.Channels;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Collections.Generic;
using DSharpPlus.SlashCommands;
using Library.BotDiscord;

namespace Library;

/// <summary>
/// Clase "Battle" que maneja la lógica de la batalla entre dos jugadores en un entorno de Discord.
/// </summary>
public class Battle : IBattle
{
    /// <summary>
    /// Jugador 1 en la batalla.
    /// </summary>
    private Player Player1;
    
    /// <summary>
    /// Jugador 2 en la batalla.
    /// </summary>
    private Player Player2;

    /// <summary>
    /// Instancia de "Shift" que maneja los turnos durante la batalla.
    /// </summary>
    public Shift shift { get; }

    /// <summary>
    /// Máximo número de Pokémon permitidos en un equipo.
    /// </summary>
    private int maxpokemons = 6;
    
    public Type Type { get; set; }

    /// <summary>
    /// Constructor de la clase "Battle".
    /// Inicializa los jugadores y el objeto "Shift".
    /// </summary>
    /// <param name="player1">El primer jugador.</param>
    /// <param name="player2">El segundo jugador.</param>
    public Battle(Player player1, Player player2)
    {
        this.Player1 = player1;
        this.Player2 = player2;
        this.shift = new Shift(player1, player2);
    }
    
    /// <summary>
    /// Método para iniciar la batalla entre dos jugadores en Discord.
    /// </summary>
    /// <param name="ctx">Contexto de la interacción en Discord.</param>
    public async Task StartBattle(InteractionContext ctx)
    {
        if (!Player1.InGame || !Player2.InGame)
        {
            await ctx.Channel.SendMessageAsync("El juego no ha iniciado.\n");
            return;
        }
        if (Player1.Team.Count < 6 || Player2.Team.Count < 6)
        {
            await ctx.Channel.SendMessageAsync("Ambos jugadores deben tener al menos 6 Pokémons.\n");
            return;
        }
        await ctx.Channel.SendMessageAsync("La batalla ha iniciado.\n================ TURNOS DE BATALLA ================\n");
        shift.ShowShift(ctx);
        PlayShift(shift.actualPlayer, shift.enemyPlayer, ctx);
    }

    /// <summary>
    /// Método para iniciar el juego y permitir a los jugadores elegir sus Pokémon.
    /// </summary>
    /// <param name="ctx">Contexto de la interacción en Discord.</param>
    public async Task StartGame(InteractionContext ctx)
    {
        Player1.InGame = true;
        Player2.InGame = true;
        await ctx.CreateResponseAsync($"Utiliza el comando /choose para elegir un pokemon. Deben elegir 6 pokemons.\n");
    }

    /// <summary>
    /// Maneja el turno del jugador actual, permitiéndole atacar, usar ítems o cambiar de Pokémon.
    /// </summary>
    /// <param name="actualPlayer">Jugador que está jugando el turno actual.</param>
    /// <param name="enemyPlayer">Jugador enemigo.</param>
    /// <param name="ctx">Contexto de la interacción en Discord.</param>
    public async void PlayShift(Player actualPlayer, Player enemyPlayer, InteractionContext ctx)
    {
        string message = "";
        if (!actualPlayer.actualPokemon.OutOfAction())
        {
            message += "\n\n";
            message += actualPlayer.actualPokemon.ProcessStatus();
            message += "\n\n" +
                       $"{actualPlayer.NamePlayer}, tu pokemon actual es {actualPlayer.actualPokemon.Name} y tiene {actualPlayer.actualPokemon.Health:F1} puntos de vida.\n" +
                       $"{actualPlayer.NamePlayer}, puedes utilizar los siguientes comandos:\n\n" +
                       $"/SelectItem <ItemNumber> <PokemonName>\n";
            message += ShowItemsByPlayer(actualPlayer, ctx);
            message += "\n\n" +
                       $"/Attack <MoveNumber>\n";
            message += ShowAttacksByPlayer(actualPlayer, ctx);
            message += "\n\n" +
                       $"/Switch <PokemonIndex>\n";
            message += ShowPokemonsToSwitch(actualPlayer, ctx);
            
        }
        else
        {
            message += $"{actualPlayer.NamePlayer} tu pokemon actual está fuera de combate. Debes elegir otro\n" +
                       $"Utiliza el siguiente comando: /Switch <PokemonIndex>\n";
            message += ShowPokemonsToSwitch(actualPlayer, ctx);
        }

        await ctx.Channel.SendMessageAsync(message);
    }

    /// <summary>
    /// Ejecuta la acción de ataque del jugador actual hacia el jugador enemigo.
    /// </summary>
    /// <param name="actualPlayer">Jugador que ataca.</param>
    /// <param name="enemyPlayer">Jugador que recibe el ataque.</param>
    /// <param name="moveNumber">Número del movimiento a usar.</param>
    /// <param name="ctx">Contexto de la interacción en Discord.</param>
    public async void Attack(Player actualPlayer, Player enemyPlayer, int moveNumber, InteractionContext ctx)
    {
        Pokemon actualPokemon = actualPlayer.actualPokemon;

        if (actualPokemon == null || actualPokemon.Moves == null || actualPokemon.Moves.Count == 0)
        {
            await ctx.Channel.SendMessageAsync($"{actualPokemon?.Name ?? "Ningún Pokémon"} no tiene movimientos disponibles.\n");
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
            await ctx.Channel.SendMessageAsync("Selección de movimiento inválida. Intenta nuevamente.\n");
            shift.ShowShift(ctx);
            PlayShift(shift.actualPlayer, shift.enemyPlayer, ctx);
            return;
        }

        var MovementSelected = actualPokemon.Moves[selectedMovement];
        
        if (MovementSelected.SpecialAttack)
        {
            // Verificar si el ataque especial puede ser usado
            bool canUseEspecialAtack = actualPlayer.CanUseEspecialAtack(MovementSelected.MoveDetails.Name, actualPlayer.ObtainPersonalShift());

            await ctx.Channel.SendMessageAsync($"Verificando uso de ataque especial: {MovementSelected.MoveDetails.Name}. Shift personal actual: {actualPlayer.ObtainPersonalShift()}, Shift último uso: {actualPlayer.ObtainLastShiftofAttack(MovementSelected.MoveDetails.Name)}\n");

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
                await ctx.Channel.SendMessageAsync($"No puedes usar el ataque especial {MovementSelected.MoveDetails.Name} en este momento. Debes esperar más turnos. Selecciona otro movimiento.\n");
                shift.ShowShift(ctx);
                PlayShift(shift.actualPlayer, shift.enemyPlayer, ctx);
                return;
            }
        }
        else
        {
            // Realizar un ataque regular si no es un ataque especial
            actualPokemon.AttackP(actualPlayer, enemyPlayer.actualPokemon, MovementSelected, actualPlayer.ObtainPersonalShift(), ctx);
            await ctx.Channel.SendMessageAsync($"{actualPlayer.NamePlayer}'s {actualPokemon.Name} ha atacado a {enemyPlayer.NamePlayer}'s {enemyPlayer.actualPokemon.Name} causando daño.\n");
        }
        
        // Incrementar _shift personal del player actual después de que termine su _shift
        actualPlayer.IncrementPersonalShift();
        
        // Verifica el ganador
        if (Player1.AllOutOfCombat())
        {
            await ctx.Channel.SendMessageAsync($"{Player1.NamePlayer} no tiene Pokémon disponibles. Todos están fuera de combate.\n");
            await ctx.Channel.SendMessageAsync($"¡{Player2.NamePlayer} ha ganado!\n");
            Player1.InGame = false;
            Player2.InGame = false;
            return;
        }
        if (Player2.AllOutOfCombat())
        {
            await ctx.Channel.SendMessageAsync($"{Player2.NamePlayer} no tiene Pokémon disponibles. Todos están fuera de combate.\n");
            await ctx.Channel.SendMessageAsync($"¡{Player1.NamePlayer} ha ganado!\n");
            Player1.InGame = false;
            Player2.InGame = false;
            return;
        }
        
        shift.SwitchShift();
        shift.ShowShift(ctx);
        PlayShift(shift.actualPlayer, shift.enemyPlayer, ctx);
    }

    /// <summary>
    /// Permite al jugador usar un ítem durante su turno.
    /// </summary>
    /// <param name="player">Jugador que usará el ítem.</param>
    /// <param name="itemNumber">Número del ítem a usar.</param>
    /// <param name="pokemonName">Nombre del Pokémon en el cual usar el ítem.</param>
    /// <param name="ctx">Contexto de la interacción en Discord.</param>
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
                        await ctx.Channel.SendMessageAsync("No hay un Pokémon activo que pueda ser curado. Por favor, selecciona otro Pokémon para curar.\n");
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
                    await ctx.Channel.SendMessageAsync("No te quedan Superpociones.\n");
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
                        await ctx.Channel.SendMessageAsync("No hay Pokémon muertos para revivir.\n");
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
                        await ctx.Channel.SendMessageAsync("NamePlayer inválido o el Pokémon no está fuera de combate. Por favor, intenta nuevamente.\n");
                    }
                }
                else
                {
                    await ctx.Channel.SendMessageAsync("No te quedan pociones para Revivir.\n");
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
                    await ctx.Channel.SendMessageAsync("No te quedan Cura Total.n");
                }
                break;
            default:
                await ctx.Channel.SendMessageAsync("Elección inválida. Por favor, intenta nuevamente.\n");
                break;
        }
        
        shift.ShowShift(ctx);
        PlayShift(shift.actualPlayer, shift.enemyPlayer, ctx);
    }

    /// <summary>
    /// Permite al jugador cambiar de Pokémon durante su turno.
    /// </summary>
    /// <param name="player">Jugador que cambiará su Pokémon.</param>
    /// <param name="pokemonIndex">Índice del Pokémon a cambiar.</param>
    /// <param name="ctx">Contexto de la interacción en Discord.</param>
    public async void SwitchPokemon(Player player, int pokemonIndex, InteractionContext ctx)
    {
        if (pokemonIndex > 0 && pokemonIndex <= player.Team.Count)
        {
            var choosedPokemon = player.Team[pokemonIndex - 1];
            if (choosedPokemon != player.actualPokemon && !choosedPokemon.OutOfAction())
            {
                player.SwitchPokemon(pokemonIndex - 1, ctx);
                shift.SwitchShift();
                await ctx.Channel.SendMessageAsync($"\nCambio realizado. Ahora tu Pokémon es {player.actualPokemon.Name} de tipo: {player.actualPokemon.Type}\n");
            }
            else
            {
                await ctx.Channel.SendMessageAsync("El Pokémon seleccionado está fuera de combate o ya es tu Pokémon actual.\n");
            }
        }
        else
        {
            await ctx.Channel.SendMessageAsync("Elección inválida. Intenta nuevamente.\n");
        }
        
        shift.ShowShift(ctx);
        PlayShift(shift.actualPlayer, shift.enemyPlayer, ctx);
    }

    /// <summary>
    /// Muestra los ítems disponibles que el jugador puede usar durante la batalla.
    /// </summary>
    /// <param name="player">Jugador que verá sus ítems.</param>
    /// <param name="ctx">Contexto de la interacción en Discord.</param>
    /// <returns>Mensaje con los ítems disponibles del jugador.</returns>
    public string ShowItemsByPlayer(Player player, InteractionContext ctx)
    {
        string message = "\nItems disponibles:\n" +
                         $"1- Superpoción: {player.Superpotion.Quantity}\n" +
                         $"2- Revivir: {player.Revive.Quantity}\n" +
                         $"3- Cura Total: {player.Totalcure.Quantity}\n";
        return message;
    }

    /// <summary>
    /// Muestra los ataques disponibles del Pokémon actual del jugador.
    /// </summary>
    /// <param name="player">Jugador que verá sus ataques disponibles.</param>
    /// <param name="ctx">Contexto de la interacción en Discord.</param>
    /// <returns>Mensaje con los ataques disponibles del Pokémon actual del jugador.</returns>
    public string ShowAttacksByPlayer(Player player, InteractionContext ctx)
    {
        string message = $"\n Movimientos disponibles de: {player.actualPokemon.Name}\n";
    
        for (int i = 0; i < player.actualPokemon.Moves.Count; i++)
        {
            var movement = player.actualPokemon.Moves[i];
            message += $"{i + 1}: {movement.MoveDetails.Name} (Poder: {movement.MoveDetails.Power}) (Precisión: {movement.MoveDetails.Accuracy}) Especial: {movement.SpecialStatus} Eficiencia contra el enemigo = {Player2.actualPokemon.Type.Effectiveness}\n";
        }

        
        return message;
    }
    
    /// <summary>
    /// Muestra los Pokémon disponibles para cambiar durante la batalla.
    /// </summary>
    /// <param name="player">Jugador que verá sus Pokémon disponibles para el cambio.</param>
    /// <param name="ctx">Contexto de la interacción en Discord.</param>
    /// <returns>Mensaje con los Pokémon disponibles para cambiar.</returns>
    public string ShowPokemonsToSwitch(Player player, InteractionContext ctx)
    {
        string message = $"\nPokemons disponibles para cambiar:\n";
        for (int i = 0; i < player.Team.Count; i++)
        {   
            var pokemon = player.Team[i];
            if (!pokemon.OutOfAction() && (pokemon != player.actualPokemon))
            {
                message += $"{i + 1}: {pokemon.Name} - {pokemon.Health} de vida\n";
            }
        }

        return message;
    }
    
    public string ShowEfectivity(Player player,InteractionContext ctx)
    {
        //muestra la efectividad del pokemon
        
        string message = $"Tu pokemon {player.actualPokemon} es de tipo : {player.actualPokemon.Type} \nTiene una efectividad de : {player.actualPokemon.Type.Effectiveness}";
        
        return message;
    }

/*    public EfectivePokemon(Player player, InteractionContext ctx)
    {
        for (int i = 0; i < player.Team.Count; i++)
        {
            List<string> equipo = player.Team[i]
        }
    }*/
}