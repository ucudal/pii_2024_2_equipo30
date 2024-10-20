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
        Efectividad = new Dictionary<string, double>();
    
    switch (name)
        {
            case "fire":
                TypeDetail.Name = "Fuego";
                Efectividad.Add("Roca", 2);
                Efectividad.Add("Agua", 2);
                Efectividad.Add("Tierra", 2);
                break;
        }
    }
    
}
public class TypeDetail
{
    public string Name { get; set; }
}