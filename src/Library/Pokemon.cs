using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Library;

public class Pokemon
{
    public string Name { get; set; }
    public double Health { get; set; }
    public int VidaMax { get; set; }
    public int Id { get; set; }  
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int SpecialAttack { get; set; }
    public int SpecialDefense { get; set; }
    public List<Move> Movimientos { get; set; }
    public Type Type { get; set; }
    public List<Stat> Stats { get; set; }  
    public List<Type> Types { get; set; }  
    public List<Move> Moves { get; set; }
    public EstadoEspecial Estado { get; set; }
    public int TurnosDormido { get; set; } = 0;
    public int TurnosRestantesDeSueño { get; set; } = 0;
    public bool FueraDeCombate{get;set;}
    private Random random = new Random();
    public Pokemon() {}


    public Pokemon(string name,  int id,  double health, int attack,int defense, int specialAttack, int specialDefense ,Type tipo,  List<Move> moves)
    {
        this.Name = name;
        this.Id = id;
        this.Health = health;
        this.Attack = attack;
        this.Defense = defense;
        this.SpecialAttack = specialAttack;
        this.SpecialDefense = specialDefense;
        this.Type = tipo;
        this.Moves = moves;
    }
    //Atencion, la clase atacar actualmente se encarga de manejar la efectividad y los Ataques especiales
    public void Atacar(Pokemon atacante,Pokemon oponente, Move movimiento)
    {
        // Verificar si el Pokémon actual puede atacar
        if (this.Estado == EstadoEspecial.Dormido)
        {
            Console.WriteLine($"{Name} está dormido y no puede atacar este turno.");
            return; 
        }
        else if (this.Estado == EstadoEspecial.Paralizado && new Random().Next(0, 2) == 0)
        {
            Console.WriteLine($"{Name} está paralizado y no puede atacar este turno.");
            return;
        }

        Console.WriteLine($"{Name} usa {movimiento.MoveDetails.Name}!");
        if (movimiento.EstadoEspecial != EstadoEspecial.Ninguno && oponente.Estado == EstadoEspecial.Ninguno)
        {
            oponente.Estado = movimiento.EstadoEspecial;
            Console.WriteLine($"{oponente.Name} ahora está {movimiento.EstadoEspecial}.");
        }
        else if (movimiento.EstadoEspecial == EstadoEspecial.Dormido && oponente.Estado == EstadoEspecial.Ninguno)
        {
            oponente.Dormir(); 
        }
        else if (movimiento.EstadoEspecial == EstadoEspecial.Paralizado && oponente.Estado == EstadoEspecial.Ninguno)
        {
            oponente.Paralizar();
        }
        else if (oponente.Estado != EstadoEspecial.Ninguno)
        {
            Console.WriteLine($"{oponente.Name} ya está afectado por {oponente.Estado}, no se puede aplicar otro estado.");
        }

        int? PoderMovimiento = movimiento.MoveDetails.Power;
        double? Efectividad = null;
        int? AtaquePokemon = atacante.Attack;
        int? Defensa = oponente.Defense;
        int? Precison = movimiento.MoveDetails.Accuracy;
        int Nivel = 100;
        Random rand = new Random();
        double V = rand.NextDouble() * (1.0 - 0.85) + 0.85;
        double? probabilidadGolpeCritico = 0.1 * (Precison / 100.0);
        double golpeCritico = (rand.NextDouble() < probabilidadGolpeCritico) ? 1.2 : 1.0;
        if (Type.Effectiveness.ContainsKey(oponente.Type.TypeDetail.Name))
        {
            Efectividad = Type.Effectiveness[oponente.Type.TypeDetail.Name];
        }
        else
        {
            Efectividad = 1;
        }
        double? daño = (0.1 * golpeCritico * Efectividad * V * (0.2 * Nivel + 1) * AtaquePokemon * PoderMovimiento) / (25 * Defensa) + 2;
        oponente.Health -= daño ?? 0;
        
        if (oponente.Health < 0)
        {
            oponente.Health = 0;  // La vida no puede ser negativa
        }
        Console.WriteLine($"{Name} usó {movimiento.MoveDetails.Name} e hizo {daño} puntos de daño! {oponente.Name} ahora tiene {oponente.Health} puntos de vida.");
    }

    public bool EstaFueraDeCombate()
    {
        if (Health == 0 || Health < 0)
        {
            return FueraDeCombate = true;
        }
        else
        {
            return FueraDeCombate = false;
        }
    }
    
    public bool PuedeAtacar()
    {
        if (Estado == EstadoEspecial.Dormido && TurnosRestantesDeSueño > 0)
        {
            TurnosRestantesDeSueño--;
            Console.WriteLine($"{Name} está dormido y no puede atacar. Turnos restantes: {TurnosRestantesDeSueño}");
            if (TurnosRestantesDeSueño == 0)
            {
                Estado = EstadoEspecial.Ninguno;
                Console.WriteLine($"{Name} se ha despertado.");
            }

            return false;
        }

        // Verificar si está paralizado
        if (Estado == EstadoEspecial.Paralizado)
        {
            // 50% de probabilidad de atacar
            bool puedeAtacar = random.Next(0, 2) == 0; // Devuelve true o false aleatoriamente
            if (!puedeAtacar)
            {
                Console.WriteLine($"{Name} está paralizado y no puede atacar este turno.");
                return false;
            }
        }

        return true;
    }

    public void ProcesarEstado()
    {
        if (Estado == EstadoEspecial.Envenenado)
        {
            int damage = (int)(Health * 0.05);
            Health -= damage;
            Console.WriteLine($"{Name} está envenenado y pierde {damage} puntos de vida.");
        }
        else if (Estado == EstadoEspecial.Quemado)
        {
            int burnDamage = (int)(Health * 0.10);
            Health -= burnDamage;
            Console.WriteLine($"{Name} está quemado y pierde {burnDamage} puntos de vida.");
        }
        else if (Estado == EstadoEspecial.Dormido)
        {
            Console.WriteLine($"{Name} está dormido, no puede atacar");
        }
    }
    public void Dormir()
    {
        Estado = EstadoEspecial.Dormido;
        Random random = new Random();
        TurnosDormido = random.Next(1, 5); // Aleatoreamente va de 1 a 4
        TurnosRestantesDeSueño = TurnosDormido;
        Console.WriteLine($"{Name} ha sido dormido y estará dormido por {TurnosDormido} turnos.");
    }
    public void Paralizar()
    {
        Estado = EstadoEspecial.Paralizado;
        Console.WriteLine($"{Name} ha sido paralizado.");
    }
}

