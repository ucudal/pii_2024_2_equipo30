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

Funcionamiento general del codigo:
-
Para la estructura del programa usamos:
-
-   los principios de GRASP y Solid.
-   Low coupling and High Coesion




Las clases usadas en nuestro proyecto son:
-

-   Movimientos(lista)

-   Movimiento(Define lo que es un movimiento,tendra un nombre y su daño asociado)

-   Tipo(modifica el daño segun la eficiencia del ataque)

-   Tipo Pokemno(se crean las relaciones de efictividad entre tipos de pokemon )

-   Pokemon(utiliza el nombres,la vida,la lista de movimientos y que tipo es. Constructor)
      - Tiene metodos como "Atacar" y  "Estafueradecombate" que seran utilizados mas adelante
      
-   jugador/Entrenador (tendra un nombre y su lista de pokemons que se podran elegir)

-   Turno (toma a los jugadores y gestiona quien debe o pueda usar un movimiento. Ademas lleva un conteo de los turnos)

-   Batalla(incluye la interfaz y muestra las estadistica de ambos jugadores en cada turno )

Errores y problemas que surgieron:
-


Comentarios:
-



Muchas gracias y un saludo. 
-