using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace API;

public class Ability
{
    [JsonPropertyName("ability")]
    public AbilityDetail AbilityDetail { get; set; }
    
    public bool IsHidden { get; set; }

    public int Slot { get; set; }
    public string Name { get; set; }

    public Ability(string name)
    {
        this.Name = name;
    }
}
public class AbilityDetail
{
    public string Name { get; set; }
    public string Url { get; set; }
}