// using System;
// using System.Collections.Generic;
//
// namespace Library
// {
//     public class Pokemon
//     {
//         public string Name { get; set; }
//         public double Health { get; set; }
//         public double VidaMax { get; set; }
//         public int Id { get; set; }
//         public int Attack { get; set; }
//         public int Defense { get; set; }
//         public int SpecialAttack { get; set; }
//         public int SpecialDefense { get; set; }
//         public Type Type { get; set; }
//         public List<Stat> Stats { get; set; }
//         public List<Type> Types { get; set; }
//         public List<Move> Moves { get; set; }
//         public EstadoEspecial Estado { get; set; }
//         public int TurnosRestantesDeSueño { get; set; } = 0;
//         public bool FueraDeCombate => Health <= 0;
//         private Random random = new Random();
//
//         public Pokemon(string name, int id, double health, int attack, int defense, int specialAttack, int specialDefense, Type tipo, List<Move> moves)
//         {
//             Name = name;
//             Id = id;
//             Health = health;
//             VidaMax = health;
//             Attack = attack;
//             Defense = defense;
//             SpecialAttack = specialAttack;
//             SpecialDefense = specialDefense;
//             Type = tipo;
//             Moves = moves;
//             Estado = EstadoEspecial.Ninguno;
//         }
//
//         public void Atacar(Pokemon oponente, Move movimiento)
//         {
//             if (!PuedeAtacar())
//             {
//                 Console.WriteLine($"{Name} no puede atacar este turno.");
//                 return;
//             }
//
//             Console.WriteLine($"{Name} usa {movimiento.MoveDetails.Name}!");
//
//             if (movimiento.EstadoEspecial != EstadoEspecial.Ninguno && oponente.Estado == EstadoEspecial.Ninguno)
//             {
//                 oponente.Estado = movimiento.EstadoEspecial;
//                 Console.WriteLine($"{oponente.Name} ahora está {movimiento.EstadoEspecial}.");
//             }
//
//             double efectividad = Type.Effectiveness.ContainsKey(oponente.Type.TypeDetail.Name) ? Type.Effectiveness[oponente.Type.TypeDetail.Name] : 1.0;
//             double daño = CalcularDaño(movimiento, efectividad, oponente);
//
//             oponente.Health -= daño;
//             if (oponente.Health < 0) oponente.Health = 0;
//
//             Console.WriteLine($"{Name} hizo {daño:F1} puntos de daño! {oponente.Name} ahora tiene {oponente.Health:F1} puntos de vida.");
//         }
//
//         private double CalcularDaño(Move movimiento, double efectividad, Pokemon oponente)
//         {
//             int poder = movimiento.MoveDetails.Power ?? 0;
//             int ataque = movimiento.EstadoEspecial == EstadoEspecial.Ninguno ? Attack : SpecialAttack;
//             int defensa = movimiento.EstadoEspecial == EstadoEspecial.Ninguno ? oponente.Defense : oponente.SpecialDefense;
//             int nivel = 100;
//             double variacion = random.NextDouble() * (1.0 - 0.85) + 0.85;
//             double critico = random.NextDouble() < 0.1 ? 1.2 : 1.0;
//
//             return (0.1 * critico * efectividad * variacion * (0.2 * nivel + 1) * ataque * poder) / (25 * defensa) + 2;
//         }
//
//         public bool PuedeAtacar()
//         {
//             if (Estado == EstadoEspecial.Dormido)
//             {
//                 if (TurnosRestantesDeSueño > 0)
//                 {
//                     TurnosRestantesDeSueño--;
//                     Console.WriteLine($"{Name} está dormido y no puede atacar. Turnos restantes: {TurnosRestantesDeSueño}");
//                     if (TurnosRestantesDeSueño == 0) Estado = EstadoEspecial.Ninguno;
//                     return false;
//                 }
//             }
//             else if (Estado == EstadoEspecial.Paralizado && random.Next(0, 2) == 0)
//             {
//                 Console.WriteLine($"{Name} está paralizado y no puede atacar este turno.");
//                 return false;
//             }
//             return true;
//         }
//
//         public void ProcesarEstado()
//         {
//             if (Estado == EstadoEspecial.Envenenado)
//             {
//                 int daño = (int)(Health * 0.05);
//                 Health -= daño;
//                 Console.WriteLine($"{Name} está envenenado y pierde {daño} puntos de vida.");
//             }
//             else if (Estado == EstadoEspecial.Quemado)
//             {
//                 int daño = (int)(Health * 0.10);
//                 Health -= daño;
//                 Console.WriteLine($"{Name} está quemado y pierde {daño} puntos de vida.");
//             }
//         }
//
//         public void Dormir()
//         {
//             Estado = EstadoEspecial.Dormido;
//             TurnosRestantesDeSueño = random.Next(1, 5);
//             Console.WriteLine($"{Name} ha sido dormido y estará dormido por {TurnosRestantesDeSueño} turnos.");
//         }
//
//         public void Paralizar()
//         {
//             Estado = EstadoEspecial.Paralizado;
//             Console.WriteLine($"{Name} ha sido paralizado.");
//         }
//     }
// }
