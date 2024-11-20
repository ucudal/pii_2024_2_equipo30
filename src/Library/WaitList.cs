using System.Collections;
using System.Collections.Generic;

namespace Library

{
    public class WaitList
    {
        private List<IPlayer> PlayerList { get; set; }

        public WaitList()
        {
            PlayerList = new List<IPlayer>();
        }

        public WaitList(IEnumerable<IPlayer> players)
        {
            PlayerList = new List<IPlayer>(players);
        }

        public void AddPlayer(IPlayer player)
        {
            if (PlayerList.Contains(player))
            {
                Console.WriteLine($"El jugador {player.NamePlayer} ya está en la lista.");
            }

            PlayerList.Add(player);
            Console.WriteLine($"El jugador {player.NamePlayer} ha sido añadido a la fila.");
        }

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

        public void ShowPlayers()
        {
            if (PlayerList.Count == 0)
            {
                Console.WriteLine("No hay jugadores en la lista de espera.");
                return;
            }

            for (int i = 0; i < PlayerList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {PlayerList[i].NamePlayer}");
            }
        }

        public bool Contains(IPlayer player)
        {
            return PlayerList.Contains(player);
        }
    }
}