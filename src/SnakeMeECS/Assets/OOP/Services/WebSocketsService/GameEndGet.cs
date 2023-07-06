using System;

namespace OOP.Services.WebSocketsService
{
    [Serializable]
    public class GameEndGet
    {
        public string type;
        public GameEndGetPayLoad payload;
    }
}