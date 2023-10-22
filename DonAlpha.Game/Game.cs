using System.Net;

namespace DonAlpha.Game;

public class Game
{
    private int _nextPlayerId = 0;
    
    public string Id { get; }
    public string[] PlayerNames => Players.Select(x => x.Name).ToArray();
    public List<Player> Players { get;  set; } = new();
    
    

    public Game()
    {
        Id = Guid.NewGuid().ToString();
    }
    
    public void AddPlayer(Player player)
    {
        Players.Add(player);
    }
    
    public Player? GetPlayer(string id)
    {
        return Players.FirstOrDefault(x => x.Id == id);
    }
    
    internal Player GetNextPlayer()
    {
        var player = Players[_nextPlayerId];
        _nextPlayerId = (_nextPlayerId + 1) % Players.Count;
        return player;
    }
}