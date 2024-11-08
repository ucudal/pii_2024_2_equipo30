using System.Threading.Channels;
using System.Collections.Generic;

namespace Library;

public class Batalla
{
    private Jugador jugador1;
    private Jugador jugador2;
    private Turno turno;

    public Batalla(Jugador jugador1, Jugador jugador2)
    {
        this.jugador1 = jugador1;
        this.jugador2 = jugador2;
        this.turno = new Turno(jugador1, jugador2);
        InicializarPokemonActual(jugador1);
        InicializarPokemonActual(jugador2);
    }

    public void IniciarBatalla()
    {
        while (!jugador1.TodosFueraDeCombate() && !jugador2.TodosFueraDeCombate())
        {
            Console.WriteLine("\n================ TURNOS DE BATALLA ================\n");
            turno.MostrarTurno();
            JugarTurno(turno.JugadorActual, turno.JugadorOponente);
            turno.CambiarTurno();
        }

        // Mostrar quién ganó
        Console.WriteLine("\n===================================================");
        if (jugador1.TodosFueraDeCombate())
        {
            Console.WriteLine($"\n {jugador2.Nombre} gana la batalla! \n");
        }
        else
        {
            Console.WriteLine($"\n {jugador1.Nombre} gana la batalla! \n");
        }
    }

    private void JugarTurno(Jugador jugadorActual, Jugador jugadorOponente)
    {
        if (!jugadorActual.PokemonActual.EstaFueraDeCombate())
        {
            Console.WriteLine("\n---------------------------------------------------");
            jugadorActual.PokemonActual.ProcesarEstado();
            Console.WriteLine("\n---------------------------------------------------");
            Console.WriteLine($"{jugadorActual.Nombre}, tu pokemon actual es {jugadorActual.PokemonActual.Name} y tiene {jugadorActual.PokemonActual.Health:F1} puntos de vida");
            Console.WriteLine($"{jugadorActual.Nombre}, elige qué quieres hacer en este turno:");
            Console.WriteLine("1: Usar un ítem");
            Console.WriteLine("2: Atacar con un movimiento");
            Console.WriteLine("3: Cambiar de Pokémon");
            int eleccionAccion = int.Parse(Console.ReadLine());

            switch (eleccionAccion)
            {
                case 1:
                    UsarItem(jugadorActual);
                    break;
                case 2:
                    Atacar(jugadorActual, jugadorOponente);
                    break;
                case 3:
                    CambiarPokemon(jugadorActual);
                    break;
                default:
                    Console.WriteLine("Elección inválida. Pierdes tu turno.");
                    break;
            }
        }
        else if (jugadorActual.PokemonActual.EstaFueraDeCombate())
        {
            Console.WriteLine($"{jugadorActual.Nombre} tu pokemon actual está fuera de combate. Debes elegir otro\n");
            CambiarPokemon(jugadorActual);
            JugarTurno(jugadorActual, jugadorOponente);
        }
        else
        {
            Console.WriteLine("\n---------------------------------------------------");
        }
        
    }

    private void Atacar(Jugador jugadorActual, Jugador jugadorOponente)
    {
        Pokemon pokemonActual = jugadorActual.PokemonActual;

        if (pokemonActual == null || pokemonActual.Moves == null || pokemonActual.Moves.Count == 0)
        {
            Console.WriteLine($"{pokemonActual?.Name ?? "Ningún Pokémon"} no tiene movimientos disponibles.");
            return;  // No tiene movimientos disponibles, salir de la función
        }

        if (!pokemonActual.PuedeAtacar())
        {
            Console.WriteLine($"{pokemonActual.Name} no puede atacar este turno debido a su estado {pokemonActual.Estado}.");
            return;  // El Pokémon no puede atacar debido a su estado (dormido, paralizado, etc.)
        }

        // Mostrar movimientos del Pokémon actual
        Console.WriteLine($"\n{jugadorActual.Nombre}, elige un movimiento de: {pokemonActual.Name}");

        for (int i = 0; i < pokemonActual.Moves.Count; i++)
        {
            var movimiento = pokemonActual.Moves[i];
            Console.WriteLine($"{i + 1}: {movimiento.MoveDetails.Name} (Poder: {movimiento.MoveDetails.Power}) (Precisión: {movimiento.MoveDetails.Accuracy}) Especial: {movimiento.EstadoEspecial}");
        }

        // Elección del movimiento
        int movimientoSeleccionado;
        bool entradaValida = int.TryParse(Console.ReadLine(), out movimientoSeleccionado);
        if (!entradaValida || movimientoSeleccionado < 1 || movimientoSeleccionado > pokemonActual.Moves.Count)
        {
            Console.WriteLine("Selección de movimiento inválida. Intenta nuevamente.");
            return;  // Salir si la entrada es inválida
        }

        // Realizar el ataque
        var movimientoSeleccionadoObjeto = pokemonActual.Moves[movimientoSeleccionado - 1];
        pokemonActual.Atacar(jugadorOponente.PokemonActual, movimientoSeleccionadoObjeto);
        Console.WriteLine($"\n{jugadorActual.Nombre}'s {pokemonActual.Name} ha atacado a {jugadorOponente.Nombre}'s {jugadorOponente.PokemonActual.Name} causando daño.\n");
    }

