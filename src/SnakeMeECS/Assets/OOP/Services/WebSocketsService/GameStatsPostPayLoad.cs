using System;

namespace OOP.Services.WebSocketsService
{
    [Serializable]
    public class GameStatsPostPayLoad
    {
        public long appleCount;
        public long snakeLength;
        public long game_id;
    }
}