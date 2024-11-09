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
    public SpecialStatus SpecialStatus { get; set; }
    
    // Nueva propiedad que indica si un movimiento es un ataque especial
    public bool EspecialAttack =>
        SpecialStatus == SpecialStatus.Poisoned ||
        SpecialStatus == SpecialStatus.Burned ||
        SpecialStatus == SpecialStatus.Asleep ||
        SpecialStatus == SpecialStatus.Paralyzed;


 
    public Move(SpecialStatus SpecialStatus = SpecialStatus.NoneStatus)
    {
        SpecialStatus = SpecialStatus;
    }
}

