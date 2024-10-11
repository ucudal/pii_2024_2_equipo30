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
    public int Slot { get; set; }
    public string Name { get; set; }

    public Type(string name)
    {
        this.Name = name;
    }
}
public class TypeDetail
{
    public string Name { get; set; }
    public string Url { get; set; }
}