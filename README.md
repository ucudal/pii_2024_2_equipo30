# pii_2024_2_equipo30

Somos el grupo 4GRAM tambien poco conocidos como el equipo 30
-

Integrantes:
- Mauricio Segovia
- Rodrigo Godoy
- Gaston Baranov
- Alejandro Hernandez

Nuestro proyecto se basa en el famoso modo de combate de Pokémon,tendra las siguientes caracteristicas:
-
  -  Se va a tratar de combates por turnos
  -  Se jugara mediante comandos de un Chatbot
  -  Sera multijugador (1 vs 1)
  -  Cada jugador contara con un equipo de 6 Pokémones en cada batalla
          - Se muestran los pokemones en pantalla
     
  -  Cada Pokémon contara con 4 movimientos de ataques y un ataque especial cada 2 turnos
          - Se podra elegir hacer daño en base a la efectividad de los tipos de Pokémons(El sistema aplica la ventaja o desventaja)
  -  Cada jugador tiene solo una accion por turno(Cambiar pokemon,atacar,etc)
  -  cada jugador puede ver la vida del Pokémon oponente,de quien es el turno y la eleccion que toma
  -  Un jugador es el ganador cuando todos los Pokemons del opononente son derrotados

## Funcionamiento general del codigo:
- Se inicializa el juego atraves del Program.
- Se utiliza la clase PokemonCreator para las instancias de los Pokemon.
- Preparacion de la batalla con la clase Battle.
- Gestion del turno con Move, sus objetos y la posibilidad de cambiar de Pokemon.
- Acciones de los jugadores donde cada accion donde cada turno tiene un impacto en Battle.
- Battle gestiona el calculo del daño
- Al finalizar el turno de cada jugador se verifica si alguno de los jugadores perdio su Pokemon
- Interaccion con la API
- El combate finaliza cuando todos los Pokémon de un jugador son derrotados.


Para la estructura del programa usamos:
-
-   los principios de GRASP y Solid.
-   el principio más se usó es el ISP. Esto se implementó en las interfaces IBattle, IPlayer, IPokemon, etc.

-   Low coupling and High Coesion




## Las clases usadas en nuestro proyecto son:
- Revive (Utilizado para revivir Pokémon)
- Battle (Gestiona el flujo de las batallas)
- Shift (Funcionalidad para cambiar el Pokémon activo)
- Stat (Estadística específica del Pokémon)
- StatsDetail (Detalles adicionales de las estadísticas del Pokémon)
- SuperPotion (Restaurar la salud de un Pokémon)
- TotalCure (Elimina cualquier estado negativo en el Pokémon)
- PokemonCreator (Permite crear instancias de nuevos Pokémon)
- Type (Define el tipo de un Pokémon (por ejemplo, Agua, Fuego, etc.))
- TypeDetail (Detalles adicionales sobre un tipo)
- Program (Punto de entrada del programa)
- EspecialStatus (Efectos especiales que afectan al Pokémon)
- IBatalla (Métodos necesarios para implementar una batalla)
- IItem (Interfaz que proporciona la estructura básica para los objetos)
- IJugador (Interfaz de métodos que deben implementarse para gestionar a un jugador)
- IPokemon (Interfaz de las operaciones relacionadas con un Pokémon.)
- IPokemonApi (Define los métodos para acceder y manipular datos de los Pokémon en la API)
- IPokemonCreator (Define cómo deben crearse los Pokémon en el juego)
- Items (Clase base para los objetos)
- Move (Representa un movimiento)
- MoveDetail (Proporciona detalles adicionales de un movimiento)
- Player (Representa al jugador que tiene un equipo de Pokémon y una colección de objetos.)
- Pokemon (Define un Pokémon con sus atributos, movimientos, tipo, y estados.)
- PokemonApi (Métodos para interactuar con los datos de los Pokémon)


## Errores y problemas que surgieron:

- Errores en la implementacion de la API.
- Correcciones para poder atacar durante la ronda con Items.
- Cambiar de Pokemon.
- Implementacion de los Test.
- Implementacion de turnos individuales para ataques especiales.
- Aplicacion de dormir al enemigo.

 
Muchas gracias y un saludo. 
-