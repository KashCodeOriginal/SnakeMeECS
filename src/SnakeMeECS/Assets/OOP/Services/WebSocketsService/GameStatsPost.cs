using System;

namespace OOP.Services.WebSocketsService
{
    [Serializable]
    public class GameStatsPost
    {
        public string type { get; set; }
        public GameStatsPostPayLoad payload { get; set; }
    }
}