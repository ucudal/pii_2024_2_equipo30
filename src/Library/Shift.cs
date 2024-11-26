namespace Library
{
    /// <summary>
    /// Clase que representa un turno (shift) en la batalla entre dos jugadores.
    /// Administra qué jugador es el actual y cuál es el enemigo, así como el número de turno.
    /// </summary>
    public class Shift
    {
        /// <summary>
        /// Jugador que está tomando el turno actual.
        /// </summary>
        public Player actualPlayer { get; private set; }

        /// <summary>
        /// Jugador enemigo que está esperando su turno.
        /// </summary>
        public Player enemyPlayer { get; private set; }

        /// <summary>
        /// Número actual del turno en la batalla.
        /// </summary>
        public int shiftNumber { get; private set; }

        /// <summary>
        /// Turno actual para el jugador. 
        /// </summary>
        private int actualShift;

        /// <summary>
        /// Constructor para inicializar un turno con dos jugadores.
        /// </summary>
        /// <param name="jugador1">Primer jugador en iniciar el turno.</param>
        /// <param name="jugador2">Segundo jugador.</param>
        public Shift(Player jugador1, Player jugador2)
        {
            actualPlayer = jugador1;
            enemyPlayer = jugador2;
            shiftNumber = 1;
        }

        /// <summary>
        /// Cambia el turno entre los dos jugadores.
        /// El jugador actual se convierte en el enemigo y viceversa.
        /// </summary>
        public void SwitchShift()
        {
            var temp = actualPlayer;
            actualPlayer = enemyPlayer;
            enemyPlayer = temp;
            shiftNumber++;
        }

        /// <summary>
        /// Muestra en la consola el jugador que tiene el turno actual.
        /// </summary>
        public void ShowShift()
        {
            Console.WriteLine($"-- turno {shiftNumber} / {actualPlayer.NamePlayer} es tu turno! --");
        }

        /// <summary>
        /// Ejecuta un ataque especial por parte del jugador con restricciones de uso.
        /// Verifica si el jugador puede usar el ataque especial antes de ejecutarlo.
        /// </summary>
        /// <param name="player">Jugador que realiza el ataque especial.</param>
        /// <param name="attacker">El Pokémon que está realizando el ataque.</param>
        /// <param name="movements">El movimiento especial a ejecutar.</param>
        /// <param name="actualShift">El turno actual en el que se está realizando el ataque.</param>
        /// <returns>Devuelve true si el ataque especial fue realizado con éxito; de lo contrario, false.</returns>
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
