using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Library
{
    /// <summary>
    /// Clase que representa el tipo de un Pokémon. Contiene detalles sobre el tipo y su efectividad contra otros tipos.
    /// </summary>
    public class Type
    {
        /// <summary>
        /// Detalles del tipo, incluyendo el nombre del tipo de Pokémon.
        /// Esta propiedad se mapea del JSON de la API utilizando <see cref="JsonPropertyNameAttribute"/>.
        /// </summary>
        [JsonPropertyName("type")]
        public TypeDetail TypeDetail { get; set; }

        /// <summary>
        /// Diccionario que define la efectividad de un tipo contra otros tipos.
        /// Las claves son los nombres de otros tipos, y los valores son los multiplicadores de daño.
        /// Por ejemplo, si el valor es 2, significa que el ataque es súper efectivo; si es 0.5, es menos efectivo.
        /// </summary>
        public Dictionary<string, double> Effectiveness { get; set; } 

        /// <summary>
        /// Constructor por defecto.
        /// </summary>
        public Type()
        {
        }

        /// <summary>
        /// Establece el tipo de Pokémon y su efectividad contra otros tipos.
        /// Llena el <see cref="Effectiveness"/> con las relaciones de daño específicas.
        /// </summary>
        /// <param name="name">El nombre del tipo de Pokémon.</param>
        public void SetType(string name)
        {
            TypeDetail = new TypeDetail();
            Effectiveness = new Dictionary<string, double>();

            // Define las relaciones de efectividad según el tipo del Pokémon
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

                case "bug":
                    TypeDetail.Name = "bug";
                    Effectiveness.Add("fire", 2);
                    Effectiveness.Add("rock", 2);
                    Effectiveness.Add("flying", 2);
                    Effectiveness.Add("poison", 2);
                    Effectiveness.Add("fighting", 0.5);
                    Effectiveness.Add("grass", 0.5);
                    Effectiveness.Add("ground", 0.5);
                    break;

                case "dragon":
                    TypeDetail.Name = "dragon";
                    Effectiveness.Add("dragon", 2);
                    Effectiveness.Add("ice", 2);
                    Effectiveness.Add("water", 0.5);
                    Effectiveness.Add("electric", 0.5);
                    Effectiveness.Add("fire", 0.5);
                    Effectiveness.Add("grass", 0.5);
                    break;

                case "electric":
                    TypeDetail.Name = "electric";
                    Effectiveness.Add("ground", 2);
                    Effectiveness.Add("flying", 0.5);
                    Effectiveness.Add("electric", 0);
                    break;

                case "ghost":
                    TypeDetail.Name = "ghost";
                    Effectiveness.Add("ghost", 2);
                    Effectiveness.Add("poison", 0.5);
                    Effectiveness.Add("fighting", 0.5);
                    Effectiveness.Add("normal", 0.5);
                    break;

                case "ice":
                    TypeDetail.Name = "ice";
                    Effectiveness.Add("fire", 2);
                    Effectiveness.Add("fighting", 2);
                    Effectiveness.Add("rock", 2);
                    Effectiveness.Add("ice", 0.5);
                    break;

                case "fighting":
                    TypeDetail.Name = "fighting";
                    Effectiveness.Add("psychic", 2);
                    Effectiveness.Add("flying", 2);
                    Effectiveness.Add("bug", 2);
                    Effectiveness.Add("rock", 2);
                    break;

                case "normal":
                    TypeDetail.Name = "normal";
                    Effectiveness.Add("fighting", 2);
                    Effectiveness.Add("ghost", 0);
                    break;

                case "grass":
                    TypeDetail.Name = "grass";
                    Effectiveness.Add("bug", 2);
                    Effectiveness.Add("fire", 2);
                    Effectiveness.Add("ice", 2);
                    Effectiveness.Add("poison", 2);
                    Effectiveness.Add("flying", 2);
                    Effectiveness.Add("water", 0.5);
                    Effectiveness.Add("electric", 0.5);
                    Effectiveness.Add("grass", 0.5);
                    Effectiveness.Add("ground", 0.5);
                    break;

                case "psychic":
                    TypeDetail.Name = "psychic";
                    Effectiveness.Add("bug", 2);
                    Effectiveness.Add("fighting", 2);
                    Effectiveness.Add("ghost", 2);
                    break;

                case "rock":
                    TypeDetail.Name = "rock";
                    Effectiveness.Add("water", 2);
                    Effectiveness.Add("fighting", 2);
                    Effectiveness.Add("grass", 2);
                    Effectiveness.Add("ground", 2);
                    Effectiveness.Add("fire", 0.5);
                    Effectiveness.Add("normal", 0.5);
                    Effectiveness.Add("poison", 0.5);
                    Effectiveness.Add("flying", 0.5);
                    break;

                case "ground":
                    TypeDetail.Name = "ground";
                    Effectiveness.Add("water", 2);
                    Effectiveness.Add("ice", 2);
                    Effectiveness.Add("grass", 2);
                    Effectiveness.Add("rock", 2);
                    Effectiveness.Add("poison", 2);
                    Effectiveness.Add("electric", 0.5);
                    break;

                case "poison":
                    TypeDetail.Name = "poison";
                    Effectiveness.Add("bug", 2);
                    Effectiveness.Add("psychic", 2);
                    Effectiveness.Add("ground", 2);
                    Effectiveness.Add("fighting", 2);
                    Effectiveness.Add("grass", 2);
                    Effectiveness.Add("poison", 0.5);
                    break;

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
