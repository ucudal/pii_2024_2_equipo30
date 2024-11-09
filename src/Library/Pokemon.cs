using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using System;
using System.Collections.Generic;

namespace Library;

public class Pokemon : IPokemon
{
        public string Name { get; set; }
        public double Health { get; set; }
        public double MaxHealt { get; set; }
        public int Id { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SpecialAttack { get; set; }
        public int SpecialDefense { get; set; }
        public Type Type { get; set; }
        public List<Stat> Stats { get; set; }
        public List<Type> Types { get; set; }
        public List<Move> Moves { get; set; }
        public SpecialStatus Status { get; set; }
        public int SleepTurnsLeft { get; set; } = 0;
        
        public bool Outofaction = false;
        
        private Random random = new Random();

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
        public Pokemon (){}

    public void AttackP(Player player, Pokemon enemy, Move movement, int currentShift)
{
    // Verificar si el Pokémon atacante puede actuar en este turno
    if (!CanAtack())
    {
        Console.WriteLine($"{Name} no puede atacar este turno debido a su estado {Status}.");
        return;
    }

    // Verificar si se trata de un ataque especial
    if (movement.EspecialAttack)
    {
        // Si el ataque especial no está permitido, salir y no atacar
        if (!player.CanUseEspecialAtack(movement.MoveDetails.Name, currentShift))
        {
            Console.WriteLine($"No puedes usar el ataque especial {movement.MoveDetails.Name} en este momento. Debes esperar más turnos.");
            return;
        }
        else
        {
            // Si el ataque especial es válido, registrar el turno y continuar
            player.RegisterSpecialAttack(movement.MoveDetails.Name, currentShift);
        }
    }

    // Ataque exitoso
    Console.WriteLine($"{Name} usa {movement.MoveDetails.Name}!");

    // Aplicar estado especial si corresponde y si el enemy no tiene un estado ya aplicado
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

    // Calcular el daño
    double Efficacy = Type.Effectiveness.ContainsKey(enemy.Type.TypeDetail.Name) ? Type.Effectiveness[enemy.Type.TypeDetail.Name] : 1.0;
    double Damage = CalculateDamage(movement, Efficacy, enemy);

    // Aplicar el daño
    enemy.Health -= Damage;
    if (enemy.Health < 0) enemy.Health = 0;

    // Mostrar resultado del ataque
    Console.WriteLine($"{Name} hizo {Damage:F1} puntos de daño! {enemy.Name} ahora tiene {enemy.Health:F1} puntos de vida.");
}

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


        public bool CanAtack()
        {
            // Procesar estado antes de permitir ataque
            ProcessStatus();

            if (Status == SpecialStatus.Asleep)
            {
                if (SleepTurnsLeft > 0)
                {
                    SleepTurnsLeft--;
                    Console.WriteLine($"{Name} está dormido y no puede atacar. Turnos restantes: {SleepTurnsLeft}");

                    // Si se agotaron los turnos de sueño, se cambia el estado a NoneStatus
                    if (SleepTurnsLeft == 0)
                    {
                        Status = SpecialStatus.NoneStatus;
                        Console.WriteLine($"{Name} se ha despertado.");
                    }

                    return false;  // El Pokémon no puede atacar mientras está dormido
                }
            }
            else if (Status == SpecialStatus.Paralyzed && random.Next(0, 2) == 0)
            {
                Console.WriteLine($"{Name} está paralizado y no puede atacar este turno.");
                return false;  // No puede atacar por parálisis
            }

            return true;  // Si no hay estado que impida, el Pokémon puede atacar
        }

public void Paralyze()
{
    Status = SpecialStatus.Paralyzed;
    Console.WriteLine($"{Name} ha sido paralizado.");
}
public void sleep()
{
    if (Status != SpecialStatus.Asleep)  // Solo se duerme si no está ya dormido
    {
        Status = SpecialStatus.Asleep;
        SleepTurnsLeft = random.Next(1, 5);  // Asigna un número aleatorio de turnos de sueño
        Console.WriteLine($"{Name} ha sido dormido y estará dormido por {SleepTurnsLeft} turnos.");
    }
    else
    {
        Console.WriteLine($"{Name} ya está dormido y no puede dormir nuevamente hasta despertar.");
    }
}

public void ProcessStatus(Pokemon enemy = null)
{
    // Si el objetivo es null, procesamos para el mismo Pokémon
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

        case SpecialStatus.Asleep: ;
            break;

        case SpecialStatus.Paralyzed:
            break;

        case SpecialStatus.NoneStatus:
            break;
    }
}

        public bool OutOfAction()
        {
            // Actualiza la propiedad Outofaction según la salud del Pokémon
            if (Health <= 0)
            {
                Outofaction = true; // Si la salud es 0 o menor, está fuera de combate
            }
            else
            {
                Outofaction = false; // Si la salud es mayor que 0, está en combate
            }
            return Outofaction;
        }


    }



