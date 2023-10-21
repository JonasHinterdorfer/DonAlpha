using Server.Data;

namespace Server.Logic
{
    public class Utils
    {
        public static bool GameKeyExist(string? key)
        {
            return GameData.Games.Any(x => x.GameKey == key);
        }

    }
}
