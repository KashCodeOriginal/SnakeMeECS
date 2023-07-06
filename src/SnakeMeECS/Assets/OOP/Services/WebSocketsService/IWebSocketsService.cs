using OOP.Services.Locator;
using UnityEngine.Events;

namespace OOP.Services.WebSocketsService
{
    public interface IWebSocketsService : IService
    {
        public event UnityAction OnGameInitialized;
        public void PostNewGame();
        public void PostSnakeCollectedApple(long appleCount, long snakeLenght);
        public void PostEndGame();
    }
}