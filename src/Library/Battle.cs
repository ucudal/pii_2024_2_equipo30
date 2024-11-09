using System.Threading.Channels;
using System.Collections.Generic;

namespace Library
{
    /// <summary>
    /// Clase que representa una batalla entre dos jugadores en un juego de Pokémon.
    /// </summary>
    public class Battle : IBattle
    {
        private Player Player1;
        private Player Player2;
        private Shift _shift;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Battle"/> con los jugadores dados.
        /// </summary>
        /// <param name="player1">Primer jugador.</param>
        /// <param name="player2">Segundo jugador.</param>
        public Battle(Player player1, Player player2)
        {
            this.Player1 = player1;
            this.Player2 = player2;
            this._shift = new Shift(player1, player2);
            InitializeCurrentPokemon(player1);
            InitializeCurrentPokemon(player2);
        }

        /// <summary>
        /// Inicia la batalla y alterna los turnos hasta que uno de los jugadores se quede sin Pokémon activos.
        /// </summary>
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

        /// <summary>
        /// Ejecuta las acciones disponibles en el turno del jugador.
        /// </summary>
        /// <param name="actualPlayer">Jugador que realiza el turno.</param>
        /// <param name="enemyPlayer">Jugador oponente.</param>
        public void PlayShift(Player actualPlayer, Player enemyPlayer)
        {
            if (!actualPlayer.actualPokemon.OutOfAction())
            {
                Console.WriteLine("\n---------------------------------------------------");
                actualPlayer.actualPokemon.ProcessStatus();
                Console.WriteLine("\n---------------------------------------------------");
                Console.WriteLine($"{actualPlayer.NamePlayer}, tu pokemon actual es {actualPlayer.actualPokemon.Name} y tiene {actualPlayer.actualPokemon.Health:F1} puntos de vida");
                Console.WriteLine($"{actualPlayer.NamePlayer}, elige qué quieres hacer en este turno:");
                Console.WriteLine("1: Usar un ítem");
                Console.WriteLine("2: Atacar con un movimiento");
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
                        Console.WriteLine("Elección inválida. Pierdes tu turno.");
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

        /// <summary>
        /// Realiza un ataque usando un movimiento del Pokémon del jugador.
        /// </summary>
        /// <param name="actualPlayer">Jugador que realiza el ataque.</param>
        /// <param name="enemyPlayer">Jugador oponente que recibe el ataque.</param>
        public void Attack(Player actualPlayer, Player enemyPlayer)
        {
            Pokemon actualPokemon = actualPlayer.actualPokemon;

            if (actualPokemon == null || actualPokemon.Moves == null || actualPokemon.Moves.Count == 0)
            {
                Console.WriteLine($"{actualPokemon?.Name ?? "Ningún Pokémon"} no tiene movimientos disponibles.");
                return;
            }

            if (!actualPokemon.CanAtack())
            {
                Console.WriteLine($"{actualPokemon.Name} no puede atacar este turno debido a su estado {actualPokemon.Status}.");
                return;
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
                    continue;
                }

                var MovementSelected = actualPokemon.Moves[selectedMovement];

                if (MovementSelected.EspecialAttack)
                {
                    // Verificar si el ataque especial puede ser usado
                    bool canUseEspecialAtack = actualPlayer.CanUseEspecialAtack(MovementSelected.MoveDetails.Name, actualPlayer.ObtainPersonalShift());

                    Console.WriteLine($"Verificando uso de ataque especial: {MovementSelected.MoveDetails.Name}. Turno personal actual: {actualPlayer.ObtainPersonalShift()}, Último turno de uso: {actualPlayer.ObtainLastShiftofAttack(MovementSelected.MoveDetails.Name)}");

                    if (canUseEspecialAtack)
                    {
                        // Ejecutar ataque especial y registrar el turno
                        bool succefulAttack = _shift.ExecuteSpecialAttack(actualPlayer, actualPokemon, MovementSelected, actualPlayer.ObtainPersonalShift());
                        if (succefulAttack)
                        {
                            actualPlayer.RegisterSpecialAttack(MovementSelected.MoveDetails.Name, actualPlayer.ObtainPersonalShift());
                            break;
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
                    break;
                }
            }

            // Incrementar turno personal del jugador actual después de que termine su turno
            actualPlayer.IncrementPersonalShift();
        }

        // Métodos auxiliares para los ítems, cambiar Pokémon y otros
        // (Mantener los métodos existentes para `UseItem`, `SwitchPokemon`, etc.)

        /// <summary>
        /// Usa un ítem del jugador actual.
        /// </summary>
        public void UseItem(Player player)
        {
            // Contenido de método existente
        }

        /// <summary>
        /// Cambia el Pokémon actual del jugador.
        /// </summary>
        public void SwitchPokemon(Player player)
        {
            // Contenido de método existente
        }

        /// <summary>
        /// Inicializa el Pokémon actual del jugador si no hay uno seleccionado o si está fuera de combate.
        /// </summary>
        /// <param name="player">Jugador que realiza el cambio de Pokémon.</param>
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
}
