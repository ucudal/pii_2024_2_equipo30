using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Library;

public class Move
{
    
    [JsonPropertyName("move")]
    public MoveDetail MoveDetails { get; set; }
    public List<Move> ListMove { get; set; }
    public EstadoEspecial EstadoEspecial { get; set; }
    public Move(EstadoEspecial estadoEspecial = EstadoEspecial.Ninguno)
    {
        EstadoEspecial = estadoEspecial;
    }
}

public class MoveDetail
{
    public string Name { get; set; }
    public int? Power { get; set; }
    public int? Accuracy { get; set; }
    public string URL { get; set; }
}