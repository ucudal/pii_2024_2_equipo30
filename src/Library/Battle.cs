using System.Threading.Channels;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Collections.Generic;
using DSharpPlus.SlashCommands;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.Entities;



namespace Library;

/// <summary>
/// Clase que representa una batalla entre dos jugadores y maneja toda la lógica de turnos, ataques y uso de ítems.
/// </summary>
public class Battle : IBattle
{
    private Player Player1;
    private Player Player2;
    private Shift _shift;
    

    /// <summary>
    /// Constructor que inicializa la batalla con dos jugadores.
    /// </summary>
    /// <param name="player1">Jugador 1 de la batalla.</param>
    /// <param name="player2">Jugador 2 de la batalla.</param>
    public Battle(Player player1, Player player2)
    {
        this.Player1 = player1;
        this.Player2 = player2;
        this._shift = new Shift(player1, player2);
    }

    
    /// <summary>
    /// Método para iniciar la batalla. Se continúa hasta que uno de los jugadores quede sin Pokémon en combate.
    /// </summary>
    public async Task StartBattle(InteractionContext ctx)
    {
        Player1.InBattle = true;
        Player2.InBattle = true;
        
        await ctx.Channel.SendMessageAsync($"{Player1.NamePlayer}, selecciona tu Pokémon inicial.");
        await Player1.PokemonElection(ctx);
        
        
        await ctx.Channel.SendMessageAsync($"{Player1.NamePlayer}, selecciona tu Pokémon inicial.");
        await Player2.PokemonElection(ctx);
        
        
        await ctx.Channel.SendMessageAsync($"{Player1.NamePlayer} ha seleccionado a {Player1.actualPokemon.Name} con {Player1.actualPokemon.Health} puntos de salud.");
        await ctx.Channel.SendMessageAsync($"{Player2.NamePlayer} ha seleccionado a {Player2.actualPokemon.Name} con {Player2.actualPokemon.Health} puntos de salud.");
        // Validar si ambos jugadores tienen al menos un Pokémon
        if (Player1.Team.Count == 0 || Player2.Team.Count == 0)
        {
            await ctx.Channel.SendMessageAsync("No se pudo obtener suficientes Pokémon para ambos jugadores.");
            return;
        }

        await ctx.Channel.SendMessageAsync("La batalla ha iniciado");
        while (!Player1.AllOutOfCombat() && !Player2.AllOutOfCombat())
        {
            await ctx.Channel.SendMessageAsync("\n================ TURNOS DE BATALLA ================\n");
            _shift.ShowShift(ctx);
            PlayShift(ctx,_shift.actualPlayer, _shift.enemyPlayer);
            _shift.SwitchShift();
        }

        // Mostrar quién ganó la batalla
        Console.WriteLine("\n===================================================");
        if (Player1.AllOutOfCombat())
        {
            await ctx.Channel.SendMessageAsync($"\n {Player2.NamePlayer} gana la batalla! \n");
        }
        else
        {
            await ctx.Channel.SendMessageAsync($"\n {Player1.NamePlayer} gana la batalla! \n");
        }
    }

