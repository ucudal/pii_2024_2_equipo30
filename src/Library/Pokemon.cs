using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using DSharpPlus.SlashCommands;

namespace Library
{
    /// <summary>
    /// Clase que representa un Pokémon en el contexto del juego.
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
        /// Tipo del Pokémon (por ejemplo, fuego, agua, planta).
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Lista de estadísticas del Pokémon.
        /// </summary>
        public List<Stat> Stats { get; set; }

        /// <summary>
        /// Lista de tipos del Pokémon.
        /// </summary>
        public List<Type> Types { get; set; }

        /// <summary>
        /// Lista de movimientos que el Pokémon conoce.
        /// </summary>
        public List<Move> Moves { get; set; }

        /// <summary>
        /// Estado especial del Pokémon (por ejemplo, envenenado, paralizado).
        /// </summary>
        public SpecialStatus Status { get; set; }

        /// <summary>
        /// Número de turnos restantes que el Pokémon permanecerá dormido.
        /// </summary>
        public int SleepTurnsLeft { get; set; } = 0;

        /// <summary>
        /// Indica si el Pokémon está fuera de combate.
        /// </summary>
        public bool Outofaction = false;

        /// <summary>
        /// Generador de números aleatorios para determinar variación de daño y estados.
        /// </summary>
        private Random random = new Random();
        
        public List<Pokemon> StrongestList { get; set; } = new List<Pokemon>();
        
        public Pokemon Strongest { get; set; } = new Pokemon();

