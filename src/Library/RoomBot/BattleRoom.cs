using System.ComponentModel.DataAnnotations.Schema;
using DSharpPlus;
using DSharpPlus.SlashCommands;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands.Attributes;
using Library.BotDiscord;

namespace Library.RoomBot
{
    public class BattleRoom : ApplicationCommandModule
    {
        private readonly BotQueuePlayers queuePlayers;

        public BattleRoom()
        {
            queuePlayers = new BotQueuePlayers();
        }

        public class Room
        {
            public DiscordChannel Channel { get; private set; }
            public List<Player> Players { get; private set; }

            public Room(DiscordChannel channel, params Player[] players)
            {
                Channel = channel;
                Players = players.ToList();
            }
        }

        [SlashCommand("Start", "Inicia una batalla con los primeros jugadores de la cola.")]
        public async Task Start(InteractionContext ctx)
        {
            var jugadores = queuePlayers.ObtenerProximosJugadores();
            if (jugadores == null)
            {
                await ctx.CreateResponseAsync("No hay suficientes jugadores en la cola para iniciar una batalla.");
                return;
            }

            var guild = ctx.Guild;
            var battleChannel = await guild.CreateVoiceChannelAsync("Sala de Batalla");

            foreach (var jugador in jugadores)
            {
                var member = await guild.GetMemberAsync(jugador.Member.Id); // Asegúrate de usar la propiedad Member.Id
                await member.ModifyAsync(x => x.VoiceChannel = battleChannel);
            }

            Room battleRoom = new Room(battleChannel, jugadores[0], jugadores[1]);
            await ctx.CreateResponseAsync(
                $"¡Los jugadores {jugadores[0].NamePlayer} y {jugadores[1].NamePlayer} han sido movidos a la sala de batalla! Seleccionen sus Pokémon con el comando /choose.");

            // Esperar hasta que ambos jugadores seleccionen sus 6 Pokémon
            while (jugadores[0].TempTeam.Count < 6 || jugadores[1].TempTeam.Count < 6)
            {
                await Task.Delay(1000); // Esperar un segundo antes de volver a verificar
            }

            // Asignar la lista temporal al equipo definitivo
            jugadores[0].Team = new List<Pokemon>(jugadores[0].TempTeam);
            jugadores[1].Team = new List<Pokemon>(jugadores[1].TempTeam);

            Battle battle = new Battle(jugadores[0], jugadores[1]);
            await battle.StartBattle(ctx);
            await battleChannel.DeleteAsync();
        }
    }
}
