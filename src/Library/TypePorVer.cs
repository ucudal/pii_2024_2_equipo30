// namespace Library;
//
// using System;
// using System.Collections.Generic;
//
// public class TypePorVer
// {
//     public List<Pokemon> ListaDePokemons { get; private set; }
//
//     public TypePorVer()
//     {
//         ListaDePokemons = new List<Pokemon>() ;
//     }
//
//     // Método para obtener una lista aleatoria de 6 pokémon
//     public List<Pokemon> ObtenerEquipoAleatorio()
//     {
//         Random random = new Random();
//         List<Pokemon> equipoAleatorio = new List<Pokemon>();
//
//         while (equipoAleatorio.Count < 6)
//         {
//             int index = random.Next(ListaDePokemons.Count);
//             Pokemon pokemonSeleccionado = ListaDePokemons[index];
//
//             if (!equipoAleatorio.Contains(pokemonSeleccionado))
//             {
//                 equipoAleatorio.Add(pokemonSeleccionado);
//             }
//         }
//
//         return equipoAleatorio;
//     }
// }

