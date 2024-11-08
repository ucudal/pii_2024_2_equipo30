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

