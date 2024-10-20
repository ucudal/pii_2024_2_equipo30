using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API;

public class Type
{
    [JsonPropertyName("type")]
    public TypeDetail TypeDetail { get; set; }
    public Dictionary<string, double> Efectividad { get; set; } 

    public Type()
    {
        
    }

    public void SetType(string name)
    {
        TypeDetail = new TypeDetail();
        Efectividad = new Dictionary<string, double>();
        switch (name)
        {
            case "fire":
                TypeDetail.Name = "fire";
                Efectividad.Add("rock", 2);
                Efectividad.Add("water", 2);
                Efectividad.Add("ground", 2);
                Efectividad.Add("bug", 0.5);
                Efectividad.Add("fire", 0.5);
                Efectividad.Add("grass", 0.5);
                break;
            case "water":
                TypeDetail.Name = "water";
                Efectividad.Add("grass", 2);
                Efectividad.Add("electric", 2);
                Efectividad.Add("water", 0.5);
                Efectividad.Add("fire", 0.5);
                Efectividad.Add("ice", 0.5);
                break;
            case "bug":
                TypeDetail.Name = "bug";
                Efectividad.Add("fire", 2);
                Efectividad.Add("rock", 2);
                Efectividad.Add("flying", 2);
                Efectividad.Add("poison", 2);
                Efectividad.Add("fighting", 0.5);
                Efectividad.Add("grass", 0.5);
                Efectividad.Add("ground", 0.5);
                break;
            case "dragon":
                TypeDetail.Name = "dragon";
                Efectividad.Add("dragon", 2);
                Efectividad.Add("ice", 2);
                Efectividad.Add("water", 0.5);
                Efectividad.Add("electric", 0.5);
                Efectividad.Add("fire", 0.5);
                Efectividad.Add("grass", 0.5);
                break;
            case "electric":
                TypeDetail.Name = "electric";
                Efectividad.Add("ground", 2);
                Efectividad.Add("flying", 0.5);
                Efectividad.Add("electric", 0);
                break;
            case "ghost":
                TypeDetail.Name = "ghost";
                Efectividad.Add("ghost", 2);
                Efectividad.Add("poison", 0.5);
                Efectividad.Add("fighting", 0.5);
                Efectividad.Add("normal", 0.5);
                break;
            case"ice":
                TypeDetail.Name = "ice";
                Efectividad.Add("fire", 2);
                Efectividad.Add("fighting", 2);
                Efectividad.Add("rock", 2);
                Efectividad.Add("ice", 0.5);
                break;
            case "fighting":
                TypeDetail.Name = "fighting";
                Efectividad.Add("psychic", 2);
                Efectividad.Add("flying", 2);
                Efectividad.Add("bug", 2);
                Efectividad.Add("rock", 2);
                break;
            case "normal":
                TypeDetail.Name = "normal";
                Efectividad.Add("fighting", 2);
                Efectividad.Add("ghost", 0);
                break;
            case"grass":
                TypeDetail.Name = "grass";
                Efectividad.Add("bug", 2);
                Efectividad.Add("fire", 2);
                Efectividad.Add("ice", 2);
                Efectividad.Add("poison", 2);
                Efectividad.Add("flying", 2);
                Efectividad.Add("water", 0.5);
                Efectividad.Add("electric", 0.5);
                Efectividad.Add("grass", 0.5);
                Efectividad.Add("ground", 0.5);
                break;
            case"psychic":
                TypeDetail.Name = "psychic";
                Efectividad.Add("bug", 2);
                Efectividad.Add("fighting", 2);
                Efectividad.Add("ghost", 2);
                break;
            case "rock":
                TypeDetail.Name = "rock";
                Efectividad.Add("water", 2);
                Efectividad.Add("fighting", 2);
                Efectividad.Add("grass", 2);
                Efectividad.Add("ground", 2);
                Efectividad.Add("fire", 0.5);
                Efectividad.Add("normal", 0.5);
                Efectividad.Add("poison", 0.5);
                Efectividad.Add("flying", 0.5);
                break;
            case "ground":
                TypeDetail.Name = "ground";
                Efectividad.Add("water", 2);
                Efectividad.Add("ice", 2);
                Efectividad.Add("grass", 2);
                Efectividad.Add("rock", 2);
                Efectividad.Add("poison", 2);
                Efectividad.Add("electric", 0.5);
                break;
            case "poison":
                TypeDetail.Name = "poison";
                Efectividad.Add("bug", 2);
                Efectividad.Add("psychic", 2);
                Efectividad.Add("ground", 2);
                Efectividad.Add("fighting", 2);
                Efectividad.Add("grass", 2);
                Efectividad.Add("grass", 0.5);
                Efectividad.Add("poison", 0.5);
                break;
            case"flying":
                TypeDetail.Name = "flying";
                Efectividad.Add("electric", 2);
                Efectividad.Add("ice", 2);
                Efectividad.Add("rock", 2);
                Efectividad.Add("bug", 0.5);
                Efectividad.Add("fighting", 0.5);
                Efectividad.Add("grass", 0.5);
                Efectividad.Add("ground", 0.5);
                break;
        }
    }
}
public class TypeDetail
{
    public string Name { get; set; }
}