    private void UsarItem(Jugador jugador)
    {
        bool itemUsado = false;
        while (!itemUsado)
        {
            Console.WriteLine("\nElige un ítem para usar:");
            Console.WriteLine("1: Superpoción");
            Console.WriteLine("2: Revivir");
            Console.WriteLine("3: Cura Total");
            int eleccion = int.Parse(Console.ReadLine());

            switch (eleccion)
            {
                case 1:
                    if (jugador.Superpotion.Quantity > 0)
                    {
                        if (jugador.PokemonActual == null || jugador.PokemonActual.EstaFueraDeCombate())
                        {
                            Console.WriteLine("No hay un Pokémon activo que pueda ser curado. Por favor, selecciona otro Pokémon para curar.");
                            CambiarPokemon(jugador);
                        }
                        jugador.Superpotion.Use(jugador.PokemonActual);
                        Console.WriteLine($"\n Superpoción usada con éxito en {jugador.PokemonActual.Name}.\n");
                        itemUsado = true;
                    }
                    else
                    {
                        Console.WriteLine("No te quedan Superpociones.");
                    }
                    break;
                case 2:
                    if (jugador.Revive.Quantity > 0)
                    {
                        List<string> pokemonsMuertos = new List<string>();
                        foreach (var pokemon in jugador.Equipo)
                        {
                            if (pokemon.EstaFueraDeCombate())
                            {
                                pokemonsMuertos.Add(pokemon.Name);
                            }
                        }

                        if (pokemonsMuertos.Count == 0)
                        {
                            Console.WriteLine("No hay Pokémon muertos para revivir.");
                            return;
                        }

                        bool pokemonRevivido = false;
                        while (!pokemonRevivido)
                        {
                            Console.WriteLine("\nElige el nombre del Pokémon para revivir (lista de Pokémon muertos):");
                            foreach (var nombre in pokemonsMuertos)
                            {
                                Console.WriteLine(nombre);
                            }

                            string nombrePokemon = Console.ReadLine();
                            if (pokemonsMuertos.Contains(nombrePokemon))
                            {
                                Pokemon pokemonARevivir = jugador.Equipo.Find(p => p.Name == nombrePokemon);
                                jugador.Revive.Use(pokemonARevivir);
                                Console.WriteLine($"\n {pokemonARevivir.Name} ha sido revivido con éxito.\n");
                                pokemonRevivido = true;
                                itemUsado = true;
                            }
                            else
                            {
                                Console.WriteLine("Nombre inválido o el Pokémon no está fuera de combate. Por favor, intenta nuevamente.");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("No te quedan Revivir.");
                    }
                    break;
                case 3:
                    if (jugador.Totalcure.Quantity > 0)
                    {
                        jugador.Totalcure.Use(jugador.PokemonActual);
                        Console.WriteLine($"\n Cura Total usada con éxito en {jugador.PokemonActual.Name}.\n");
                        itemUsado = true;
                    }
                    else
                    {
                        Console.WriteLine("No te quedan Cura Total.");
                    }
                    break;
                default:
                    Console.WriteLine("Elección inválida. Por favor, intenta nuevamente.");
                    break;
            }
        }
    }
    private void CambiarPokemon(Jugador jugador)
    {
        while (true)
        {
            Console.WriteLine($"\n{jugador.Nombre}, elige un Pokémon para cambiar:\n");
            for (int i = 0; i < jugador.Equipo.Count; i++)
            {
                var pokemon = jugador.Equipo[i];
                if (!pokemon.EstaFueraDeCombate() && (pokemon != jugador.PokemonActual))
                {
                    Console.WriteLine($"{i + 1}: {pokemon.Name} - {pokemon.Health} de vida");
                }
            }
            try
            {
                int eleccion = int.Parse(Console.ReadLine());
                if (eleccion > 0 && eleccion <= jugador.Equipo.Count)
                {
                    var pokemonSeleccionado = jugador.Equipo[eleccion - 1];
                    if (pokemonSeleccionado != jugador.PokemonActual && !pokemonSeleccionado.EstaFueraDeCombate())
                    {
                        jugador.CambiarPokemon(eleccion - 1);
                        Console.WriteLine($"\nCambio realizado. Ahora tu Pokémon es {jugador.PokemonActual.Name}\n");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("El Pokémon seleccionado está fuera de combate o ya es tu Pokémon actual.");
                    }
                }
                else
                {
                    Console.WriteLine("Elección inválida. Intenta nuevamente.");
                }
            }
            catch
            {
                Console.WriteLine("Entrada inválida. Intenta nuevamente.");
            }

        }
    }
    


    private void InicializarPokemonActual(Jugador jugador)
    {
        if (jugador.PokemonActual == null || jugador.PokemonActual.EstaFueraDeCombate())
        {
            foreach (var pokemon in jugador.Equipo)
            {
                if (!pokemon.EstaFueraDeCombate())
                {
                    jugador.PokemonActual = pokemon;
                    Console.WriteLine(
                        $"\n{jugador.Nombre} ha seleccionado a {pokemon.Name} como su Pokémon inicial.\n");
                    break;
                }
            }
        }
    }

    
}
