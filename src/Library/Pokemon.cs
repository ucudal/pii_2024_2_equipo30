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
        public double VidaMax { get; set; }
        public int Id { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SpecialAttack { get; set; }
        public int SpecialDefense { get; set; }
        public Type Type { get; set; }
        public List<Stat> Stats { get; set; }
        public List<Type> Types { get; set; }
        public List<Move> Moves { get; set; }
        public EstadoEspecial Estado { get; set; }
        public int TurnosRestantesDeSueño { get; set; } = 0;
        public bool FueraDeCombate = false;
        private Random random = new Random();

        public Pokemon(string name, int id, double health, int attack, int defense, int specialAttack, int specialDefense, Type tipo, List<Move> moves)
        {
            Name = name;
            Id = id;
            Health = health;
            VidaMax = health;
            Attack = attack;
            Defense = defense;
            SpecialAttack = specialAttack;
            SpecialDefense = specialDefense;
            Type = tipo;
            Moves = moves;
            Estado = EstadoEspecial.Ninguno;
        } 
        public Pokemon (){}

    public void Atacar(Jugador jugador, Pokemon oponente, Move movimiento, int turnoActual)
{
    // Verificar si el Pokémon atacante puede actuar en este turno
    if (!PuedeAtacar())
    {
        Console.WriteLine($"{Name} no puede atacar este turno debido a su estado {Estado}.");
        return;
    }

    // Verificar si se trata de un ataque especial
    if (movimiento.EsAtaqueEspecial)
    {
        // Si el ataque especial no está permitido, salir y no atacar
        if (!jugador.PuedeUsarAtaqueEspecial(movimiento.MoveDetails.Name, turnoActual))
        {
            Console.WriteLine($"No puedes usar el ataque especial {movimiento.MoveDetails.Name} en este momento. Debes esperar más turnos.");
            return;
        }
        else
        {
            // Si el ataque especial es válido, registrar el turno y continuar
            jugador.RegistrarAtaqueEspecial(movimiento.MoveDetails.Name, turnoActual);
        }
    }

    // Ataque exitoso
    Console.WriteLine($"{Name} usa {movimiento.MoveDetails.Name}!");

    // Aplicar estado especial si corresponde y si el oponente no tiene un estado ya aplicado
    if (movimiento.EstadoEspecial != EstadoEspecial.Ninguno && oponente.Estado == EstadoEspecial.Ninguno)
    {
        oponente.Estado = movimiento.EstadoEspecial;
        if (movimiento.EstadoEspecial == EstadoEspecial.Dormido)
        {
            oponente.Dormir();
        }
        else
        {
            Console.WriteLine($"{oponente.Name} ahora está {movimiento.EstadoEspecial}.");
        }
    }

    // Calcular el daño
    double efectividad = Type.Effectiveness.ContainsKey(oponente.Type.TypeDetail.Name) ? Type.Effectiveness[oponente.Type.TypeDetail.Name] : 1.0;
    double daño = CalcularDaño(movimiento, efectividad, oponente);

    // Aplicar el daño
    oponente.Health -= daño;
    if (oponente.Health < 0) oponente.Health = 0;

    // Mostrar resultado del ataque
    Console.WriteLine($"{Name} hizo {daño:F1} puntos de daño! {oponente.Name} ahora tiene {oponente.Health:F1} puntos de vida.");
}

public double CalcularDaño(Move movimiento, double efectividad, Pokemon oponente)
{
    int poder = movimiento.MoveDetails.Power ?? 0;
    int ataque = movimiento.EsAtaqueEspecial ? SpecialAttack : Attack;
    int defensa = movimiento.EsAtaqueEspecial ? oponente.SpecialDefense : oponente.Defense;
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
                {
                    TurnosRestantesDeSueño--;
                    Console.WriteLine($"{Name} está dormido y no puede atacar. Turnos restantes: {TurnosRestantesDeSueño}");

                    // Si se agotaron los turnos de sueño, se cambia el estado a Ninguno
                    if (TurnosRestantesDeSueño == 0)
                    {
                        Estado = EstadoEspecial.Ninguno;
                        Console.WriteLine($"{Name} se ha despertado.");
                    }

                    return false;  // El Pokémon no puede atacar mientras está dormido
                }
            }
            else if (Estado == EstadoEspecial.Paralizado && random.Next(0, 2) == 0)
            {
                Console.WriteLine($"{Name} está paralizado y no puede atacar este turno.");
                return false;  // No puede atacar por parálisis
            }

            return true;  // Si no hay estado que impida, el Pokémon puede atacar
        }

public void Paralizar()
{
    Estado = EstadoEspecial.Paralizado;
    Console.WriteLine($"{Name} ha sido paralizado.");
}
public void Dormir()
{
    if (Estado != EstadoEspecial.Dormido)  // Solo se duerme si no está ya dormido
    {
        Estado = EstadoEspecial.Dormido;
        TurnosRestantesDeSueño = random.Next(1, 5);  // Asigna un número aleatorio de turnos de sueño
        Console.WriteLine($"{Name} ha sido dormido y estará dormido por {TurnosRestantesDeSueño} turnos.");
    }
    else
    {
        Console.WriteLine($"{Name} ya está dormido y no puede dormir nuevamente hasta despertar.");
    }
}

public void ProcesarEstado(Pokemon oponente = null)
{
    // Si el objetivo es null, procesamos para el mismo Pokémon
    Pokemon target = oponente ?? this; 

    switch (target.Estado)
    {
        case EstadoEspecial.Envenenado:
            int poisonDamage = (int)(target.Health * 0.05);
            target.Health -= poisonDamage;
            Console.WriteLine($"{target.Name} está envenenado y pierde {poisonDamage} puntos de vida.");
            break;

        case EstadoEspecial.Quemado:
            int burnDamage = (int)(target.Health * 0.10);
            target.Health -= burnDamage;
            Console.WriteLine($"{target.Name} está quemado y pierde {burnDamage} puntos de vida.");
            break;

        case EstadoEspecial.Dormido: ;
            break;

        case EstadoEspecial.Paralizado:
            break;

        case EstadoEspecial.Ninguno:
            break;
    }
}

        public bool EstaFueraDeCombate()
        {
            // Actualiza la propiedad FueraDeCombate según la salud del Pokémon
            if (Health <= 0)
            {
                FueraDeCombate = true; // Si la salud es 0 o menor, está fuera de combate
            }
            else
            {
                FueraDeCombate = false; // Si la salud es mayor que 0, está en combate
            }
            return FueraDeCombate;
        }


    }



