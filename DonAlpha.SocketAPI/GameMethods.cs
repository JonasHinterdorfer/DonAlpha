using System.Net;
using System.Net.Sockets;

namespace DonAlpha.SocketAPI;

public static class GameMethods
{    internal static  async Task<string> CreateGame(string[] args, IPAddress? clientIp, Socket handler)
    {
        if (args.Length != 1)
            return "Please do it right";

        Game.Game game = new Game.Game();
        Game.Main.Games.Add(game);

        return game.Id;
    }
    internal static  async Task<string> CreatePlayer(string[] args, IPAddress? clientIp, Socket handler)
    {
        if (args.Length != 2)
            return "Please do it right";

        Game.Game? game = Game.Main.Games.FirstOrDefault(x => x.Id == args[0]);
        if (game == null)
            return "Game not found";
        
        Game.Player player = new(args[1], clientIp);
        game.AddPlayer(player);
        return player.Id;
    }
    internal static async Task<string> GetPlayers(string[] args, IPAddress? clientIp, Socket handler)
    {
        if (args.Length != 2)
            return "Please do it right";

        Game.Game? game = Game.Main.Games.FirstOrDefault(x => x.Id == args[0]);
        if (game == null)
            return "Game not found";

        return string.Join(", ", game.PlayerNames);
    }
    
    internal static  async Task<string> Move(string[] arg, IPAddress? clientIp, Socket handler)
    {
        throw new NotImplementedException();
    }

    internal static  async Task<string> StartGame(string[] args, IPAddress? clientIp, Socket handler)
    {
        throw new NotImplementedException();
    }


    public static async Task<string> Broadcast(string[] arg, IPAddress? clientIp, Socket handler)
    {
        if(arg.Length != 2)
            return "Please do it right";
        
        await Program.BroadcastToAllExceptOne(arg[1], handler);

        return arg[1];
    }
}