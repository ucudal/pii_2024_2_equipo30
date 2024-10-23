using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API;

public class Move
{
    
    [JsonPropertyName("move")]
    public MoveDetail MoveDetails { get; set; }
    public Move()
    {
        
    }
}

public class MoveDetail
{
    public string Name { get; set; }
    public int? Power { get; set; }
    public int? Accuracy { get; set; }
    public string URL { get; set; }
}