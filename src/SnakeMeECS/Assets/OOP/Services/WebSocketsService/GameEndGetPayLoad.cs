using System;

namespace OOP.Services.WebSocketsService
{
    [Serializable]
    public class GameEndGetPayLoad
    {
        public long id;
        public string startAt;
        public string finishAt;
        public string clientAddress;
        public long collectedApples;
        public long snakeLength;
        public DateTimeOffset created_at;
        public DateTimeOffset updated_at;
    }
}