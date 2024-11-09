using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Library
{
    /// <summary>
    /// Clase que representa un Pokémon, con sus atributos, movimientos y estado.
    /// Implementa la interfaz "IPokemon".
    /// </summary>
    public class Pokemon : IPokemon
    {
        /// <summary>
        /// Nombre del Pokémon.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Salud actual del Pokémon.
        /// </summary>
        public double Health { get; set; }

        /// <summary>
        /// Salud máxima del Pokémon.
        /// </summary>
        public double MaxHealt { get; set; }

        /// <summary>
        /// Identificador del Pokémon.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Valor de ataque del Pokémon.
        /// </summary>
        public int Attack { get; set; }

        /// <summary>
        /// Valor de defensa del Pokémon.
        /// </summary>
        public int Defense { get; set; }

        /// <summary>
        /// Valor de ataque especial del Pokémon.
        /// </summary>
        public int SpecialAttack { get; set; }

        /// <summary>
        /// Valor de defensa especial del Pokémon.
        /// </summary>
        public int SpecialDefense { get; set; }

        /// <summary>
        /// Tipo del Pokémon.
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Estadísticas del Pokémon.
        /// </summary>
        public List<Stat> Stats { get; set; }

        /// <summary>
        /// Tipos del Pokémon (algunos Pokémon pueden tener más de un tipo).
        /// </summary>
        public List<Type> Types { get; set; }

        /// <summary>
        /// Lista de movimientos que el Pokémon puede realizar.
        /// </summary>
        public List<Move> Moves { get; set; }

        /// <summary>
        /// Estado especial actual del Pokémon (Paralizado, Quemado, Envenenado, Dormido, etc.).
        /// </summary>
        public SpecialStatus Status { get; set; }

        /// <summary>
        /// Número de turnos restantes que el Pokémon permanecerá dormido.
        /// </summary>
        public int SleepTurnsLeft { get; set; } = 0;

        /// <summary>
        /// Indica si el Pokémon está fuera de combate (sin vida).
        /// </summary>
        public bool Outofaction = false;

        private Random random = new Random();

        /// <summary>
        /// Constructor que inicializa los valores básicos del Pokémon.
        /// </summary>
        /// <param name="name">Nombre del Pokémon.</param>
        /// <param name="id">Identificador del Pokémon.</param>
        /// <param name="health">Valor de salud del Pokémon.</param>
        /// <param name="attack">Valor de ataque.</param>
        /// <param name="defense">Valor de defensa.</param>
        /// <param name="specialAttack">Valor de ataque especial.</param>
        /// <param name="specialDefense">Valor de defensa especial.</param>
        /// <param name="type">Tipo del Pokémon.</param>
        /// <param name="moves">Lista de movimientos del Pokémon.</param>
        public Pokemon(string name, int id, double health, int attack, int defense, int specialAttack, int specialDefense, Type type, List<Move> moves)
        {
            Name = name;
            Id = id;
            Health = health;
            MaxHealt = health;
            Attack = attack;
            Defense = defense;
            SpecialAttack = specialAttack;
            SpecialDefense = specialDefense;
            Type = type;
            Moves = moves;
            Status = SpecialStatus.NoneStatus;
        }

        /// <summary>
        /// Método que permite que el Pokémon ataque a un enemigo.
        /// </summary>
        /// <param name="player">Jugador que controla al Pokémon atacante.</param>
        /// <param name="enemy">Pokémon enemigo que recibirá el ataque.</param>
        /// <param name="movement">Movimiento que se utilizará en el ataque.</param>
        /// <param name="currentShift">Turno actual para registrar ataques especiales.</param>
        public void AttackP(Player player, Pokemon enemy, Move movement, int currentShift)
        {
            if (!CanAtack())
            {
                Console.WriteLine($"{Name} no puede atacar este turno debido a su estado {Status}.");
                return;
            }

            if (movement.EspecialAttack)
            {
                if (!player.CanUseEspecialAtack(movement.MoveDetails.Name, currentShift))
                {
                    Console.WriteLine($"No puedes usar el ataque especial {movement.MoveDetails.Name} en este momento. Debes esperar más turnos.");
                    return;
                }
                else
                {
                    player.RegisterSpecialAttack(movement.MoveDetails.Name, currentShift);
                }
            }

            Console.WriteLine($"{Name} usa {movement.MoveDetails.Name}!");

            if (movement.SpecialStatus != SpecialStatus.NoneStatus && enemy.Status == SpecialStatus.NoneStatus)
            {
                enemy.Status = movement.SpecialStatus;
                if (movement.SpecialStatus == SpecialStatus.Asleep)
                {
                    enemy.sleep();
                }
                else
                {
                    Console.WriteLine($"{enemy.Name} ahora está {movement.SpecialStatus}.");
                }
            }

            double Efficacy = Type.Effectiveness.ContainsKey(enemy.Type.TypeDetail.Name) ? Type.Effectiveness[enemy.Type.TypeDetail.Name] : 1.0;
            double Damage = CalculateDamage(movement, Efficacy, enemy);

            enemy.Health -= Damage;
            if (enemy.Health < 0) enemy.Health = 0;

            Console.WriteLine($"{Name} hizo {Damage:F1} puntos de daño! {enemy.Name} ahora tiene {enemy.Health:F1} puntos de vida.");
        }

        /// <summary>
        /// Calcula el daño que hará un movimiento, teniendo en cuenta el tipo, eficacia y otras variables.
        /// </summary>
        /// <param name="movimiento">Movimiento que se utilizará.</param>
        /// <param name="efectividad">Factor de eficacia según el tipo.</param>
        /// <param name="oponente">Pokémon oponente que recibirá el daño.</param>
        /// <returns>El daño calculado.</returns>
        public double CalculateDamage(Move movimiento, double efectividad, Pokemon oponente)
        {
            int power = movimiento.MoveDetails.Power ?? 0;
            int attack = movimiento.EspecialAttack ? SpecialAttack : Attack;
            int defence = movimiento.EspecialAttack ? oponente.SpecialDefense : oponente.Defense;
            int level = 100;
            double variation = random.NextDouble() * (1.0 - 0.85) + 0.85;
            double critic = random.NextDouble() < 0.1 ? 1.2 : 1.0;

            return (0.1 * critic * efectividad * variation * (0.2 * level + 1) * attack * power) / (25 * defence) + 2;
        }

        /// <summary>
        /// Determina si el Pokémon puede atacar en su turno según su estado actual.
        /// </summary>
        /// <returns>Devuelve true si el Pokémon puede atacar, de lo contrario false.</returns>
        public bool CanAtack()
        {
            ProcessStatus();

            if (Status == SpecialStatus.Asleep && SleepTurnsLeft > 0)
            {
                SleepTurnsLeft--;
                Console.WriteLine($"{Name} está dormido y no puede atacar. Turnos restantes: {SleepTurnsLeft}");
                if (SleepTurnsLeft == 0)
                {
                    Status = SpecialStatus.NoneStatus;
                    Console.WriteLine($"{Name} se ha despertado.");
                }
                return false;
            }
            else if (Status == SpecialStatus.Paralyzed && random.Next(0, 2) == 0)
            {
                Console.WriteLine($"{Name} está paralizado y no puede atacar este turno.");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Pone al Pokémon a dormir durante un número aleatorio de turnos.
        /// </summary>
        public void sleep()
        {
            if (Status != SpecialStatus.Asleep)
            {
                Status = SpecialStatus.Asleep;
                SleepTurnsLeft = random.Next(1, 5);
                Console.WriteLine($"{Name} ha sido dormido y estará dormido por {SleepTurnsLeft} turnos.");
            }
            else
            {
                Console.WriteLine($"{Name} ya está dormido y no puede dormir nuevamente hasta despertar.");
            }
        }

        /// <summary>
        /// Procesa el estado del Pokémon para determinar efectos secundarios (veneno, quemaduras, etc.).
        /// </summary>
        /// <param name="enemy">Pokémon enemigo, si se aplica.</param>
        public void ProcessStatus(Pokemon enemy = null)
        {
            Pokemon target = enemy ?? this;

            switch (target.Status)
            {
                case SpecialStatus.Poisoned:
                    int poisonDamage = (int)(target.Health * 0.05);
                    target.Health -= poisonDamage;
                    Console.WriteLine($"{target.Name} está envenenado y pierde {poisonDamage} puntos de vida.");
                    break;

                case SpecialStatus.Burned:
                    int burnDamage = (int)(target.Health * 0.10);
                    target.Health -= burnDamage;
                    Console.WriteLine($"{target.Name} está quemado y pierde {burnDamage} puntos de vida.");
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Determina si el Pokémon está fuera de combate (su salud es 0 o menor).
        /// </summary>
        /// <returns>Devuelve true si el Pokémon está fuera de combate, de lo contrario false.</returns>
        public bool OutOfAction()
        {
            Outofaction = Health <= 0;
            return Outofaction;
        }
    }
}
