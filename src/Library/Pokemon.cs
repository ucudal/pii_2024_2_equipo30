﻿using System;
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
        public SpecialStatus Status { get; set; }

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

<<<<<<< HEAD
        public Pokemon(string name, int id, double health, int attack, int defense, int specialAttack, int specialDefense, Type tipo, List<Move> moves)
=======
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
>>>>>>> main
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
<<<<<<< HEAD
            Estado = EstadoEspecial.Ninguno;
        } 
        public Pokemon (){}

        public void Atacar(Pokemon oponente, Move movimiento)
        {
            if (!PuedeAtacar())
            {
                Console.WriteLine($"{Name} no puede atacar este turno.");
                return;
=======
            Status = SpecialStatus.NoneStatus;
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
            if (movement.SpecialAttack && !player.CanUseEspecialAtack(movement.MoveDetails.Name, currentShift))
            {
                Console.WriteLine($"No puedes usar el ataque especial {movement.MoveDetails.Name} en este momento. Debes esperar más turnos.");
                return;
            }
            if (movement.SpecialAttack)
            {
                player.RegisterSpecialAttack(movement.MoveDetails.Name, currentShift);
>>>>>>> main
            }

            Console.WriteLine($"{Name} usa {movement.MoveDetails.Name}!");
            if (enemy.Status == SpecialStatus.Asleep)
            {
                Console.WriteLine($"{Name} ya está dormido y no puede dormir nuevamente hasta despertar.");
            }

<<<<<<< HEAD
            if (movimiento.EstadoEspecial != EstadoEspecial.Ninguno && oponente.Estado == EstadoEspecial.Ninguno)
            {
                oponente.Estado = movimiento.EstadoEspecial;
                Console.WriteLine($"{oponente.Name} ahora está {movimiento.EstadoEspecial}.");
            }

            double efectividad = Type.Effectiveness.ContainsKey(oponente.Type.TypeDetail.Name) ? Type.Effectiveness[oponente.Type.TypeDetail.Name] : 1.0;
            double daño = CalcularDaño(movimiento, efectividad, oponente);

            oponente.Health -= daño;
            if (oponente.Health < 0) oponente.Health = 0;

            Console.WriteLine($"{Name} hizo {daño:F1} puntos de daño! {oponente.Name} ahora tiene {oponente.Health:F1} puntos de vida.");
        }

        public double CalcularDaño(Move movimiento, double efectividad, Pokemon oponente)
        {
            int poder = movimiento.MoveDetails.Power ?? 0;
            int ataque = movimiento.EstadoEspecial == EstadoEspecial.Ninguno ? Attack : SpecialAttack;
            int defensa = movimiento.EstadoEspecial == EstadoEspecial.Ninguno ? oponente.Defense : oponente.SpecialDefense;
            int nivel = 100;
            double variacion = random.NextDouble() * (1.0 - 0.85) + 0.85;
            double critico = random.NextDouble() < 0.1 ? 1.2 : 1.0;

            return (0.1 * critico * efectividad * variacion * (0.2 * nivel + 1) * ataque * poder) / (25 * defensa) + 2;
        }

        public bool PuedeAtacar()
        {
            // Procesar estado antes de permitir ataque
            ProcesarEstado();

            if (Estado == EstadoEspecial.Dormido)
            {
                if (TurnosRestantesDeSueño > 0)
=======
            if (movement.SpecialStatus != SpecialStatus.NoneStatus && enemy.Status == SpecialStatus.NoneStatus)
            {
                enemy.Status = movement.SpecialStatus;
                if (movement.SpecialStatus == SpecialStatus.Asleep)
>>>>>>> main
                {
                    enemy.SleepTurnsLeft = random.Next(1, 5);
                    Console.WriteLine($"{enemy.Name} ha sido dormido y estará dormido por {enemy.SleepTurnsLeft} turnos.");
                }
                else
                {
                    Console.WriteLine($"{enemy.Name} ahora está {movement.SpecialStatus}.");
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
            int attack = movimiento.SpecialAttack ? SpecialAttack : Attack;
            int defence = movimiento.SpecialAttack ? oponente.SpecialDefense : oponente.Defense;
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
        /// Aplica el estado de parálisis al Pokémon.
        /// </summary>
        public void Paralyze()
        {
            Status = SpecialStatus.Paralyzed;
            Console.WriteLine($"{Name} ha sido paralizado.");
        }
        

        /// <summary>
        /// Procesa el estado actual del Pokémon, aplicando los efectos correspondientes.
        /// </summary>
        /// <param name="enemy">El Pokémon enemigo, si es aplicable.</param>
        public void ProcessStatus()
        {
            switch (this.Status)
            {
                case SpecialStatus.Poisoned:
                    int poisonDamage = (int)(this.Health * 0.05);
                    this.Health -= poisonDamage;
                    Console.WriteLine($"{this.Name} está envenenado y pierde {poisonDamage} puntos de vida.");
                    break;
                case SpecialStatus.Burned:
                    int burnDamage = (int)(this.Health * 0.10);
                    this.Health -= burnDamage;
                    Console.WriteLine($"{this.Name} está quemado y pierde {burnDamage} puntos de vida.");
                    break;
                case SpecialStatus.Asleep:
                    break;
                case SpecialStatus.Paralyzed:
                    break;
                case SpecialStatus.NoneStatus:
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
