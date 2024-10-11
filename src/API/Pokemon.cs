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

    public List<Ability> Abilities { get; set; }
    public List<Type> Types { get; set; }
    public List<Stat> Stats { get; set; } 

// Puedes agregar más propiedades si necesitas más información del Pokémon
    public Pokemon(string name, int height, int weight, int id, int order, List<Ability> abilities, List<Type> types, List<Stat> stats)
    {
        this.Name = name;
        this.Height = height;
        this.Weight = weight;
        this.Id = id;
        this.Order = order;
        this.Abilities = abilities;
        this.Types = types;
        this.Stats = stats;
    }
}