    /// <summary>
    /// Método que ejecuta el turno de un jugador.
    /// </summary>
    /// <param name="actualPlayer">Jugador que realiza la acción en este turno.</param>
    /// <param name="enemyPlayer">Jugador enemigo que recibe la acción.</param>
    /*public async Task PlayShift(InteractionContext ctx, Player actualPlayer, Player enemyPlayer)
{
    if (!actualPlayer.actualPokemon.OutOfAction())
    {
        // Agrupar mensajes para reducir ratelimit
        string message = $"\n---------------------------------------------------\n" +
                         $"{actualPlayer.NamePlayer}, tu Pokémon actual es {actualPlayer.actualPokemon.Name} y tiene {actualPlayer.actualPokemon.Health:F1} puntos de vida.\n" +
                         "Elige qué quieres hacer en este turno:\n" +
                         "1: Usar un ítem\n" +
                         "2: Atacar a un Pokémon con un movimiento\n" +
                         "3: Cambiar de Pokémon\n" +
                         "Responde con el número de tu elección.";
        await ctx.Channel.SendMessageAsync(message);

        // Esperar la respuesta del usuario
        var interactivity = ctx.Client.GetInteractivity();
        var response = await interactivity.WaitForMessageAsync(
            m => m.Author.Id == ctx.User.Id && m.ChannelId == ctx.Channel.Id,
            TimeSpan.FromMinutes(1)
        );

        if (response.TimedOut || response.Result == null)
        {
            await ctx.Channel.SendMessageAsync($"{actualPlayer.NamePlayer}, no respondiste a tiempo. Pierdes tu turno.");
            return; // Salir del turno si no hay respuesta
        }

        if (int.TryParse(response.Result.Content, out int actionChoose))
        {
            switch (actionChoose)
            {
                case 1:
                    await UseItem(ctx, actualPlayer);
                    break;
                case 2:
                    await Attack(ctx, actualPlayer, enemyPlayer);
                    break;
                case 3:
                    await SwitchPokemon(ctx, actualPlayer);
                    break;
                default:
                    await ctx.Channel.SendMessageAsync("Elección inválida. Por favor, elige entre 1, 2 o 3.");
                    break;
            }
        }
        else
        {
            await ctx.Channel.SendMessageAsync("Entrada no válida. Por favor, responde con un número.");
        }
    }
    else
    {
        await ctx.Channel.SendMessageAsync($"{actualPlayer.NamePlayer}, tu Pokémon actual está fuera de combate. Debes elegir otro.");
        await SwitchPokemon(ctx, actualPlayer);
        await PlayShift(ctx, actualPlayer, enemyPlayer); // Reintentar turno
    }
}*/
    public async Task PlayShift(InteractionContext ctx, Player actualPlayer, Player enemyPlayer)
    {
        if (actualPlayer.actualPokemon.OutOfAction())
        {
            await ctx.Channel.SendMessageAsync($"{actualPlayer.NamePlayer}, tu Pokémon está fuera de combate. Cambiando Pokémon automáticamente...");
            await SwitchPokemon(ctx, actualPlayer);
            await PlayShift(ctx, actualPlayer, enemyPlayer); // Reintentar turno
            return;
        }

        // Mensaje inicial
        var embed = new DiscordEmbedBuilder
        {
            Title = "Tu turno",
            Description = $"Tu Pokémon actual: {actualPlayer.actualPokemon.Name} ({actualPlayer.actualPokemon.Health:F1} HP)\n" +
                          "Elige una acción:\n1: Usar un ítem\n2: Atacar\n3: Cambiar de Pokémon",
            Color = DiscordColor.Aquamarine
        };

        var turnoMessage = await ctx.Channel.SendMessageAsync(embed: embed);

        // Esperar respuesta
        var interactivity = ctx.Client.GetInteractivity();
        var response = await interactivity.WaitForMessageAsync(
            m => m.Author.Id == ctx.User.Id && m.ChannelId == ctx.Channel.Id,
            TimeSpan.FromMinutes(1)
        );

        if (response.TimedOut)
        {
            await ctx.Channel.SendMessageAsync($"{actualPlayer.NamePlayer}, no respondiste a tiempo. Pierdes tu turno.");
            return;
        }

        // Procesar respuesta
        if (int.TryParse(response.Result.Content, out int actionChoose))
        {
            switch (actionChoose)
            {
                case 1:
                    await UseItem(ctx, actualPlayer);
                    break;
                case 2:
                    await Attack(ctx, actualPlayer, enemyPlayer);
                    break;
                case 3:
                    await SwitchPokemon(ctx, actualPlayer);
                    break;
                default:
                    await ctx.Channel.SendMessageAsync("Elección inválida. Por favor, elige entre 1, 2 o 3.");
                    break;
            }
        }
        else
        {
            await ctx.Channel.SendMessageAsync("Entrada no válida. Por favor, responde con un número.");
        }
    }

    
    /// <summary>
    /// Método para realizar un ataque del Pokémon del jugador actual al del jugador enemigo.
    /// </summary>
    /// <param name="actualPlayer">Jugador que realiza el ataque.</param>
    /// <param name="enemyPlayer">Jugador enemigo que recibe el ataque.</param>
    public async Task Attack(InteractionContext ctx, Player actualPlayer, Player enemyPlayer)
    {
        Pokemon actualPokemon = actualPlayer.actualPokemon;

        if (actualPokemon == null || actualPokemon.Moves == null || actualPokemon.Moves.Count == 0)
        {
            await ctx.Channel.SendMessageAsync($"{actualPlayer.NamePlayer}, tu Pokémon no tiene movimientos disponibles.");
            return;
        }

        if (!actualPokemon.CanAtack())
        {
            await ctx.Channel.SendMessageAsync($"{actualPlayer.NamePlayer}, tu Pokémon no puede atacar debido a su estado.");
            return;
        }

        while (true)
        {
            // Mostrar lista de movimientos
            string movesMessage = $"{actualPlayer.NamePlayer}, elige un movimiento de {actualPokemon.Name}:\n";
            for (int i = 0; i < actualPokemon.Moves.Count; i++)
            {
                var move = actualPokemon.Moves[i];
                movesMessage += $"{i + 1}: {move.MoveDetails.Name} (Poder: {move.MoveDetails.Power}, Precisión: {move.MoveDetails.Accuracy})\n";
            }
            movesMessage += "Responde con el número del movimiento.";

            await ctx.Channel.SendMessageAsync(movesMessage);

            // Esperar respuesta del jugador
            var interactivity = ctx.Client.GetInteractivity();
            var response = await interactivity.WaitForMessageAsync(
                m => m.Author.Id == ctx.User.Id && m.ChannelId == ctx.Channel.Id,
                TimeSpan.FromMinutes(1)
            );

            if (response.TimedOut || response.Result == null)
            {
                await ctx.Channel.SendMessageAsync($"{actualPlayer.NamePlayer}, no respondiste a tiempo. Pierdes tu turno.");
                return;
            }

            if (int.TryParse(response.Result.Content, out int selectedMovement) &&
                selectedMovement > 0 && selectedMovement <= actualPokemon.Moves.Count)
            {
                var moveSelected = actualPokemon.Moves[selectedMovement - 1];

                // Lógica de ataque
                if (moveSelected.SpecialAttack)
                {
                    bool canUseSpecialAttack = actualPlayer.CanUseEspecialAtack(moveSelected.MoveDetails.Name, actualPlayer.ObtainPersonalShift());
                    if (canUseSpecialAttack)
                    {
                        bool successfulAttack = _shift.ExecuteSpecialAttack(actualPlayer, actualPokemon, moveSelected, actualPlayer.ObtainPersonalShift());
                        if (successfulAttack)
                        {
                            actualPlayer.RegisterSpecialAttack(moveSelected.MoveDetails.Name, actualPlayer.ObtainPersonalShift());
                            break;
                        }
                        else
                        {
                            await ctx.Channel.SendMessageAsync($"El ataque especial {moveSelected.MoveDetails.Name} falló. Intenta de nuevo.");
                        }
                    }
                    else
                    {
                        await ctx.Channel.SendMessageAsync($"No puedes usar el ataque especial {moveSelected.MoveDetails.Name} en este momento.");
                    }
                }
                else
                {
                    actualPokemon.AttackP(actualPlayer, enemyPlayer.actualPokemon, moveSelected, actualPlayer.ObtainPersonalShift());
                    await ctx.Channel.SendMessageAsync($"{actualPlayer.NamePlayer}'s {actualPokemon.Name} atacó a {enemyPlayer.actualPokemon.Name} causando daño.");
                    break;
                }
            }
            else
            {
                await ctx.Channel.SendMessageAsync("Selección inválida. Por favor, responde con un número válido.");
            }
        }

        // Incrementar turno personal
        actualPlayer.IncrementPersonalShift();
    }


