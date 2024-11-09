namespace Library
{
    /// <summary>
    /// Representa el turno en un combate, gestionando el jugador actual y el cambio de turnos entre los jugadores.
    /// </summary>
    public class Shift
    {
        /// <summary>
        /// Jugador que tiene el turno actual.
        /// </summary>
        public Player actualPlayer { get; private set; }

        /// <summary>
        /// Jugador oponente al jugador actual.
        /// </summary>
        public Player enemyPlayer { get; private set; }

        /// <summary>
        /// Número de turno actual en el combate.
        /// </summary>
        public int shiftNumber { get; private set; }

        /// <summary>
        /// Contador interno para gestionar los turnos.
        /// </summary>
        private int actualShift;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Shift"/>, definiendo los jugadores iniciales.
        /// </summary>
        /// <param name="jugador1">Jugador que inicia el turno.</param>
        /// <param name="jugador2">Jugador oponente.</param>
        public Shift(Player jugador1, Player jugador2)
        {
            actualPlayer = jugador1;
            enemyPlayer = jugador2;
            shiftNumber = 1;
        }

        /// <summary>
        /// Alterna el turno entre el jugador actual y el jugador oponente, incrementando el número de turno.
        /// </summary>
        public void SwitchShift()
        {
            var temp = actualPlayer;
            actualPlayer = enemyPlayer;
            enemyPlayer = temp;
            shiftNumber++;
        }

        /// <summary>
        /// Muestra el turno actual y anuncia de quién es el turno.
        /// </summary>
        public void ShowShift()
        {
            Console.WriteLine($"-- Shift {shiftNumber} / {actualPlayer.NamePlayer} es tu turno! --");
        }

        /// <summary>
        /// Ejecuta un ataque especial si se cumplen las condiciones de turno necesarias.
        /// </summary>
        /// <param name="player">El jugador que realiza el ataque especial.</param>
        /// <param name="attacker">El Pokémon que realiza el ataque.</param>
        /// <param name="movements">Movimiento especial a ejecutar.</param>
        /// <param name="actualShift">Turno actual en el combate.</param>
        /// <returns>Retorna <c>true</c> si el ataque especial fue exitoso; de lo contrario, <c>false</c>.</returns>
        public bool ExecuteSpecialAttack(Player player, Pokemon attacker, Move movements, int actualShift)
        {
            // Verificar si el ataque especial se puede realizar
            if (!player.CanUseEspecialAtack(movements.MoveDetails.Name, actualShift))
            {
                Console.WriteLine($"No puedes usar el ataque especial {movements.MoveDetails.Name} en este momento. Debes esperar más turnos.");
                return false;
            }

            // Realizar el ataque especial
            attacker.AttackP(player, enemyPlayer.actualPokemon, movements, actualShift);
            Console.WriteLine($"{player.NamePlayer} usó el ataque especial {movements.MoveDetails.Name} causando daño!");

            // Registrar el ataque especial solo si el ataque fue exitoso
            player.RegisterSpecialAttack(movements.MoveDetails.Name, actualShift);

            return true;
        }
    }
}
