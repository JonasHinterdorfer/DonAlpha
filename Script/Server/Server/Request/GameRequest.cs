using Server.Logic;

namespace Server.Request
{
    public class GameRequest
    {
        private string? _gameKey;
        public string? GameKey
        {
            get => _gameKey;
            set
            {
                if (Utils.GameKeyExist(value))
                    throw new ArgumentException("Gamekey already exist");
                _gameKey = value;
            }
        }
    }
}
