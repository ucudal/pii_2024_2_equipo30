using System.Collections;
using System.Collections.Generic;

namespace Library
{
    /// <summary>
    /// Clase que representa una lista de espera para los jugadores.
    /// </summary>
    public class WaitList
    {
        /// <summary>
        /// Lista de jugadores en espera.
        /// </summary>
        private List<IPlayer> PlayerList { get; set; }

        /// <summary>
        /// Constructor que inicializa una nueva lista de espera vacía.
        /// </summary>
        public WaitList()
        {
            PlayerList = new List<IPlayer>();
        }

        /// <summary>
        /// Constructor que inicializa una nueva lista de espera con jugadores existentes.
        /// </summary>
        /// <param name="players">Una enumeración de jugadores para añadir a la lista de espera.</param>
        public WaitList(IEnumerable<IPlayer> players)
        {
            PlayerList = new List<IPlayer>(players);
        }

        /// <summary>
        /// Añade un jugador a la lista de espera.
        /// </summary>
        /// <param name="player">El jugador que se desea añadir a la lista de espera.</param>
        public void AddPlayer(IPlayer player)
        {
            if (PlayerList.Contains(player))
            {
                Console.WriteLine($"El jugador {player.NamePlayer} ya está en la lista.");
                return;
            }

            if (player.InGame)
            {
                Console.WriteLine($"El jugador {player.NamePlayer} está en una batalla.");
                return;
            }

            PlayerList.Add(player);
            Console.WriteLine($"El jugador {player.NamePlayer} ha sido añadido a la fila.");
        }

        /// <summary>
        /// Elimina un jugador de la lista de espera.
        /// </summary>
        /// <param name="player">El jugador que se desea eliminar de la lista de espera.</param>
        public void DeletePlayer(IPlayer player)
        {
            if (!PlayerList.Contains(player))
            {
                Console.WriteLine($"El jugador {player.NamePlayer} no está en la lista.");
                return;
            }

            PlayerList.Remove(player);
            Console.WriteLine($"El jugador {player.NamePlayer} ya no está en la fila de espera.");
        }

        /// <summary>
        /// Muestra todos los jugadores en la lista de espera.
        /// </summary>
        public void ShowPlayers()
        {
            if (PlayerList.Count == 0)
            {
                Console.WriteLine("No hay jugadores en la lista de espera.");
                return;
            }

            for (int i = 0; i < PlayerList.Count; i++)
            {
                Console.WriteLine($"{PlayerList[i].NamePlayer}");
            }
        }

        /// <summary>
        /// Comprueba si un jugador está en la lista de espera.
        /// </summary>
        /// <param name="player">El jugador que se desea comprobar si está en la lista.</param>
        /// <returns>True si el jugador está en la lista, de lo contrario false.</returns>
        public bool Contains(IPlayer player)
        {
            return PlayerList.Contains(player);
        }

        /// <summary>
        /// Limpia la lista de espera eliminando jugadores que estén actualmente en una batalla.
        /// </summary>
        public void CleanWaitList()
        {
            for (int i = PlayerList.Count - 1; i >= 0; i--)
            {
                if (PlayerList[i].InGame)
                {
                    PlayerList.Remove(PlayerList[i]);
                }
            }
        }
    }
}

/*


 
 */
