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

        static readonly string[] _dropLocations = { 
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

        private string getRandomDropLocation()
        {
            return _dropLocations[_random.Next(0, _dropLocations.Length)];
        }

        private async Task MessageReceived(SocketMessage message)
        {
            Console.WriteLine(message.Content);
            if (message.Content.Equals("!drop"))
            {
                var dropLocation = getRandomDropLocation();
                await message.Channel.SendMessageAsync($"Drop at {dropLocation}!");
            }
        }

        private Task Log(LogMessage message)
        {
            Console.WriteLine(message);
            return Task.CompletedTask;
        }
    }
}
