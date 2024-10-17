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
    public int Height { get; set; }
    public int Weight { get; set; }
    public int Id { get; set; }
    public int Order { get; set; }
    public int Vida { get; set; }
    public int Ataque { get; set; }
    public Type Tipo { get; set; }
    public List<Ability> Abilities { get; set; }
    public List<Stat> Stats { get; set; }  // Propiedad para almacenar los stats
    public List<Type> Types { get; set; }  // Propiedad para almacenar los tipos
   
    public Pokemon(string name, int height, int weight, int id, int order, int vida, int ataque,Type tipo, List<Ability> abilities)
    {
        this.Name = name;
        this.Height = height;
        this.Weight = weight;
        this.Id = id;
        this.Order = order;
        this.Vida = vida;
        this.Ataque = ataque;
        this.Tipo = tipo;
        this.Abilities = abilities;
    }
}
