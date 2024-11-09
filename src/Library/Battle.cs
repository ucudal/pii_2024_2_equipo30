using System.Threading.Channels;
using System.Collections.Generic;

namespace Library;

public class Battle : IBattle
{
    private Player Player1;
    private Player Player2;
    private Shift _shift;

    public Battle(Player player1, Player player2)
    {
        this.Player1 = player1;
        this.Player2 = player2;
        this._shift = new Shift(player1, player2);
        InitializeCurrentPokemon(player1);
        InitializeCurrentPokemon(player2);
    }

    public void StartBattle()
    {
        while (!Player1.AllOutOfCombat() && !Player2.AllOutOfCombat())
        {
            Console.WriteLine("\n================ TURNOS DE BATALLA ================\n");
            _shift.ShowShift();
            PlayShift(_shift.actualPlayer, _shift.enemyPlayer);
            _shift.SwitchShift();
        }

        // Mostrar quién ganó
        Console.WriteLine("\n===================================================");
        if (Player1.AllOutOfCombat())
        {
            Console.WriteLine($"\n {Player2.NamePlayer} gana la batalla! \n");
        }
        else
        {
            Console.WriteLine($"\n {Player1.NamePlayer} gana la batalla! \n");
        }
    }

    public void PlayShift(Player actualPlayer, Player enemyPlayer)
    {
        if (!actualPlayer.actualPokemon.OutOfAction())
        {
            Console.WriteLine("\n---------------------------------------------------");
            actualPlayer.actualPokemon.ProcessStatus();
            Console.WriteLine("\n---------------------------------------------------");
            Console.WriteLine($"{actualPlayer.NamePlayer}, tu pokemon actual es {actualPlayer.actualPokemon.Name} y tiene {actualPlayer.actualPokemon.Health:F1} puntos de vida");
            Console.WriteLine($"{actualPlayer.NamePlayer}, elige qué quieres hacer en este _shift:");
            Console.WriteLine("1: Usar un ítem");
            Console.WriteLine("2: atacar a un pokemon con un movimiento");
            Console.WriteLine("3: Cambiar de Pokémon");
            int actionChoose = int.Parse(Console.ReadLine());

            switch (actionChoose)
            {
                case 1:
                    UseItem(actualPlayer);
                    break;
                case 2:
                    Attack(actualPlayer, enemyPlayer);
                    break;
                case 3:
                    SwitchPokemon(actualPlayer);
                    break;
                default:
                    Console.WriteLine("Elección inválida. Pierdes tu _shift.");
                    break;
            }
        }
        else if (actualPlayer.actualPokemon.OutOfAction())
        {
            Console.WriteLine($"{actualPlayer.NamePlayer} tu pokemon actual está fuera de combate. Debes elegir otro\n");
            SwitchPokemon(actualPlayer);
            PlayShift(actualPlayer, enemyPlayer);
        }
        else
        {
            Console.WriteLine("\n---------------------------------------------------");
        }

    }

    public void Attack(Player actualPlayer, Player enemyPlayer)
    {
        Pokemon actualPokemon = actualPlayer.actualPokemon;

        if (actualPokemon == null || actualPokemon.Moves == null || actualPokemon.Moves.Count == 0)
        {
            Console.WriteLine($"{actualPokemon?.Name ?? "Ningún Pokémon"} no tiene movimientos disponibles.");
            return;  // No tiene movimientos disponibles, salir de la función
        }

        if (!actualPokemon.CanAtack())
        {
            Console.WriteLine($"{actualPokemon.Name} no puede atacar este _shift debido a su estado {actualPokemon.Status}.");
            return;  // El Pokémon no puede atacar debido a su estado (dormido, paralizado, etc.)
        }

        while (true)
        {
            Console.WriteLine($"\n{actualPlayer.NamePlayer}, elige un movimiento de: {actualPokemon.Name}");

            for (int i = 0; i < actualPokemon.Moves.Count; i++)
            {
                var movement = actualPokemon.Moves[i];
                Console.WriteLine($"{i + 1}: {movement.MoveDetails.Name} (Poder: {movement.MoveDetails.Power}) (Precisión: {movement.MoveDetails.Accuracy}) Especial: {movement.SpecialStatus}");
            }

            int selectedMovement = int.Parse(Console.ReadLine()) - 1;
            
            if (selectedMovement < 0 || selectedMovement >= actualPokemon.Moves.Count)
            {
                Console.WriteLine("Selección de movimiento inválida. Intenta nuevamente.");
                continue; // Permitir al player elegir de nuevo
            }

            var MovementSelected = actualPokemon.Moves[selectedMovement];

            if (MovementSelected.EspecialAttack)
            {
                // Verificar si el ataque especial puede ser usado
                bool canUseEspecialAtack = actualPlayer.CanUseEspecialAtack(MovementSelected.MoveDetails.Name, actualPlayer.ObtainPersonalShift());

                Console.WriteLine($"Verificando uso de ataque especial: {MovementSelected.MoveDetails.Name}. Shift personal actual: {actualPlayer.ObtainPersonalShift()}, Shift último uso: {actualPlayer.ObtainLastShiftofAttack(MovementSelected.MoveDetails.Name)}");

                if (canUseEspecialAtack)
                {
                    // Ejecutar ataque especial y registrar el _shift
                    bool succefulAttack = _shift.ExecuteSpecialAttack(actualPlayer, actualPokemon, MovementSelected, actualPlayer.ObtainPersonalShift());
                    if (succefulAttack)
                    {
                        actualPlayer.RegisterSpecialAttack(MovementSelected.MoveDetails.Name, actualPlayer.ObtainPersonalShift());
                        break; // Salir del bucle si el ataque fue ejecutado exitosamente
                    }
                }
                else
                {
                    Console.WriteLine($"No puedes usar el ataque especial {MovementSelected.MoveDetails.Name} en este momento. Debes esperar más turnos. Selecciona otro movimiento.");
                }
            }
            else
            {
                // Realizar un ataque regular si no es un ataque especial
                actualPokemon.AttackP(actualPlayer, enemyPlayer.actualPokemon, MovementSelected, actualPlayer.ObtainPersonalShift());
                Console.WriteLine($"{actualPlayer.NamePlayer}'s {actualPokemon.Name} ha atacado a {enemyPlayer.NamePlayer}'s {enemyPlayer.actualPokemon.Name} causando daño.");
                break; // Salir del bucle si el ataque regular fue ejecutado
            }
        }

        // Incrementar _shift personal del player actual después de que termine su _shift
        actualPlayer.IncrementPersonalShift();
    }

    // Métodos auxiliares para los ítems, cambiar Pokémon y otros
    // (Mantener los métodos existentes para `UseItem`, `SwitchPokemon`, etc.)

public void UseItem(Player player)
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
                            SwitchPokemon(player);
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
    public void SwitchPokemon(Player player)
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

    public void InitializeCurrentPokemon(Player player)
    {
        if (player.actualPokemon == null || player.actualPokemon.OutOfAction())
        {
            foreach (var pokemon in player.Team)
            {
                if (!pokemon.OutOfAction())
                {
                    player.actualPokemon = pokemon;
                    Console.WriteLine($"\n{player.NamePlayer} ha seleccionado a {pokemon.Name} como su Pokémon inicial.\n");
                    break;
                }
            }
        }
    }
}
