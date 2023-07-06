using System;

namespace OOP.Services.WebSocketsService
{
    [Serializable]
    public class GameStatsPostPayLoad
    {
        public long appleCount { get; set; }
        public long snakeLength { get; set; }
        public long game_id { get; set; }
    }
}