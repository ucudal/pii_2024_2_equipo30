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
            CambiarPokemonFueraDeCombate(jugadorActual);
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

    // Verificar si el Pokémon actual tiene movimientos disponibles
    if (pokemonActual == null || pokemonActual.Moves == null || pokemonActual.Moves.Count == 0)
    {
        Console.WriteLine($"{pokemonActual?.Name ?? "Ningún Pokémon"} no tiene movimientos disponibles.");
        return;
    }

    // Verificar si el Pokémon puede atacar (por ejemplo, si está dormido o paralizado)
    if (!pokemonActual.PuedeAtacar())
    {
        return;
    }

    while (true)
    {
        Console.WriteLine($"\n{jugadorActual.Nombre}, elige un movimiento de: {pokemonActual.Name}");

        // Mostrar movimientos disponibles
        for (int i = 0; i < pokemonActual.Moves.Count; i++)
        {
            var movimiento = pokemonActual.Moves[i];
            Console.WriteLine($"{i + 1}: {movimiento.MoveDetails.Name} (Poder: {movimiento.MoveDetails.Power}) (Precisión: {movimiento.MoveDetails.Accuracy}) Especial: {movimiento.EstadoEspecial}");
        }

        // Leer la elección del jugador
        int movimientoSeleccionado = int.Parse(Console.ReadLine()) - 1;

        // Verificar que el índice seleccionado sea válido
        if (movimientoSeleccionado < 0 || movimientoSeleccionado >= pokemonActual.Moves.Count)
        {
            Console.WriteLine("Movimiento inválido. Por favor, intenta nuevamente.");
            continue;
        }

        var movimientoElegido = pokemonActual.Moves[movimientoSeleccionado];

        if (movimientoElegido.EsAtaqueEspecial)
        {
            // Verificar si el ataque especial puede ser usado
            bool puedeUsarAtaqueEspecial = jugadorActual.PuedeUsarAtaqueEspecial(movimientoElegido.MoveDetails.Name, jugadorActual.ObtenerTurnoPersonal());

            Console.WriteLine($"Verificando uso de ataque especial: {movimientoElegido.MoveDetails.Name}. Turno personal actual: {jugadorActual.ObtenerTurnoPersonal()}, Turno último uso: {jugadorActual.ObtenerUltimoTurnoDeAtaque(movimientoElegido.MoveDetails.Name)}");

            if (puedeUsarAtaqueEspecial)
            {
                // Ejecutar ataque especial y registrar el turno
                bool ataqueExitoso = turno.EjecutarAtaqueEspecial(jugadorActual, pokemonActual, movimientoElegido, jugadorActual.ObtenerTurnoPersonal());
                if (ataqueExitoso)
                {
                    jugadorActual.RegistrarAtaqueEspecial(movimientoElegido.MoveDetails.Name, jugadorActual.ObtenerTurnoPersonal());
                    break; // Salir del bucle si el ataque fue ejecutado exitosamente
                }
            }
            else
            {
                Console.WriteLine($"No puedes usar el ataque especial {movimientoElegido.MoveDetails.Name} en este momento. Debes esperar más turnos. Selecciona otro movimiento.");
            }
        }
        else
        {
            // Realizar un ataque regular si no es un ataque especial
            pokemonActual.Atacar(jugadorActual, jugadorOponente.PokemonActual, movimientoElegido, jugadorActual.ObtenerTurnoPersonal());
            Console.WriteLine($"{jugadorActual.Nombre}'s {pokemonActual.Name} ha atacado a {jugadorOponente.Nombre}'s {jugadorOponente.PokemonActual.Name} causando daño.");
            break; // Salir del bucle si el ataque regular fue ejecutado
        }
    }

    // Incrementar turno personal del jugador actual después de que termine su turno
    jugadorActual.IncrementarTurnoPersonal();
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
        while (true) // Bucle para permitir que el jugador elija varias veces si hay un error
        {
            Console.WriteLine($"\n{jugador.Nombre}, elige un Pokémon para cambiar:\n");

            // Aquí recorremos la lista de Pokémon del jugador
            for (int i = 0; i < jugador.Equipo.Count; i++)
            {
                var pokemon = jugador.Equipo[i];

                // Verificamos que el Pokémon no esté fuera de combate y que no sea el Pokémon actual
                if (!pokemon.EstaFueraDeCombate() && pokemon != jugador.PokemonActual)
                {
                    Console.WriteLine($"{i + 1}: {pokemon.Name} - {pokemon.Health} de vida");
                }
            }

            int eleccion;

            // Intentamos obtener la elección del jugador
            try
            {
                eleccion = int.Parse(Console.ReadLine()) - 1;

                // Comprobamos si la elección es válida
                if (eleccion < 0 || eleccion >= jugador.Equipo.Count)
                {
                    Console.WriteLine("Elección inválida. El número elegido no está en el rango de Pokémon disponibles.");
                    continue; // Permite que el jugador elija nuevamente sin avanzar el turno
                }

                var pokemonSeleccionado = jugador.Equipo[eleccion];

                // Si el Pokémon seleccionado es el mismo que el actual, no lo cambiamos
                if (pokemonSeleccionado == jugador.PokemonActual)
                {
                    Console.WriteLine("¡Ya estás usando este Pokémon! Elige otro.");
                    continue; // Permite que el jugador elija nuevamente sin avanzar el turno
                }

                // Si el Pokémon seleccionado está fuera de combate, no se puede cambiar
                if (pokemonSeleccionado.EstaFueraDeCombate())
                {
                    Console.WriteLine("El Pokémon está fuera de combate, elija otro.");
                    continue; // Permite que el jugador elija nuevamente sin avanzar el turno
                }

                // Cambiar el Pokémon actual a la elección
                jugador.CambiarPokemon(eleccion);
                Console.WriteLine($"\nSe realizó el cambio correctamente. Su Pokémon actual es {jugador.PokemonActual.Name}\n");
                break; // Si todo está bien, rompemos el bucle y avanzamos el turno
            }
            catch (FormatException)
            {
                // Si la entrada no es un número, mostramos un mensaje de error
                Console.WriteLine("Entrada no válida. Por favor ingrese un número.");
            }
            catch (Exception ex)
            {
                // Captura cualquier otro tipo de error
                Console.WriteLine($"Ocurrió un error inesperado: {ex.Message}");
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

    private void CambiarPokemonFueraDeCombate(Jugador jugador)
    {
        while (true)
        {
            for (int i = 0; i < jugador.Equipo.Count; i++)
            {
                var pokemon = jugador.Equipo[i];
                if (!pokemon.EstaFueraDeCombate() && pokemon != jugador.PokemonActual)
                {
                    Console.WriteLine($"{i + 1}: {pokemon.Name} - {pokemon.Health} de vida");
                }
            }

            try
            {
                int eleccion = int.Parse(Console.ReadLine()) - 1;

                // Validar que el número esté dentro del rango de la lista
                if (eleccion < 0 || eleccion >= jugador.Equipo.Count)
                {
                    Console.WriteLine("Elección inválida. Intente nuevamente.");
                    continue;
                }

                var pokemonElegido = jugador.Equipo[eleccion];

                // Verificar que el Pokémon elegido no esté fuera de combate
                if (pokemonElegido.EstaFueraDeCombate())
                {
                    Console.WriteLine("El Pokémon está fuera de combate, elija otro.");
                    continue;
                }

                jugador.CambiarPokemon(eleccion);
                Console.WriteLine($"\nSe realizó el cambio correctamente. Su Pokémon actual es {jugador.PokemonActual.Name}\n");
                break; 
            }
            catch (FormatException)
            {
                Console.WriteLine("Entrada no válida. Ingrese un número.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error: {ex.Message}");
            }
        }
    }
}
