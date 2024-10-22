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
}
public class AbilityDetail
{
    public string Name { get; set; }
}