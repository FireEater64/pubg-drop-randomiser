using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace pubg_drop_randomiser
{
    class Program
    {
        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        DiscordSocketClient _client;
        Random _random = new Random();

        static readonly string[] _erangleDropLocations =
        { 
            "Pier Town",
            "Hospital",
            "Water Town",
            "Kameshki",
            "Shelter",
            "Yasnaya Polyana",
            "Novorepnoye",
            "Georgopol",
            "Severny",
            "Plane Crash",
            "Pochinki",
            "Lipovka",
            "Mylta Power",
            "Primorsk",
            "Shooting Range",
            "Rozhok",
            "Mylta",
            "Military Base",
            "Spawn Island",
            "Swamp Town",
            "Ruins",
            "Stalber",
            "Woodcutters Camp",
            "Gatka",
            "Novorepnoye Radio",
            "Mansion",
            "Zharki",
            "School",
            "Farm",
            "Quarry",
            "Prison"
        };

        private static readonly string[] _miramarDropLocations =
        {
            "Power Grid",
            "San Martin",
            "El Pozo",
            "Ladrillera",
            "Minas Generales",
            "Torre Ahumada",
            "Campo Militar",
            "Trailer Park",
            "El Azahar",
            "Impala",
            "Ruins",
            "Hacienda del Patron",
            "Tierra Bronca",
            "La Cobreria",
            "Minas del Sur",
            "La Bendita",
            "Valle del Mar",
            "Crater Fields",
            "Puerto Paraiso",
            "Minas del Valle",
            "Water Treatment",
            "Los Higos",
            "Prison",
            "Chumacera",
            "Junkyard",
            "Los Leones",
            "Monte Nuevo",
            "Pecado",
            "Cruz del Valle",
            "Graveyard"
        };

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();

            _client.Log += Log;
            _client.MessageReceived += MessageReceived;

            var token = Environment.GetEnvironmentVariable("DISCORD_API_KEY");
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException("DISCORD_API_KEY", "DISCORD_API_KEY environment variable cannot be null");   
            }

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        private string getRandomDropLocation(string[] givenDropLocations)
        {
            return givenDropLocations[_random.Next(0, givenDropLocations.Length)];
        }

        private async Task MessageReceived(SocketMessage message)
        {
            Console.WriteLine(message.Content);

            switch (message.Content.ToLowerInvariant())
            {
                case "!drop erangel":
                    await message.Channel.SendMessageAsync($"Drop at {getRandomDropLocation(_erangleDropLocations)}", true);
                    break;
                case "!drop miramar":
                    await message.Channel.SendMessageAsync($"Drop at {getRandomDropLocation(_miramarDropLocations)}", true);
                    break;
            }
        }

        private Task Log(LogMessage message)
        {
            Console.WriteLine(message);
            return Task.CompletedTask;
        }
    }
}

