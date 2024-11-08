// using System.Collections.Generic;
//
// namespace Library
// {
//     public class Batalla
//     {
//         private Jugador jugador1;
//         private Jugador jugador2;
//         private Turno turno;
//
//         public Batalla(Jugador jugador1, Jugador jugador2)
//         {
//             this.jugador1 = jugador1;
//             this.jugador2 = jugador2;
//             this.turno = new Turno(jugador1, jugador2);
//             InicializarPokemonActual(jugador1);
//             InicializarPokemonActual(jugador2);
//         }
//
//         public void IniciarBatalla()
//         {
//             while (!jugador1.TodosFueraDeCombate() && !jugador2.TodosFueraDeCombate())
//             {
//                 Console.WriteLine("\n================ TURNOS DE BATALLA ================\n");
//                 turno.MostrarTurno();
//                 JugarTurno(turno.JugadorActual, turno.JugadorOponente);
//                 turno.CambiarTurno();
//             }
//
//             // Mostrar el ganador
//             Console.WriteLine("\n===================================================");
//             string ganador = jugador1.TodosFueraDeCombate() ? jugador2.Nombre : jugador1.Nombre;
//             Console.WriteLine($"\n {ganador} gana la batalla! \n");
//         }
//
//         private void JugarTurno(Jugador jugadorActual, Jugador jugadorOponente)
//         {
//             if (!jugadorActual.PokemonActual.EstaFueraDeCombate())
//             {
//                 Console.WriteLine("\n---------------------------------------------------");
//                 jugadorActual.PokemonActual.ProcesarEstado();
//                 Console.WriteLine("\n---------------------------------------------------");
//                 Console.WriteLine($"{jugadorActual.Nombre}, tu Pokémon actual es {jugadorActual.PokemonActual.Name} y tiene {jugadorActual.PokemonActual.Health:F1} puntos de vida.");
//                 Console.WriteLine($"{jugadorActual.Nombre}, elige qué quieres hacer en este turno:");
//                 Console.WriteLine("1: Usar un ítem");
//                 Console.WriteLine("2: Atacar con un movimiento");
//                 Console.WriteLine("3: Cambiar de Pokémon");
//
//                 int eleccionAccion = LeerEntrada();
//
//                 switch (eleccionAccion)
//                 {
//                     case 1:
//                         UsarItem(jugadorActual);
//                         break;
//                     case 2:
//                         Atacar(jugadorActual, jugadorOponente);
//                         break;
//                     case 3:
//                         CambiarPokemon(jugadorActual);
//                         break;
//                     default:
//                         Console.WriteLine("Elección inválida. Pierdes tu turno.");
//                         break;
//                 }
//             }
//             else
//             {
//                 Console.WriteLine($"{jugadorActual.Nombre}, tu Pokémon actual está fuera de combate. Debes elegir otro.\n");
//                 CambiarPokemonFueraDeCombate(jugadorActual);
//                 JugarTurno(jugadorActual, jugadorOponente);
//             }
//         }
//
//         private void Atacar(Jugador jugadorActual, Jugador jugadorOponente)
//         {
//             Pokemon pokemonActual = jugadorActual.PokemonActual;
//             if (pokemonActual == null || pokemonActual.Moves == null || pokemonActual.Moves.Count == 0)
//             {
//                 Console.WriteLine($"{pokemonActual?.Name ?? "Ningún Pokémon"} no tiene movimientos disponibles.");
//                 return;
//             }
//
//             if (!pokemonActual.PuedeAtacar())
//             {
//                 return;
//             }
//
//             Console.WriteLine($"\n{jugadorActual.Nombre}, elige un movimiento para {pokemonActual.Name}:");
//             for (int i = 0; i < pokemonActual.Moves.Count; i++)
//             {
//                 var movimiento = pokemonActual.Moves[i];
//                 Console.WriteLine($"{i + 1}: {movimiento.MoveDetails.Name} (Poder: {movimiento.MoveDetails.Power}) (Precisión: {movimiento.MoveDetails.Accuracy}) Especial: {movimiento.EstadoEspecial}");
//             }
//
//             int movimientoSeleccionado = LeerEntrada() - 1;
//
//             if (movimientoSeleccionado >= 0 && movimientoSeleccionado < pokemonActual.Moves.Count)
//             {
//                 pokemonActual.Atacar(jugadorActual.PokemonActual, jugadorOponente.PokemonActual, pokemonActual.Moves[movimientoSeleccionado]);
//                 Console.WriteLine($"\n{jugadorActual.Nombre}'s {pokemonActual.Name} ha atacado a {jugadorOponente.Nombre}'s {jugadorOponente.PokemonActual.Name} causando daño.\n");
//             }
//             else
//             {
//                 Console.WriteLine("Movimiento inválido.");
//             }
//         }
//
//         private void UsarItem(Jugador jugador)
//         {
//             bool itemUsado = false;
//             while (!itemUsado)
//             {
//                 Console.WriteLine("\nElige un ítem para usar:");
//                 Console.WriteLine("1: Superpoción");
//                 Console.WriteLine("2: Revivir");
//                 Console.WriteLine("3: Cura Total");
//
//                 int eleccion = LeerEntrada();
//
//                 switch (eleccion)
//                 {
//                     case 1:
//                         itemUsado = UsarSuperpotion(jugador);
//                         break;
//                     case 2:
//                         itemUsado = UsarRevive(jugador);
//                         break;
//                     case 3:
//                         itemUsado = UsarTotalCure(jugador);
//                         break;
//                     default:
//                         Console.WriteLine("Elección inválida. Intenta nuevamente.");
//                         break;
//                 }
//             }
//         }
//
//         private bool UsarSuperpotion(Jugador jugador)
//         {
//             if (jugador.Superpotion.Quantity > 0)
//             {
//                 jugador.Superpotion.Use(jugador.PokemonActual);
//                 Console.WriteLine($"\nSuperpoción usada con éxito en {jugador.PokemonActual.Name}.\n");
//                 return true;
//             }
//             Console.WriteLine("No te quedan Superpociones.");
//             return false;
//         }
//
//         private bool UsarRevive(Jugador jugador)
//         {
//             if (jugador.Revive.Quantity > 0)
//             {
//                 List<string> pokemonsMuertos = new List<string>();
//                 foreach (var pokemon in jugador.Equipo)
//                 {
//                     if (pokemon.EstaFueraDeCombate())
//                     {
//                         pokemonsMuertos.Add(pokemon.Name);
//                     }
//                 }
//
//                 if (pokemonsMuertos.Count == 0)
//                 {
//                     Console.WriteLine("No hay Pokémon muertos para revivir.");
//                     return false;
//                 }
//
//                 Console.WriteLine("\nElige el nombre del Pokémon para revivir:");
//                 foreach (var nombre in pokemonsMuertos)
//                 {
//                     Console.WriteLine(nombre);
//                 }
//
//                 string nombrePokemon = Console.ReadLine();
//                 if (pokemonsMuertos.Contains(nombrePokemon))
//                 {
//                     Pokemon pokemonARevivir = jugador.Equipo.Find(p => p.Name == nombrePokemon);
//                     jugador.Revive.Use(pokemonARevivir);
//                     Console.WriteLine($"\n{pokemonARevivir.Name} ha sido revivido con éxito.\n");
//                     return true;
//                 }
//
//                 Console.WriteLine("Nombre inválido o el Pokémon no está fuera de combate.");
//                 return false;
//             }
//             Console.WriteLine("No te quedan Revivir.");
//             return false;
//         }
//
//         private bool UsarTotalCure(Jugador jugador)
//         {
//             if (jugador.Totalcure.Quantity > 0)
//             {
//                 jugador.Totalcure.Use(jugador.PokemonActual);
//                 Console.WriteLine($"\nCura Total usada con éxito en {jugador.PokemonActual.Name}.\n");
//                 return true;
//             }
//             Console.WriteLine("No te quedan Cura Total.");
//             return false;
//         }
//
//         private void CambiarPokemon(Jugador jugador)
//         {
//             while (true)
//             {
//                 Console.WriteLine($"\n{jugador.Nombre}, elige un Pokémon para cambiar:\n");
//                 for (int i = 0; i < jugador.Equipo.Count; i++)
//                 {
//                     var pokemon = jugador.Equipo[i];
//                     if (!pokemon.EstaFueraDeCombate() && pokemon != jugador.PokemonActual)
//                     {
//                         Console.WriteLine($"{i + 1}: {pokemon.Name} - {pokemon.Health} de vida");
//                     }
//                 }
//
//                 int eleccion = LeerEntrada() - 1;
//
//                 if (eleccion >= 0 && eleccion < jugador.Equipo.Count && !jugador.Equipo[eleccion].EstaFueraDeCombate())
//                 {
//                     jugador.CambiarPokemon(eleccion);
//                     Console.WriteLine($"\nCambio realizado. Pokémon actual: {jugador.PokemonActual.Name}\n");
//                     break;
//                 }
//                 Console.WriteLine("Selección inválida.");
//             }
//         }
//
//         private void CambiarPokemonFueraDeCombate(Jugador jugador)
//         {
//             while (true)
//             {
//                 for (int i = 0; i < jugador.Equipo.Count; i++)
//                 {
//                     var pokemon = jugador.Equipo[i];
//                     if (!pokemon.EstaFueraDeCombate() && pokemon != jugador.PokemonActual)
//                     {
//                         Console.WriteLine($"{i + 1}: {pokemon.Name} - {pokemon.Health} de vida");
//                     }
//                 }
//
//                 int eleccion = LeerEntrada() - 1;
//                 if (eleccion >= 0 && eleccion < jugador.Equipo.Count && !jugador.Equipo[eleccion].EstaFueraDeCombate())
//                 {
//                     jugador.CambiarPokemon(eleccion);
//                     Console.WriteLine($"\nCambio realizado. Pokémon actual: {jugador.PokemonActual.Name}\n");
//                     break;
//                 }
//                 Console.WriteLine("Selección inválida.");
//             }
//         }
//
//         private void InicializarPokemonActual(Jugador jugador)
//         {
//             foreach (var pokemon in jugador.Equipo)
//             {
//                 if (!pokemon.EstaFueraDeCombate())
//                 {
//                     jugador.CambiarPokemon(jugador.Equipo.IndexOf(pokemon));
//                     break;
//                 }
//             }
//         }
//
//         private int LeerEntrada()
//         {
//             while (true)
//             {
//                 if (int.TryParse(Console.ReadLine(), out int resultado))
//                 {
//                     return resultado;
//                 }
//                 Console.WriteLine("Entrada inválida. Ingresa un número.");
//             }
//         }
//     }
// }
