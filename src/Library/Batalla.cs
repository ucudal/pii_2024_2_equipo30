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
    }

    public void IniciarBatalla()
    {
        while (!jugador1.TodosFueraDeCombate() && !jugador2.TodosFueraDeCombate())
        {
            turno.MostrarTurno();
            JugarTurno(turno.JugadorActual, turno.JugadorOponente);
            turno.CambiarTurno();
        }

        // Mostrar quién ganó
        if (jugador1.TodosFueraDeCombate())
        {
            Console.WriteLine($"{jugador2.Nombre} gana la batalla!");
        }
        else
        {
            Console.WriteLine($"{jugador1.Nombre} gana la batalla!");
        }
    }

    private void JugarTurno(Jugador jugadorActual, Jugador jugadorOponente)
    {
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

    private void Atacar(Jugador jugadorActual, Jugador jugadorOponente)
    {
        Console.WriteLine($"{jugadorActual.Nombre}, elige un Pokémon para atacar:");

        // Mostrar Pokémon del jugador
        for (int i = 0; i < jugadorActual.Equipo.Count; i++)
        {
            var pokemon = jugadorActual.Equipo[i]; 
            if (!pokemon.EstaFueraDeCombate())
            {
                Console.WriteLine($"{i + 1}: {pokemon.Name} - {pokemon.Health} de vida");
            }
        }
        // Elección del Pokémon
        int eleccion = int.Parse(Console.ReadLine()) - 1;
        if (jugadorActual.Equipo == null || jugadorActual.Equipo.Count == 0)
        {
            Console.WriteLine($"{jugadorActual.Nombre} no tiene Pokémon disponibles para luchar.");
            return;
        }
        
        Pokemon pokemonSeleccionado = jugadorActual.Equipo[eleccion];
        if (pokemonSeleccionado.Moves == null || pokemonSeleccionado.Moves.Count == 0)
        {
            Console.WriteLine($"{pokemonSeleccionado.Name} no tiene movimientos disponibles.");
            return;
        }
        
        if (!pokemonSeleccionado.PuedeAtacar())
        {
            Console.WriteLine($"{pokemonSeleccionado.Name} no puede atacar este turno.");
            return;
        }
        // Mostrar movimientos del Pokémon seleccionado
        Console.WriteLine($"{pokemonSeleccionado.Name}, elige un movimiento:");

        for (int i = 0; i < pokemonSeleccionado.Moves.Count; i++)
        {
            var movimiento = pokemonSeleccionado.Moves[i];
            Console.WriteLine($"{i + 1}: {movimiento.MoveDetails.Name} (Poder: {movimiento.MoveDetails.Power}) (Precisión: {movimiento.MoveDetails.Accuracy}) Especial: {movimiento.EstadoEspecial}");
        }

        // Elección del movimiento
        int movimientoSeleccionado = int.Parse(Console.ReadLine()) - 1;

        // Realizar el ataque
        pokemonSeleccionado.Atacar(jugadorActual.Equipo[0], jugadorOponente.Equipo[0], pokemonSeleccionado.Moves[movimientoSeleccionado]); // Suponiendo que el oponente es el primer Pokémon
    }

    private void UsarItem(Jugador jugador)
    {
        bool itemUsado = false;
        while (!itemUsado)
        {
            Console.WriteLine("Elige un ítem para usar:");
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
                            Console.WriteLine("Elige el nombre del Pokémon para revivir (lista de Pokémon muertos):");
                            foreach (var nombre in pokemonsMuertos)
                            {
                                Console.WriteLine(nombre);
                            }

                            string nombrePokemon = Console.ReadLine();
                            if (pokemonsMuertos.Contains(nombrePokemon))
                            {
                                Pokemon pokemonARevivir = jugador.Equipo.Find(p => p.Name == nombrePokemon);
                                jugador.Revive.Use(pokemonARevivir);
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
        Console.WriteLine($"{jugador.Nombre}, elige un Pokémon para cambiar:");

        for (int i = 0; i < jugador.Equipo.Count; i++)
        {
            var pokemon = jugador.Equipo[i];
            if (!pokemon.EstaFueraDeCombate())
            {
                Console.WriteLine($"{i + 1}: {pokemon.Name} - {pokemon.Health} de vida");
            }
        }

        int eleccion = int.Parse(Console.ReadLine()) - 1;
        if (eleccion < 0 || eleccion >= jugador.Equipo.Count)
        {
            Console.WriteLine("Elección inválida.");
            return;
        }

        jugador.CambiarPokemon(eleccion);
    }
} //a