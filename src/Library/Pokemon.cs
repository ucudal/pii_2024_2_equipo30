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
        this.VidaMax = health;
        this.Attack = attack;
        this.Defense = defense;
        this.SpecialAttack = specialAttack;
        this.SpecialDefense = specialDefense;
        this.Type = tipo;
        this.Moves = moves;
        this.Estado = EstadoEspecial.Ninguno;
    }
    //Atencion, la clase atacar actualmente se encarga de manejar la efectividad y los Ataques especiales
    public void Atacar(Jugador jugador, Pokemon oponente, Move movimiento, int turnoActual)
    {
        // Verificar si el Pokémon atacante puede actuar en este turno
        if (this.Estado == EstadoEspecial.Dormido)
        {
            Console.WriteLine($"\n {this.Name} está dormido y no puede atacar este turno.\n");
            return; 
        }
        else if (this.Estado == EstadoEspecial.Paralizado)
        {
            if (new Random().Next(0, 2) == 0)  // Probabilidad del 50%
            {
                Console.WriteLine($"\n {this.Name} está paralizado y no puede atacar este turno.\n");
                return;
            }
            else
            {
                Console.WriteLine($"\n {this.Name} logra superar la parálisis y puede atacar.\n");
            }
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
        Console.WriteLine($"\n {this.Name} usa {movimiento.MoveDetails.Name}!\n");

        // Aplicar estado especial si corresponde y si el oponente no tiene un estado ya aplicado
        if (movimiento.EstadoEspecial != EstadoEspecial.Ninguno && oponente.Estado == EstadoEspecial.Ninguno)
        {
            oponente.Estado = movimiento.EstadoEspecial;
            ProcesarEstado(this, oponente);
            Console.WriteLine($" {oponente.Name} ahora está {movimiento.EstadoEspecial}.\n");
        }

        // Calcular el daño
        int? PoderMovimiento = movimiento.MoveDetails.Power;
        double Efectividad = Type.Effectiveness.ContainsKey(oponente.Type.TypeDetail.Name) ? Type.Effectiveness[oponente.Type.TypeDetail.Name] : 1.0;
        int Nivel = 100;
        double Variacion = new Random().NextDouble() * (1.0 - 0.85) + 0.85;
        double GolpeCritico = (new Random().NextDouble() < 0.1) ? 1.2 : 1.0;

        double ataqueBase = movimiento.EsAtaqueEspecial ? this.SpecialAttack : this.Attack;
        double defensaOponente = movimiento.EsAtaqueEspecial ? oponente.SpecialDefense : oponente.Defense;

        double? Daño = (0.1 * GolpeCritico * Efectividad * Variacion * (0.2 * Nivel + 1) * ataqueBase * PoderMovimiento) / (25 * defensaOponente) + 2;
        oponente.Health -= Daño ?? 0;

        // Mostrar resultado del ataque
        if (movimiento.EsAtaqueEspecial)
        {
            Console.WriteLine($" {this.Name} usó {movimiento.MoveDetails.Name} (especial) e hizo {Daño:F2} puntos de daño! {oponente.Name} ahora tiene {oponente.Health:F2} puntos de vida.\n");
        }
        else
        {
            Console.WriteLine($" {this.Name} usó {movimiento.MoveDetails.Name} e hizo {Daño:F1} puntos de daño! {oponente.Name} ahora tiene {oponente.Health:F1} puntos de vida.\n");
        }

        // Evitar valores negativos de salud
        if (oponente.Health < 0) oponente.Health = 0;
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

    
    public bool PuedeAtacar()
    {
        if (Estado == EstadoEspecial.Dormido && TurnosRestantesDeSueño > 0)
        {
            TurnosRestantesDeSueño--;
            Console.WriteLine($"{Name} está dormido y no puede atacar. Turnos restantes: {TurnosRestantesDeSueño}\n");
            if (TurnosRestantesDeSueño == 0)
            {
                Estado = EstadoEspecial.Ninguno;
                Console.WriteLine($"{Name} se ha despertado.\n");
            }
            return false;
        }
        else if (Estado == EstadoEspecial.Paralizado && new Random().Next(0, 2) == 0)
        {
            Console.WriteLine($"{Name} está paralizado y no puede atacar este turno.\n");
            return false;
        }
        return true;
    }


    public void ProcesarEstado(Pokemon atacante,Pokemon oponente)
    {
        if (oponente.Estado == EstadoEspecial.Envenenado)
        {
            int damage = (int)(oponente.Health * 0.05);
            oponente.Health -= damage;
            Console.WriteLine($" {oponente.Name} está envenenado y pierde {damage} puntos de vida.\n");
        }
        else if (oponente.Estado == EstadoEspecial.Quemado)
        {
            int burnDamage = (int)(oponente.Health * 0.10);
            oponente.Health -= burnDamage;
            Console.WriteLine($" {oponente.Name} está quemado y pierde {burnDamage} puntos de vida.\n");
        }
        else if (oponente.Estado == EstadoEspecial.Dormido)
        {
            Console.WriteLine($" {oponente.Name} está dormido, no puede ser quemado ni envenenado\n");
        }
    }
    public void ProcesarEstado()
    {
        if (Estado == EstadoEspecial.Envenenado)
        {
            int damage = (int)(Health * 0.05);
            if (damage < 1)
            {
                damage = 1; // Asegurarse de que el daño mínimo sea al menos 1 punto de vida
            }
            this.Health -= damage;
            Console.WriteLine($"{Name} está envenenado y pierde {damage} puntos de vida.\n");
        }
        else if (Estado == EstadoEspecial.Quemado)
        {
            int burnDamage = (int)(Health * 0.10);
            if (burnDamage < 1)
            {
                burnDamage = 1; // Asegurarse de que el daño mínimo sea al menos 1 punto de vida
            }
            this.Health -= burnDamage;
            Console.WriteLine($"{Name} está quemado y pierde {burnDamage} puntos de vida.\n");
        }
        else if (this.Estado == EstadoEspecial.Dormido)
        {
            Console.WriteLine($"{Name} está dormido, no puede ser quemado ni envenenado\n");
            
        }
        else if (this.Estado == EstadoEspecial.Paralizado)
        {
            Console.WriteLine($"{Name} está paralizado, no puede ser quemado ni envenenado\n");
        }
        else if (this.Estado == EstadoEspecial.Ninguno)
        {
            Console.WriteLine($"{Name} No tiene ningún efecto \n");
        }
    }

    public void Dormir()
    {
        Estado = EstadoEspecial.Dormido;
        Random random = new Random();
        TurnosDormido = random.Next(1, 5); // Aleatoreamente va de 1 a 4
        TurnosRestantesDeSueño = TurnosDormido;
        Console.WriteLine($" {Name} ha sido dormido y estará dormido por {TurnosDormido} turnos.\n");
    }
    public void Paralizar()
    {
        Estado = EstadoEspecial.Paralizado;
        Console.WriteLine($" {Name} ha sido paralizado.\n");
    }
}
