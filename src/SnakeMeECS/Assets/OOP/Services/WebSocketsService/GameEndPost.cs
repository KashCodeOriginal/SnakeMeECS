using System;

namespace OOP.Services.WebSocketsService
{
    [Serializable]
    public class GameEndPost
    {
        public string type;
        public GameEndPostPayLoad payload;
    }
}