using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace API;

public class Pokemon
{
    public string Name { get; set; }
    public int Id { get; set; }
    public int Health { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int SpecialAttack { get; set; }
    public int SpecialDefense { get; set; }
    
    public Type Tipo { get; set; }
    public List<Ability> Abilities { get; set; }
    public List<Stat> Stats { get; set; }  
    public List<Type> Types { get; set; }  
   public List<Move> Moves { get; set; }
    public Pokemon(string name,  int id,  int health, int attack,int defense, int specialAttack, int specialDefense ,Type tipo, List<Ability> abilities, List<Move> moves)
    {
        this.Name = name;
        this.Id = id;
        this.Health = health;
        this.Attack = attack;
        this.Defense = defense;
        this.SpecialAttack = specialAttack;
        this.SpecialDefense = specialDefense;
        this.Tipo = tipo;
        this.Abilities = abilities;
        this.Moves = moves;
    }
}
