using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Library
{
    /// <summary>
    /// Representa un tipo con detalles de su efectividad en combate.
    /// </summary>
    public class Type
    {
        /// <summary>
        /// Obtiene o establece el detalle del tipo, incluyendo el nombre del tipo.
        /// </summary>
        [JsonPropertyName("type")]
        public TypeDetail TypeDetail { get; set; }

        /// <summary>
        /// Obtiene o establece la efectividad de este tipo contra otros tipos.
        /// Las claves son los nombres de otros tipos, y los valores indican la
        /// efectividad en forma de multiplicador de daño (e.g., 2 para doble daño).
        /// </summary>
        public Dictionary<string, double> Effectiveness { get; set; } 

        /// <summary>
        /// Constructor de la clase <see cref="Type"/>. Inicializa las propiedades.
        /// </summary>
        public Type()
        {
        }

        /// <summary>
        /// Establece el tipo y su efectividad contra otros tipos, según el nombre especificado.
        /// </summary>
        /// <param name="name">Nombre del tipo, como "fire", "water", "electric", etc.</param>
        /// <remarks>
        /// La efectividad de cada tipo se define por valores fijos, donde
        /// valores mayores a 1 indican efectividad alta y menores a 1 indican baja.
        /// </remarks>
        public void SetType(string name)
        {
            TypeDetail = new TypeDetail();
            Effectiveness = new Dictionary<string, double>();
            switch (name)
            {
                case "fire":
                    TypeDetail.Name = "fire";
                    Effectiveness.Add("rock", 2);
                    Effectiveness.Add("water", 2);
                    Effectiveness.Add("ground", 2);
                    Effectiveness.Add("bug", 0.5);
                    Effectiveness.Add("fire", 0.5);
                    Effectiveness.Add("grass", 0.5);
                    break;
                case "water":
                    TypeDetail.Name = "water";
                    Effectiveness.Add("grass", 2);
                    Effectiveness.Add("electric", 2);
                    Effectiveness.Add("water", 0.5);
                    Effectiveness.Add("fire", 0.5);
                    Effectiveness.Add("ice", 0.5);
                    break;
                // Otros casos omitidos por brevedad, pero siguen el mismo formato.
                case "flying":
                    TypeDetail.Name = "flying";
                    Effectiveness.Add("electric", 2);
                    Effectiveness.Add("ice", 2);
                    Effectiveness.Add("rock", 2);
                    Effectiveness.Add("bug", 0.5);
                    Effectiveness.Add("fighting", 0.5);
                    Effectiveness.Add("grass", 0.5);
                    Effectiveness.Add("ground", 0.5);
                    break;
                case "fairy":
                    TypeDetail.Name = "fairy";
                    Effectiveness.Add("steel", 2);
                    Effectiveness.Add("poison", 2);
                    Effectiveness.Add("fighting", 0.5);
                    Effectiveness.Add("bug", 0.5);
                    Effectiveness.Add("dark", 0.5);
                    Effectiveness.Add("dragon", 0);
                    break;
                case "dark":
                    TypeDetail.Name = "dark";
                    Effectiveness.Add("fighting", 2);
                    Effectiveness.Add("bug", 2);
                    Effectiveness.Add("fairy", 2);
                    Effectiveness.Add("ghost", 0.5);
                    Effectiveness.Add("dark", 0.5);
                    Effectiveness.Add("psychic", 0);
                    break;
                case "steel":
                    TypeDetail.Name = "steel";
                    Effectiveness.Add("fire", 2);
                    Effectiveness.Add("fighting", 2);
                    Effectiveness.Add("ground", 2);
                    Effectiveness.Add("normal", 0.5);
                    Effectiveness.Add("flying", 0.5);
                    Effectiveness.Add("rock", 0.5);
                    Effectiveness.Add("bug", 0.5);
                    Effectiveness.Add("steel", 0.5);
                    Effectiveness.Add("grass", 0.5);
                    Effectiveness.Add("psychic", 0.5);
                    Effectiveness.Add("ice", 0.5);
                    Effectiveness.Add("dragon", 0.5);
                    Effectiveness.Add("fairy", 0.5);
                    Effectiveness.Add("poison", 0);
                    break;
            }
        }
    }
}
