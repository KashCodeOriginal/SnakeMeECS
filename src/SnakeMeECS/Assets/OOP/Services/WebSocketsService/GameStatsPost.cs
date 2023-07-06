using System;

namespace OOP.Services.WebSocketsService
{
    [Serializable]
    public class GameStatsPost
    {
        public string type;
        public GameStatsPostPayLoad payload;
    }
}
