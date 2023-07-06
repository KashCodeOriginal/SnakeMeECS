using System;

namespace OOP.Services.WebSocketsService
{
    [Serializable]
    public class GameGetPayload
    {
        public string clientAddress;
        public string startAt;
        public object finishAt;
        public long id;
        public long collectedApples;
        public long snakeLength;
        public DateTimeOffset created_at;
        public DateTimeOffset updated_at;
    }
}