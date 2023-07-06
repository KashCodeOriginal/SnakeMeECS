using System;

namespace OOP.Services.WebSocketsService
{
    [Serializable]
    public class GameGet
    {
        public string type;
        public GameGetPayload payload;
    }
}