using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Library
{
    /// <summary>
    /// Clase que representa un Pokémon, con sus atributos, movimientos y métodos para interactuar durante una batalla.
    /// Implementa la interfaz <see cref="IPokemon"/>.
    /// </summary>
    public class Pokemon : IPokemon
    {
        /// <summary>
        /// Nombre del Pokémon.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Puntos de salud actuales del Pokémon.
        /// </summary>
        public double Health { get; set; }

        /// <summary>
        /// Máximo de puntos de salud que el Pokémon puede tener.
        /// </summary>
        public double MaxHealt { get; set; }

        /// <summary>
        /// Identificador único del Pokémon.
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
        /// Tipo principal del Pokémon.
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Lista de estadísticas del Pokémon.
        /// </summary>
        public List<Stat> Stats { get; set; }

        /// <summary>
        /// Lista de tipos secundarios del Pokémon.
        /// </summary>
        public List<Type> Types { get; set; }

        /// <summary>
        /// Lista de movimientos que el Pokémon conoce.
        /// </summary>
        public List<Move> Moves { get; set; }

        /// <summary>
        /// Estado especial actual del Pokémon (por ejemplo, envenenado, quemado, dormido, paralizado).
        /// </summary>
        public EspecialStatus Status { get; set; }

        /// <summary>
        /// Número de turnos restantes que el Pokémon estará dormido.
        /// </summary>
        public int SleepTurnsLeft { get; set; } = 0;

        /// <summary>
        /// Indica si el Pokémon está fuera de combate.
        /// </summary>
        public bool Outofaction = false;

        /// <summary>
        /// Generador de números aleatorios, utilizado para variaciones de daño y otros efectos.
        /// </summary>
        private Random random = new Random();

        /// <summary>
        /// Constructor para inicializar un Pokémon con sus atributos básicos.
        /// </summary>
        /// <param name="name">Nombre del Pokémon.</param>
        /// <param name="id">Identificador único del Pokémon.</param>
        /// <param name="health">Puntos de salud del Pokémon.</param>
        /// <param name="attack">Valor de ataque del Pokémon.</param>
        /// <param name="defense">Valor de defensa del Pokémon.</param>
        /// <param name="specialAttack">Valor de ataque especial del Pokémon.</param>
        /// <param name="specialDefense">Valor de defensa especial del Pokémon.</param>
        /// <param name="type">Tipo principal del Pokémon.</param>
        /// <param name="moves">Lista de movimientos que el Pokémon conoce.</param>
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
            Status = EspecialStatus.NoneStatus;
        }

        /// <summary>
        /// Constructor vacío.
        /// </summary>
        public Pokemon() {}

        /// <summary>
        /// Realiza un ataque a un Pokémon enemigo usando un movimiento específico.
        /// </summary>
        /// <param name="player">Jugador que posee el Pokémon atacante.</param>
        /// <param name="enemy">El Pokémon enemigo que recibe el ataque.</param>
        /// <param name="movement">Movimiento usado por el Pokémon atacante.</param>
        /// <param name="currentShift">Turno actual en el que se realiza el ataque.</param>
        public void AttackP(Player player, Pokemon enemy, Move movement, int currentShift)
        {
            if (!CanAtack())
            {
                Console.WriteLine($"{Name} no puede atacar este turno debido a su estado {Status}.");
                return;
            }

            if (movement.EspecialAttack && !player.CanUseEspecialAtack(movement.MoveDetails.Name, currentShift))
            {
                Console.WriteLine($"No puedes usar el ataque especial {movement.MoveDetails.Name} en este momento. Debes esperar más turnos.");
                return;
            }
            else if (movement.EspecialAttack)
            {
                player.RegisterSpecialAttack(movement.MoveDetails.Name, currentShift);
            }

            Console.WriteLine($"{Name} usa {movement.MoveDetails.Name}!");

            if (movement.EspecialStatus != EspecialStatus.NoneStatus && enemy.Status == EspecialStatus.NoneStatus)
            {
                enemy.Status = movement.EspecialStatus;
                if (movement.EspecialStatus == EspecialStatus.Asleep)
                {
                    enemy.sleep();
                }
                else
                {
                    Console.WriteLine($"{enemy.Name} ahora está {movement.EspecialStatus}.");
                }
            }

            double efficacy = Type.Effectiveness.ContainsKey(enemy.Type.TypeDetail.Name) ? Type.Effectiveness[enemy.Type.TypeDetail.Name] : 1.0;
            double damage = CalculateDamage(movement, efficacy, enemy);

            enemy.Health -= damage;
            if (enemy.Health < 0) enemy.Health = 0;

            Console.WriteLine($"{Name} hizo {damage:F1} puntos de daño! {enemy.Name} ahora tiene {enemy.Health:F1} puntos de vida.");
        }

        /// <summary>
        /// Calcula el daño causado por un movimiento específico.
        /// </summary>
        /// <param name="movimiento">Movimiento usado para el ataque.</param>
        /// <param name="efectividad">Factor de efectividad del movimiento en relación al tipo del oponente.</param>
        /// <param name="oponente">El Pokémon oponente que recibe el ataque.</param>
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
        /// Verifica si el Pokémon puede atacar, teniendo en cuenta su estado actual.
        /// </summary>
        /// <returns>Devuelve true si el Pokémon puede atacar, de lo contrario false.</returns>
        public bool CanAtack()
        {
            ProcessStatus();
            if (Status == EspecialStatus.Asleep && SleepTurnsLeft > 0)
            {
                SleepTurnsLeft--;
                Console.WriteLine($"{Name} está dormido y no puede atacar. Turnos restantes: {SleepTurnsLeft}");

                if (SleepTurnsLeft == 0)
                {
                    Status = EspecialStatus.NoneStatus;
                    Console.WriteLine($"{Name} se ha despertado.");
                }

                return false;
            }
            else if (Status == EspecialStatus.Paralyzed && random.Next(0, 2) == 0)
            {
                Console.WriteLine($"{Name} está paralizado y no puede atacar este turno.");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Aplica el estado de parálisis al Pokémon.
        /// </summary>
        public void Paralyze()
        {
            Status = EspecialStatus.Paralyzed;
            Console.WriteLine($"{Name} ha sido paralizado.");
        }

        /// <summary>
        /// Aplica el estado de sueño al Pokémon.
        /// </summary>
        public void sleep()
        {
            if (Status != EspecialStatus.Asleep)
            {
                Status = EspecialStatus.Asleep;
                SleepTurnsLeft = random.Next(1, 5);
                Console.WriteLine($"{Name} ha sido dormido y estará dormido por {SleepTurnsLeft} turnos.");
            }
            else
            {
                Console.WriteLine($"{Name} ya está dormido y no puede dormir nuevamente hasta despertar.");
            }
        }

        /// <summary>
        /// Procesa el estado actual del Pokémon, aplicando los efectos correspondientes.
        /// </summary>
        /// <param name="enemy">El Pokémon enemigo, si es aplicable.</param>
        public void ProcessStatus(Pokemon enemy = null)
        {
            Pokemon target = enemy ?? this;

            switch (target.Status)
            {
                case EspecialStatus.Poisoned:
                    int poisonDamage = (int)(target.Health * 0.05);
                    target.Health -= poisonDamage;
                    Console.WriteLine($"{target.Name} está envenenado y pierde {poisonDamage} puntos de vida.");
                    break;
                case EspecialStatus.Burned:
                    int burnDamage = (int)(target.Health * 0.10);
                    target.Health -= burnDamage;
                    Console.WriteLine($"{target.Name} está quemado y pierde {burnDamage} puntos de vida.");
                    break;
                case EspecialStatus.Asleep:
                    break;
                case EspecialStatus.Paralyzed:
                    break;
                case EspecialStatus.NoneStatus:
                    break;
            }
        }

        /// <summary>
        /// Verifica si el Pokémon está fuera de combate (sin puntos de salud).
        /// </summary>
        /// <returns>Devuelve true si el Pokémon está fuera de combate, de lo contrario false.</returns>
        public bool OutOfAction()
        {
            Outofaction = Health <= 0;
            return Outofaction;
        }
    }
}
