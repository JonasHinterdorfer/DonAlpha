using System.Net;

namespace DonAlpha.Game;

public class Player
{
    public string Id { get;}
    public string Name { get;}

    internal IPAddress IpAddress{ get;}
    
    public Player(string name, IPAddress ipAddress)
    {
        Id = Guid.NewGuid().ToString();
        Name = name;
        IpAddress = ipAddress;
    }
}