    /// <summary>
    /// Método para usar un ítem durante el turno de un jugador.
    /// </summary>
    /// <param name="player">Jugador que va a usar el ítem.</param>
    public async Task UseItem(InteractionContext ctx,Player player)
    {
        bool usedItem = false;
        while (!usedItem)
        {
            Console.WriteLine("\nElige un ítem para usar:");
            Console.WriteLine("1: Superpoción");
            Console.WriteLine("2: Revivir");
            Console.WriteLine("3: Cura Total");
            int choose = int.Parse(Console.ReadLine());

            switch (choose)
            {
                case 1:
                    if (player.Superpotion.Quantity > 0)
                    {
                        if (player.actualPokemon == null || player.actualPokemon.OutOfAction())
                        {
                            Console.WriteLine("No hay un Pokémon activo que pueda ser curado. Por favor, selecciona otro Pokémon para curar.");
                            SwitchPokemon(ctx,player);
                        }
                        player.Superpotion.Use(player.actualPokemon);
                        Console.WriteLine($"\n Superpoción usada con éxito en {player.actualPokemon.Name}.\n");
                        usedItem = true;
                    }
                    else
                    {
                        Console.WriteLine("No te quedan Superpociones.");
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
                            Console.WriteLine("No hay Pokémon muertos para revivir.");
                            return;
                        }

                        bool revivedPokemons = false;
                        while (!revivedPokemons)
                        {
                            Console.WriteLine("\nElige el nombre del Pokémon para revivir (lista de Pokémon muertos):");
                            foreach (var name in deadPokemons)
                            {
                                Console.WriteLine(name);
                            }

                            string pokemonName = Console.ReadLine();
                            if (deadPokemons.Contains(pokemonName))
                            {
                                Pokemon pokemonToRevive = player.Team.Find(ok => ok.Name == pokemonName);
                                player.Revive.Use(pokemonToRevive);
                                Console.WriteLine($"\n {pokemonToRevive.Name} ha sido revivido con éxito.\n");
                                revivedPokemons = true;
                                usedItem = true;
                            }
                            else
                            {
                                Console.WriteLine("NamePlayer inválido o el Pokémon no está fuera de combate. Por favor, intenta nuevamente.");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("No te quedan Revivir.");
                    }
                    break;
                case 3:
                    if (player.Totalcure.Quantity > 0)
                    {
                        player.Totalcure.Use(player.actualPokemon);
                        Console.WriteLine($"\n Cura Total usada con éxito en {player.actualPokemon.Name}.\n");
                        usedItem = true;
                    }
                    else
                    {
                        Console.WriteLine("No te quedan Cura Total.");
                    }
                    break;
                default:
                    Console.WriteLine("Elección inválida. Por favor, intenta nuevamente.");
                    break;
            }
        }
    }

    /// <summary>
    /// Método para cambiar el Pokémon del jugador durante un turno.
    /// </summary>
    /// <param name="player">Jugador que cambiará su Pokémon.</param>
    public async Task SwitchPokemon(InteractionContext ctx,Player player)
    {
        while (true)
        {
            Console.WriteLine($"\n{player.NamePlayer}, elige un Pokémon para cambiar:\n");
            for (int i = 0; i < player.Team.Count; i++)
            {
                var pokemon = player.Team[i];
                if (!pokemon.OutOfAction() && (pokemon != player.actualPokemon))
                {
                    Console.WriteLine($"{i + 1}: {pokemon.Name} - {pokemon.Health} de vida");
                }
            }
            try
            {
                int choose = int.Parse(Console.ReadLine());
                if (choose > 0 && choose <= player.Team.Count)
                {
                    var choosedPokemon = player.Team[choose - 1];
                    if (choosedPokemon != player.actualPokemon && !choosedPokemon.OutOfAction())
                    {
                        player.SwitchPokemon(choose - 1);
                        Console.WriteLine($"\nCambio realizado. Ahora tu Pokémon es {player.actualPokemon.Name}\n");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("El Pokémon seleccionado está fuera de combate o ya es tu Pokémon actual.");
                    }
                }
                else
                {
                    Console.WriteLine("Elección inválida. Intenta nuevamente.");
                }
            }
            catch
            {
                Console.WriteLine("Entrada inválida. Intenta nuevamente.");
            }

        }
    }
    

}