        /// <summary>
        /// Constructor de la clase Pokemon.
        /// </summary>
        /// <param name="name">Nombre del Pokémon.</param>
        /// <param name="id">Identificador único del Pokémon.</param>
        /// <param name="health">Salud del Pokémon.</param>
        /// <param name="attack">Valor de ataque del Pokémon.</param>
        /// <param name="defense">Valor de defensa del Pokémon.</param>
        /// <param name="specialAttack">Valor de ataque especial del Pokémon.</param>
        /// <param name="specialDefense">Valor de defensa especial del Pokémon.</param>
        /// <param name="type">Tipo del Pokémon.</param>
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
            Status = SpecialStatus.NoneStatus;
        }

        /// <summary>
        /// Constructor sin parámetros de la clase Pokemon.
        /// </summary>
        public Pokemon() { }

        /// <summary>
        /// Realiza un ataque hacia un Pokémon enemigo.
        /// </summary>
        /// <param name="player">El jugador propietario del Pokémon atacante.</param>
        /// <param name="enemy">El Pokémon enemigo que recibirá el ataque.</param>
        /// <param name="movement">El movimiento utilizado para el ataque.</param>
        /// <param name="currentShift">El turno actual en la batalla.</param>
        /// <param name="ctx">El contexto de la interacción en Discord.</param>
        public async void AttackP(Player player, Pokemon enemy, Move movement, int currentShift, InteractionContext ctx)
        {
            if (movement.SpecialAttack && !player.CanUseEspecialAtack(movement.MoveDetails.Name, currentShift))
            {
                await ctx.Channel.SendMessageAsync($"No puedes usar el ataque especial {movement.MoveDetails.Name} en este momento. Debes esperar más turnos.");
                return;
            }
            if (movement.SpecialAttack)
            {
                player.RegisterSpecialAttack(movement.MoveDetails.Name, currentShift);
            }

            await ctx.Channel.SendMessageAsync($"{Name} usa {movement.MoveDetails.Name}!");
            if (enemy.Status == SpecialStatus.Asleep)
            {
                await ctx.Channel.SendMessageAsync($"{enemy.Name} ya está dormido y no puede dormir nuevamente hasta despertar.");
            }

            if (movement.SpecialStatus != SpecialStatus.NoneStatus && enemy.Status == SpecialStatus.NoneStatus)
            {
                enemy.Status = movement.SpecialStatus;
                if (movement.SpecialStatus == SpecialStatus.Asleep)
                {
                    enemy.SleepTurnsLeft = random.Next(1, 5);
                    await ctx.Channel.SendMessageAsync($"{enemy.Name} ha sido dormido y estará dormido por {enemy.SleepTurnsLeft} turnos.");
                }
                else
                {
                    await ctx.Channel.SendMessageAsync($"{enemy.Name} ahora está {movement.SpecialStatus}.");
                }
            }

            double efficacy = Type.Effectiveness.ContainsKey(enemy.Type.TypeDetail.Name) ? Type.Effectiveness[enemy.Type.TypeDetail.Name] : 1.0;
            double damage = CalculateDamage(movement, efficacy, enemy);

            enemy.Health -= damage;
            if (enemy.Health < 0) enemy.Health = 0;

            await ctx.Channel.SendMessageAsync($"{Name} hizo {damage:F1} puntos de daño! {enemy.Name} ahora tiene {enemy.Health:F1} puntos de vida.");

        }
        /// <summary>
        /// Retorna el nombre(String) del pokemon mas poderoso(el que mayor daño puede causarle al oponente.
        /// </summary>
        /// <param name="movimiento">El movimiento que el Pokémon usará para atacar.</param>
        /// <param name="efectividad">El factor de efectividad del movimiento.</param>
        /// <param name="oponente">El Pokémon enemigo que recibirá el daño.</param>
        /// <returns>El valor de daño calculado.</returns>
        public string StrongestPokemon(Player actualPlayer, Player enemyPlayer, Move movement, InteractionContext ctx)
        {
            Pokemon actualPokemon = actualPlayer.actualPokemon;
            Pokemon enemyActualPokemon = enemyPlayer.actualPokemon;
            double efficacy = Type.Effectiveness.ContainsKey(enemyActualPokemon.Type.TypeDetail.Name)
                ? Type.Effectiveness[enemyActualPokemon.Type.TypeDetail.Name]
                : 1.0;
            foreach (Pokemon pokemon in actualPlayer.Team)
            {
                if (pokemon.CalculateDamage(movement, efficacy, enemyActualPokemon) > actualPokemon.CalculateDamage(movement, efficacy, enemyActualPokemon))
                {
                    StrongestList.Add(pokemon);
                }
            }
            Strongest = StrongestList.Last();
            //await ctx.Channel.SendMessageAsync($"{Strongest.Name} es el mas efectivo y poderoso del equipo.");
            return Strongest.Name;
        }

        /// <summary>
        /// Calcula el daño infligido por un movimiento específico al oponente.
        /// </summary>
        /// <param name="movimiento">El movimiento que el Pokémon usará para atacar.</param>
        /// <param name="efectividad">El factor de efectividad del movimiento.</param>
        /// <param name="oponente">El Pokémon enemigo que recibirá el daño.</param>
        /// <returns>El valor de daño calculado.</returns>
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
        /// Determina si el Pokémon puede atacar según su estado actual.
        /// </summary>
        /// <param name="ctx">El contexto de la interacción en Discord.</param>
        /// <returns>True si el Pokémon puede atacar, de lo contrario false.</returns>
        public async Task<bool> CanAtack(InteractionContext ctx)
        {
            ProcessStatus();
            if (Status == SpecialStatus.Asleep && SleepTurnsLeft > 0)
            {
                SleepTurnsLeft--;
                await ctx.Channel.SendMessageAsync($"{Name} está dormido y no puede atacar. Turnos restantes: {SleepTurnsLeft}");

                if (SleepTurnsLeft == 0)
                {
                    Status = SpecialStatus.NoneStatus;
                    await ctx.Channel.SendMessageAsync($"{Name} se ha despertado.");
                }

                return false;
            }
            if (Status == SpecialStatus.Paralyzed && random.Next(0, 2) == 0)
            {
                await ctx.Channel.SendMessageAsync($"{Name} está paralizado y no puede atacar este turno.");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Paraliza al Pokémon, cambiando su estado a paralizado.
        /// </summary>
        public void Paralyze()
        {
            Status = SpecialStatus.Paralyzed;
            Console.WriteLine($"{Name} ha sido paralizado.");
        }

        /// <summary>
        /// Procesa el estado actual del Pokémon y aplica los efectos correspondientes.
        /// </summary>
        /// <returns>Una cadena de texto que describe el efecto del estado especial del Pokémon.</returns>
        public string ProcessStatus()
        {
            switch (this.Status)
            {
                case SpecialStatus.Poisoned:
                    int poisonDamage = (int)(this.Health * 0.05);
                    this.Health -= poisonDamage;
                    return $"{this.Name} está envenenado y pierde {poisonDamage} puntos de vida.";
                case SpecialStatus.Burned:
                    int burnDamage = (int)(this.Health * 0.10);
                    this.Health -= burnDamage;
                    return $"{this.Name} está quemado y pierde {burnDamage} puntos de vida.";
                case SpecialStatus.Asleep:
                    break;
                case SpecialStatus.Paralyzed:
                    break;
                case SpecialStatus.NoneStatus:
                    break;
            }

            return "";
        }

        /// <summary>
        /// Verifica si el Pokémon está fuera de combate.
        /// </summary>
        /// <returns>True si el Pokémon ya no puede combatir, de lo contrario false.</returns>
        public bool OutOfAction()
        {
            Outofaction = Health <= 0;
            return Outofaction;
        }
    }
}
