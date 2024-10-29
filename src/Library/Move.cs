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
    public Move(EstadoEspecial estadoEspecial = Library.EstadoEspecial.Ninguno)
    {
        ListMove = new List<Move>(0);
        EstadoEspecial = estadoEspecial;
    }
    public List<Move> ObtenerMovimientosAleatorios()
    {
        Random random = new Random();
        List<Move> movimientosAleatorios = new List<Move>();

        while (movimientosAleatorios.Count < 4)
        {
            int index = random.Next(ListMove.Count);
            Move movimientoSeleccionado = ListMove[index];

            if (!movimientosAleatorios.Contains(movimientoSeleccionado))
            {
                movimientosAleatorios.Add(movimientoSeleccionado);
            }
        }

        return movimientosAleatorios;
    }
}

public class MoveDetail
{
    public string Name { get; set; }
    public int? Power { get; set; }
    public int? Accuracy { get; set; }
    public string URL { get; set; }
}