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
    public EspecialStatus EspecialStatus { get; set; }
    
    // Nueva propiedad que indica si un movimiento es un ataque especial
    public bool EspecialAttack =>
        EspecialStatus == EspecialStatus.Poisoned ||
        EspecialStatus == EspecialStatus.Burned ||
        EspecialStatus == EspecialStatus.Asleep ||
        EspecialStatus == EspecialStatus.Paralyzed;


 
    public Move(EspecialStatus especialStatus = EspecialStatus.NoneStatus)
    {
        EspecialStatus = especialStatus;
    }
}

