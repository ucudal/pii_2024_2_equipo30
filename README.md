# 🎮pii_2024_2_equipo30 - Poke Fight🐾

## Somos el grupo 4GRAM tambien poco conocidos como el equipo 30

---

## 👨‍💻 *Integrantes:*  ‍💻
- ### __*Mauricio Segovia*__
- ### __*Rodrigo Godoy*__
- ### __*Gaston Baranov*__
- ### __*Alejandro Hernandez*__

---
> ## 🔧 Cómo usar el proyecto🌟
>   1. ### Requisitos previos:
>      - Bot de Discord: tener un token del bot previamente creado en [Discord Developer Portal](https://discord.com/developers/applications).
>   2. ### Configuracion Inicial: 
>      - Configurar el token como una variable de entorno con el siguiente comando: setx DISCORD_TOKEN "token del bot"
> 3. ### Ejecuta y juega

## 🌟Nuestro proyecto se basa en un combate de Pokémons🌟

*El juego es un sistema de combate por turnos multijugador, diseñado en un formato 1 vs 1 mediante comandos de un chatbot. Cada jugador tendrá un equipo de 6 Pokémon disponibles para la batalla, los cuales se mostrarán en pantalla para facilitar la selección estratégica. Cada Pokémon podrá utilizar 4 movimientos de ataque estándar, además de contar con un ataque especial disponible cada dos turnos. El sistema calculará automáticamente la efectividad de los ataques según los tipos de los Pokémon, aplicando ventajas o desventajas de manera dinámica.*

![Pelea 1vs1](https://imgur.com/CBLMbd9)

*Durante cada turno, los jugadores podrán realizar una sola acción, como atacar, cambiar de Pokémon u optar por usar un item. La interfaz del juego permitirá a cada jugador visualizar la vida restante de los Pokémon del oponente, de quién es el turno actual y la acción seleccionada. El combate continuará hasta que todos los Pokémon de un jugador sean derrotados, declarando como ganador al último entrenador con Pokémon en pie.*

---

## 💻 Funcionamiento general del codigo: 🛠️

### 🛠️ Sistema de combate 1vs1

- Equipo de 6 Pokémon: Cada jugador seleccionar su equipo.

- Combates por turnos
- Ataques especiales

### 🎮 Integración con Discord
- Juega desde la comodidad de un chatbot.
- Comandos amigables para facilitar la interacción.

### 🌈 Cálculo de Daño
- Efectividad basada en tipos (fuego, agua, planta, etc.).
- Estados especiales como Dormido y Envenenado.

### 🔧 Interacción con APIs
- Implementación de APIs para enriquecer la experiencia de juego y agregar datos personalizados.

### 💎 Objetos y estrategias
- Usa ítems como Pociones, Revive o TotalCure para cambiar la batalla.
- 
- Cambia de Pokémon en el momento
---

## 📂 En la estructura del programa usamos:

## 🧱 Principios y Patrones de Diseños 
-   ### Principios de GRASP y Solid.

-   ### Interfaz Segregation Principle (ISP) 
>   - Fue usado en: Clases con interfaces como IBattle, IPlayer, IPokemon, IItems.
>   - Por que: Modularidad

-   ### Low Coupling, High Cohesion.
> - Fue usado en: Generalmente, Items se basa en este principio
> - Por que: Mayor capacidad de adaptacion de las clases y ayudar 

-   ### Patron de Diseño: Singleton
>   - Fue usado en : BotQueuePlayers 
>   - Por que: A medida que se ejecutaba el programa se instanciaban varias filas
> 

---

## 📝 Clases principales
### Core del juego

- **Battle**: Coordina el flujo de los combates.

- **Shift**: Cambia el Pokémon activo durante el combate.

- **Pokemon**: Define características, movimientos y estados de un Pokémon.

- **Move y MoveDetail**: Modelan los ataques y sus detalles.

- **Type y TypeDetail**: Definen los tipos y sus ventajas/desventajas.
- 
### Interacción con jugadores

- **Player**: Administra el equipo y los objetos del jugador. 
- **Items**: Clases base para objetos como <ins>SuperPotion </ins>, <ins>Revive </ins> y <ins>TotalCure </ins>.
### Integración y APIs
- **PokemonApi**: Proporciona datos adicionales para los Pokémon.
- **IPokemonCreator**: Genera Pokémon personalizados con atributos definidos.

---

## Errores y problemas que surgieron:

### Segunda Entrega
- Errores en la implementacion de la API. (solucionado)
- Correcciones para poder atacar durante la ronda con Items. (solucionado)
- Cambiar de Pokemon.(solucionado)
- Implementacion de los Test.(echo)
- Implementacion de turnos individuales para ataques especiales.(echo)
- Aplicacion de dormir al enemigo.(echo)

### Tercera entrega
>Tuvimos muchos problemas con el discord,su integracion y la sintaxis.

- El que creaba mas problemas en esta entrega fue battle,botqueue y battle commands, ya que la logica nueva mayormente esta albergada en estas clases.
Lo solucionamos
 
# Muchas gracias y un saludo. 
