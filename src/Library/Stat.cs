using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace Library;

public class Stat
{
    [JsonPropertyName("stat")]
   
    public StatsDetail StatsDetail { get; set; }
    public int base_stat { get; set; }
}
public class StatsDetail
{
    public string Name { get; set; }
}